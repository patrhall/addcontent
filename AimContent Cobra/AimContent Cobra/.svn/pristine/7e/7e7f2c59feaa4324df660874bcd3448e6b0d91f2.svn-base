using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace AIM.Base.Classes
{
    public class Preference
    {
        XmlDocument xml = new XmlDocument();
        XmlDocument xml_default = new XmlDocument();

        public Preference(string customerPreferencePath)
        {
            string defaultpath;

            defaultpath = HttpContext.Current.Server.MapPath("/App_Data/Preference/defaultpreference.xml");

            try
            {
                xml.Load(customerPreferencePath);
            }
            catch
            {
                xml.Load(defaultpath);
            }

            xml_default.Load(defaultpath);            
        }

        /// <summary>
        /// Returns an instance of Preference
        /// </summary>
        /// <param name="customerpath"></param>
        /// <returns></returns>
        public static Preference getPreference(string customerpath)
        {
            return new Preference(HttpContext.Current.Server.MapPath(customerpath + "preference/preference.xml"));
        }

        /// <summary>
        /// returns preference value specific for customer, otherwise default value
        /// </summary>
        /// <param name="groupname">ex. news, intranet</param>
        /// <param name="preferenceName">Tagname</param>
        /// <param name="preferenceValue">which value to get</param>
        /// <returns></returns>
        public string getValue(string groupname, string preferenceName, string preferenceValue)
        {
            try
            {
                XmlNodeList xmlnews = xml.GetElementsByTagName(groupname);
                XmlNode xmlnewsnode = xmlnews[0].SelectSingleNode(preferenceName);
                return xmlnewsnode.Attributes[preferenceValue].Value;
            }
            catch
            {
                XmlNodeList xmlnews = xml_default.GetElementsByTagName(groupname);
                XmlNode xmlnewsnode = xmlnews[0].SelectSingleNode(preferenceName);
                return xmlnewsnode.Attributes[preferenceValue].Value;
            }
        }
    }
}
