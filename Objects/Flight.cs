using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AirlinePlanner
{
  public class Flight
  {
    private int _id;
    private DateTime? _departureTime;
    private string _status;
    private int _departureCityId;

    public Flight(DateTime? DepartureTime, string Status, int DepartureCityId)
    {
      _departureTime = DepartureTime;
      _status = Status;
      _departureCityId = DepartureCityId;
    }
    public int GetId()
    {
      return _id;
    }
    public DateTime? GetDeparture()
    {
      return _departureTime;
    }
    public void SetDeparture(DateTime NewTime)
    {
      _departureTime = NewTime;
    }
    public string GetStatus()
    {
      return _status;
    }
    public void SetStatus(string NewStatus)
    {
      _status = NewStatus;
    }
    public int GetDepartureCityId()
    {
      return _departureCityId;
    }
    public override bool Equals(System.Object otherFlight)
    {
      if (!(otherFlight is Flight))
      {
        return false;
      }
      else
      {
        Flight newFlight = (Flight) otherFlight;
        bool idEquality = this.GetId() == newFlight.GetId();
        bool departureEquality = (this.GetDeparture() == newFlight.GetDeparture());
        bool statusEquality = this.GetStatus() == newFlight.GetStatus();
        return (idEquality && departureEquality && statusEquality);
      }
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (departure_time, status, departure_city_id) OUTPUT INSERTED.id VALUES (@DepartureTime, @Status, @DepartureCityId)", conn);

      SqlParameter departureParam = new SqlParameter();
      departureParam.ParameterName = "@DepartureTime";
      departureParam.Value = this.GetDeparture();

      SqlParameter statusParam = new SqlParameter();
      statusParam.ParameterName = "@Status";
      statusParam.Value = this.GetStatus();

      SqlParameter departureCityParam = new SqlParameter();
      departureCityParam.ParameterName = "@DepartureCityId";
      departureCityParam.Value = this.GetDepartureCityId();

      cmd.Parameters.Add(departureParam);
      cmd.Parameters.Add(statusParam);
      cmd.Parameters.Add(departureCityParam);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
  }

}
