using System;
using System.ComponentModel;

namespace ShareReading.WebSite.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        [DisplayName("用户名")]
        public string UserName { get; set; }
        [DisplayName("密码")]
        public string PassWord { get; set; }
        [DisplayName("确认密码")]
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
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}