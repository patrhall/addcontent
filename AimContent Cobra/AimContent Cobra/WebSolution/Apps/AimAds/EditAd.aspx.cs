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
using System.Data.Linq;
using System.Collections.Generic;
using System.IO;

namespace AimAds
{
    /*
     Framtida funktioner:
     * Flytta en annons mellan olika grupper
     */
    public partial class EditAd : AIM.Base.Modules.Page.AimAds
    {
        private int ADID;
        private int GroupID;

        protected override void Page_Load()
        {
            ADID = Convert.ToInt32(Request["ADID"]);
            GroupID = Convert.ToInt32(Request["GroupID"]);

            if (!IsPostBack)
            {
                init();
                hlReturn.NavigateUrl = "Ads.aspx?GroupID=" + Request["GroupID"].ToString();

                fillTargetDropdownList();

                AIM.Business.AimAds.AimAds_Group theGroup = (from linq_gr in db.AimAds_Groups where linq_gr.GroupID == GroupID select linq_gr).First<AIM.Business.AimAds.AimAds_Group>();
                if (!theGroup.AllowScripts.Value)
                {
                    trAllowScripts.Visible = false;
                }
                else
                {
                    FileUpLoadValidator.Enabled = false;
                }

                if (ADID != 0)
                    fillAd();
            }                        
        }

        private void init()
        {
            ListItem item = new ListItem("Svart", "solid 1px black");
            ListItem item2 = new ListItem("Röd", "solid 1px red");

            ddlBorders.Items.Insert(0, "Välj ram");
            ddlBorders.Items.Add(item);
            ddlBorders.Items.Add(item2);

        }

        protected void fillTargetDropdownList()
        {
            ListItem listItem = new ListItem();
            listItem.Text = "Samma Fönster";
            listItem.Value = "_self";

            ListItem listItem2 = new ListItem();
            listItem2.Text = "Nytt fönster";
            listItem2.Value = "_blank";

            ddlTarget.Items.Insert(0, "Välj");
            ddlTarget.Items.Insert(1, listItem);
            ddlTarget.Items.Insert(2, listItem2);
        }

        protected void fillAd()
        {
            AIM.Business.AimAds.AimAds_Item theItems = (from linq_ad in db.AimAds_Items where linq_ad.ID == ADID select linq_ad).First<AIM.Business.AimAds.AimAds_Item>();

            tbTitle.Text = theItems.Title;
            tbDescription.Text = theItems.Description;
            tbHref.Text = theItems.Href;
            
            ddlBorders.SelectedIndex = ddlBorders.Items.IndexOf(ddlBorders.Items.FindByValue(theItems.Border));


            if (theItems.ImageFilename != null || theItems.ImageFilename != "")
            {
                imgCurrent.Src = "../.." + config.customerpath + "ads/" + theItems.ImageFilename;
            }
            if (theItems.ImageFilename == null)
            {
                imgCurrent.Src = "../.." + config.customerpath + "ads/" + "noimageorscript.jpg";
            }

            if (theItems.LinkTarget != null || theItems.LinkTarget != "")
            {
                ddlTarget.SelectedIndex = ddlTarget.Items.IndexOf(ddlTarget.Items.FindByValue(theItems.LinkTarget));
                
                theItems.LinkTarget = "_blanc";//TEMP
            }            

             cbShow.Checked = theItems.Show;

             if (theItems.PublishFrom != null)
             {
                 if(theItems.PublishFrom.HasValue)
                    calFrom.SelectedDate = theItems.PublishFrom.Value;
                 
                 if(theItems.PublishTo.HasValue)
                    calTo.SelectedDate = theItems.PublishTo.Value;
                 
                 //cbUsePublicationDate.Checked = true;
             }

             tbScriptCode.Text = theItems.ScriptCode;
        }


        protected void btnSave_OnClick(object sender, System.EventArgs e)
        {
            AIM.Business.AimAds.AimAds_Item theItems;

            if (ADID == 0)
            {
                theItems = new AIM.Business.AimAds.AimAds_Item();
                db.AimAds_Items.InsertOnSubmit(theItems);
                theItems.CreatedDate = DateTime.Now;

                int maxSort = 0;

                if (db.AimAds_Items.Max(Sort => Sort.Sort).HasValue)
                {
                    maxSort = db.AimAds_Items.Max(Sort => Sort.Sort).Value;
                }

                theItems.Sort = maxSort + 1;
            }
            else
                theItems = (from linq_ad in db.AimAds_Items where linq_ad.ID == ADID select linq_ad).First<AIM.Business.AimAds.AimAds_Item>();


            theItems.Title = tbTitle.Text;
            theItems.Description = tbDescription.Text;
            theItems.Href = tbHref.Text;

            if (ddlBorders.SelectedIndex != 0)
                theItems.Border = ddlBorders.SelectedValue;
            else
                theItems.Border = "";

            theItems.AimAds_Group_FK = GroupID;            
            
            if(ddlTarget.SelectedIndex!= 0)
                theItems.LinkTarget = ddlTarget.SelectedValue;

            if (cbDelete.Checked)
            {
                theItems.DeletedDate = DateTime.Now;
                theItems.Deleted = true;
            }
            else
            {
                theItems.Deleted = false;
            }
            
            theItems.ModifiedDate = DateTime.Now;
            
            theItems.Show = cbShow.Checked;


            //if (cbUsePublicationDate.Checked == true)
            //{
                theItems.PublishFrom = calFrom.SelectedDate;
                theItems.PublishTo = calTo.SelectedDate;
            //}
            //else
            //{
                //theItems.PublishFrom = null;
                //theItems.PublishTo = null;
            //}            

            theItems.ScriptCode = tbScriptCode.Text;

            if (fuImageUploader.HasFile && fuImageUploader.PostedFile.ContentLength > 0)
            {
                //100204
                try
                {
                    //if (Boolean.Parse(Preference.getValue("news", "picture", "used")))
                    //{
                    //    //spara nyheterna här

                    //    DateTime now = DateTime.Now;

                    //    string upFilename = fuImageUploader.PostedFile.FileName;
                    //    int length = upFilename.Length;

                    //    for(int i = 0; i < length; i++)
                    //    {   
                    //        if(upFilename.Contains("\\"))
                    //        {
                    //            upFilename = upFilename.Remove(0, fuImageUploader.PostedFile.FileName.IndexOf("\\"));
                    //        }
                    //    }

                    //    string objectimagefilename = now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + upFilename;

                    //    theObject.Picture = objectimagefilename;
                    //    theObject.UploadedImagePath = objectimagefilename;                        

                    //    string newsFolder = config.objectimagepath.Replace("ObjectImages/", "news/images/");

                    //    fuImageUploader.SaveAs(Server.MapPath(newsFolder + objectimagefilename));

                    //    //fuImageUploader.SaveAs(Server.MapPath(newsFolder + "small/" + "resized_" + objectimagefilename));

                    //    AimResources.ImageHandler.ResizeImageBox(Server.MapPath(newsFolder), Server.MapPath(newsFolder + "small/"), objectimagefilename, "resized_" + objectimagefilename, int.Parse(Preference.getValue("photoalbum", "imagesize_slideshow", "width")), int.Parse(Preference.getValue("photoalbum", "imagesize_slideshow", "height")), false);
                    //}
                    //else
                    //{

                    DateTime now = DateTime.Now;


                    string objectimagefilename = now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + AimResources.StringFunctions.GenerateRandomLowerCaseString(6) + Path.GetExtension(fuImageUploader.PostedFile.FileName);

                    string newsFolder = config.objectimagepath.Replace("ObjectImages/", "ads/");

                    fuImageUploader.SaveAs(Server.MapPath(newsFolder + objectimagefilename));

                    theItems.ImageFilename = objectimagefilename;

                }
                catch
                {

                }
            }

            db.SubmitChanges();

            Response.Redirect("Ads.aspx?GroupID=" + Request["GroupID"].ToString());
        }        
    }
}
