using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Asaal.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Asaal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [MinLength(4, ErrorMessageResourceName = "min4", ErrorMessageResourceType = typeof(ViewsKeys)), MaxLength(50, ErrorMessageResourceName = "max50", ErrorMessageResourceType = typeof(ViewsKeys))]
        [RegularExpression("^[a-zA-Zء-ي ]*$", ErrorMessageResourceName = "vlaidFullNameMsg", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "fullName", ResourceType = typeof(ViewsKeys))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "country", ResourceType = typeof(ViewsKeys))]
        public string CountryCode { get; set; }

        public string ProfileImgSrc { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Society> Societies { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Replay> Replays { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}