namespace CharacterOfTheDay.Swapi.Models
{
    public class StarWarsCharacter
    {
        public string Name { get; set; }
        public string[] Starships { get; set; }
    }

    public class StarWarsCharacterResultSet
    {
        public int Count { get; set; }
        public StarWarsCharacter[] Results { get; set; }
    }
}
