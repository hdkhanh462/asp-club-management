using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IctuTaekwondo.Api.Models
{
    public class User : IdentityUser
    {
        [StringLength(50, MinimumLength = 6)]
        public string FullName { get; set; } = null!;
        public string? AvatarUrl { get; set; } 

        [InverseProperty("User")]
        public virtual UserProfile? UserProfile { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

        [InverseProperty("User")]
        public virtual ICollection<Achievement> AchievementUsers { get; set; } = new List<Achievement>();
    }
}
