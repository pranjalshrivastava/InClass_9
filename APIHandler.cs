using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using NationalAeronautics.Models;

namespace NationalAeronautics.APIHandlerManager
{
  public class APIHandler
  {
    // Obtaining the API key is easy. The same key should be usable across the entire
    // data.gov developer network, i.e. all data sources on data.gov.
    // https://www.nps.gov/subjects/developer/get-started.htm

    static string BASE_URL = "https://api.nasa.gov/";
    static string API_KEY = "kwUte78FrHVCBpzVmqMmuSbGqGXFZvEBqlbxkAzl"; //Add your API key here inside ""

    HttpClient httpClient;

    /// <summary>
    ///  Constructor to initialize the connection to the data source
    /// </summary>
    public APIHandler()
    {
      httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
      httpClient.DefaultRequestHeaders.Accept.Add(
          new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <summary>
    /// Method to receive data from API end point as a collection of objects
    /// 
    /// JsonConvert parses the JSON string into classes
    /// </summary>
    /// <returns></returns>
    public Nasa GetNasa()
    {
      string NATIONAL_AERONAUTICS_API_PATH = BASE_URL + "/nasa?limit=20";
      string nasaData = "kwUte78FrHVCBpzVmqMmuSbGqGXFZvEBqlbxkAzl";

      Nasa nasa = null;

      httpClient.BaseAddress = new Uri(NATIONAL_AERONAUTICS_API_PATH);

      // It can take a few requests to get back a prompt response, if the API has not received
      //  calls in the recent past and the server has put the service on hibernation
      try
      {
        HttpResponseMessage response = httpClient.GetAsync(NATIONAL_AERONAUTICS_API_PATH).GetAwaiter().GetResult();
        if (response.IsSuccessStatusCode)
        {
          nasaData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        if (!nasaData.Equals(""))
        {
          // JsonConvert is part of the NewtonSoft.Json Nuget package
          nasa = JsonConvert.DeserializeObject<nasa>(nasaData);
        }
      }
      catch (Exception e)
      {
        // This is a useful place to insert a breakpoint and observe the error message
        Console.WriteLine(e.Message);
      }

      return nasa;
    }
  }
}