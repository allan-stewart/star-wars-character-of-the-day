using CharacterOfTheDay;
using CharacterOfTheDay.Swapi;
using CharacterOfTheDay.Swapi.Models;
using NUnit.Framework;

namespace UnitTests
{
    public class StarWarsCharacterOfTheDayTests : With_an_automocked<StarWarsCharacterOfTheDay>
    {
        [Test]
        public void when_getting_a_random_character_but_there_is_only_one()
        {
            var character = new StarWarsCharacter();
            GetMock<ISwapiWrapper>().Setup(x => x.GetCharacters()).Returns(new []{
                character
            });
            var result = ClassUnderTest.GetRandomCharacter();
            Assert.That(result,Is.SameAs(character));
        }
    }
}