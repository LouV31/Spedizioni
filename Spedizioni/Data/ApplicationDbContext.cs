using Microsoft.EntityFrameworkCore;
using Spedizioni.Models;

namespace Spedizioni.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clienti { get; set; }
    }
}
