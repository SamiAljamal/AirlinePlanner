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
  }
}
