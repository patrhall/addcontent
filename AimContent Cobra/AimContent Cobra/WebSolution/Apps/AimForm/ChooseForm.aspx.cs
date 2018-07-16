using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AIM.General.UserControls;
using System.Data.Linq;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.Web.Profile;

namespace AimForm
{
    public partial class ChooseForm : AIM.Base.Modules.Page.AimForm
    {
        public int siteID;
        public int formID = 0;
        public int sectionID;
        public DataSet dsSections;
        public DataSet dsForm;
        public DataSet dsCompany;
        public string strCompany = "";
        public int objectID;
        AIM.General.UserControls.PagePreference pref;

        protected override void Page_Load()
        {
            config.GetConfig();
            
            objectID = Convert.ToInt32(Request["ID"]);

            loadPagePreference();
            setLanguage();

            if (!IsPostBack)
            {
                phPagePreference.Visible = false;
                pnl_ChooseFrom.Visible = true;

                List<AIM.Business.AimForm.Form_Form> formList =
                (from o in db.Form_Forms
                 where o.SiteID == config.siteID && o.Deleted != true
                 orderby o.FormName ascending
                 select o).ToList<AIM.Business.AimForm.Form_Form>();

                bool gate = false;
                foreach (AIM.Business.AimForm.Form_Form f in formList)
                {
                    gate = true;
                }

                if (gate)
                {
                    rblForm.DataSource = formList;
                    rblForm.DataTextField = "FormName";
                    rblForm.DataValueField = "FormID";
                    rblForm.DataBind();

                    AIM.Business.AimForm.Object theObject = (from linq_object in db.Objects where linq_object.ObjectID == Convert.ToInt32(Request["oID"].ToString()) select linq_object).First<AIM.Business.AimForm.Object>();
                    try
                    {
                        string formID = theObject.HTMLContent.ToString();
                        rblForm.SelectedValue = formID;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    lblForm.Visible = true;
                    rblForm.Visible = false;
                    btnSave.Visible = false;
                }
            }
        }

        protected override int SiteModuleId
        {
            get
            {
                return 1;
            }
        }   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPreference_OnClick(object sender, EventArgs e)
        {
            phPagePreference.Visible = true;
            pnl_ChooseFrom.Visible = false;
            initPagePreference();
        }

        protected void btnPref_Clicked(object sender, EventArgs e)
        {
            phPagePreference.Visible = true;

            initPagePreference();
            
        }

        private void setLanguage()
        {
            lbPreference.Text = Language.getValue("lbPreference");       
        }

        private void loadPagePreference()
        {
            Control controlPagePreference = LoadControl("/General/UserControls/PagePreference.ascx");
            pref = (AIM.General.UserControls.PagePreference)controlPagePreference;
            pref.setObjectID(Convert.ToInt32(Request["oID"]));
            phPagePreference.Controls.Add(pref);
        }

        /// <summary>
        /// Init pagepreference
        /// </summary>
        private void initPagePreference()
        {
            pref.viewUrl = Boolean.Parse(Preference.getValue("content", "directurl", "used"));

            switch (Preference.getValue("content", "directurl", "urlrewrite"))
            {
                case "NOTUSED":
                    pref.viewUrlRewrite = PagePreference.UrlRewriteOptions.NOTUSED;
                    break;
                case "NOMASTER":
                    pref.viewUrlRewrite = PagePreference.UrlRewriteOptions.NOMASTER;
                    break;
                case "MASTER":
                    pref.viewUrlRewrite = PagePreference.UrlRewriteOptions.MASTER;
                    break;
            }

            //Load preference
            pref.init();

        }

               

        protected void btnSave_Clicked(object sender, EventArgs e)
        {
            objectID = Convert.ToInt32(Request["oID"]);
            
            siteID = Convert.ToInt32(config.siteID);
            int formID = Convert.ToInt32(rblForm.SelectedValue);
            

            if (Request["oID"] != null)
            {
                AIM.Business.AimForm.Object theObject = (from linq_object in db.Objects where linq_object.ObjectID == Convert.ToInt32(Request["oID"]) select linq_object).First<AIM.Business.AimForm.Object>();                            

                //Connect form to object

                theObject.HTMLContent = rblForm.SelectedValue;

                db.SubmitChanges();
                Response.Redirect(Request.Url.ToString());

            }

        }

    }
}
