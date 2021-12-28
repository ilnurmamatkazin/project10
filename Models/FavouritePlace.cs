using Newtonsoft.Json;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json.Linq;
using System;

namespace project10.Models
{
    public class FavouritePlace
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("geom")]
        public int Point { get; set; }

        // public FavouritePlace()
        // {
        //     Position position = new Position(51.899523, -2.124156);
        //     this.Point = new Point(position);
        // }

    }
}