using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ShareReading.WebSite.Common
{
    public static class EnumExtension
    {    /// <summary>
         /// RemarkAttribute
         /// </summary>
        public class RemarkAttribute : Attribute
        {
            private string _remark = string.Empty;
            public RemarkAttribute(string remark)
            {
                _remark = remark;
            }

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark
            {
                get
                {
                    return _remark;
                }
                set
                {
                    _remark = value;
                }
            }

            public string Description
            { get; set; }
        }
        /// <summary>
        /// 获取当前枚举值的Remark
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetRemark(this Enum value)
        {
            string remark = string.Empty;
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());


            object[] attrs = fieldInfo.GetCustomAttributes(typeof(RemarkAttribute), false);
            RemarkAttribute attr = (RemarkAttribute)attrs.FirstOrDefault(a => a is RemarkAttribute);
            if (attr == null)
            {
                remark = fieldInfo.Name;
            }
            else
            {
                remark = attr.Remark;
            }


            return remark;
        }
    }
}