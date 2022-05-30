
using System;
using Newtonsoft.Json.Linq;
using project10.Models;
using System.IO;
using Npgsql;
using System.Text.Json;


namespace project10.Repositories
{

    public class UserRepository
    {
        private string strConnect;

        //static string routePath = @"C:\ARINA\WORK\project10\data\route.json";

        public UserRepository(string strConnect)
        {
            this.strConnect = strConnect;
        }


        
        public int Login(string login, string password)
        {
            var connect = new NpgsqlConnection(this.strConnect);
            try
            {
                connect.Open();
                JArray result = new JArray();

                var query = "select id, login, password from public.users where login=@p1";

                using (var cmd = new NpgsqlCommand(query, connect))
                {

                    cmd.Parameters.AddWithValue("p1", login);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Users user = new Users();

                        user.id = reader.GetInt32(0);
                        user.login = reader.GetString(1);
                        user.password = reader.GetString(2);             
                        
                        if(user.password == password){
                            return user.id;
                        }
                        else{
                        return 0;
                        }          
                    }
                    reader.Close();
                }
                return 0;
            }
            finally
            {
                connect.Close(); 
            }
        }


        public int Registration(Users user)
        {
            var connect = new NpgsqlConnection(this.strConnect);
            try
            {
                connect.Open();
                var id = 0;
                var query = "insert into public.users (name, login, password) values (@p1, @p2, @p3) returning id";
                
                using (var cmd = new NpgsqlCommand(query, connect))
                {             
                    cmd.Parameters.AddWithValue("p1", user.name);
                    cmd.Parameters.AddWithValue("p2", user.login);
                    cmd.Parameters.AddWithValue("p3", user.password);
                    id = (int)cmd.ExecuteScalar();            
                }   

                return id;
            }
            finally
            {
                connect.Close(); 
            }
        }
    }
}
