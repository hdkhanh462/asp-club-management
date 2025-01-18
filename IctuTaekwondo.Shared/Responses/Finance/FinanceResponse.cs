using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Finance
{
    public class FinanceResponse
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual UserResponse CreatedBy { get; set; }
    }
}
