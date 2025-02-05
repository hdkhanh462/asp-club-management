using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IctuTaekwondo.Shared.Enums
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum? enumValue)
        {
            if (enumValue == null) return string.Empty;

            var attribute = enumValue.GetType()
                                     .GetField(enumValue.ToString())
                                     ?.GetCustomAttribute<DisplayAttribute>();

            return attribute?.Name ?? enumValue.ToString();
        }
    }

    public enum Gender
    {
        [Display(Name = "Nam")]
        Male,

        [Display(Name = "Nữ")]
        Female
    }

    public enum RankName
    {
        [Display(Name = "Đai Trắng")]
        WhiteBelt,

        [Display(Name = "Đai Vàng")]
        YellowBelt,

        [Display(Name = "Đai Xanh")]
        BlueBelt,

        [Display(Name = "Đai Đỏ")]
        RedBelt,

        [Display(Name = "Đai Đen")]
        BlackBelt
    }
    
    public enum TransactionType
    {
        [Display(Name = "Thu nhập")]
        Income,

        [Display(Name = "Chi tiêu")]
        Expense
    }
    
    public enum Role
    {
        [Display(Name = "Hội viên")]
        Member,

        [Display(Name = "Quản trị viên")]
        Manager,

        [Display(Name = "Quản trị viên hệ thống")]
        Admin,
    }

    public enum EventStatus
    {
        [Display(Name = "Chưa diễn ra")]
        NotStarted,

        [Display(Name = "Đã bắt đầu")]
        Started,
        
        [Display(Name = "Đã kết thúc")]
        Ended,
    }

    public enum ToastType
    {
        Info,

        Warning,

        Success,

        Error,
    }
}
