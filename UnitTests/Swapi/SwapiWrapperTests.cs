using System;
using CharacterOfTheDay.Http;
using CharacterOfTheDay.Swapi;
using NUnit.Framework;

namespace UnitTests.Swapi
{
    public class SwapiWrapperTests : With_an_automocked<SwapiWrapper>
    {
        [Test]
        public void When_getting_characters()
        {
            var json = @"{
                ""count"": 87,
                ""next"": ""https://swapi.co/api/people/?page=2"",
                ""previous"": null,
                ""results"": [
                    {
                        ""name"": ""Luke Skywalker"",
                        ""starships"": [
                            ""https://swapi.co/api/starships/12/"",
                            ""https://swapi.co/api/starships/22/""
                        ]
                    },
                    {
                        ""name"": ""C-3PO"",
                        ""starships"": []
                    }
                ]
            }";

            GetMock<IHttpClient>().Setup(x => x.Execute(IsAny<HttpRequest>())).Returns(new HttpResponse(200, new HttpHeaders(), json));
            
            var result = ClassUnderTest.GetCharacters();

            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(result[0].Starships.Length, Is.EqualTo(2));
            Assert.That(result[0].Starships[0], Is.EqualTo("https://swapi.co/api/starships/12/"));
            Assert.That(result[0].Starships[1], Is.EqualTo("https://swapi.co/api/starships/22/"));
        }

        [TestCase(401)]
        [TestCase(500)]
        public void When_getting_characters_but_the_return_status_is_not_200(int statusCode)
        {
            GetMock<IHttpClient>().Setup(x => x.Execute(IsAny<HttpRequest>())).Returns(new HttpResponse(statusCode, new HttpHeaders(), "Some error happened"));
            
            var result = Assert.Catch<Exception>(() => ClassUnderTest.GetCharacters());
            Assert.That(result.Message, Is.EqualTo($"Unexpected status code: {statusCode}"));
        }

        // [Test]
        // public void When_getting_a_starship()
        // {
        //     var result = ClassUnderTest.GetVehicleFromUrl("https://swapi.co/api/starships/12/");

        //     Assert.That(result.Name, Is.EqualTo("X-wing"));
        //     Assert.That(result.Model, Is.EqualTo("T-65 X-wing"));
        // }
    }
}
