
using System;
using Newtonsoft.Json.Linq;
using project10.Models;
using System.IO;
using Npgsql;
using System.Text.Json;


namespace project10.Repositories
{

    public class RouteRepository
    {
        private NpgsqlConnection _connect;

        //static string routePath = @"C:\ARINA\WORK\project10\data\route.json";

        public RouteRepository(NpgsqlConnection connect)
        {
            this._connect = connect;
        }


        public JArray List()
        {


            JArray result = new JArray();

            var query = "select id, name, typeroutes_id, color, about from public.routes";
            // var query = "select id, name from public.routes where typeroutes_id=@p1";


            using (var cmd = new NpgsqlCommand(query, this._connect))
            {

                // cmd.Parameters.AddWithValue("p1", typeroutes_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Route tr = new Route();

                    tr.id = reader.GetInt32(0);
                    tr.name = reader.GetString(1);
                    tr.typeroutes_id = reader.GetInt32(2);              
                    tr.color = reader.GetString(3);
                    tr.about = reader.GetString(4);

                    result.Add((JObject)JToken.FromObject(tr));
                }

                reader.Close();

            }

            return result;
        }


        public JToken Show(int typeroutes_id)
        {

            JArray result = new JArray();

            var query = "select id, name, typeroutes_id, color, about, ST_AsGeoJSON(geom) from public.routes where typeroutes_id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {

                cmd.Parameters.AddWithValue("p1", typeroutes_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Route tr = new Route();

                    tr.id = reader.GetInt32(0);
                    tr.name = reader.GetString(1);
                    tr.typeroutes_id = reader.GetInt32(2);
                    tr.color = reader.GetString(3);
                    tr.about = reader.GetString(4);
                    // Console.WriteLine(reader.GetString(5));
                    tr.geometry = JsonSerializer.Deserialize<GeoLineString>(reader.GetString(5));

                    result.Add((JObject)JToken.FromObject(tr));
                    
                }
                reader.Close();
            }

            Console.WriteLine(result);


         return result;

        }


        public void Create(Route route)
        {
            var query = "insert into public.routes (name, typeroutes_id, color, about) values (@p1, @p2, @p3, @p4)";
            
            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                
                cmd.Parameters.AddWithValue("p1", route.name);
                cmd.Parameters.AddWithValue("p2", route.typeroutes_id);
                cmd.Parameters.AddWithValue("p3", route.color);
                cmd.Parameters.AddWithValue("p4", route.about);
                 // cmd.Parameters.AddWithValue("p6", route.Geom);
                cmd.ExecuteNonQuery();
                
            }
        }

        public void Update(int id, Route route)
        { 
            
            var query = @"update public.routes
             set typeroutes_id = @p2, name = @p3, about = @p4, color=@p5
             where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", route.id);
                cmd.Parameters.AddWithValue("p2", route.typeroutes_id);
                cmd.Parameters.AddWithValue("p3", route.name);
                cmd.Parameters.AddWithValue("p4", route.about);
                cmd.Parameters.AddWithValue("p5", route.color);
                //cmd.Parameters.AddWithValue("p4", route.Geom);
                cmd.ExecuteNonQuery();              
            }
        }



         public void Delete(int id)
        {

            var query = "delete from public.routes where id = @p1";

               using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlTypes.NpgsqlDbType.Integer, id);
                cmd.ExecuteNonQuery();               
            }
  
        }

    }
}
