using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Identity
{
    public class RegisteredUserModel : IdentityUser
    {
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public ProfileModel Profile { get; set; }
    }
}
