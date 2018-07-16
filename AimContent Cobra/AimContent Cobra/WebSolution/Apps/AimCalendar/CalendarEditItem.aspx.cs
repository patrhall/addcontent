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

using System.Collections.Generic;
using System.Data.Linq;

namespace AimCalendar
{
    public partial class CalendarEditItem : AIM.Base.Modules.Page.AimCalendar
    {
        private int itemID;
        private int calendargroupID;

        protected override void Page_Load()
        {
            itemID = Convert.ToInt32(Request["calID"]);
            calendargroupID = Convert.ToInt32(Request["calgroupID"]);

            if (!IsPostBack)
            {
                init();
                if(Request["date"] == null)
                    hlReturn.NavigateUrl = "CalendarList.aspx?calgroupID=" + Request["calgroupID"].ToString() + "&calID=" + Request["calID"].ToString() + "&troopid=" + Request["troopid"].ToString();                
                else
                    hlReturn.NavigateUrl = "CalendarList.aspx?calgroupID=" + Request["calgroupID"].ToString() + "&calID=" + Request["calID"].ToString() + "&troopid=" + Request["troopid"].ToString() + "&date=" + Request["date"].ToString().Substring(0, 10);                
            }
        }

        private void init()
        {
            if (itemID != 0)
            {
                fillItemInformation();
            }
            else
            {

            }
        }

        protected void fillItemInformation()
        {
            AIM.Business.AimCalendar.Calendar theItem = (from linq_cal in db.Calendars where linq_cal.CalendarID == itemID select linq_cal).First<AIM.Business.AimCalendar.Calendar>();

            tbTitle.Text = theItem.Titel;
            tbDescription.Text = theItem.Description;
            rdpDate.SelectedDate = theItem.Date;
                        
            cbShow.Checked = theItem.Show;
            cbisMarkt.Checked = theItem.isMarked;            
        }

        protected void btnSave_OnClick(object sender, System.EventArgs e)
        {
            if (rdpDate.SelectedDate.HasValue)
            {
                AIM.Business.AimCalendar.Calendar theItems;

                if (itemID == 0)
                {
                    theItems = new AIM.Business.AimCalendar.Calendar();
                    db.Calendars.InsertOnSubmit(theItems);
                    theItems.CalendarGroupID_FK = calendargroupID;
                }
                else
                    theItems = (from linq_cal in db.Calendars where linq_cal.CalendarID == itemID select linq_cal).First<AIM.Business.AimCalendar.Calendar>();

                theItems.Titel = tbTitle.Text;
                theItems.Description = tbDescription.Text;
                theItems.Date = rdpDate.SelectedDate;

                if (cbShow.Checked == true)
                {
                    theItems.Show = true;
                }
                else
                {
                    theItems.Show = false;
                }

                if (cbisMarkt.Checked == true)
                {
                    theItems.isMarked = true;
                }
                else
                {
                    theItems.isMarked = false;
                }

                db.SubmitChanges();

                if (itemID == 0)
                {
                    AIM.Business.AimCalendar.CalendarTroopConnection theTroop = new AIM.Business.AimCalendar.CalendarTroopConnection();
                    theTroop.CalendarID_FK = theItems.CalendarID;
                    theTroop.TroopID_FK = Convert.ToInt32(Request["troopid"]);

                    db.CalendarTroopConnections.InsertOnSubmit(theTroop);
                    db.SubmitChanges();
                }


                if (Request["date"] == null)
                    hlReturn.NavigateUrl = "CalendarList.aspx?calgroupID=" + Request["calgroupID"].ToString() + "&calID=" + Request["calID"].ToString() + "&troopid=" + Request["troopid"].ToString() + "&date=" + rdpDate.SelectedDate.ToString().Substring(0, 10);
                else
                    hlReturn.NavigateUrl = "CalendarList.aspx?calgroupID=" + Request["calgroupID"].ToString() + "&calID=" + Request["calID"].ToString() + "&troopid=" + Request["troopid"].ToString() + "&date=" + Request["date"].ToString().Substring(0, 10);

                Response.Redirect(hlReturn.NavigateUrl.ToString());
            }
            else
            {
                lblError.Text = "* Välj ett datum";
                lblError.Visible = true;
            }
        }


    }
}
