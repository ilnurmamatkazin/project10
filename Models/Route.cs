using System.Collections.Generic;
using Newtonsoft.Json;
using GeoJSON.Net.Geometry;

namespace project10.Models
{
    public class Route
    {
        [JsonProperty("id")]
        public int Id {get; set;}

        [JsonProperty("typeroutes_id")]
        public int Typeroutes_id {get; set;}

        [JsonProperty("name")]
        public string Name {get; set;}

        [JsonProperty("trips")]
        public List<Trip> Trips {get;}
        
        [JsonProperty("points")]
        public List<Point> Points{get; set;}

            [JsonProperty("color")]
        public string Color {get; set;}
        

        
        [JsonProperty("icon")]
        public string Icon {get; set;}


        public Route(){
            this.Trips = new List<Trip>();
            this.Points = new List<Point>();
        }
       
    }
}