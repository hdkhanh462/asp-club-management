using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Finance
{
    public class FinanceResponse
    {
        public int Id { get; set; }

        [DisplayName("Loại giao dịch")]
        public TransactionType Type { get; set; }

        [DisplayName("Danh mục")]
        public string? Category { get; set; }

        [DisplayName("Số tiền")]
        public long Amount { get; set; }

        [DisplayName("Ngày giao dịch")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; }

        [DisplayName("Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
    }
}
