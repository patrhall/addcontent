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
using Telerik.Charting;
 
namespace AimNews
{
    public partial class Statistics : AIM.Base.Modules.Page.AimNews
    {
        protected override void Page_Load()
        {
            lblError.Visible = false;

            setLanguage();

            if (!IsPostBack)
                init();
        }
        /// <summary>
        /// Set language
        /// </summary>
        private void setLanguage()
        {
            lblMailTitle.Text = Language.getValue("lblMailTitle");
            lblSummaryHeader.Text = Language.getValue("lblSummaryHeader");
            lblNbrRecipients.Text = Language.getValue("lblNbrRecipients");
            lblNbrDelivered.Text = Language.getValue("lblNbrDelivered");
            lblSendTime.Text = Language.getValue("lblSendTime");
            lblNbrOpens.Text = Language.getValue("lblNbrOpens");
            lblLinkSummaryHeader.Text = Language.getValue("lblLinkSummaryHeader");
            lblNbrUniqueClickers.Text = Language.getValue("lblNbrUniqueClickers");
            lblNbrIsError.Text = Language.getValue("lblNbrIsError");
            lblPercentClickers.Text = Language.getValue("lblPercentClickers");
            lblDaySummaryHeader.Text = Language.getValue("lblDaySummaryHeader");
            lblStatisticByDayHeader.Text = Language.getValue("lblStatisticByDayHeader");
            lblWeekBydayDescription.Text = Language.getValue("lblWeekBydayDescription");
            lblStatisticByWeekHeader.Text = Language.getValue("lblStatisticByWeekHeader");

            lblPercentOpened.Text = Language.getValue("lblPercentOpened");

            lbDetailedStat.Text = "<img src='/Images/excel.gif' alt='' /> " + Language.getValue("lbDetailedStat");
        }

        /// <summary>
        /// Init the statistic
        /// </summary>
        private void init()
        {
            List<AIM.Business.AimNews.AimNews_History> sendedMail = (from linq_o in db.AimNews_Histories where linq_o.MailID == int.Parse(Request["ID"]) orderby linq_o.SendTime descending select linq_o).ToList<AIM.Business.AimNews.AimNews_History>();
            
            if (sendedMail.Count != 0)
            {
                lblMailTitle.Text = Language.getValue("lblMailTitle") + " " + sendedMail[0].AimNews_Mail.Header;

                ddMailHistory.DataSource = sendedMail;
                ddMailHistory.DataValueField = "ID";
                ddMailHistory.DataTextField = "SendTime";
                ddMailHistory.DataBind();
                ddMailHistory.SelectedIndex = 0;

                initStat(sendedMail[0].ID);
                
            }
            else
            {
                pnlContent.Visible = false;
                lblError.Text = Language.getValue("lblError");
                lblError.Visible = true;
            }
        }

        /// <summary>
        /// Init statistic for one mail
        /// </summary>
        /// <param name="mailHistoryID"></param>
        private void initStat(int mailHistoryID)
        {
            try
            {

                AIM.Business.AimNews.AimNews_History historyLog = (from linq_o in db.AimNews_Histories where linq_o.ID == mailHistoryID select linq_o).First<AIM.Business.AimNews.AimNews_History>();

                List<AIM.Business.AimNews.AimNews_Log> logList = (from linq_logs in db.AimNews_Logs where linq_logs.HistoryID == mailHistoryID select linq_logs).ToList<AIM.Business.AimNews.AimNews_Log>();
                List<AIM.Business.AimNews.AimNews_Log> userWhichHasOpen = new List<AIM.Business.AimNews.AimNews_Log>();

                int nbrDeliveredMails = logList.Count();
                int errorMessage = 0;
                int NbrUniqueClicks = 0;

                foreach (AIM.Business.AimNews.AimNews_Log log in logList)
                {
                    //Count number 
                    if (log.isError)
                        errorMessage++;

                    List<AIM.Business.AimNews.AimNews_LinkedClicked> linksClicked = log.AimNews_LinkedClickeds.ToList<AIM.Business.AimNews.AimNews_LinkedClicked>();

                    //If a user has clicked any links, increment unique count
                    if (log.AimNews_LinkedClickeds.Count > 0)
                        NbrUniqueClicks++;

                    if (log.openDate.HasValue)
                        userWhichHasOpen.Add(log);
                }

                lblNbrRecipients_value.Text = historyLog.NbrRecipients.ToString();
                lblNbrDelivered_value.Text = nbrDeliveredMails.ToString();
                lblNbrOpens_value.Text = historyLog.NbrOpens.ToString();
                lblSendTime_value.Text = historyLog.SendTime.ToString("yyyy-MM-dd HH:mm");
                lblNbrIsError_value.Text = errorMessage.ToString();
                lblNbrUniqueClickers_value.Text = NbrUniqueClicks.ToString();

                try
                {
                    lblPercentOpened_value.Text = ((Convert.ToInt32(historyLog.NbrOpens.ToString()) * 100) / nbrDeliveredMails).ToString() + "%";

                }
                catch
                {
                    lblPercentOpened_value.Text = "test";
                }


                if (userWhichHasOpen.Count != 0)
                {
                    getOpenerStatisticsByDate(userWhichHasOpen, logList.Count);
                    getOpenerStatisticsByWeek(userWhichHasOpen);

                    repLinks.DataSource = (from o in db.AimNews_Links where o.HistoryID_FK == mailHistoryID orderby o.AimNews_LinkedClickeds.Count descending select o).ToList<AIM.Business.AimNews.AimNews_Link>();
                    repLinks.DataBind();

                    if (repLinks.Items.Count != 0)
                    {
                        repLinks.Visible = true;
                        lblLinkSummaryError.Visible = false;
                    }
                    else
                    {
                        repLinks.Visible = false;
                        lblLinkSummaryError.Visible = true;
                        lblLinkSummaryError.Text = Language.getValue("lblLinkSummaryError");
                    }

                    pnlDetails.Visible = true;
                }
                else
                {
                    pnlDetails.Visible = false;
                    lblError.Text = Language.getValue("lblError_nodetails");
                    lblError.Visible = true;
                }




                if (logList[0].AimNews_History.NbrOpens != 0)
                    lblPercentClickers_value.Text = ((100 * NbrUniqueClicks) / (historyLog.NbrOpens)).ToString() + "%";
                else
                    lblPercentClickers_value.Text = "0%";
            }
            catch { }
        }

        /// <summary>
        /// Creates statistics about opener
        /// </summary>
        /// <param name="logs">Users which has open the newsletter</param>
        private void getOpenerStatisticsByDate(List<AIM.Business.AimNews.AimNews_Log> logs, int nbrTotalSendedMail)
        {
            OpenerStatisticsByDate sorter = new OpenerStatisticsByDate();
            double[] partOfTotalOpenerByDay = new double[21];
            
            //logs have at least one row, thereforepick the first to get history object
            DateTime newsMailSendTime = logs[0].AimNews_History.SendTime;

            TableHeaderRow tblHeaderRow = new TableHeaderRow();

            TableHeaderCell tblHeaderCell = new TableHeaderCell();
            tblHeaderCell.Text = Language.getValue("StatisticByDayHeader_date");
            tblHeaderCell.ColumnSpan = 2;
            tblHeaderRow.Cells.Add(tblHeaderCell); 

            tblHeaderCell = new TableHeaderCell();
            tblHeaderCell.Text = Language.getValue("StatisticByDayHeader_dayfromsend");
            tblHeaderRow.Cells.Add(tblHeaderCell);

            tblHeaderCell = new TableHeaderCell();
            tblHeaderCell.Text = Language.getValue("StatisticByDayHeader_count");
            tblHeaderRow.Cells.Add(tblHeaderCell);

            tblHeaderCell = new TableHeaderCell();
            tblHeaderCell.Text = Language.getValue("StatisticByDayHeader_precent");
            tblHeaderRow.Cells.Add(tblHeaderCell);

            tblStatisticByDay.Rows.Add(tblHeaderRow);

            try
            {
                //Get each day two weeks after the mail was send
                for (int i = 0; i <= 13; i++)
                {
                    sorter.DayToGet = newsMailSendTime.AddDays(i);
                    int nbrOpenerThisDay = logs.Count<AIM.Business.AimNews.AimNews_Log>(sorter.Count);

                    if (i != 0)
                        partOfTotalOpenerByDay[i] = Convert.ToDouble(nbrOpenerThisDay) / Convert.ToDouble(nbrTotalSendedMail) + partOfTotalOpenerByDay[i - 1];
                    else
                        partOfTotalOpenerByDay[i] = Convert.ToDouble(nbrOpenerThisDay) / Convert.ToDouble(nbrTotalSendedMail);

                    //Table data should only be viewed for days after the sending
                    if (newsMailSendTime.Date.AddDays(i) > DateTime.Now.Date)
                        continue;

                    //Create table row with statistic
                    TableRow tr = new TableRow();

                    if (i % 2 != 0)
                        tr.CssClass = "lightgray_noalign";

                    TableCell tc = new TableCell();
                    tc.Text = Language.getValue(newsMailSendTime.AddDays(i).DayOfWeek.ToString()) + " ";
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = newsMailSendTime.AddDays(i).ToString("yyyy-MM-dd");
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = i.ToString();
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(tc);
                    
                    tc = new TableCell();
                    tc.Text = nbrOpenerThisDay.ToString();                    
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = ((nbrOpenerThisDay * 100) / logs.Count).ToString() + "%";
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    tr.Cells.Add(tc);

                    tblStatisticByDay.Rows.Add(tr);
                }

                tblStatisticByDay.Visible = true;
                generateChartDayAfterSending(partOfTotalOpenerByDay);
            }
            catch 
            {
                tblStatisticByDay.Visible = false;
            }
        }

        /// <summary>
        /// Creates statistics about when reader is opening the mail
        /// </summary>
        /// <param name="logs">Users which has open the newsletter</param>
        private void getOpenerStatisticsByWeek(List<AIM.Business.AimNews.AimNews_Log> logs)
        {
            TableHeaderRow tblHeaderRow = new TableHeaderRow();

            TableHeaderCell tblHeaderCell = new TableHeaderCell();
            tblHeaderCell.Text = Language.getValue("StatisticByWeekTblHeader");
            tblHeaderRow.Cells.Add(tblHeaderCell);

            for (int i = 0; i < 24; i++)
            {
                tblHeaderCell = new TableHeaderCell();
                if (i < 10)
                    tblHeaderCell.Text = "0" + i.ToString();
                else
                    tblHeaderCell.Text = i.ToString();

                tblHeaderRow.Cells.Add(tblHeaderCell);
            }

            tblStatisticByWeek.Rows.Add(tblHeaderRow);

            ChartWeekByDay chartWeekByDay = new ChartWeekByDay();
            chartWeekByDay.totalOpener = logs.Count;

            getOpenerStatisticsByWeekGetDay(DayOfWeek.Monday, logs, ref chartWeekByDay);
            getOpenerStatisticsByWeekGetDay(DayOfWeek.Tuesday, logs, ref chartWeekByDay);
            getOpenerStatisticsByWeekGetDay(DayOfWeek.Wednesday, logs, ref chartWeekByDay);
            getOpenerStatisticsByWeekGetDay(DayOfWeek.Thursday, logs, ref chartWeekByDay);
            getOpenerStatisticsByWeekGetDay(DayOfWeek.Friday, logs, ref chartWeekByDay);
            getOpenerStatisticsByWeekGetDay(DayOfWeek.Saturday, logs, ref chartWeekByDay);
            getOpenerStatisticsByWeekGetDay(DayOfWeek.Sunday, logs, ref chartWeekByDay);

            generateChartWeekByDay(chartWeekByDay);
        }

        /// <summary>
        /// Creates statistics about week
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="logs"></param>
        /// <param name="chartWeekByDay"></param>
        private void getOpenerStatisticsByWeekGetDay(DayOfWeek dayOfWeek, List<AIM.Business.AimNews.AimNews_Log> logs, ref ChartWeekByDay chartWeekByDay)
        {
            OpenerStatisticsByWeek sorter = new OpenerStatisticsByWeek();
            sorter.dayToGet = dayOfWeek;

            TableRow tr = new TableRow();

            TableCell tc = new TableCell();
            tc.Text = Language.getValue(dayOfWeek.ToString());
            tr.Cells.Add(tc);                

            for (int i = 0; i <= 23; i++)
            {                                                                
                sorter.hourToGet = i;
                int nbrOpener = logs.Count<AIM.Business.AimNews.AimNews_Log>(sorter.Count);
                chartWeekByDay.setNbrOfUsersByHour(dayOfWeek, i, nbrOpener);

                tc = new TableCell();
                tc.Text = nbrOpener.ToString();

                if (i % 2 == 0)
                    tc.CssClass = "lightgray_noalign";

                tc.HorizontalAlign = HorizontalAlign.Center;

                tr.Cells.Add(tc);                
            }

            tblStatisticByWeek.Rows.Add(tr);
        }

        /// <summary>
        /// Databound links, fill the repeater with data about link clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repLinks_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                AIM.Business.AimNews.AimNews_Link linkData = (AIM.Business.AimNews.AimNews_Link)e.Item.DataItem;

                //Url
                HyperLink _hpRepLinks_links = (HyperLink)e.Item.FindControl("hpRepLinks_links");
                _hpRepLinks_links.NavigateUrl = linkData.Url;

                if (linkData.Url.Length > 55)
                    _hpRepLinks_links.Text = linkData.Url.Remove(50);
                else
                    _hpRepLinks_links.Text = linkData.Url;

                List<AIM.Business.AimNews.AimNews_LinkedClicked> linkClicked = linkData.AimNews_LinkedClickeds.ToList<AIM.Business.AimNews.AimNews_LinkedClicked>();

                //Unique clicks
                Label _lblUniqueClick = (Label)e.Item.FindControl("lblRepLinks_NbrUniqueClick");
                _lblUniqueClick.Text = linkData.AimNews_LinkedClickeds.Count.ToString();

                
                Label _lblRepLinks_PercentTot = (Label)e.Item.FindControl("lblRepLinks_PercentTot");
                _lblRepLinks_PercentTot.Text = Convert.ToString((linkData.AimNews_LinkedClickeds.Count * 100) / int.Parse(lblNbrRecipients_value.Text)) + "%";

                int totClicks = 0;

                foreach (AIM.Business.AimNews.AimNews_LinkedClicked linkClick in linkClicked)
                    totClicks += linkClick.Count;

                //Total clicks on the link
                Label _lblRepLinks_totClicks = (Label)e.Item.FindControl("lblRepLinks_totClicks");
                _lblRepLinks_totClicks.Text = totClicks.ToString();

                Label _lblRepLinks_avgClicks = (Label)e.Item.FindControl("lblRepLinks_avgClicks");
                _lblRepLinks_avgClicks.Text = ((float)totClicks / (float)linkData.AimNews_LinkedClickeds.Count).ToString("F");

                ((ImageButton)e.Item.FindControl("ibExport")).CommandArgument = linkData.ID.ToString();
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                //Set repeater headers
                Label _lblRepLinksHeader_links = (Label)e.Item.FindControl("lblRepLinksHeader_links");
                _lblRepLinksHeader_links.Text = Language.getValue("lblRepLinksHeader_links");

                Label _lblRepLinksHeader_NbrUniqueClick = (Label)e.Item.FindControl("lblRepLinksHeader_NbrUniqueClick");
                _lblRepLinksHeader_NbrUniqueClick.Text = Language.getValue("lblRepLinksHeader_NbrUniqueClick");

                Label _lblRepLinksHeader_totClicks = (Label)e.Item.FindControl("lblRepLinksHeader_totClicks");
                _lblRepLinksHeader_totClicks.Text = Language.getValue("lblRepLinksHeader_totClicks");

                Label _lblRepLinksHeader_avgClicks = (Label)e.Item.FindControl("lblRepLinksHeader_avgClicks");
                _lblRepLinksHeader_avgClicks.Text = Language.getValue("lblRepLinksHeader_avgClicks");

                Label _lblRepLinksHeader_PercentTot = (Label)e.Item.FindControl("lblRepLinksHeader_PercentTot");
                _lblRepLinksHeader_PercentTot.Text = Language.getValue("lblRepLinksHeader_PercentTot");
            }
        }

        /// <summary>
        /// Change history
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddMailHistory_Change(object sender, EventArgs e)
        {
            initStat(int.Parse(ddMailHistory.SelectedValue));
        }

        /// <summary>
        /// Back to mail list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibBack_OnClick(object sender, System.EventArgs e)
        {
            Response.Redirect("MailList.aspx");
        }

        /// <summary>
        /// Generate graphs about opening statistics
        /// </summary>
        /// <param name="partOfTotalOpenerByDay"></param>
        private void generateChartDayAfterSending(double[] partOfTotalOpenerByDay)
        {
            rcDayAfterSending.PlotArea.Appearance.Dimensions.Margins.Right = 20;                        
            rcDayAfterSending.PlotArea.XAxis.AddRange(0, 13.0, 1.0);
            rcDayAfterSending.PlotArea.YAxis.AddRange(0, 1, 0.1);
            rcDayAfterSending.ChartTitle.TextBlock.Text = Language.getValue("rcDayAfterSending_ChartTitle");

            rcDayAfterSending.Series.Clear();

            //ChartSeries s = rcWeekByDay.Series[i];
            ChartSeries s = new ChartSeries();
            s.Name = "";
            s.Type = ChartSeriesType.Line;
            s.Appearance.ShowLabels = false;

            //Iterate thru all hours
            for (int i = 0; i < 14; i++)
            {
                ChartSeriesItem item = new ChartSeriesItem();
                item.XValue = Convert.ToInt32(i);
                item.YValue = partOfTotalOpenerByDay[i];
                s.Items.Add(item);
            }

            rcDayAfterSending.Series.Add(s);
        }

        /// <summary>
        /// Generate graphs about week statistics
        /// </summary>
        /// <param name="chartWeekByDay"></param>
        private void generateChartWeekByDay(ChartWeekByDay chartWeekByDay)
        {
            rcWeekByDay.Legend.Appearance.Position.Auto = false;
            rcWeekByDay.Legend.Appearance.Position.X = 710;
            rcWeekByDay.Legend.Appearance.Position.Y = 70;
            rcWeekByDay.PlotArea.Appearance.Dimensions.Margins.Right = 100;
            rcWeekByDay.PlotArea.XAxis.AddRange(0, 24.0, 1.0);
            rcWeekByDay.PlotArea.YAxis.AddRange(0, 0.6, 0.1);
            rcWeekByDay.ChartTitle.TextBlock.Text = Language.getValue("rcWeekByDay_ChartTitle");

            DateTime aMonday = AimResources.Week.getFirstDayInWeek(DateTime.Now).Date;

            rcWeekByDay.Series.Clear();

            //Iterate thru whole week
            for (int i = 0; i < 7; i++)
            {
                //ChartSeries s = rcWeekByDay.Series[i];
                ChartSeries s = new ChartSeries();
                s.Name = Language.getValue(aMonday.AddDays(i).DayOfWeek.ToString());
                s.Type = ChartSeriesType.Line;
                s.Appearance.ShowLabels = false;

                ChartSeriesItem item = new ChartSeriesItem();

                //Iterate thru all hours
                for (double j = 0.0; j < 24.0; j += 1.0)
                {
                    item = new ChartSeriesItem();
                    item.XValue = Convert.ToInt32(j);                    
                    item.YValue = chartWeekByDay.getPercentUsersByHour(aMonday.AddDays(i).DayOfWeek, Convert.ToInt32(j));
                    s.Items.Add(item);
                }

                //hour 24 is the same as hour 00, set the value to the same
                item = new ChartSeriesItem();
                item.XValue = 24;
                item.YValue = chartWeekByDay.getPercentUsersByHour(aMonday.AddDays(i).DayOfWeek, 0);
                s.Items.Add(item);

                rcWeekByDay.Series.Add(s);
            }            
        }

        protected void lbDetailedStat_Click(object sender, EventArgs e)
        {
            int HistoryID = int.Parse(Request["ID"].ToString());
            try
            {
                HistoryID = int.Parse(ddMailHistory.SelectedValue);
            }
            catch
            {

            }
            rgDetailOpenStatistics.DataSource = (from o in db.AimNews_Logs
                                                 where o.HistoryID == int.Parse(ddMailHistory.SelectedValue) && !o.isError
                                                 orderby !o.isOpened, o.MailAdress
                                                 select new
                                                 {
                                                     o.MailAdress,                                                                                                          
                                                     OpenDate = (o.openDate.HasValue ? o.openDate.Value.ToString() : "-"),                                                     
                                                 });
            rgDetailOpenStatistics.DataBind();

            exportToExcel();
        }

        protected void ibExport_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "export")
            {
                int linkid = Convert.ToInt32(e.CommandArgument);

                rgDetailOpenStatistics.DataSource = (from o in db.AimNews_LinkedClickeds
                                                     where o.LinkID_FK == linkid
                                                     select new
                                                     {
                                                         o.AimNews_Link.Url,
                                                         o.AimNews_Log.MailAdress                                                         
                                                     });
                rgDetailOpenStatistics.DataBind();
                exportToExcel();
            }
        }

        private void exportToExcel()
        {
            rgDetailOpenStatistics.ExportSettings.IgnorePaging = true;
            rgDetailOpenStatistics.ExportSettings.OpenInNewWindow = true;
            rgDetailOpenStatistics.ExportSettings.FileName = "Detailed Statistics";
            rgDetailOpenStatistics.ExportSettings.ExportOnlyData = true;            
            rgDetailOpenStatistics.MasterTableView.ExportToExcel();
        }

        /// <summary>
        /// Class to handle statistics List<AIM.Business.AimNews.AimNews_Log>.Find
        /// </summary>
        private class OpenerStatisticsByDate
        {
            private DateTime _startTime;
            private DateTime _endTime;

            /// <summary>
            /// Set day to check
            /// </summary>
            public DateTime DayToGet
            {
                set
                {
                    _startTime = new DateTime(value.Year, value.Month, value.Day);
                    _endTime = _startTime.AddDays(1);
                }
            }

            /// <summary>
            /// Returns true if the day is the same as <paramref name="DayToGet"/> 
            /// </summary>
            /// <param name="log"></param>
            /// <returns></returns>
            public bool Count(AIM.Business.AimNews.AimNews_Log log)
            {
                if (!log.openDate.HasValue)
                    return false;

                return (log.openDate.Value >= _startTime && log.openDate.Value < _endTime);
            }
        }

        /// <summary>
        /// Class to handle statistics List<AIM.Business.AimNews.AimNews_Log>.Find
        /// </summary>
        private class OpenerStatisticsByWeek
        {
            private DayOfWeek _dayToGet;
            private int _hourToGet;            

            /// <summary>
            /// Set day to check
            /// </summary>
            public DayOfWeek dayToGet
            {
                set
                {
                    _dayToGet = value;
                }
            }

            /// <summary>
            /// Set hour to check
            /// </summary>
            public int hourToGet
            {
                set
                {
                    _hourToGet = value;
                }
            }

            /// <summary>
            /// Returns true if <paramref name="dayToGet"/> and <paramref name="hourToGet"/> is forfilled
            /// </summary>
            /// <param name="log"></param>
            /// <returns></returns>
            public bool Count(AIM.Business.AimNews.AimNews_Log log)
            {
                if (!log.openDate.HasValue)
                    return false;

                return (log.openDate.Value.DayOfWeek == _dayToGet && log.openDate.Value.Hour == _hourToGet);
            }
        }

        /// <summary>
        /// Class to store statistics about when users is active (hours and weekdays)
        /// </summary>
        private class ChartWeekByDay
        {
            private Dictionary<DayOfWeek, int[]> _nbrOfUsersByDay = new Dictionary<DayOfWeek, int[]>();
            public int totalOpener;

            /// <summary>
            /// Returns number of users
            /// </summary>
            /// <param name="dayOfWeek"></param>
            /// <param name="hour"></param>
            /// <returns></returns>
            public int getNbrOfUsersByHour(DayOfWeek dayOfWeek, int hour)
            {
                if (_nbrOfUsersByDay.ContainsKey(dayOfWeek))                    
                        return _nbrOfUsersByDay[dayOfWeek][hour];

                return 0;
            }
            
            /// <summary>
            /// Returns part of total users 
            /// </summary>
            /// <param name="dayOfWeek"></param>
            /// <param name="hour"></param>
            /// <returns>between 0 and 1</returns>
            public double getPercentUsersByHour(DayOfWeek dayOfWeek, int hour)
            {
                if (_nbrOfUsersByDay.ContainsKey(dayOfWeek))
                    return (Convert.ToDouble(_nbrOfUsersByDay[dayOfWeek][hour]) / Convert.ToDouble(totalOpener));

                return 0.0;
            }

            /// <summary>
            /// Returns number of user
            /// </summary>
            /// <param name="dayOfWeek"></param>
            /// <param name="hour"></param>
            /// <param name="NbrOfUsers"></param>
            public void setNbrOfUsersByHour(DayOfWeek dayOfWeek, int hour, int NbrOfUsers)
            {
                if (!_nbrOfUsersByDay.ContainsKey(dayOfWeek))
                    _nbrOfUsersByDay[dayOfWeek] = new int[24];

                _nbrOfUsersByDay[dayOfWeek][hour] = NbrOfUsers;
            }
        }
    }
}
