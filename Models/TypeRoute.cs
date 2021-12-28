using Newtonsoft.Json;


namespace project10.Models
{
    public class TypeRoute
    {
        [JsonProperty("id")]
        public int Id {get; set;}



        [JsonProperty("name")]
        public string Name {get; set;}
        
        [JsonProperty("color")]
        public string Color {get; set;}
        
        [JsonProperty("line")]
        public string Line {get;}
        
        [JsonProperty("icon")]
        public string Icon {get; set;}
    }
}