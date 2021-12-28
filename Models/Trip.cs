using Newtonsoft.Json;
using GeoJSON.Net.Geometry;
namespace project10.Models


{
    public class Trip
    {
        [JsonProperty("id")]
        public int Id {get; set;}
        
        [JsonProperty("geom")]
        public LineString Geom{get;}
    }
}