using Microsoft.Extensions.DependencyInjection;

namespace CharacterOfTheDay
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.Scan(scan => {
                scan.FromEntryAssembly()
                    .AddClasses()
                    .AsSelfWithInterfaces();
            });

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<StarWarsCharacterOfTheDay>().Display();
        }
    }
}
