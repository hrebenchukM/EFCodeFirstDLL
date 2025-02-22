using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CountryContinentLibrary;

namespace CountryContinentContextLibrary
{
    // Для работы с БД MS SQL Server необходимо добавить пакет:
    // Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)
    // Microsoft.Extensions.Configuration.Json. Этот пакет специально предназначен для работы с конфигурацией в формате json.


    // Lazy loading или ленивая загрузка предполагает неявную автоматическую загрузку связанных данных при обращении к навигационному свойству.
    // Microsoft.EntityFrameworkCore.Proxies
    public class CountryContinentContext : DbContext
    {

        static DbContextOptions<CountryContinentContext> _options;



        static CountryContinentContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CountryContinentContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }


        public CountryContinentContext()
            : base(_options)
        {
            if (Database.EnsureCreated())
            {
                Country country1 = new Country { Name = "Франция", Capital = "Париж", QuantitieOfCitizens = 67000000, Square = 643801 };
                Country country2 = new Country { Name = "Греция", Capital = "Афины", QuantitieOfCitizens = 10500000, Square = 131957 };
                Country country3 = new Country { Name = "Азербайджан ", Capital = "Баку", QuantitieOfCitizens = 10000000, Square = 86600 };
                Country country4 = new Country { Name = "Турция", Capital = "Анкара", QuantitieOfCitizens = 88000000, Square = 643801 };

                Countries.Add(country1);
                Countries.Add(country2);
                Countries.Add(country3);
                Countries.Add(country4);

                Continent c1 = new Continent
                {
                    Name = "Азия",
                    Countries = new List<Country>() { country2, country3, country4 }
                };
                Continent c2 = new Continent
                {
                    Name = "Южная Америка",
                    Countries = new List<Country>() { country1 }
                };
                Continent c3 = new Continent
                {
                    Name = "Европа",
                    Countries = new List<Country>() { country1, country2, country3, country4 }
                };

                Continents.Add(c1);
                Continents.Add(c2);
                Continents.Add(c3);
                SaveChanges();
            }
        }




        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }

    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // метод UseLazyLoadingProxies() делает доступной ленивую загрузку.
            optionsBuilder.UseLazyLoadingProxies();
        }


    }
}
