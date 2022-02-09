using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music_Entity
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Track
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
    }

    public class Music_DB: DbContext
    {
        public Music_DB() : base("name=Music") { }
        public Music_DB(string connectionString) : base(connectionString) { }

        public DbSet<Track> Tracks { get; set; }
    }
}