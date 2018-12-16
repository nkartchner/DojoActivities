using Microsoft.EntityFrameworkCore;
using CBelt.Models;

namespace CBelt.Data
{
    public class BeltContext : DbContext
    {
        public BeltContext(DbContextOptions<BeltContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Participant> participants { get; set; }

    }
}
