using System;
using CharacterOfTheDay.Http;
using CharacterOfTheDay.Swapi.Models;
using Newtonsoft.Json;

namespace CharacterOfTheDay.Swapi
{
    public class SwapiWrapper
    {
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
            var client = new HttpClient();
            var request = new HttpRequest {
                Method = HttpMethod.GET,
                Url = url
            };
            var response = client.Execute(request);
            if (response.StatusCode != 200) {
                throw new Exception($"Unexpected status code: {response.StatusCode}");
            }
            return response.Body;
        }
    }
}
