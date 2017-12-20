using Microsoft.EntityFrameworkCore;
 
namespace loginregister.Models
{
    public class MainContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<User> users { get; set;}

        public DbSet<Idea> ideas { get; set;}

        public DbSet<Like> likes { get; set;}

    }
}