using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AIM.Base;

namespace AIM.Base.Modules.Page
{
    public abstract class AimPhoto : PageBase
    {
        protected AIM.Business.AimPhoto.DataObjectsDataContext db = new AIM.Business.AimPhoto.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        protected override int SiteModuleId
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Web Form startup function, please notice zero parameters
        /// </summary>
        protected abstract override void Page_Load();

        /// <summary>
        /// Init AimPhoto specific properties
        /// </summary>
        protected override void InitModule()
        {
            try
            {
                AIM.Business.AimPhoto.AlbumPhotoCategory tempCat;

                if (!String.IsNullOrEmpty(HttpContext.Current.Request["CategoryId"]))
                    tempCat = (from o in db.AlbumPhotoCategories where o.ID == int.Parse(HttpContext.Current.Request["CategoryId"]) && o.ObjectID == int.Parse(HttpContext.Current.Request["oID"]) select o).First<AIM.Business.AimPhoto.AlbumPhotoCategory>();
            }
            catch
            {
                throw new Exception("AIM.Base.Page.AimPhoto error: User has no access to this category");
            }
        }
    }
}
