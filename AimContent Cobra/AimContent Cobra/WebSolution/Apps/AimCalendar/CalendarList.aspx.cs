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

using Telerik.Web.UI;

namespace AimCalendar
{
    public partial class CalendarList : AIM.Base.Modules.Page.AimCalendar
    {//using System.Data.Linq;
        protected override void Page_Load()
        {
            if (!IsPostBack)
            {
                init();                

                if (Request["calgroupID"] != null && Request["troopid"] != null && Request["date"] == null)
                {
                    ddCalendarGroups.SelectedIndex = ddCalendarGroups.Items.IndexOf(ddCalendarGroups.Items.FindByValue(Request["calgroupID"].ToString()));
                    ddTroop.SelectedIndex = ddTroop.Items.IndexOf(ddTroop.Items.FindByValue(Request["troopid"].ToString()));
                    ShowDates(Convert.ToInt32(Request["troopid"].ToString()));
                    loadCalendarList(Convert.ToInt32(Request["calgroupID"].ToString()), Convert.ToInt32(Request["troopid"]), DateTime.Now);
                    
                }else if (Request["calgroupID"] != null && Request["troopid"] != null && Request["date"] != null)
                {
                    ddCalendarGroups.SelectedIndex = ddCalendarGroups.Items.IndexOf(ddCalendarGroups.Items.FindByValue(Request["calgroupID"].ToString()));
                    ddTroop.SelectedIndex = ddTroop.Items.IndexOf(ddTroop.Items.FindByValue(Request["troopid"].ToString()));
                    ShowDates(Convert.ToInt32(Request["troopid"].ToString()));
                    
                    loadCalendarList(Convert.ToInt32(Request["calgroupID"].ToString()), Convert.ToInt32(Request["troopid"]), Convert.ToDateTime(Request["date"].ToString()));                    
                }
                else
                {
                    rgCalendar.Visible = false;
                    lblEmtyRepHorizontal.Visible = false;
                    lblrepHorizontalHeader.Visible = false;
                    hlNewAd.Visible = false;
                }

                if (Request["date"] != null)
                {
                    rgCalendar.FocusedDate = Convert.ToDateTime(Request["date"].ToString());
                }
                else
                {
                    rgCalendar.FocusedDate = DateTime.Now;
                }
            }            
        }

        private void init()
        {
            ddCalendarGroups.DataSource = loadCalendarGroup(config.siteID);                        
            ddCalendarGroups.DataBind();
            //ddCalendarGroups.Items.Insert(0, "Välj");

            ddTroop.DataSource = loadTroops();
            ddTroop.DataBind();
            ddTroop.Items.Insert(0, "Välj kategori");
        }
        private List<AIM.Business.AimCalendar.CalendarGroup> loadCalendarGroup(int siteID)
        {
            List<AIM.Business.AimCalendar.CalendarGroup> calendarList =
                (from o in db.CalendarGroups
                 where o.SiteID_FK == siteID
                 orderby o.CalendarGroupID ascending
                 select o).ToList<AIM.Business.AimCalendar.CalendarGroup>();

            if (calendarList.Count == 1)
            {
                ddCalendarGroups.Visible = false;
                lblCalendarName.Visible = true;
                lblCalendarName.Text = calendarList.First().Name;
            }
            else
            {
                ddCalendarGroups.Visible = true;
                lblCalendarName.Visible = false;
            }

            return calendarList;
        }

        private List<AIM.Business.AimCalendar.Ois_Troop> loadTroops()
        {
            List<AIM.Business.AimCalendar.Ois_Troop> troopList =
                (from o in db.Ois_Troops
                 orderby o.TroopID ascending
                 select o).ToList<AIM.Business.AimCalendar.Ois_Troop>();

            return troopList;            
        }
        //Fassas ut
        protected void fillCalendarItemList(int calendarID, int troopID)
        {
            if (calendarID != 0 && troopID != 0)
            {
                hlNewAd.NavigateUrl = "CalendarEditItem.aspx?calgroupID=" + ddCalendarGroups.SelectedValue + "&calID=0&troopid=" + ddTroop.SelectedValue;
                hlNewAd.Visible = true;
                rgCalendar.Visible = true;
                lblEmtyRepHorizontal.Visible = true;
                lblrepHorizontalHeader.Visible = true;
                loadCalendarList(calendarID, troopID);
            }
            else
            {
                rgCalendar.Visible = false;
                lblEmtyRepHorizontal.Visible = false;
                lblrepHorizontalHeader.Visible = false;
                hlNewAd.Visible = false;
            }
        }

        protected void ddCalendarGroups_Changed(object sender, EventArgs e)
        {
            if (ddCalendarGroups.Items.Count != 0 && ddTroop.SelectedIndex != 0)
            {
                //fillCalendarItemList(Convert.ToInt32(ddCalendarGroups.SelectedValue), Convert.ToInt32(ddTroop.SelectedValue));
                ShowDates(Convert.ToInt32(ddTroop.SelectedValue));

                hlNewAd.NavigateUrl = "CalendarEditItem.aspx?calgroupID=" + ddCalendarGroups.SelectedValue + "&calID=0&troopid=" + ddTroop.SelectedValue;
                hlNewAd.Visible = true;
                rgCalendar.Visible = true;
                lblEmtyRepHorizontal.Visible = true;
                lblrepHorizontalHeader.Visible = true;
                fillCalendarItemList(0, 0);
            }
            else
            {
                hlNewAd.Visible = false;
                rgCalendar.Visible = false;
                lblEmtyRepHorizontal.Visible = false;
                lblrepHorizontalHeader.Visible = false;
                lblEmtyRepHorizontal.Visible = false;
                lblrepHorizontalHeader.Visible = false;
                fillCalendarItemList(0, 0);
            }
        }


        protected void ddTroop_Changed(object sender, EventArgs e)
        {
            if (ddCalendarGroups.Items.Count != 0 && ddTroop.SelectedIndex != 0)
            {
                //fillCalendarItemList(Convert.ToInt32(ddCalendarGroups.SelectedValue), Convert.ToInt32(ddTroop.SelectedValue));
                ShowDates(Convert.ToInt32(ddTroop.SelectedValue));
                loadCalendarList(0, 0, DateTime.Now);
                hlNewAd.NavigateUrl = "CalendarEditItem.aspx?calgroupID=" + ddCalendarGroups.SelectedValue + "&calID=0&troopid=" + ddTroop.SelectedValue;
                hlNewAd.Visible = true;
                rgCalendar.Visible = true;
                lblEmtyRepHorizontal.Visible = true;
                lblrepHorizontalHeader.Visible = true;
                //fillCalendarItemList(0, 0);
            }
            else
            {                
                hlNewAd.Visible = false;
                rgCalendar.Visible = false;
                lblEmtyRepHorizontal.Visible = false;
                lblrepHorizontalHeader.Visible = false;
                loadCalendarList(0, 0, DateTime.Now);
                //fillCalendarItemList(0, 0);
            }
        }

        private void loadCalendarList(int calendargroupid, int troopID)
        {
            var Items = from o in db.CalendarTroopConnections
                        where o.TroopID_FK == troopID && o.Calendar.CalendarGroupID_FK == calendargroupid 
                        orderby o.Calendar.Date ascending
                        select new { o.Calendar.Titel, o.Calendar.Description, o.Calendar.Date, o.Calendar.CalendarID, o.Calendar.CalendarGroupID_FK, o.TroopID_FK };
            
            repHorizontal.DataSource = Items;

            repHorizontal.DataBind();
        }

        private void loadCalendarList(int calendargroupid, int troopID, DateTime date)
        {
            //repHorizontal.DataSource = null;
            //repHorizontal.DataBind();

            if (calendargroupid != 0 && troopID != 0)
            {
                hlNewAd.NavigateUrl = "CalendarEditItem.aspx?calgroupID=" + ddCalendarGroups.SelectedValue + "&calID=0&troopid=" + ddTroop.SelectedValue;
                hlNewAd.Visible = true;
            }

            var Items = from o in db.CalendarTroopConnections
                        where o.TroopID_FK == troopID && o.Calendar.CalendarGroupID_FK == calendargroupid && o.Calendar.Date.Value.Date == date.Date
                        orderby o.Calendar.Date ascending
                        select new { o.Calendar.Titel, o.Calendar.Description, o.Calendar.Date, o.Calendar.CalendarID, o.Calendar.CalendarGroupID_FK, o.TroopID_FK };


            if (Items.Count() == 0)
            {
                lblEmtyRepHorizontal.Visible = true;
                //lblEmtyRepHorizontal.Style.Remove("display");
                //lblEmtyRepHorizontal.Text = "tom";
            }
            else
            {
                lblEmtyRepHorizontal.Visible = false;
                //lblEmtyRepHorizontal.Style.Add("display", "none");
                //lblEmtyRepHorizontal.Text = "full";
            }

            try
            {
                repHorizontal.DataSource = Items;
                repHorizontal.DataBind();                
            }
            catch
            {
                
            }
        }

        protected void repHorizontal_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "delete")
                {
                    DeleteEventDate(Convert.ToInt32(e.CommandArgument));
                }
            }
            else
            {
                Response.Write("not working" + e.CommandName + "*");
            }
        }

        protected void DeleteEventDate(int ID)
        {
            AIM.Business.AimCalendar.CalendarTroopConnection theTroopCon = (from linq_trpcon in db.CalendarTroopConnections where linq_trpcon.CalendarID_FK == ID select linq_trpcon).First<AIM.Business.AimCalendar.CalendarTroopConnection>();
            AIM.Business.AimCalendar.Calendar theItem = (from linq_itm in db.Calendars where linq_itm.CalendarID == ID select linq_itm).First<AIM.Business.AimCalendar.Calendar>();

            db.CalendarTroopConnections.DeleteOnSubmit(theTroopCon);
            db.Calendars.DeleteOnSubmit(theItem);


            db.SubmitChanges();

            ShowDates(Convert.ToInt32(ddTroop.SelectedValue));
            loadCalendarList(Convert.ToInt32(ddCalendarGroups.SelectedValue), Convert.ToInt32(ddTroop.SelectedValue), rgCalendar.SelectedDate.Date);
            //Response.Redirect(Request.Url.ToString());
            //Ladda om
            
        }

        protected void rgCalendar_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        {
            if (ddCalendarGroups.Items.Count != 0 && ddTroop.SelectedIndex != 0)
            {
                loadCalendarList(Convert.ToInt32(ddCalendarGroups.SelectedValue), Convert.ToInt32(ddTroop.SelectedValue), rgCalendar.SelectedDate.Date);
        
                //fillCalendarItemList(Convert.ToInt32(ddCalendarGroups.SelectedValue), Convert.ToInt32(ddTroop.SelectedValue));
            }
            //Response.Redirect("CalendarEditItem.aspx?calgroupID=" + ddCalendarGroups.SelectedValue.ToString() + "&calID=0&troopid=" + ddTroop.SelectedValue.ToString() + "&date=" + rgCalendar.SelectedDate.Date.ToString());
            //fillCalendarEvent(Convert.ToInt32(hf_current_Calendar.Value), rgCalendar.SelectedDate);
        }

        private void ShowDates(int troop)
        {
            //Clears out all the old specialdays
            rgCalendar.SpecialDays.Clear();

            var Found = from o in db.CalendarTroopConnections
                        where o.TroopID_FK == troop && o.Calendar.CalendarGroupID_FK == Convert.ToInt32(ddCalendarGroups.SelectedValue) && o.Calendar.Show == true
                        orderby o.Calendar.Date ascending
                        select new { o.TroopID_FK, o.Calendar.Titel, o.Calendar.Description, o.Calendar.Date, o.Calendar.CalendarID, o.Calendar.CalendarGroupID_FK, o.Calendar.isMarked, o.Calendar.Color };

            try
            {
                DataSet ds = new DataSet();

                DataTable dsTable = ds.Tables.Add();

                dsTable.Columns.Add("Title", typeof(string));
                dsTable.Columns.Add("Description", typeof(string));
                dsTable.Columns.Add("CalendarGroupID_FK", typeof(int));
                dsTable.Columns.Add("CalendarID", typeof(int));
                dsTable.Columns.Add("Date", typeof(DateTime));
                dsTable.Columns.Add("isMarked", typeof(bool));
                dsTable.Columns.Add("Color", typeof(string));

                string currentDate = "";
                string nextToLastDate = "";
                string title = "";
                bool isMarked = false;
                string color = "";

                int outerloop = 0;
                int innerloop = 0;

                foreach (var v in Found)
                {
                    foreach (var inv in Found)
                    {
                        if (innerloop >= outerloop)
                        {
                            if (inv.Date.ToString() == v.Date.ToString())
                            {
                                title += inv.Titel + ": " + inv.Description + ",  " + Environment.NewLine;
                                currentDate = inv.Date.ToString();
                                nextToLastDate = inv.Date.ToString();

                                if (isMarked == false)
                                {
                                    isMarked = inv.isMarked;
                                }

                                if (color == "")
                                    color = inv.Color;

                            }
                        }

                        innerloop += 1;
                    }

                    DataRow row = dsTable.NewRow();

                    row["Title"] = title;
                    row["Date"] = Convert.ToDateTime(currentDate);
                    row["isMarked"] = isMarked;
                    row["Color"] = color;
                    dsTable.Rows.Add(row);

                    title = "";
                    currentDate = "";
                    isMarked = false;
                    color = "";

                    outerloop += 1;
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DateTime eventdate = Convert.ToDateTime(dr["Date"].ToString());

                    Telerik.Web.UI.RadCalendarDay rcd = new Telerik.Web.UI.RadCalendarDay();

                    rcd.Date = Convert.ToDateTime(eventdate);
                    rcd.IsSelectable = true;
                    //rcd.ItemStyle.BackColor = System.Drawing.Color.Red;
                    //rcd.ItemStyle.BorderColor = System.Drawing.Color.Red;                                
                    rcd.ToolTip = dr["Title"].ToString();

                    if (Convert.ToBoolean(dr["isMarked"].ToString()))
                    {

                        rcd.ItemStyle.Font.Underline = true;
                        rcd.ItemStyle.CssClass = "markedDate";

                        //rcd.ItemStyle.ForeColor = System.Drawing.Color.Red;
                        rcd.ItemStyle.BackColor = System.Drawing.Color.Red;//System.Drawing.ColorTranslator.FromHtml(dr["Color"].ToString());
                    }
                    else
                    {
                        rcd.ItemStyle.BackColor = System.Drawing.Color.LightGray;
                    }

                    rgCalendar.SpecialDays.Add(rcd);
                }
            }
            catch
            {
                foreach (var v in Found)
                {
                    DateTime eventdate = Convert.ToDateTime(v.Date);

                    Telerik.Web.UI.RadCalendarDay rcd = new Telerik.Web.UI.RadCalendarDay();

                    rcd.Date = Convert.ToDateTime(eventdate);
                    rcd.IsSelectable = true;
                    //rcd.ItemStyle.BackColor = System.Drawing.Color.Red;
                    //rcd.ItemStyle.BorderColor = System.Drawing.Color.Red;                                
                    rcd.ToolTip = v.Titel;


                    if (v.isMarked == true)
                    {
                        //rcd.ItemStyle.BackColor = System.Drawing.Color.Red;//System.Drawing.ColorTranslator.FromHtml(dr["Color"].ToString());

                        rcd.ItemStyle.ForeColor = System.Drawing.Color.Red;

                        rcd.ItemStyle.Font.Underline = true;
                        rcd.ItemStyle.CssClass = "markedDate";
                    }


                    rgCalendar.SpecialDays.Add(rcd);
                }
            }
        }


    }
}
