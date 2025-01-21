using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IctuTaekwondo.Shared.Models
{
    public class User : IdentityUser
    {
        [DisplayName("Họ và tên")]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;

        [DisplayName("Địa chỉ avatar")]
        public string? AvatarUrl { get; set; } 

        [InverseProperty("User")]
        public virtual UserProfile? UserProfile { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

        [InverseProperty("User")]
        public virtual ICollection<Achievement> AchievementUsers { get; set; } = new List<Achievement>();
    }
}
