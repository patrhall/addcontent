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



/*
 * Created by: Martin Zacharoff
 * Removed: 
 * <asp:ListItem Text="Enval med fritextruta" Value="6"></asp:ListItem>
 * <asp:ListItem Text="Flerval med fritextruta" Value="7"></asp:ListItem>
*/
namespace AimForm
{
    public partial class EditQuestion : AIM.Base.Modules.Page.AimForm
    {
        public string cssfile_config;
        public string customerpath;
        public int qID = 0;
        public int cQID = 0;
        public int altID = 0;
        public string cID = "0";
        public DataSet dsQuestion;
        public DataSet dsFormQuestions;
        public DataSet dsChildQuestions;
        //public DataSet dsAlternatives;

        protected override void Page_Load()
        {   
            //hämta configvariabler
            config.GetConfig();

            //this.linkStyle.Attributes.Add("href", config.customerpath + config.cssfile_config);
            
            if (!IsPostBack)
            {
                if(Request["qID"]!= null)
                    qID = Convert.ToInt32(Request["qID"]);

                if (qID != 0)
                {
                    AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();
                   
                    tbQuestion.Text = theQuestion.QuestionText;
                    rblTypes.SelectedValue = theQuestion.QuestionType.ToString();
                    int questionType = theQuestion.QuestionType;

                    if (theQuestion.Required == "T")
                        cbRequired.Checked = true;
                }
                else
                    rblTypes.SelectedValue = "3";
            }

            if (rblTypes.SelectedValue == "1" || rblTypes.SelectedValue == "6")
            {
                lblRequired.Visible = false;
                cbRequired.Visible = false;
            }         
        }       

        protected void btnSave_Click(object sender, System.EventArgs e)
        {

            


            qID = Convert.ToInt32(Request["qID"]);
            
            //dsAlternatives = AIM.DBcom.ComTrans.GetAlternatives(qID);

            int qType = 0;
            //int newID = 0;
            int fID = 0;
            int sort = 0;
            //int cQID = 0;
            //int altID = 0;

            //string kID = Request["ObjectKey"].ToString();
                        
            //  sID = Convert.ToInt32(siteID_cookie.Value);
            fID = Convert.ToInt32(Request["ID"]);

            if (qID != 0)
            {
                AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();

                qType = Convert.ToInt32(rblTypes.SelectedValue);

                if (theQuestion.Sort.Value.ToString() != "")
                    sort = theQuestion.Sort.Value;
                else
                {
                    if (db.Form_Questions.Max(Sort => Sort.Sort).HasValue)
                    {
                        try
                        {
                            List<AIM.Business.AimForm.Form_Question> listOfQuestions = (from o in db.Form_Questions
                                                                                        where o.FormID == fID && o.Deleted != true
                                                                                        orderby o.Sort ascending
                                                                                        select o).ToList<AIM.Business.AimForm.Form_Question>();

                            theQuestion.Sort = listOfQuestions.Max(Sort => Sort.Sort).Value + 1;
                        }
                        catch
                        {
                            theQuestion.Sort = 1;
                        }
                    }
                    else
                    {
                        theQuestion.Sort = 1;
                    }
                }

                if (theQuestion.FormID.ToString() != "")
                    fID = theQuestion.FormID.Value;


                theQuestion.SectionID = 0;
                theQuestion.QuestionType = qType;
                theQuestion.QuestionText = tbQuestion.Text;
                theQuestion.Sort = sort;                

                //Save Question's Required status                
                if (cbRequired.Checked)
                    theQuestion.Required = "T";
                else
                    theQuestion.Required = "F";

                db.SubmitChanges();

                if (qType == 1 || qType == 2 || qType == 6 || qType == 7)
                    Response.Redirect("EditQuestionStep2.aspx?" + Request.QueryString);
                else
                {
                    Response.Redirect("Form.aspx?ID=" + fID);                    
                }
            }
            else
            {
                AIM.Business.AimForm.Form_Question theQuestion = new AIM.Business.AimForm.Form_Question();
                

                qType = Convert.ToInt32(rblTypes.SelectedValue);

                //Create Question

                theQuestion.SectionID = 0;
                theQuestion.QuestionType = qType;
                theQuestion.QuestionText = tbQuestion.Text;

                config.GetConfig();
                theQuestion.SiteID = config.siteID;

                if (db.Form_Questions.Max(Sort => Sort.Sort).HasValue)
                {
                    try
                    {
                        List<AIM.Business.AimForm.Form_Question> listOfQuestions = (from o in db.Form_Questions
                                                                                    where o.FormID == fID && o.Deleted != true
                                                                                    orderby o.Sort ascending
                                                                                    select o).ToList<AIM.Business.AimForm.Form_Question>();

                        theQuestion.Sort = listOfQuestions.Max(Sort => Sort.Sort).Value + 1;
                    }
                    catch
                    {
                        theQuestion.Sort = 1;
                    }
                }
                else
                {
                    theQuestion.Sort = 1;
                }

                //Save Question's Required status
                if (cbRequired.Checked)
                    theQuestion.Required = "T";
                else
                    theQuestion.Required = "F";

                theQuestion.FormID = fID;
                db.Form_Questions.InsertOnSubmit(theQuestion);
                db.SubmitChanges();

                if (qType == 1 || qType == 2 || qType == 6 || qType == 7)
                    Response.Redirect("EditQuestionStep2.aspx?" + Request.QueryString + "&qID=" + theQuestion.QuestionID.ToString());
                else
                {
                    Response.Redirect("Form.aspx?ID=" + fID);                    
                }
            }
        }
    }
}
