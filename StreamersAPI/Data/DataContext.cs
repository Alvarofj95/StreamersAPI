using Microsoft.EntityFrameworkCore;

namespace StreamersAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Streamers> Streamers { get; set; }
    }
}
