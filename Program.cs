using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace events
{

    class Event
    {
      public string name { get; set; }
      public string desc { get; set; }
      public string time { get; set; }
      public string[] addr { get; set; }
    }


    class Program
    {
      public static void Main(string[] args)
      {
        if(args[0] == "")
        {
          SendRequest(args[0]).Wait();
        else
        {
          Console.WriteLine("Please enter a location as an Argument!")
        }
      }

      private static async Task SendRequest(string location)
      {
          using (var client = new HttpClient())
          {
              try
              {
                  client.BaseAddress = new Uri("https://api-event.herokuapp.com");
                  var response = await client.GetAsync("/" + location);
                  response.EnsureSuccessStatusCode(); // Throw in not success

                  var stringResponse = await response.Content.ReadAsStringAsync();
                  var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(stringResponse);

                  Console.WriteLine($"Found {events.Count()} events");
                  Console.WriteLine($"First Event found {JsonConvert.SerializeObject(events.First())}");
              }
              catch (HttpRequestException e)
              {
                  Console.WriteLine($"Error handling request: {e.Message}");
              }
          }
      }
    }
}
