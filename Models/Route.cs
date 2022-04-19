using System.Collections.Generic;
using Newtonsoft.Json;
using GeoJSON.Net.Geometry;

namespace project10.Models
{
    public class Route
    {
        [JsonProperty("id")]
        public int id {get; set;}

        [JsonProperty("typeroutes_id")]
        public int typeroutes_id {get; set;}

        [JsonProperty("name")]
        public string name {get; set;}

        [JsonProperty("about")]
        public string about {get; set;}
        [JsonProperty("color")]
        public string color {get; set;}

        [JsonProperty("distance")]
        public int distance {get; set;}
         [JsonProperty("time")]
        public int time {get; set;}
         [JsonProperty("level")]
        public int level {get; set;}
        [JsonProperty("geometry")]
        public GeoLineString geometry { get; set; }
       
        
        

        // public Route(){
        //     this.Trips = new List<Trip>();
        //     this.Points = new List<Point>();
        // }
       
    }
    public class GeoLineString
        {
            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("coordinates")]
            public float[][] coordinates { get; set; }
        }
}