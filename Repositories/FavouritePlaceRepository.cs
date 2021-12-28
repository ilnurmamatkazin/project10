
using System;
using Newtonsoft.Json.Linq;
using project10.Models;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Npgsql;

namespace project10.Repositories
{
    public class FavouritePlaceRepository
    {
        static string favouritePlacePath = @"C:\ARINA\WORK\project10\data\favouriteplace.json";


        private NpgsqlConnection _connect;

        public FavouritePlaceRepository(NpgsqlConnection connect)
        {
            this._connect = connect;
        }

       public void Create(FavouritePlace fp)
        {
            var query = "insert into public.temp (name, about, geom) values (@p1, @p2, ST_SetSRID(ST_GeomFromGeoJSON(@p3),4326))";
            // var query = "insert into public.favouriteplaces (name, about, geom) values (@p1, @p2, @p3)";
            
            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", fp.Name);
                cmd.Parameters.AddWithValue("p2", fp.About);
                cmd.Parameters.AddWithValue("p3", fp.Point);
                cmd.ExecuteNonQuery();
                
            }

        }

        public void Update(int id, FavouritePlace fp)
        { 
            
            var query = @"update public.favouriteplaces
             set name=@p2, about=@p3
             where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", id);
                cmd.Parameters.AddWithValue("p2", fp.Name);
                cmd.Parameters.AddWithValue("p3", fp.About);
                //cmd.Parameters.AddWithValue("p4", fp.Point);
                cmd.ExecuteNonQuery();              
            }
        }

        public JArray List()
        {
            JArray result = new JArray();

            var query = "select id, name, about from public.favouriteplaces";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FavouritePlace tr = new FavouritePlace();

                    tr.Id = reader.GetInt32(0);
                    tr.Name = reader.GetString(1);
                    tr.About = reader.GetString(2);
                    result.Add((JObject)JToken.FromObject(tr));
                }

                reader.Close();
            }
            return result;
        }


        public JToken Show(int id)
        {

            JArray result = new JArray();

            var query = "select id, name, about from public.favouriteplaces where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {

                cmd.Parameters.AddWithValue("p1", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FavouritePlace tr = new FavouritePlace();

                        tr.Id = reader.GetInt32(0);
                        tr.Name = reader.GetString(1);
                        tr.About = reader.GetString(2);
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
