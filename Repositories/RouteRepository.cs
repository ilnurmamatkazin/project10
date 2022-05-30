

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
        private string strConnect;

        //static string routePath = @"C:\ARINA\WORK\project10\data\route.json";

        public RouteRepository(string strConnect)
        {
            this.strConnect = strConnect;
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


        public JToken Show(int typeroutes_id, int distance, int time, int level)
        {
            JArray result = new JArray();

            var query = "select id, name, typeroutes_id, color, about, ST_AsGeoJSON(geom), distance, time, level from public.routes where typeroutes_id=@p1 and (-1=@p2 or distance=@p3) and (-1=@p4 or time=@p5) and (-1=@p6 or level=@p7)";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {

                cmd.Parameters.AddWithValue("p1", typeroutes_id);
                cmd.Parameters.AddWithValue("p2", distance);
                cmd.Parameters.AddWithValue("p3", distance);
                cmd.Parameters.AddWithValue("p4", time);
                cmd.Parameters.AddWithValue("p5", time);
                cmd.Parameters.AddWithValue("p6", level);
                cmd.Parameters.AddWithValue("p7", level);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Route tr = new Route();

                    tr.id = reader.GetInt32(0);
                    tr.name = reader.GetString(1);
                    tr.typeroutes_id = reader.GetInt32(2);
                    tr.color = reader.GetString(3);             
                    tr.about = reader.GetString(4);
                    tr.geometry = JsonSerializer.Deserialize<GeoLineString>(reader.GetString(5));
                    tr.distance = reader.GetInt32(6);
                    tr.time = reader.GetInt32(7);
                    tr.level = reader.GetInt32(8);
                    result.Add((JObject)JToken.FromObject(tr));
                    
                }
                reader.Close();
            }

         return result;
        }


        public void Create(Route route)
        {
            var query = "insert into public.routes (name, typeroutes_id, color, about, distance, time, level) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
            
            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                
                cmd.Parameters.AddWithValue("p1", route.name);
                cmd.Parameters.AddWithValue("p2", route.typeroutes_id);
                cmd.Parameters.AddWithValue("p3", route.color);
                cmd.Parameters.AddWithValue("p4", route.about);
                cmd.Parameters.AddWithValue("p5", route.distance);
                cmd.Parameters.AddWithValue("p6", route.time);
                cmd.Parameters.AddWithValue("p7", route.level);
                 // cmd.Parameters.AddWithValue("p6", route.Geom);
                cmd.ExecuteNonQuery();
                
            }
        }

        public void Update(int id, Route route)
        { 

            var query = @"update public.routes
             set typeroutes_id = @p2, name = @p3, about = @p4, color=@p5, distance = @p6, time = @p7, level = @p8
             where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", route.id);
                cmd.Parameters.AddWithValue("p2", route.typeroutes_id);
                cmd.Parameters.AddWithValue("p3", route.name);
                cmd.Parameters.AddWithValue("p4", route.about);
                cmd.Parameters.AddWithValue("p5", route.color);
                cmd.Parameters.AddWithValue("p6", route.distance);
                cmd.Parameters.AddWithValue("p7", route.time);
                cmd.Parameters.AddWithValue("p8", route.level);

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
