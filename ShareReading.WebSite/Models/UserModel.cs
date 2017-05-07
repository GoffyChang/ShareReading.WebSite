using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShareReading.WebSite.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        [DisplayName("用户名")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string UserName { get; set; }
        [DisplayName("密码")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0}长度必须在{2}跟{1}之间")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string PassWord { get; set; }
        [DisplayName("确认密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Compare("PassWord", ErrorMessage = "密码要一致")]
        public string PassWordConfirm { get; set; }
        public int UserSex { get; set; }
        public int UserAge { get; set; }
        [DisplayName("手机号")]
        public string UserTel { get; set; }
        [DisplayName("邮箱")]
        public string UserEmail { get; set; }
        public string UserIMage { get; set; }
        public int State { get; set; }
        public bool Remember { get; set; }

        public string VerifyCode { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}