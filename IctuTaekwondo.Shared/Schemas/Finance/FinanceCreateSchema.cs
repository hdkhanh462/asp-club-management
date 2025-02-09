using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Schemas.Finance
{
    public class FinanceCreateSchema
    {
        [Display(Name = "Loại giao dịch")]
        [Required(ErrorMessage = "Loại giao dịch không được để trống")]
        [EnumDataType(typeof(TransactionType), ErrorMessage = "Loại giao dịch không hợp lệ")]
        public TransactionType Type { get; set; }

        [Display(Name = "Danh mục")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Danh mục phải có độ dài từ 2 đến 50 ký tự")]
        public string? Category { get; set; }

        [Display(Name = "Số tiền")]
        [Required(ErrorMessage = "Số tiền không được để trống")]
        [Range(1000, long.MaxValue, ErrorMessage = "Số tiền phải có giá trị lớn hơn hoặc bằng 1000")]
        public long Amount { get; set; } = 1000;

        [Display(Name = "Ngày giao dịch")]
        [Required(ErrorMessage = "Ngày giao dịch không được để trống")]
        [DataType(DataType.DateTime, ErrorMessage = "Đinh dạng ngày giờ không chính xác")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
    }
}
