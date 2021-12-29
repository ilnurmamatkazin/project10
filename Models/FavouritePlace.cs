using Newtonsoft.Json;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json.Linq;
using System;

namespace project10.Models
{
    public class FavouritePlace
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("about")]
        public string about { get; set; }

        [JsonProperty("geometry")]
        public GeoPoint point { get; set; }

        // public FavouritePlace()
        // {
        //     Position position = new Position(51.899523, -2.124156);
        //     this.Point = new Point(position);
        // }

    }

    public class GeoPoint
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("coordinates")]
        public float[] coordinates { get; set; }
    }
}