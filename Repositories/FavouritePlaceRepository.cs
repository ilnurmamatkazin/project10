
using System;
using Newtonsoft.Json.Linq;
using project10.Models;
using Npgsql;
using System.Text.Json;

namespace project10.Repositories
{
    public class FavouritePlaceRepository
    {
        //static string favouritePlacePath = @"C:\ARINA\WORK\project10\data\favouriteplace.json";
        private NpgsqlConnection _connect;

        public FavouritePlaceRepository(NpgsqlConnection connect)
        {
            this._connect = connect;
        }

       public void Create(FavouritePlace fp)
        {
            var geoJson = JsonSerializer.Serialize(fp.point);
            Console.WriteLine(geoJson);

            var query = "insert into public.favouriteplaces (name, about, geom, user_id) values (@p1, @p2, ST_SetSRID(ST_GeomFromGeoJSON(@p3),4326), @p4)";
            
            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", fp.name);
                cmd.Parameters.AddWithValue("p2", fp.about);
                cmd.Parameters.AddWithValue("p3", geoJson);
                cmd.Parameters.AddWithValue("p4", fp.user_id);

                cmd.ExecuteNonQuery();        
            }
        }

        public void Update(FavouritePlace fp)
        { 
            var geoJson = JsonSerializer.Serialize(fp.point);

            var query = @"update public.favouriteplaces
             set name=@p2, about=@p3, geom=ST_SetSRID(ST_GeomFromGeoJSON(@p4),4326)
             where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", fp.id);
                cmd.Parameters.AddWithValue("p2", fp.name);
                cmd.Parameters.AddWithValue("p3", fp.about);
                cmd.Parameters.AddWithValue("p4", geoJson);
                cmd.ExecuteNonQuery();              
            }
        }

        public JArray List(int user_id)
        {
            JArray result = new JArray();

            var query = "select id, name, about, ST_AsGeoJSON(geom) from public.favouriteplaces where user_id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", user_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FavouritePlace tr = new FavouritePlace();

                    tr.id = reader.GetInt32(0);
                    tr.name = reader.GetString(1);
                    tr.about = reader.GetString(2);                    
                    tr.point = JsonSerializer.Deserialize<GeoPoint>(reader.GetString(3));

                    result.Add((JObject)JToken.FromObject(tr));
                }

                reader.Close();
            }
            return result;
        }

        public JToken Show(int id)
        {
            JArray result = new JArray();

            var query = "select id, name, about, ST_AsGeoJSON(geom) from public.favouriteplaces where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FavouritePlace tr = new FavouritePlace();

                        tr.id = reader.GetInt32(0);
                        tr.name = reader.GetString(1);
                        tr.about = reader.GetString(2);
                        tr.point = JsonSerializer.Deserialize<GeoPoint>(reader.GetString(3));

                        result.Add((JObject)JToken.FromObject(tr));
                    
                }
                reader.Close();
            }
         return result;
        }
        public void Delete(int id)
        {
            
            var query = "delete from public.favouriteplaces where id = @p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlTypes.NpgsqlDbType.Integer, id);
                cmd.ExecuteNonQuery();               
            }
        }

    }
}
