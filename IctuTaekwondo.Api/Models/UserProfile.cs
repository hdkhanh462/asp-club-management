using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IctuTaekwondo.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Api.Models
{
    [Index("UserId", Name = "UserProfiles_UserId_Key", IsUnique = true)]
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public Gender? Gender { get; set; } 
        public DateOnly? DateOfBirth { get; set; } 
        public string? Address { get; set; }
        
        [StringLength(20)]
        public RankName? CurrentRank { get; set; } 
        public DateOnly? JoinDate { get; set; } 
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("UserProfile")]
        public virtual User User { get; set; } = null!;
    }
}
