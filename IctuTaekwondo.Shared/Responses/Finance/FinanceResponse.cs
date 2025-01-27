using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Finance
{
    public class FinanceResponse
    {
        public int Id { get; set; }

        [Display(Name = "Loại giao dịch")]
        public TransactionType Type { get; set; }

        [Display(Name = "Loại chi phí")]
        public string Category { get; set; }

        [Display(Name = "Số tiền")]
        public int Amount { get; set; }

        [Display(Name = "Ngày giao dịch")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
    }
}
