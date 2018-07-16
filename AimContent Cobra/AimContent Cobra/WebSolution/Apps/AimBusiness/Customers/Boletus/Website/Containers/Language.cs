using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AimBusiness.Customers.Boletus.Website.Containers
{
    public static class Language 
    {
        public static string Latin = "Latin";
        public static string English = "English";
        public static string German = "German";

        public static List<string> AllLanguage
        {
            get
            {
                return new List<string>() { Latin, English, German };
            }
        }
    }

}