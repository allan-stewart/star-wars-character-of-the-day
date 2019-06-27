using System;
using System.Linq;
using CharacterOfTheDay.Swapi;
using CharacterOfTheDay.Swapi.Models;

namespace CharacterOfTheDay
{
    public class StarWarsCharacterOfTheDay
    {
        private readonly ISwapiWrapper swapiWrapper;

        public StarWarsCharacterOfTheDay(ISwapiWrapper swapiWrapper)
        {
            this.swapiWrapper = swapiWrapper;
        }

        public void Display()
        {
            var character = GetRandomCharacter();
            Console.WriteLine($"Character of the day: {character.Name}");
            if (character.Starships.Length > 0)
            {
                Console.WriteLine("Pilots:");
                character.Starships.ToList().ForEach(DisplayStarship);
            }
        }

        public StarWarsCharacter GetRandomCharacter()
        {
            var characters = swapiWrapper.GetCharacters();
            var random = new Random();
            var index = random.Next(0, characters.Length);
            return characters[index];
        }

        private void DisplayStarship(string url)
        {
            var vehicle = swapiWrapper.GetVehicleFromUrl(url);
            Console.WriteLine($"* {vehicle.Name} ({vehicle.Model})");
        }
    }
}
