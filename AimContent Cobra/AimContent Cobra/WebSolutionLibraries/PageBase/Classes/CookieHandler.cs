using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AIM.Base.Classes
{
    /// <summary>
    /// Handles the login AIM-cookie
    /// Created by: Andreas Ivarsson 2008-10-21
    /// UPDATES:
    /// -
    /// </summary>
    public class CookieHandler
    {        
        public CookieHandler()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AIM"];

            string[] cookieValues = cookie.Value.Split(new char[] { ':' });
            _siteid = Convert.ToInt32(cookieValues[0]);
            _adminuserid = Convert.ToInt32(cookieValues[1]);
        }

        /// <summary>
        /// Create a site cookie
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="adminlanguageid"></param>
        public static void SetCookie(int siteid, int adminuserid)
        {
            HttpCookie AIM_cookie = new HttpCookie("AIM");
            AIM_cookie.Value = siteid.ToString() + ":" + adminuserid.ToString();
            HttpContext.Current.Response.Cookies.Add(AIM_cookie);
        }

        /// <summary>
        /// Reset the site cookie, i.e. logging out
        /// </summary>
        public static void ResetCookie()
        {
            HttpContext.Current.Request.Cookies["AIM"].Value = String.Empty;
            HttpContext.Current.Request.Cookies["AIM"].Expires = DateTime.MinValue;
        }

        /// <summary>
        /// Session stored siteid
        /// </summary>
        public int SiteId
        {
            get
            {
                return _siteid;
            }
        }

        /// <summary>
        /// Session stored administrator admin id
        /// </summary>
        public int AdminUserId
        {
            get
            {
                return _adminuserid;
            }
        }        

        private int _siteid;
        private int _adminuserid;        
    }
}
