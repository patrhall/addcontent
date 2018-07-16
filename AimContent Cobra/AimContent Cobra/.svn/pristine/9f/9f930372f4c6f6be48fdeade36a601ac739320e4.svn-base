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
using AIM.General.UserControls;

namespace AimForm
{
    public partial class FormList : AIM.Base.Modules.Page.AimForm
    {
        public string cssfile_config;
        public string customerpath;
        public int FormID = 0;
        //List<AIM.Business.AimForm.Form_Form> dsFormList = new List<AIM.Business.AimForm.Form_Form>();
        ////DataSet dsForm;

        protected override void Page_Load()
        {           
            //hämta configvariabler                        
            config.GetConfig();                  
        }                

        //private void GridPageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        //{
        //    dgForm.CurrentPageIndex = e.NewPageIndex;
        //    dgForm.DataSource = dsFormList;
        //    dgForm.DataBind();
        //}

        protected void btnNew_Click(object sender, System.EventArgs e)
        {

            Response.Redirect("Form.aspx?ID=0");

        }

        protected void rgForm_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<AIM.Business.AimForm.Form_Form> List =
                (from o in db.Form_Forms
                 where o.SiteID == config.siteID && o.Deleted != true
                 orderby o.FormName ascending
                 select o).ToList<AIM.Business.AimForm.Form_Form>();

            rgForm.DataSource = List;
        }               

        protected void rgForm_ItemCommand(object sender, GridCommandEventArgs e)
        {           
            if (e.CommandName == "Answers")
            {
                //HyperLink linkAnswers = (HyperLink)e.Item.FindControl("hl_ShowAnswers");

                //DataRowView row = e.Item.DataItem as DataRowView;

                //linkAnswers.NavigateUrl = "/Apps/AimForm/FormAnswers.aspx?formID=" + e.CommandArgument.ToString();
                Response.Redirect("/Apps/AimForm/FormAnswers.aspx?formID=" + rgForm.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FormID"].ToString());
            }
            if (e.CommandName == "redigera")
            {
                Response.Redirect("Form.aspx?ID=" + rgForm.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FormID"].ToString());                
            }
            if (e.CommandName == "radera")
            {
                AIM.Business.AimForm.Form_Form theForm = (from linq_form in db.Form_Forms where linq_form.FormID == Convert.ToInt32(rgForm.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FormID"].ToString()) select linq_form).First<AIM.Business.AimForm.Form_Form>();

                theForm.Deleted = true;
                theForm.DeleteDate = DateTime.Now;

                db.SubmitChanges();

                Response.Redirect("FormList.aspx");                
            }
            if (e.CommandName == "copyForm")
            {
                copyForm(Convert.ToInt32(rgForm.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FormID"].ToString()));
            }
        }

        protected void copyForm(int formid)
        {
            AIM.Business.AimForm.Form_Form form = (from linq_form in db.Form_Forms where linq_form.FormID == formid select linq_form).First<AIM.Business.AimForm.Form_Form>();

            AIM.Business.AimForm.Form_Form newform = new AIM.Business.AimForm.Form_Form();

            //newform.ExternalConnection_FK = form.ExternalConnection_FK;
            //newform.ExternalConnectionEnabled = form.ExternalConnectionEnabled;
            //newform.Form_ExternalConnection = form.Form_ExternalConnection;
            //newform.Form_ExternalConnections_ReturnValues = form.Form_ExternalConnections_ReturnValues;
            //newform.Form_ExternalConnections_Values = form.Form_ExternalConnections_Values;
            newform.FormName = "kopia av " + form.FormName;
            newform.FormText = form.FormText;
            newform.ObjectID = form.ObjectID;
            newform.SiteID = form.SiteID;

            db.Form_Forms.InsertOnSubmit(newform);
            db.SubmitChanges();            

            List<AIM.Business.AimForm.Form_Question> questionList =
            (from o in db.Form_Questions
             where o.FormID == formid && o.Deleted != true
             orderby o.Sort ascending
             select o).ToList<AIM.Business.AimForm.Form_Question>();


            foreach (AIM.Business.AimForm.Form_Question q in questionList)//drA
            {
                AIM.Business.AimForm.Form_Question newquestion = new AIM.Business.AimForm.Form_Question();

                newquestion.AlternativesMax = q.AlternativesMax;
                newquestion.FormID = newform.FormID;
                newquestion.Interval = q.Interval;
                newquestion.MainQuestionID = q.MainQuestionID;
                newquestion.QuestionText = q.QuestionText;
                newquestion.QuestionType = q.QuestionType;
                newquestion.Required = q.Required;
                newquestion.SectionID = q.SectionID;
                newquestion.SiteID = q.SiteID;
                newquestion.Sort = q.Sort;
                
                db.Form_Questions.InsertOnSubmit(newquestion);
                db.SubmitChanges();            

                List<AIM.Business.AimForm.Form_Alternative> alternativeList =
               (from o in db.Form_Alternatives
                where o.QuestionID == q.QuestionID
                select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                foreach (AIM.Business.AimForm.Form_Alternative qa in alternativeList)//drA
                {
                    AIM.Business.AimForm.Form_Alternative newalternativ = new AIM.Business.AimForm.Form_Alternative();

                    newalternativ.AlternativeText = qa.AlternativeText;
                    newalternativ.AlternativeType = qa.AlternativeType;
                    newalternativ.Correct = qa.Correct;
                    newalternativ.FormID = newform.FormID;
                    newalternativ.GroupKey = qa.GroupKey;
                    newalternativ.Jump = qa.Jump;
                    newalternativ.Point = qa.Point;
                    newalternativ.QuestionID = newquestion.QuestionID;
                    newalternativ.QuestionType = qa.QuestionType;
                    newalternativ.SectionID = qa.SectionID;
                    newalternativ.SiteID = qa.SiteID;
                    
                    db.Form_Alternatives.InsertOnSubmit(newalternativ);
                    db.SubmitChanges();
                }
            }

            rgForm.Rebind();
        }
    }
}
