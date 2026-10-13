using Microsoft.EntityFrameworkCore;

namespace ComputerShopApi.Models
{
    public class ComputerShopDbContext : DbContext
    {
        public ComputerShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public ComputerShopDbContext()
        {
        }

        public DbSet<Osystem> Osystems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connstr = "server=localhost;database=computer;user=root;password=";

            optionsBuilder.UseMySql(connstr, ServerVersion.AutoDetect(connstr));
        }
    }
}
