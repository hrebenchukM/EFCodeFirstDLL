

namespace CountryContinentLibrary
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Capital { get; set; }
        public int? QuantitieOfCitizens { get; set; }
        public double? Square { get; set; }

        public virtual ICollection<Continent> Continents { get; set; }
    }
}
