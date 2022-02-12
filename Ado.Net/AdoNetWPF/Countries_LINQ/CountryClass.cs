using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace Countries_LINQ
{
    [Table(Name = "Countries")]
    public class CountryClass
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "CountryName")]
        public string Name { get; set; }
        [Column(Name = "Capital")]
        public string Capital { get; set; }
        [Column(Name = "Population")]
        public int Population { get; set; }
        [Column(Name = "Area")]
        public int Area { get; set; }
        [Column(Name = "Continent")]
        public string Continent { get; set; }
    }

    public class Countries : DataContext
    {
        const string connectionString = "Data Source=I7-4700;Initial Catalog=Countries_DB;Integrated Security=True;";
        public Countries() : base(connectionString) { }
        public Countries(string conStr) : base(conStr) { }
        public Table<CountryClass> Country => this.GetTable<CountryClass>();
    }
}