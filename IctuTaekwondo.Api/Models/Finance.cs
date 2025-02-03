using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Api.Models
{
    public class Finance
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public TransactionType Type { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? Category { get; set; }

        [Range(1000, long.MaxValue)]
        public long Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
