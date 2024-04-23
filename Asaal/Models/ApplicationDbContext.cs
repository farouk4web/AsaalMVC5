using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Asaal.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // custom tables

        public DbSet<Society> Societies { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Replay> Replays { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Vote> Votes { get; set; }





        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}