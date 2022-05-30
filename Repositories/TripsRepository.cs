
using System;
using Newtonsoft.Json.Linq;
using project10.Models;
using System.IO;
using Npgsql;
using System.Text.Json;



namespace project10.Repositories
{
  public class TripsRepository
  {
    private string strConnect;
    public TripsRepository(string strConnect)
    {
      this.strConnect = strConnect;
    }


    public JArray List(int user_id, DateTime date)
    {

      var connect = new NpgsqlConnection(this.strConnect);

      try
      {
        connect.Open();
        JArray result = new JArray();

        var query = "select id, name, about, distance, kalories, time, trips_date, ST_AsGeoJSON(geom) from public.trips where user_id=@p1 and trips_date=@p2";

        using (var cmd = new NpgsqlCommand(query, connect))
        {
          cmd.Parameters.AddWithValue("p1", user_id);
          cmd.Parameters.AddWithValue("p2", date);
          NpgsqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            Trips tr = new Trips();

            tr.id = reader.GetInt32(0);
            tr.name = reader.GetString(1);
            tr.about = reader.GetString(2);
            tr.distance = reader.GetInt32(3);
            tr.kalories = reader.GetInt32(4);
            tr.time = reader.GetInt32(5);
            tr.trips_date = reader.GetDateTime(6);
            tr.geometry = JsonSerializer.Deserialize<GeoLineStringTrips>(reader.GetString(7));

            Console.WriteLine(tr);

            result.Add((JObject)JToken.FromObject(tr));
          }
          reader.Close();
        }
        return result;
      }
      finally
      {
        connect.Close(); 
      }

      
    }

    public JToken Show(int id)
    {
      var connect = new NpgsqlConnection(this.strConnect);

      try
      {
        connect.Open();
        JArray result = new JArray();

        var query = "select name, about, distance, kalories, time, ST_AsGeoJSON(geom), trips_date from from public.trips where id=@p1";

        using (var cmd = new NpgsqlCommand(query, connect))
        {

          cmd.Parameters.AddWithValue("p1", id);
          NpgsqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            Trips tr = new Trips();

            tr.id = reader.GetInt32(0);
            tr.name = reader.GetString(1);
            tr.about = reader.GetString(2);
            tr.distance = reader.GetInt32(3);
            tr.kalories = reader.GetInt32(4);
            tr.time = reader.GetInt32(5);
            tr.trips_date = DateTime.Parse(reader.GetString(6));
            tr.geometry = JsonSerializer.Deserialize<GeoLineStringTrips>(reader.GetString(6));
            result.Add((JObject)JToken.FromObject(tr));
          }
          reader.Close();
        }
        return result;
      }
      finally
      {
        connect.Close(); 
      }
    }


    public void Create(Trips trip)
    {
      
      var connect = new NpgsqlConnection(this.strConnect);
      try
      {
        connect.Open();
        var geoJson = JsonSerializer.Serialize<GeoLineStringTrips>(trip.geometry);
        Console.WriteLine(geoJson);
        JObject json = JObject.Parse(geoJson);
        Console.WriteLine(json);

        // var query = "insert into public.trips (name, about, distance, kalories, time, geom) values (@p1, @p2, @p3, @p4, @p5, @p6)";
        var query = "insert into public.trips (name, about, distance, kalories, time, trips_date, geom, user_id) values (@p1, @p2, @p3, @p4, @p5, @p6, ST_SetSRID(ST_GeomFromGeoJSON(@p7),4326), @p8)";

        using (var cmd = new NpgsqlCommand(query, connect))
        {

          cmd.Parameters.AddWithValue("p1", trip.name);
          cmd.Parameters.AddWithValue("p2", trip.about);
          cmd.Parameters.AddWithValue("p3", trip.distance);
          cmd.Parameters.AddWithValue("p4", trip.kalories);
          cmd.Parameters.AddWithValue("p5", trip.time);
          cmd.Parameters.AddWithValue("p6", trip.trips_date);
          cmd.Parameters.AddWithValue("p7", geoJson);
          cmd.Parameters.AddWithValue("p8", trip.user_id);

          cmd.ExecuteNonQuery();
        }
      }
      finally
      {
        connect.Close(); 
      }
    }

    // public void Update(int id, Trips trip)
    // {

    //   var query = @"update public.trips
    //          set name = @p2, about = @p3, distance = @p4, kalories=@p5, time = @p6
    //          where id=@p1";

    //   using (var cmd = new NpgsqlCommand(query, this._connect))
    //   {
    //     cmd.Parameters.AddWithValue("p1", trip.id);
    //     cmd.Parameters.AddWithValue("p2", trip.name);
    //     cmd.Parameters.AddWithValue("p3", trip.about);
    //     cmd.Parameters.AddWithValue("p4", trip.distance);
    //     cmd.Parameters.AddWithValue("p5", trip.kalories);
    //     cmd.Parameters.AddWithValue("p6", trip.time);
    //     cmd.ExecuteNonQuery();
    //   }
    // }

    public void Delete(int id)
    {
      var connect = new NpgsqlConnection(this.strConnect);
      try
      {
        connect.Open();
        var query = "delete from public.trips where id = @p1";

        using (var cmd = new NpgsqlCommand(query, connect))
        {
          cmd.Parameters.AddWithValue("p1", NpgsqlTypes.NpgsqlDbType.Integer, id);
          cmd.ExecuteNonQuery();
        }
      }
      finally
      {
        connect.Close(); 
      }
    }
  }
}
