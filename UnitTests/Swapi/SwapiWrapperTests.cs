using CharacterOfTheDay.Swapi;
using NUnit.Framework;

namespace UnitTests.Swapi
{
    public class SwapiWrapperTests : With_an_automocked<SwapiWrapper>
    {
        [Test]
        public void When_getting_characters()
        {
            var result = ClassUnderTest.GetCharacters();

            Assert.That(result.Length, Is.EqualTo(10));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(result[0].Starships.Length, Is.EqualTo(2));
            Assert.That(result[0].Starships[0], Is.EqualTo("https://swapi.co/api/starships/12/"));
            Assert.That(result[0].Starships[1], Is.EqualTo("https://swapi.co/api/starships/22/"));
        }

        [Test]
        public void When_getting_a_starship()
        {
            var result = ClassUnderTest.GetVehicleFromUrl("https://swapi.co/api/starships/12/");

            Assert.That(result.Name, Is.EqualTo("X-wing"));
            Assert.That(result.Model, Is.EqualTo("T-65 X-wing"));
        }
    }
}
