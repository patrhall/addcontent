using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace AIM.Business
{
    public class DataManager
    {
        public static string ConnectionString
        {
            get
            {
                try
                {
                    //Get host.
                    string host = System.Web.HttpContext.Current.Request.Url.Host;
                    //Get xmlpath. The xml file must be located in App_Data.Connections/Hosts.xml.
                    string xmlPath = System.Web.HttpContext.Current.Server.MapPath("/App_Data/Connections/Hosts.xml");
                    XDocument xmlDoc = XDocument.Load(xmlPath);
                    //Get the element with correct host.
                    XElement element = (from e in xmlDoc.Root.Descendants("host")
                                        where e.Attribute("url").Value == host
                                        select e).Single<XElement>();

                    //Return the connectionsstring. If null, return ugly ugly ugly hack
                    if (!String.IsNullOrEmpty(element.Attribute("databaseConnectionString").Value))
                        return element.Attribute("databaseConnectionString").Value;
                    else
                        return "workstation id=aimit1;packet size=4096;user id=aimcontent;data source=aimit1;persist security info=True;initial catalog=AimContent;password=jit38#L%;Min Pool Size=10;Max Pool Size=100";
                    //return WebConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
                }
                catch
                {
                    return "workstation id=aimit1;packet size=4096;user id=aimcontent;data source=aimit1;persist security info=True;initial catalog=AimContent;password=jit38#L%;Min Pool Size=10;Max Pool Size=100";
                    //return "workstation id=AIMITUTV3;packet size=4096;user id=aimit_aim;data source=AIMITUTV3;persist security info=True;initial catalog=Aimit_Aim;password=cmstilltusen;Min Pool Size=10;Max Pool Size=100";
                }
            }
        }
    }
}
