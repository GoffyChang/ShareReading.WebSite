using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareReading.WebSite.Models
{
    public class CommonEnum
    {
        public enum UserState
        {
            Normal = 0,
            Frozen = 1,
            Deleted = 2
        }

        public enum UserType
        {
            User = 1,
            Admin = 2,
            SuperAdmin = 4
        }

        public enum CategoryState
        {
            Normal = 0,
            Frozen = 1,
            Deleted = 2
        }
    }
}