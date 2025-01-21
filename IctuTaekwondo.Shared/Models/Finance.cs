using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Models
{
    public class Finance
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Loại giao dịch")]
        [MaxLength(10)]
        public TransactionType Type { get; set; }

        [DisplayName("Danh mục")]
        [MaxLength(50)]
        public string Category { get; set; } = null!;

        [DisplayName("Số tiền")]
        public int Amount { get; set; }

        [DisplayName("Ngày giao dịch")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; } 

        [DisplayName("Ngày tạo")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
    }
}
