using System.Collections.Generic;
using Newtonsoft.Json;
using GeoJSON.Net.Geometry;

namespace project10.Models
{
    public class Users
    {
        [JsonProperty("id")]
        public int id {get; set;}

        [JsonProperty("name")]
        public string name {get; set;}

        [JsonProperty("login")]
        public string login {get; set;}

        [JsonProperty("password")]
        public string password {get; set;}
    }

     public class UserID 
     {
         [JsonProperty("user_id")]
        public int id {get; set;}
     }

     public class UserLogin 
     {
        [JsonProperty("login")]
        public string login {get; set;}

        [JsonProperty("password")]
        public string password {get; set;}
     }
}