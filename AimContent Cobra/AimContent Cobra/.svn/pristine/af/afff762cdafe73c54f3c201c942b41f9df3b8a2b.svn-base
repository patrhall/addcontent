using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace AimBusiness.Customers.Whiskyflaskan.Website
{
    public partial class HandleCalendarHatches : AIM.Base.Modules.Page.AimBusiness.Whiskyflaskan.Website.AimBusiness
    {
        protected override void Page_Load()
        {
            if (!IsPostBack)
                init();
        }

        protected void init()
        {
            Session.LCID = config.lcid_config;

            //this.linkStyle.Attributes.Add("href", config.customerpath + config.cssfile_config);

            //editor1.Editable = true;
            //editor1.HasPermission = true;
            //editor1.Scheme = config.editorscheme_config;

            string[] images = new string[1];
            images[0] = config.imagefolder_config + "/hatches";//lägger luckbilderna i denna mappen

            string[] imagefilters = new string[1];
            imagefilters[0] = config.imagefilters_config;

            string[] media = new string[1];
            media[0] = config.mediafolder_config;

            string[] documents = new string[1];
            documents[0] = config.documentfolder_config;

            string[] flash = new string[1];
            flash[0] = config.flashfolder_config;

            string[] documentfilters = new string[1];
            documentfilters[0] = config.documentfilters_config;

            string[] templates = new string[1];
            templates[0] = config.templatefolder_config;

            string[] css = new string[1];
            css[0] = config.customerpath + config.cssfile_editor_config;

            editor1.ImageManager.ViewPaths = images;
            editor1.ImageManager.UploadPaths = images;
            editor1.ImageManager.DeletePaths = images;
            editor1.MediaManager.ViewPaths = media;
            editor1.MediaManager.UploadPaths = media;
            editor1.MediaManager.DeletePaths = media;
            editor1.DocumentManager.ViewPaths = documents;
            editor1.DocumentManager.UploadPaths = documents;
            editor1.DocumentManager.DeletePaths = documents;
            editor1.TemplateManager.ViewPaths = templates;
            editor1.TemplateManager.UploadPaths = templates;
            editor1.TemplateManager.DeletePaths = templates;
            editor1.FlashManager.ViewPaths = flash;
            editor1.FlashManager.UploadPaths = flash;
            editor1.FlashManager.DeletePaths = flash;
            editor1.TableLayoutCssFile = css[0];
            editor1.DocumentManager.MaxUploadFileSize = config.maxuploadbytesize;
            editor1.FlashManager.MaxUploadFileSize = config.maxuploadbytesize;
            editor1.ImageManager.MaxUploadFileSize = config.maxuploadbytesize;
            editor1.MediaManager.MaxUploadFileSize = config.maxuploadbytesize;
            editor1.TemplateManager.MaxUploadFileSize = config.maxuploadbytesize;

            if (!IsPostBack)
            {
                Hashtable hash = new Hashtable();

                if (Request["lID"].ToString() != "0")
                {
                    var result = db.usp_CalendarR_Get_SpecificHatch(Convert.ToInt32(Request["lID"])).ToList();
                    var dsHatch = AimResources.Collections.List2DataTable<AIM.Business.AimBusiness.Whiskyflaskan.Website.usp_CalendarR_Get_SpecificHatchResult>(result);
                    fillInformation(dsHatch);
                }
            }

        }

        protected void fillInformation(DataTable ds)
        {
            tbNo.Text = ds.Rows[0]["HatchNo"].ToString();
            editor1.Content = ds.Rows[0]["HTML"].ToString();
            datePicker.SelectedDate = DateTime.Parse(ds.Rows[0]["Date"].ToString());
        }
        
        protected void editor1_SubmitClicked(object sender, EventArgs e)
        {
            if (tbNo.Text != "")
            {
                int lID = Convert.ToInt32(Request["lID"].ToString());
                int cID = Convert.ToInt32(Request["cID"].ToString());
                int hatchNo = Convert.ToInt32(tbNo.Text);
                string html = editor1.Content;
                DateTime date = (DateTime)datePicker.SelectedDate;

                db.usp_CalendarR_Save_Hatch(lID, cID, hatchNo, html, date);

                lblError.Text = "";

                Response.Redirect("EditCalendar.aspx?oID=" + Request["oID"].ToString());
            }
            else
            {
                lblError.Text = "Du måste fylla i ett nummer och ett datum för luckan.";
            }
        }
    }
}