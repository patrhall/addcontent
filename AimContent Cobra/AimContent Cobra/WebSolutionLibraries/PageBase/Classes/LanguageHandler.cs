using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

namespace AIM.Base.Classes
{
    public class LanguageHandler
    {
        /// <summary>
        /// Available languages
        /// </summary>
        public enum Language { SWEDISH, ENGLISH };

        private XmlDocument _xml = new XmlDocument();
        private Language _language;

        /// <summary>
        /// Used for all pages
        /// </summary>
        /// <param name="language"></param>
        public LanguageHandler(Language language)
        {
            init(language, HttpContext.Current.Request.FilePath.Remove(HttpContext.Current.Request.FilePath.LastIndexOf(".")));
        }

        /// <summary>
        /// Used for usercontrols with own languagefile
        /// </summary>
        /// <param name="language"></param>
        /// <param name="userControlPath"></param>
        public LanguageHandler(Language language, string userControlPath)
        {
            init(language, userControlPath.Remove(userControlPath.LastIndexOf(".")));
        }

        /// <summary>
        /// Initialize languagehandler
        /// </summary>
        /// <param name="language"></param>
        /// <param name="userControlPath"></param>
        private void init(Language language, string userControlPath)
        {
            _language = language;

            try
            {
                string path = HttpContext.Current.Server.MapPath("/App_Data/Language/"
                    + language.ToString().ToLower()
                    + userControlPath
                    + ".xml");

                _xml.Load(path);
            }
            catch
            {
                throw new Exception("AIM.Base.Classes.LanguageHandler Initial error: language xml is required /Language/"
                    + language.ToString().ToLower()
                    + userControlPath
                    + ".xml"
                    );
            }
        }

        /// <summary>
        /// Get language value, if not exists return String.Empty
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public string getValue(string tag)
        {
            try
            {
                XmlNode xmlnewsnode = _xml.GetElementsByTagName(tag)[0];
                return xmlnewsnode.Attributes["value"].Value;
            }
            catch { }

            return String.Empty;
        }

        public string getLanguageName()
        {
            return _language.ToString().ToLower();
        }

        /// <summary>
        /// Get language for logged in user
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <returns></returns>
        public static LanguageHandler getLanguage(int adminUserId)
        {
            return getLanguage(adminUserId, String.Empty);
        }

        /// <summary>
        /// Get language for logged in user and init for used usercontrol
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="userControlPath"></param>
        /// <returns></returns>
        public static LanguageHandler getLanguage(int adminUserId, string userControlPath)
        {
            LanguageHandler language;

            AIM.Business.WebSolution.DataObjectsDataContext db = new AIM.Business.WebSolution.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

            string languagename = (from linq_admin in db.Admins
                                   where linq_admin.ID == adminUserId
                                   select linq_admin.AdminLanguage.Name
                                   ).ToList<string>()[0];

            AIM.Base.Classes.LanguageHandler.Language choosenLanguage;

            switch (languagename)
            {
                case "English":
                    choosenLanguage = AIM.Base.Classes.LanguageHandler.Language.ENGLISH;
                    break;
                case "Swedish":
                default:
                    choosenLanguage = AIM.Base.Classes.LanguageHandler.Language.SWEDISH;
                    break;
            }

            if (userControlPath == String.Empty)                
                language = new AIM.Base.Classes.LanguageHandler(choosenLanguage);
            else
                language = new AIM.Base.Classes.LanguageHandler(choosenLanguage, userControlPath);

            return language;
        }
    }
}
