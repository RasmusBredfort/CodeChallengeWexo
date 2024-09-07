using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CodechallengeWexo.Models
{
    public class ApiDbContext : DbContext
    {
        // These DbSet properties represent your database tables
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }

        //Contructor to pass to base class
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
    }
}
