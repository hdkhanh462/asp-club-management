using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Api.Models
{
    public class Finance
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        public TransactionType Type { get; set; }

        [StringLength(50)]
        public string Category { get; set; } = null!;

        public int Amount { get; set; }

        //[Column(TypeName = "timestamp without time zone")]
        public DateTime TransactionDate { get; set; }

        public string? Description { get; set; } 

        //[Column(TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
    }
}
