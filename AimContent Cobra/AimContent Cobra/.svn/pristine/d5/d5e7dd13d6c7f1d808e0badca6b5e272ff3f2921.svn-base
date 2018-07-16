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

namespace AimForm
{

    public partial class FormAnswers : AIM.Base.Modules.Page.AimForm
    {

        public Dictionary<int, string> questionText = new Dictionary<int, string>();

        protected override void Page_Load()
        {

        }

        public void Page_PreRender(object o, System.EventArgs e)
        {
            //fillGrid();            
        }

        protected void rgformanswers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

                rgformanswers.DataSource = changeHeader(getAnswerDataset(Convert.ToInt32(Request["FormID"].ToString())));

        }

        protected void rgformanswers_PreRender(object sender, EventArgs e)
        {
            //for (int i = 0; i < rgformanswers.MasterTableView.Columns.Count; i++)
            //{
            //    rgformanswers.MasterTableView.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;//getTheQuestionText2(rgformanswers.MasterTableView.Columns[i].HeaderText);
            //    rgformanswers.MasterTableView.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //}  

            //rgformanswers.MasterTableView.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            //rgformanswers.MasterTableView.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        }

        protected void rgformanswers_ItemDataBound(object sender, GridItemEventArgs e)
        {   
        }
        protected void rgformanswers_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridItem)
            {
                GridItem item = (GridItem)e.Item;
                item.Style["font-size"] = "11px";
                //item.Style["color"] = "red";
                //item.Style["background-color"] = "yellow";
                //item.Style["height"] = "50px";
                item.Style["vertical-align"] = "top";
                foreach (TableCell cell in item.Cells)
                {
                    cell.Style["text-align"] = "left";
                    cell.Style["font-weight"] = "normal";
                    //cell.Style["border-color"] = "red";
                }
            }

            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                headerItem.Style["font-size"] = "12px";
                //headerItem.Style["color"] = "red";
                //headerItem.Style["background-color"] = "yellow";
                //headerItem.Style["height"] = "50px";
                headerItem.Style["vertical-align"] = "bottom";
                foreach (TableCell cell in headerItem.Cells)
                {
                    cell.Style["text-align"] = "left";
                    cell.Style["font-weight"] = "bold";
                    //cell.Style["border-color"] = "red";
                }
            }
            
        }

        protected DataSet getAnswerDataset(int formID)
        {
            List<AIM.Business.AimForm.Form_Question> Questions = (from o in db.Form_Questions
                                                                where o.FormID == formID && o.Deleted != true
                                                                orderby o.Sort ascending
                                                                select o).ToList<AIM.Business.AimForm.Form_Question>();

            DataSet dsAnswers = new DataSet();
            DataTable AnswersTable = dsAnswers.Tables.Add();
                       
            
            AnswersTable.Columns.Add("ResponderID", typeof(string));
                        
            foreach (AIM.Business.AimForm.Form_Question question in Questions)
            {
                AnswersTable.Columns.Add(question.QuestionID.ToString(), typeof(string));

                if(questionText.ContainsValue(question.QuestionText))
                {
                    questionText.Add(question.QuestionID, "(" + question.QuestionText + ")");
                }
                else if (questionText.ContainsValue("(" + question.QuestionText + ")"))
                {
                    questionText.Add(question.QuestionID, "((" + question.QuestionText + "))");
                }else
                {
                    questionText.Add(question.QuestionID, question.QuestionText);
                }
            }

            //for (int i = 0; i < questionText.Count; i++)
            //{
            //    Response.Write(questionText.ElementAt(i).Key + " - " + questionText.ElementAt(i).Value + "<br>");
            //}

            AnswersTable.Columns.Add("Datum", typeof(string));

            string answer = "";            
            string responderID = "";
            int questionID = 0;
            string datum = "";

            List<AIM.Business.AimForm.Form_Answer> Answers = (from o in db.Form_Answers
                                                              join q in db.Form_Questions on o.QuestionID equals q.QuestionID
                                                              where o.FormID == formID &&
                                                              o.Deleted != true &&
                                                              q.Deleted != true
                                                              orderby o.Date descending
                                                              select o).ToList<AIM.Business.AimForm.Form_Answer>();

            var theForm = (from f in db.Form_Forms
                           where f.FormID == formID
                           select f).Single<AIM.Business.AimForm.Form_Form>();
            
            if (Answers.Count() == 0)
            {   
                lblNoAnswers.Text = "Det finns inga svar på detta formulär.";
                lblNoAnswers.Visible = true;
            }
            else
            {
                DataRow drRow = AnswersTable.NewRow();

                foreach (AIM.Business.AimForm.Form_Answer answ in Answers)
                {
                    
                    
                    if (responderID == "")
                    {
                        responderID = answ.ResponderID;
                    }

                    if (answ.ResponderID == responderID)
                    {
                        datum = answ.Form_Responder.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
                        if (questionID == 0)
                        {
                            questionID = answ.QuestionID;
                        }

                        if (answ.QuestionID == questionID)
                        {
                            if (answer == "")
                            {
                                questionID = answ.QuestionID;
                                answer = answ.AlternativeText;
                            }
                            else
                            {
                                answer += "," + answ.AlternativeText;
                            }
                        }
                        else
                        {
                            if (theForm.ExternalConnectionEnabled)
                                drRow["ResponderID"] = "<a href=\"ExternalConnectionValues.aspx?fID=" + formID + "&rID=" + responderID + "\">" + responderID + "</a>";
                            else
                                drRow["ResponderID"] = responderID;
                            drRow[questionID.ToString()] = answer;
                            responderID = answ.ResponderID;
                            answer = answ.AlternativeText;
                            questionID = answ.QuestionID;
                        }
                    }
                    else
                    {
                        if (theForm.ExternalConnectionEnabled)
                            drRow["ResponderID"] = "<a href=\"ExternalConnectionValues.aspx?fID=" + formID + "&rID=" + responderID + "\">" + responderID + "</a>";
                        else
                            drRow["ResponderID"] = responderID;
                        drRow[questionID.ToString()] = answer;
                        
                        drRow["Datum"] = datum;
                        AnswersTable.Rows.Add(drRow);

                        drRow = AnswersTable.NewRow();

                        responderID = answ.ResponderID;
                        answer = answ.AlternativeText;
                        questionID = answ.QuestionID;
                    }
                }

                //Sparar sista
                if (theForm.ExternalConnectionEnabled)
                    drRow["ResponderID"] = "<a href=\"ExternalConnectionValues.aspx?fID=" + formID + "&rID=" + responderID + "\">" + responderID + "</a>";
                else
                    drRow["ResponderID"] = responderID;
                drRow[questionID.ToString()] = answer;
                drRow["Datum"] = datum;
                AnswersTable.Rows.Add(drRow);
                                
                lblNoAnswers.Visible = false;
            }
            return dsAnswers;
        } 

        private string getTheQuestionText2(string p)
        {
            try
            {
                if (p != "" && p != "ResponderID")
                {
                    string questionID = p;

                    if (questionID != "")
                    {
                        if (questionText.ContainsKey(Convert.ToInt32(questionID))) // True
                        {                            
                            return questionText[Convert.ToInt32(questionID)];
                        }
                        else
                        {
                            return "*";
                        }
                    }
                    else
                        return "";
                }
                else
                {                    
                    return p + "2";// "Respondent";
                }
            }
            catch
            {
                return "";
            }
        }

        protected DataSet changeHeader(DataSet ds)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                try
                {
                    if (ds.Tables[0].Columns[i].ColumnName != "ResponderID")
                        ds.Tables[0].Columns[i].ColumnName = getTheQuestionText2(ds.Tables[0].Columns[i].ColumnName);
                    else
                        ds.Tables[0].Columns[i].ColumnName = "ID";
                }
                catch
                {                    
                }
            }
            return ds;
        }

        protected void ibExcel_OnClick(object sender, System.EventArgs e)
        {
            ConfigureExport();
            rgformanswers.MasterTableView.ExportToExcel();           
        }
        public void ConfigureExport()
        {
            rgformanswers.ExportSettings.ExportOnlyData = true;
            rgformanswers.ExportSettings.IgnorePaging = true;
            rgformanswers.ExportSettings.OpenInNewWindow = true;
            rgformanswers.ExportSettings.FileName = "Formularsvar" + DateTime.Now.ToString();
        }
        protected void fillGrid()
        {
            dgFormAnswers.DataSource = getAnswerDataset(Convert.ToInt32(Request["FormID"].ToString()));
            dgFormAnswers.DataBind();

            dgFormAnswers.ItemDataBound += new DataGridItemEventHandler(dgFormAnswers_ItemDataBound);
        }

        protected void dgFormAnswers_ItemDataBound(Object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                DataGridItem column = (DataGridItem)e.Item.DataItem;

                for (int i = 0; i < e.Item.Cells.Count; i++)
                {
                    e.Item.Cells[i].Text = getTheQuestionText(i);
                    e.Item.Cells[i].Style.Add("Font-Bold", "true");
                }
            }
        }
        private string getTheQuestionText(int p)
        {
            try
            {
                if (p != 0)
                {
                    int questionID = Convert.ToInt32(p);

                    if (questionID != 0)
                    {
                        //var question =
                        //  from q in questionText
                        //  where q.Key == questionID
                        //  select q.Value;

                        return questionText.Values.ElementAt(questionID - 1).ToString();
                    }
                    else
                        return "";
                }
                else
                {
                    return "ID";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
