using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Enums
{
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

    public enum RegistrationStatus
    {
        [Display(Name = "Đang chờ")]
        Pending,

        [Display(Name = "Đã duyệt")]
        Approved,

        [Display(Name = "Đã huỷ")]
        Cancelled,
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
}
