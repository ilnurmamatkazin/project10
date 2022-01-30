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

        [JsonProperty("about")]
        public string About {get; set;}
        [JsonProperty("color")]
        public string Color {get; set;}
        [JsonProperty("geometry")]
        public GeoLineString geometry { get; set; }
       
        public class GeoLineString
        {
            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("coordinates")]
            public float[][] coordinates { get; set; }
        }
        

        // public Route(){
        //     this.Trips = new List<Trip>();
        //     this.Points = new List<Point>();
        // }
       
    }
}