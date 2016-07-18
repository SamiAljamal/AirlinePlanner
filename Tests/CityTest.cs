using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AirlinePlanner
{
  public class CityTest : IDisposable
  {
    public CityTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_planner_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      //Arrange, Act
      int result = City.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      //Arrange, Act
      City firstCity = new City("Mow the lawn");
      City secondCity = new City("Mow the lawn");

      //Assert
      Assert.Equal(firstCity, secondCity);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      City testCity = new City("Mow the lawn");
      testCity.Save();

      //Act
      List<City> result = City.GetAll();
      List<City> testList = new List<City>{testCity};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      City testCity = new City("Mow the lawn");
      testCity.Save();

      //Act
      City savedCity = City.GetAll()[0];

      int result = savedCity.GetId();
      int testId = testCity.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsCityInDatabase()
    {
      //Arrange
      City testCity = new City("Mow the lawn");
      testCity.Save();

      //Act
      City result = City.Find(testCity.GetId());

      //Assert
      Assert.Equal(testCity, result);
    }
    [Fact]
    public void Test_Delete_DeletesCityFromDatabase()
    {
      //Arrange
      string name1 = "Home stuff";
      City testCity1 = new City(name1);
      testCity1.Save();

      string name2 = "Work stuff";
      City testCity2 = new City(name2);
      testCity2.Save();

      //Act
      testCity1.Delete();
      List<City> resultCategories = City.GetAll();
      List<City> testCityList = new List<City> {testCity2};

      //Assert
      Assert.Equal(testCityList, resultCategories);
    }
    [Fact]
    public void Test_AddFlight_AddsFlightToCity()
    {
      //Arrange
      DateTime testDepartureTime = new DateTime(2016, 7, 23, 0, 30, 0);
      Flight testFlight = new Flight(testDepartureTime, "On Time", 0);
      testFlight.Save();
      System.Console.WriteLine(testFlight.GetFlights());

      City testCity = new City("Portland");
      testCity.Save();

      City testCity2 = new City("Chicago");
      testCity2.Save();

      //Act
      testCity.AddFlight(testFlight);
      // testCategory.AddCity(testCity2);

      List<Flight> result = testCity.GetFlights();
      List<Flight> testList = new List<Flight>{testFlight};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_GetFlights_ReturnsAllFlights()
    {
      //Arrange
      DateTime? testDepartureTime = new DateTime(2016, 7, 23, 0, 30, 0);
      Flight testFlight = new Flight(testDepartureTime, "On Time", 0);
      testFlight.Save();

      City testCity1 = new City("Portland");
      testCity1.Save();

      City testCity2 = new City("Buy plane ticket");
      testCity2.Save();

      //Act
      testCity1.AddFlight(testFlight);
      List<Flight> savedFlights = testCity1.GetFlights();
      List<Flight> testList = new List<Flight> {testFlight};

      //Assert
      Assert.Equal(testList, savedFlights);
    }


    public void Dispose()
    {
      City.DeleteAll();
      City.DeleteAll();
    }
  }
}
