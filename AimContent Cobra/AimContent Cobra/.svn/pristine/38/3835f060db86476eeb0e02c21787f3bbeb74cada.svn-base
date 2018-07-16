using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace AIM.Base.Classes
{
    /// <summary>
    /// UPDATES: 
    /// 2008-03-18 (aniv): Added startpageID
    /// 2008-05-14 (aniv): Added objectimagepath
    /// 2008-05-26 (aniv); Added customerpath to secureediting path
    /// 2008-10-21 (aniv): Added to AimContent Cobra
    /// </summary>
    public class Config
    {
        public string cssfile_config;
        public string cssfile_editor_config;
        public string customerpath;        
        public string siteurl_config;
        public string frommail_config;
        public string imagefolder_config;
        public string documentfolder_config;
        public string templatefolder_config;
        public string flashfolder_config;
        public string mediafolder_config;
        public string documentfilters_config;
        public string imagefilters_config;
        public string editorscheme_config;
        public int maxuploadbytesize;
        public string imagespathstostrip;
        public string anchorpathstostrip;
        public string mailserver_config;
        public int lcid_config;
        public int rootID;
        public int startpageID;
        public int siteID;
        public string applicationname;
        public string filearchivepath;
        public string aimbusinesspath;
        public string aimstatspath;
        public string visitors;
        public string liveStats;
        public string aimphotopath;
        public string objectimagepath;
        public string publicurl;

        public string secureEditing;
        public string secureEditingDocument;
        public string secureEditingImages;
        public string secureEditingFlash;
        public string secureEditingMedia;

        public bool useJournal;
        public string productregister;
        public int adminuserid;
        public bool useAdminRoles;

        public void GetConfig()
        {            
            CookieHandler cookieHandler = new CookieHandler();

            AIM.Business.WebSolution.DataObjectsDataContext db = new AIM.Business.WebSolution.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

            AIM.Business.WebSolution.Config config = (from linq_config in db.Configs
                                                      from linq_Site in linq_config.Sites
                                                      where linq_Site.SiteID == cookieHandler.SiteId
                                                      select linq_config
                                                          ).First<AIM.Business.WebSolution.Config>();

            try
            {
                adminuserid = cookieHandler.AdminUserId;

                customerpath = config.customerpath;
                siteurl_config = config.siteurl;
                frommail_config = config.frommail;
                cssfile_config = config.cssfile;

                imagefolder_config = customerpath + "images";
                flashfolder_config = customerpath + "flash";
                documentfolder_config = customerpath + "documents";
                templatefolder_config = customerpath + "templates";
                documentfilters_config = config.documentfilters;
                imagefilters_config = config.imagefilters;
                editorscheme_config = config.editorscheme;

                if (config.maxuploadbytesize.HasValue)
                    maxuploadbytesize = config.maxuploadbytesize.Value;
                else
                    maxuploadbytesize = 0;

                imagespathstostrip = config.imagespathstostrip;
                anchorpathstostrip = config.anchorpathstostrip;
                mailserver_config = config.mailserver;

                if (config.lcid.HasValue)
                    lcid_config = config.lcid.Value;
                else
                    lcid_config = 0;

                cssfile_editor_config = config.cssfile_editor;
                rootID = config.rootID.Value;
                startpageID = config.startpageID.Value;
                siteID = cookieHandler.SiteId;
                applicationname = config.applicationname;
                filearchivepath = config.filearchivepath;
                aimbusinesspath = config.aimbusinesspath;
                aimstatspath = config.aimstatspath;

                if (config.visitors.HasValue)
                    visitors = config.visitors.Value.ToString();
                else
                    visitors = "0";

                liveStats = config.liveStats;
                aimphotopath = config.aimphotopath;
                secureEditing = config.secureEditing;
                useAdminRoles = config.useAdminRoles;

                if (secureEditing != String.Empty)
                    objectimagepath = customerpath + secureEditing + "/ObjectImages/";
                else
                    objectimagepath = customerpath + "ObjectImages/";

                publicurl = config.publicurl;

                secureEditingDocument = customerpath + secureEditing + "/documents";
                secureEditingFlash = customerpath + secureEditing + "/flash";
                secureEditingImages = customerpath + secureEditing + "/images";
                secureEditingMedia = customerpath + secureEditing + "/media";

                productregister = config.productregisterpath;

                try
                {
                    useJournal = config.useJournal.Value;
                }
                catch
                {
                    useJournal = false;
                }

                mediafolder_config = customerpath + "media";
            }
            catch(Exception ex)
            {
                throw new Exception("AIM.Base.Classes.Config() error: All nessacery values has to be set: " + ex.Message);
            }
        }

    }
}
