using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;
using AimResources;
using System.IO;

namespace AimBusiness.Customers.Whiskyflaskan.Website
{
    public partial class EditCalendar : AIM.Base.Modules.Page.AimBusiness.Whiskyflaskan.Website.AimBusiness
    {
        protected override void Page_Load()
        {
            Session.LCID = config.lcid_config;

            //this.linkStyle.Attributes.Add("href", config.customerpath + config.cssfile_config);

            if (!IsPostBack)
                init();
        }

        protected void init()
        {
            var objectID = Convert.ToInt32(Request["oID"]);
            var siteID = config.siteID;

            if (objectID > 0)
            {
                var calendarItems = db.usp_CalendarR_Get_Calendar(objectID, siteID).ToList();
                lblID.Text = "";                

                colorPickBtn.OnClientClick = "showColorPicker(this, document.getElementById('" + rgb2.ClientID + "')); return false;";

                if (calendarItems.Count() > 0)
                {
                    fillCalendar(calendarItems);
                }
            }
        }

        protected void fillCalendar(List<AIM.Business.AimBusiness.Whiskyflaskan.Website.usp_CalendarR_Get_CalendarResult> items)
        {
            tbName.Text = items[0].Name.ToString();
            tbDescription.Text = items[0].Text.ToString();
            lblPicture.Text = items[0].Picture.ToString();

            lblID.Text = items[0].CID.ToString();
            fillGrid(Convert.ToInt32(lblID.Text));

            tbMessage.Text = items[0].Message.ToString();

            rgb2.Value = items[0].bgColor.ToString();
            rgb2.Style.Value = "background-color:" + rgb2.Value + ";";
            lblChosenColor.Style.Value = "background-color:" + rgb2.Value + ";";
        }

        protected void fillGrid(int ID)
        {
            var calendarHatches = db.usp_CalendarR_Get_Hatches(ID).ToList();

            gvHatches.DataSource = calendarHatches;
            gvHatches.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string fullName;
            string fileName2;

            if (tbName.Text != "")
            {
                if (filePic.PostedFile.FileName != "")
                {
                    string pic = filePic.PostedFile.FileName.Substring(filePic.PostedFile.FileName.LastIndexOf(@"\") + 1);
                    string pathOrig = Server.MapPath(config.customerpath + "/Calendars/");
                    string path_thumb = pathOrig + "Resized/";

                    fileName2 = pic;
                    fullName = pathOrig + pic;

                    filePic.PostedFile.SaveAs(fullName);
                    ImageHandler.ResizeImageBox(pathOrig, path_thumb, pic, pic, 640, 360, false);

                    if (pic != lblPicture.Text && lblPicture.Text != "n/a")
                    {
                        DbConnection connection = db.Connection;
                        DbCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT Picture FROM Calendar_Calendar WHERE Picture ='@PIC'";
                        SqlParameter param1 = new SqlParameter("@PIC", SqlDbType.Text);
                        param1.Value = lblPicture.Text;
                        command.Parameters.Add(param1);

                        connection.Open();
                        DbDataReader dataReader = command.ExecuteReader();
                        connection.Close();

                        try
                        {
                            if (dataReader.HasRows && dataReader.GetString(0) == lblPicture.Text)
                            {
                                File.Delete(pathOrig + lblPicture.Text);
                            }
                        }
                        catch
                        {

                        }
                    }
                }

                //try
                //{
                    int? cID = null;
                    if (lblID.Text != "")
                    {
                        cID = Convert.ToInt32(lblID.Text);
                    }

                    int oID = Convert.ToInt32(Request["oID"].ToString());
                    string name = tbName.Text;
                    string picture = lblPicture.Text;
                    if (filePic.PostedFile.FileName != "")
                    {
                        picture = filePic.PostedFile.FileName.Substring(filePic.PostedFile.FileName.LastIndexOf(@"\") + 1);
                    }

                    int siteID = Convert.ToInt32(config.siteID);
                    string text = tbDescription.Text;
                    string bgColor = rgb2.Value.ToString();
                    string message = tbMessage.Text;

                    db.usp_CalendarR_Save(cID, oID, name, picture, siteID, text, bgColor, message);

                    lblError.Text = "";

                    Response.Redirect("EditCalendar.aspx?oID=" + Request["oID"].ToString());
                //}
                //catch
                //{
                //    lblError.Text = "Det gick inte att spara kalendern.";
                //}
            }
            else
            {
                lblError.Text = "Du måste fylla i ett namn på den nya sidan.";
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("HandleCalendarHatches.aspx?oID=" + Request["oID"].ToString() + "&cID=" + lblID.Text + "&lID=" + "0");
        }

        public void gvHatches_Edit(object sender, CommandEventArgs e)
        {
            Response.Redirect("HandleCalendarHatches.aspx?oID=" + Request["oID"].ToString() + "&cID=" + lblID.Text + "&lID=" + e.CommandArgument.ToString());
        }

        public void gvHatches_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.FindControl("linkDelete") != null)
            {
                LinkButton linkDelete = (LinkButton)e.Row.FindControl("linkDelete");
                linkDelete.Attributes.Add("onclick", "javascript:return confirm('Vill du radera denna lucka?')");
            }
        }

        public void gvHatches_Delete(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DELETE1")
            {
                try
                {
                    DbConnection connection = db.Connection;
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM dbo.Calendar_Hatch WHERE LID = @LID";
                    SqlParameter param1 = new SqlParameter("@LID", SqlDbType.Int);
                    param1.Value = Convert.ToInt32(e.CommandArgument.ToString());
                    command.Parameters.Add(param1);

                    connection.Open();
                    int numRowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (numRowsAffected == 0)
                    {
                        //lblError.Text = "Det går inte att ta bort denna Sidan då det finns bundna användningsområden till den";
                    }
                    else
                    {
                        Response.Redirect("EditCalendar.aspx?oID=" + Request["oID"].ToString());
                    }
                }
                catch
                {
                    lblError.Text = "Det går inte att ta bort denna lucka.";
                }
            }
        }
    }
}