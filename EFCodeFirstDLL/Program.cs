using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CountryContinentLibrary;
using CountryContinentContextLibrary;

namespace EFCodeFirstCore
{
    class Program
    {
     
        static void Main(string[] args)
        {
            try
            {
             
                using (CountryContinentContext db = new CountryContinentContext())
                {


                    List<Continent> list = db.Continents.Include(l => l.Countries).ToList();
                    foreach (var l in list)
                    {
                        Console.WriteLine(l.Name);
                        foreach (var cont in l.Countries)
                        {
                            Console.WriteLine("\t" + cont.Name);
                        }
                    }


                }

                while (true)
                {
                    //Console.Clear();
                    Console.WriteLine("1. Добавить данные о стране ");
                    Console.WriteLine("2. Обновить данные о стране ");
                    Console.WriteLine("3. Удалить данные о стране ");
                    Console.WriteLine("4. Добавить континент ");
                    Console.WriteLine("5. Обновить континент ");
                    Console.WriteLine("6. Удалить континент ");
                    Console.WriteLine("7. Показать континенты и их страны ");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            AddCountry();
                            break;
                        case 2:
                            EditCountry();
                            break;
                        case 3:
                            RemoveCountry();
                            break;
                        case 4:
                            AddContinent();
                            break;
                        case 5:
                            EditContinent();
                            break;
                        case 6:
                            RemoveContinent();
                            break;
                            case 7:
                            ShowInfo();
                            break;
                        case 0:
                            return;
                    };
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ShowInfo()
        {
            using (CountryContinentContext db = new CountryContinentContext())
            {


                List<Continent> list = db.Continents.Include(l => l.Countries).ToList();
                foreach (var l in list)
                {
                    Console.WriteLine("Континент: " + l.Name);

                    foreach (var cont in l.Countries)
                    {
                        Console.WriteLine("\t" + "Страна:" + cont.Name);
                        Console.WriteLine("\t\t" + "Столица:" + cont.Capital);
                        Console.WriteLine("\t\t" + "Количество граждан:" + cont.QuantitieOfCitizens);
                        Console.WriteLine("\t\t" + "Площадь:" + cont.Square);
                    }
                }


            }
        }

        static void AddContinent()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Введите название континента:");
                string continentName = Console.ReadLine().Trim();

                if (continentName == "")
                {
                    Console.WriteLine("Не задано название континента!");
                    return;
                }

                using (var db = new CountryContinentContext())
                {
                    db.Continents.Add(new Continent { Name = continentName });
                    db.SaveChanges();
                    Console.WriteLine("Континент добавлен!");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }




        static void RemoveContinent()
        {
            Console.Clear();

            try
            {
                Console.WriteLine("Введите название континента:");
                string continentName = Console.ReadLine().Trim();

                if (continentName == "")
                {
                    Console.WriteLine("Не задано название континента!");
                    return;
                }
                using (var db = new CountryContinentContext())
                {

                    var query = (from b in db.Continents.Include(c => c.Countries)
                                 where b.Name == continentName
                                 select b).Single();

                    db.Countries.RemoveRange(query.Countries);
                    db.Continents.Remove(query);
                    db.SaveChanges();

                    Console.WriteLine("Континент и его страны удалены ");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }




        static void EditContinent()
        {
            Console.Clear();
           

            try
            {
                Console.WriteLine("Введите название континента:");
                string continentName = Console.ReadLine().Trim();

                if (continentName == "")
                {
                    Console.WriteLine("Не задано название континента!");
                    return;
                }

                using (var db = new CountryContinentContext())
                {
                  
                    var query = (from b in db.Continents
                                 where b.Name == continentName
                                 select b).Single();



                    Console.WriteLine("Введите новое название континента:");
                    string newContinentName = Console.ReadLine().Trim();

                    if (newContinentName == "")
                    {
                        Console.WriteLine("Не задано новое название континента!");
                        return;
                    }


                    query.Name = newContinentName;
                    db.SaveChanges();

                    Console.WriteLine("Континент переименован!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }



        static void AddCountry()
        {
            Console.Clear();

            try
            {
                Console.WriteLine("Введите название страны:");
                string countryName = Console.ReadLine().Trim();

                if (countryName == "")
                {
                    Console.WriteLine("Не задано название страны!");
                    return;
                }

                Console.WriteLine("Введите столицу:");
                string capital = Console.ReadLine().Trim();

                if (capital == "")
                {
                    Console.WriteLine("Не задано название столицы!");
                    return;
                }

                Console.WriteLine("Введите количество граждан:");
                int? population = null;
                string populationInput = Console.ReadLine().Trim();
                if (populationInput != "")
                    population = Convert.ToInt32(populationInput);

              

                Console.WriteLine("Введите площадь:");
                double? square = null;
                string squareInput = Console.ReadLine().Trim();
                if (squareInput != "")
                    square = Convert.ToDouble(squareInput);



                Console.WriteLine("Введите название континента:");
                string continentname = Console.ReadLine().Trim();

                if (continentname == "")
                {
                    Console.WriteLine("Не задано название континента!");
                    return;
                }





                using (var db = new CountryContinentContext())
                {

                    var continent = (from b in db.Continents
                                 where b.Name == continentname
                                 select b).Single();


                    if (continent == null)
                    {
                        Console.WriteLine("Континент не найден!");
                        return;
                    }


                    var country = new Country
                    {
                        Name = countryName,
                        Capital = capital,
                        QuantitieOfCitizens = population,
                        Square = square
                    };

                    continent.Countries.Add(country);
                    db.SaveChanges();


                    Console.WriteLine("Страна добавлена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();

        }






        static void RemoveCountry()
        {

            Console.Clear();

            try
            {
                Console.WriteLine("Введите название страны:");
                string countryName = Console.ReadLine().Trim();

                if (countryName == "")
                {
                    Console.WriteLine("Не задано название страны!");
                    return;
                }

                using (var db = new CountryContinentContext())
                {
               
                    var country = (from b in db.Countries
                                where b.Name == countryName
                                select b).Single();

                    db.Countries.Remove(country);
                    db.SaveChanges();

                    Console.WriteLine("Страна удалёна!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        static void EditCountry()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Введите название страны:");
                string countryName = Console.ReadLine().Trim();

                if (countryName == "")
                {
                    Console.WriteLine("Не задано название страны!");
                    return;
                }



                using (var db = new CountryContinentContext())
                {

                    var country = (from b in db.Countries
                                 where b.Name == countryName
                                 select b).Single();

                  

                    Console.WriteLine("Введите новое название страны:");
                    string newCountryName = Console.ReadLine().Trim();

                    if (newCountryName != "")
                    {
                        country.Name = newCountryName;
                    }

                    Console.WriteLine("Введите новую столицу:");
                    string newCapital = Console.ReadLine().Trim();

                    if (newCapital != "")
                    {
                        country.Capital = newCapital;
                    }

                    Console.WriteLine("Введите новое количество граждан:");
                    int? newPopulation = null;
                    string newPopulationInput = Console.ReadLine().Trim();
                   
                    if (newPopulationInput != "")
                    {
                        newPopulation = Convert.ToInt32(newPopulationInput);
                        country.QuantitieOfCitizens = newPopulation;
                    }



                    Console.WriteLine("Введите новую площадь:");
                    double? newSquare = null;
                    string newSquareInput = Console.ReadLine().Trim();
                    
                    if (newSquareInput != "")
                    {
                        newSquare = Convert.ToDouble(newSquareInput);
                        country.Square = newSquare;
                    }


                    db.SaveChanges();

                    Console.WriteLine("Данные о стране изменены!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }


    }
}