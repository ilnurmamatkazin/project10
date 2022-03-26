using System.Collections.Generic;
using Newtonsoft.Json;
using GeoJSON.Net.Geometry;

using System;

namespace project10.Models
{
  public class Trips
  {
    [JsonProperty("id")]
    public int id { get; set; }

    [JsonProperty("name")]
    public string name { get; set; }

    [JsonProperty("about")]
    public string about { get; set; }

    [JsonProperty("distance")]
    public int distance { get; set; }

    [JsonProperty("kalories")]
    public int kalories { get; set; }

    [JsonProperty("time")]
    public int time { get; set; }

    [JsonProperty("geometry")]
    public GeoLineStringTrips geometry { get; set; }

    [JsonProperty("trips_date")]
    public DateTime trips_date { get; set; }

  }
  public class GeoLineStringTrips
  {
    [JsonProperty("type")]
    public string type { get; set; }

    [JsonProperty("coordinates")]
    public float[][] coordinates { get; set; }
  }
}