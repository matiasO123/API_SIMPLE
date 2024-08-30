using API_Rest_Simple.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Rest_Simple.Context
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options): base(options)
        {

        }

        public DbSet<Person> persons { get; set; }

    }
}
