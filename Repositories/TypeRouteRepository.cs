//using Internal;
using System;
using Newtonsoft.Json.Linq;
using project10.Models;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Npgsql;


namespace project10.Repositories
{

    public class TypeRouteRepository
    {
        
        private NpgsqlConnection _connect;
        
        public TypeRouteRepository(NpgsqlConnection connect)
        {
            this._connect = connect;
        }
        
        public JArray List()
        {
            JArray result = new JArray();

            var query = "select id, name from public.typeroutes order by id ASC";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TypeRoute tr = new TypeRoute();

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

            var query = "select id, name from public.typeroutes where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {

                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TypeRoute tr = new TypeRoute();

                        tr.Id = reader.GetInt32(0);
                        tr.Name = reader.GetString(1);
                        tr.Icon = reader.GetString(2);
                        result.Add((JObject)JToken.FromObject(tr));
                    
                }
                reader.Close();
            }

         return result;

        }

        public void Create(TypeRoute tr)
        {
            var query = "insert into public.typeroutes (name, color, icon, about) values (@p1, @p2, @p3, @p4)";
            
            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", tr.Name);
                cmd.Parameters.AddWithValue("p2", tr.Color);
                cmd.Parameters.AddWithValue("p3", "tr.Icon");
                cmd.Parameters.AddWithValue("p4", tr.About);
                cmd.ExecuteNonQuery();
                
            }

        }

        public void Delete(int id)
        {
            var query = "delete from public.typeroutes where id = @p1";

               using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlTypes.NpgsqlDbType.Integer, id);
                cmd.ExecuteNonQuery();               
            }
        }

        
        public void Update(int id, TypeRoute tr)
        { 
            
            var query = @"update public.typeroutes
             set name = @p2, icon = @p3, color = @p4, about = @p5
             where id=@p1";

            using (var cmd = new NpgsqlCommand(query, this._connect))
            {
                cmd.Parameters.AddWithValue("p1", tr.Id);
                cmd.Parameters.AddWithValue("p2", tr.Name);
                cmd.Parameters.AddWithValue("p3", "tr.Icon");
                cmd.Parameters.AddWithValue("p4", tr.Color);
                cmd.Parameters.AddWithValue("p5", tr.About);
                cmd.ExecuteNonQuery();              
            }
        }

    }
}

// try
//             {
                
//             using (var cmd = new NpgsqlCommand(query, this.connect))
//             {
//                 // try
//                 // {
//                     // if (parentID != -1)
//                     //     cmd.Parameters.AddWithValue("p1", parentID);
//                     // else
//                     //     cmd.Parameters.AddWithValue("p1", DBNull.Value);

//                     // cmd.Parameters.AddWithValue("p2", modeID);

//                     NpgsqlDataReader reader = cmd.ExecuteReader();


//                     while (reader.Read())
//                     {
//                         //read data here
//                         string field1 = reader.GetString(0);
//                         string field3 = reader.GetString(2);

//                         Console.WriteLine(field1);
//                         Console.WriteLine(field3);
//                     }


//                     reader.Close();

//                 // }
//                 // catch (System.Exception e)
//                 // {
//                 //     Console.WriteLine(e);

//                 //     // var err = new AnswerErrors("Can not get data from 'Layers.List'");

//                 //     Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;

//                 //     return new JsonResult("");
//                 // }
//             }

//                 // JArray rt = _typeRouteRepository.List();
//                 return StatusCode(200, null);
//             }
//             catch (Exception err)
//             {
//                 return StatusCode(500, err);
//             }