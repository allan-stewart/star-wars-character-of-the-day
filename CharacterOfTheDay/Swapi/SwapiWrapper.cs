using System;
using CharacterOfTheDay.Http;
using CharacterOfTheDay.Swapi.Models;
using Newtonsoft.Json;

namespace CharacterOfTheDay.Swapi
{
    public interface ISwapiWrapper
    {
        StarWarsCharacter[] GetCharacters();
        StarWarsVehicle GetVehicleFromUrl(string url);
    }
    public class SwapiWrapper: ISwapiWrapper
    {
        private readonly IHttpClient httpClient;

        public SwapiWrapper(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public StarWarsCharacter[] GetCharacters()
        {
            var data = JsonConvert.DeserializeObject<StarWarsCharacterResultSet>(GetData("https://swapi.co/api/people/"));
            return data.Results;
        }

        public StarWarsVehicle GetVehicleFromUrl(string url)
        {
            return JsonConvert.DeserializeObject<StarWarsVehicle>(GetData(url));
        }

        private string GetData(string url) {
    
            var request = new HttpRequest {
                Method = HttpMethod.GET,
                Url = url
            };
            var response = this.httpClient.Execute(request);
            if (response.StatusCode != 200) {
                throw new Exception($"Unexpected status code: {response.StatusCode}");
            }
            return response.Body;
        }
    }
}
