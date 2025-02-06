using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.DataAnnotations;
using IctuTaekwondo.Shared.Enums;
namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class UpdateRolesSchema
    {
        [Display(Name = "Mật khẩu của bạn")]
        [Required(ErrorMessage = "Mật khẩu của bạn không được để trống")]
        [DataType(DataType.Password)]
        public string YourPassword { get; set; }

        [Display(Name = "Vai trò")]
        [EnumList(typeof(Role), ErrorMessage = "Vai trò không hợp lệ")]
        public List<Role> Roles { get; set; } = [];
    }
}
