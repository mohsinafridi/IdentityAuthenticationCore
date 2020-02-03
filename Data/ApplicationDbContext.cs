using IdentityAuthenticationCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuthenticationCore.Data
{
    // IdentityDbContext contains all the user tables
    public class ApplicationDbContext  : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option)
            :base(option)
        {
            // public DbSet<AppUser> User { get; set; }
        }
    }
}
