using System;
using Newtonsoft.Json.Linq;
using project10.Models;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Npgsql;

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


        public JArray List(int typeroutes_id)
        {


            JArray result = new JArray();

            var query = "select id, name from public.routes";
            // var query = "select id, name from public.routes where typeroutes_id=@p1";


            using (var cmd = new NpgsqlCommand(query, this._connect))
            {

                // cmd.Parameters.AddWithValue("p1", typeroutes_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Route tr = new Route();

                    tr.Id = reader.GetInt32(0);
                    tr.Name = reader.GetString(1);


                    result.Add((JObject)JToken.FromObject(tr));
                }

                reader.Close();

            }

            return result;
        }


        public JToken Show(int id)
        {

            JArray result = new JArray();

            var query = "select id, name from public.routes where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {

                NpgsqlDataReader reader = cmd.ExecuteReader();
                cmd.Parameters.AddWithValue("p1", id);
                while (reader.Read())
                {
                    Route tr = new Route();

                        tr.Id = reader.GetInt32(0);
                        tr.Name = reader.GetString(1);
                        result.Add((JObject)JToken.FromObject(tr));
                    
                }
                reader.Close();
            }

         return result;

        }


        public void Create(Route route)
        {
            var query = "insert into public.routes (name, typeroutes_id, color, icon, about) values (@p1, @p2, @p3, @p4, @p5)";
            
            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                
                cmd.Parameters.AddWithValue("p1", route.Name);
                cmd.Parameters.AddWithValue("p2", route.Typeroutes_id);
                cmd.Parameters.AddWithValue("p3", route.Color);
                cmd.Parameters.AddWithValue("p4", "route.Icon");
                cmd.Parameters.AddWithValue("p5", route.About);
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
                cmd.Parameters.AddWithValue("p1", route.Id);
                cmd.Parameters.AddWithValue("p2", route.Typeroutes_id);
                cmd.Parameters.AddWithValue("p3", route.Name);
                cmd.Parameters.AddWithValue("p4", route.About);
                cmd.Parameters.AddWithValue("p5", route.Color);
                //cmd.Parameters.AddWithValue("p4", route.Geom);
                cmd.ExecuteNonQuery();              
            }
        }


        //   public void Update(int id, Route route)
        // { 
            
        //     var query = @"update public.routes
        //      set name = @p2, typeroutes_id = @p3, color=@p4, icon = @p5
        //      where id=@p1";

        //     using (var cmd = new NpgsqlCommand(query, this._connect))
        //     {
        //         cmd.Parameters.AddWithValue("p1", route.Id);
        //         cmd.Parameters.AddWithValue("p2", route.Name);
        //         cmd.Parameters.AddWithValue("p3", route.Typeroutes_id);
        //         cmd.Parameters.AddWithValue("p4", route.Color);
        //         cmd.Parameters.AddWithValue("p5", "route.Icon");
        //         cmd.ExecuteNonQuery();              
        //     }
        // }

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



//  public JArray List(int typeOfId)
//         {
//             JArray roures = JArray.Parse(File.ReadAllText(routePath));
//             Console.WriteLine("!!!!!!");
//             var listRoutes = new JArray(roures.Where(item => (string)item["typeOfId"] == typeOfId.ToString()));
//             Console.WriteLine($"!!!!----{listRoutes.ToString()}----");

//             return listRoutes;
//         }

//         public JArray Create(JObject typeRt)
//         {
//             JArray route = JArray.Parse(File.ReadAllText(routePath));

//             JToken lastElement = route.Last;
//             typeRt["id"] = (int)lastElement["id"] + 1;


//             route.Add(typeRt);
//             File.WriteAllText(routePath, route.ToString());
//             return null;
//         }

//         public JToken Show(int id)
//         {

//             JArray roures = JArray.Parse(File.ReadAllText(routePath));

//             var route = roures.Where(item => (string)item["id"] == id.ToString()).FirstOrDefault();
//             Console.WriteLine(route);

//             if (route != null)

//                 return route;
//             else
//             {
//                 throw new Exception();
//             }

//         }

//         public void Delete(int id)
//         {

//             JArray routes = JArray.Parse(File.ReadAllText(routePath));
//             var route = routes.Where(item => (string)item["id"] == id.ToString()).FirstOrDefault();

//             if (route != null)
//             {
//                 routes.Remove(route);
//                 File.WriteAllText(routePath, routes.ToString());
//             }
//         }
