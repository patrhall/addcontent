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
    public partial class EditQuestionStep2 : AIM.Base.Modules.Page.AimForm
    {        
        public string customerpath;
        public string correct;
        public int aID = 0;
        public int qID = 0;
        public int childQID = 0;
        public int fID = 0;
        public int sID = 0;
        public int catID = 0;
        public int cID = 0;
        public int point = 0;
        public int jump = 0;
        public int countAlt = 0;
        public DataSet dsAlternative;
        public DataSet dsAlternatives;
        public DataSet dsFreeAlternatives;
        public DataSet dsQuestion;
        public DataSet dsChildQuestions;
        public int siteID;

        protected override void Page_Load()
        {
            config.GetConfig();
            siteID = config.siteID;
            //this.linkStyle.Attributes.Add("href", config.customerpath + config.cssfile_config);

            if (!IsPostBack)
            {                
                if (Request["qID"] != null)
                    qID = Convert.ToInt32(Request["qID"]);

                if (qID != 0)
                {
                    AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();

                    int Type = theQuestion.QuestionType;
                    int MaxAlt = 0;

                    if (theQuestion.AlternativesMax.ToString() != "")
                        MaxAlt = theQuestion.AlternativesMax.Value;

                    switch (Type)
                    {
                        case 1:
                            lblAlternative.Visible = true;

                            List<AIM.Business.AimForm.Form_Alternative> alternativeList =
                           (from o in db.Form_Alternatives
                            where o.QuestionID == qID
                            select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                            Repeater1.DataSource = alternativeList;
                            Repeater1.DataBind();
                            foreach (RepeaterItem item in Repeater1.Items)
                            {
                                RadioButtonList rblC = (RadioButtonList)item.FindControl("rblCorr");

                                if (rblC == null)
                                    rblC = new RadioButtonList();

                                Label label = (Label)item.FindControl("lbl");
                                aID = Convert.ToInt32(label.Text);

                                //dsAlternative = alternativeList.Find(p => p.ID == aID);//AIM.DBcom.ComTrans.GetAlternative(aID);

                                var AlternativInfo = alternativeList.Find(p => p.ID == aID && p.Correct != null);

                                if (AlternativInfo != null)
                                    rblC.SelectedValue = AlternativInfo.Correct.ToString();
                                else
                                    rblC.SelectedValue = "false";
                            }
                            break;
                        case 2:
                            lblAlternative.Visible = true;

                            List<AIM.Business.AimForm.Form_Alternative> alternativeList2 =
                           (from o in db.Form_Alternatives
                            where o.QuestionID == qID
                            select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                            countAlt = alternativeList2.Count();

                            Repeater1.DataSource = alternativeList2;
                            Repeater1.DataBind();

                            foreach (RepeaterItem item in Repeater1.Items)
                            {
                                RadioButtonList rblC = (RadioButtonList)item.FindControl("rblCorr");

                                if (rblC == null)
                                    rblC = new RadioButtonList();

                                Label label = (Label)item.FindControl("lbl");
                                aID = Convert.ToInt32(label.Text);

                                var AlternativInfo = alternativeList2.Find(p => p.ID == aID && p.Correct != null);

                                if (AlternativInfo != null)
                                    rblC.SelectedValue = AlternativInfo.Correct.ToString();
                                else
                                    rblC.SelectedValue = "false";
                            }


                            if (countAlt >= 3)
                            {

                                lblMaxAlternatives.Visible = true;
                                ddlMaxAlternatives.Visible = true;
                                for (int i = 2; i <= countAlt; i++)
                                {
                                    ListItem ddlItem = new ListItem();
                                    ddlItem.Text = i.ToString();
                                    ddlMaxAlternatives.Items.Add(ddlItem);

                                }
                                if (MaxAlt != 0)
                                    ddlMaxAlternatives.Items[MaxAlt - 2].Selected = true;

                                else
                                    ddlMaxAlternatives.Items[countAlt - 2].Selected = true;

                            }

                            //Repeater1.DataSource = dsAlternatives;
                            // Repeater1.DataBind();
                            break;

                        case 6:
                        case 7:
                            lblAlternative.Visible = true;

                            List<AIM.Business.AimForm.Form_Alternative> alternativeList7 =
                           (from o in db.Form_Alternatives
                            where o.QuestionID == qID
                            select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                            countAlt = alternativeList7.Count();

                            Repeater1.DataSource = alternativeList7;// dsAlternatives;
                            Repeater1.DataBind();

                            //DataSet dsFreeAlternatives = AIM.DBcom.ComTrans.GetFreeAlternatives(qID);

                            List<AIM.Business.AimForm.Form_Alternative> FreealternativeList =
                           (from o in db.Form_Alternatives
                            where o.QuestionID == qID && o.AlternativeType == 3
                            select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                            countAlt += FreealternativeList.Count();
                            Repeater3.DataSource = FreealternativeList;
                            Repeater3.DataBind();
                            Repeater3.Visible = true;
                            lblFree2.Visible = true;
                            btnNew2.Visible = true;

                            foreach (RepeaterItem item in Repeater1.Items)
                            {
                                RadioButtonList rblC = (RadioButtonList)item.FindControl("rblCorr");

                                if (rblC == null)
                                    rblC = new RadioButtonList();

                                Label label = (Label)item.FindControl("lbl");
                                aID = Convert.ToInt32(label.Text);

                                var AlternativInfo = alternativeList7.Find(p => p.ID == aID && p.Correct != null);

                                if (AlternativInfo != null)
                                    rblC.SelectedValue = AlternativInfo.Correct.ToString();
                                else
                                    rblC.SelectedValue = "false";
                            }


                            if (countAlt >= 3)
                            {

                                lblMaxAlternatives.Visible = true;
                                ddlMaxAlternatives.Visible = true;
                                for (int i = 2; i <= countAlt; i++)
                                {
                                    ListItem ddlItem = new ListItem();
                                    ddlItem.Text = i.ToString();
                                    ddlMaxAlternatives.Items.Add(ddlItem);

                                }
                                if (MaxAlt != 0)
                                    ddlMaxAlternatives.Items[MaxAlt - 2].Selected = true;

                                else
                                    ddlMaxAlternatives.Items[countAlt - 2].Selected = true;

                            }

                            //Repeater1.DataSource = dsAlternatives;
                            // Repeater1.DataBind();
                            break;

                        // tbQuestion.Text = dsQuestion.Tables[0].Rows[0]["QuestionText"].ToString();
                        // rblTypes.SelectedValue = dsQuestion.Tables[0].Rows[0]["QuestionType"].ToString();
                    }
                }
            }
        }

        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            SaveQuestion();
            Response.Redirect("Form.aspx?ID=" + fID);
        }

        protected void btnPrevious_Click(object sender, System.EventArgs e)
        {
            SaveQuestion();
            Response.Redirect("EditQuestion.aspx?" + Request.QueryString);
        }

        protected void btnNew_Click(object sender, EventArgs e) //Lägg till nytt alternativ enval, flerval, kombination.
        {
            SaveQuestion();            

            qID = Convert.ToInt32(Request["qID"]);

            AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();

            int fID = 0;
            int sID = 0;
            int qType = theQuestion.QuestionType;
            int aType;
            int aID;
            int j = 0;
            string alternativeText;
            string interval = "";

            if (theQuestion.FormID.ToString() != "")
                fID = theQuestion.FormID.Value;

            if (theQuestion.SectionID.ToString() != "")
                sID = theQuestion.SectionID.Value;

            /*//Spara befintliga alternativ
              foreach (RepeaterItem item in Repeater1.Items)
              {

                  TextBox Answer = (TextBox)item.FindControl("tbRepeat");
             
                  RadioButtonList rblC = (RadioButtonList)item.FindControl("rblCorr");
                  Label Label = (Label)item.FindControl("lbl");
                  alternativeText = Answer.Text;
                  aID = Convert.ToInt32((Label.Text));
                  correct = rblC.SelectedValue;
                  DBcom.ComTrans.SaveAlternativeCorrect(aID, correct);

                if (alternativeText != "")
                 {
                    alternativeText = alternativeText.ToString().Replace("\r", "<br>");
                    dsAlternative = AIM.DBcom.ComTrans.GetAlternative(aID);
                    aType = Convert.ToInt32(dsAlternative.Tables[0].Rows[0]["AlternativeType"]);
                    AIM.DBcom.ComTrans.SaveAlternative(Convert.ToInt32(siteID_cookie.Value), fID, sID, qID, aID, aType, alternativeText);
                    dsAlternative.Clear();
                 }
                 j++;
              }
              DBcom.ComTrans.SaveInterval(qID, interval);
              j = 0;

                foreach (RepeaterItem item in Repeater2.Items)
                {
                    TextBox Answer2 = (TextBox)item.FindControl("tbRepeat");
                    TextBox tbPoint2 = (TextBox)item.FindControl("tbP2");
                    Label Label2 = (Label)item.FindControl("lbl");
                    TextBox tbInterval = (TextBox)item.FindControl("tbI3");
                    TextBox tbInterval2 = (TextBox)item.FindControl("tbI4");
                    alternativeText = Answer2.Text;
                    aID = Convert.ToInt32(Label2.Text);

                    if (tbPoint2.Visible == true) //if  point alternative
                    {
                        dsAlternatives = AIM.DBcom.ComTrans.GetAlternative(aID);

                        string gKey = dsAlternatives.Tables[0].Rows[0]["GroupKey"].ToString();
                        DataSet dsAlternativeGroup = AIM.DBcom.ComTrans.GetAlternativeGroup(gKey);

                        foreach (DataRow dr in dsAlternativeGroup.Tables[0].Rows)
                        {
                            int gAID = Convert.ToInt32(dr["ID"]);
                            point = Convert.ToInt32(tbPoint2.Text);
                            DBcom.ComTrans.SaveAlternativePoint(gAID, point);
                        }

                    }

                    //Save Interval questiontype 5
                    if (tbInterval.Visible == true)
                        interval = interval + j.ToString() + "," + tbInterval.Text + ";";

                    if (tbInterval2.Visible == true && j == Repeater2.Items.Count - 1)
                        interval = interval + Repeater2.Items.Count + "," + tbInterval2.Text + ";";

                
                    //

                    if (alternativeText != "")
                    {
                        alternativeText = alternativeText.ToString().Replace("\r", "<br>");
                        dsAlternatives = AIM.DBcom.ComTrans.GetAlternative(aID);
                        aType = Convert.ToInt32(dsAlternatives.Tables[0].Rows[0]["AlternativeType"]);
                        AIM.DBcom.ComTrans.SaveAlternative(Convert.ToInt32(siteID_cookie.Value), fID, sID, qID, aID, aType, alternativeText);
                    }
                    j++;
                }

                DBcom.ComTrans.SaveInterval(qID, interval);*/
            

            //if (Request["cID"].ToString() != "")
            //  catID = Convert.ToInt32(Request["cID"]);


            AIM.Business.AimForm.Form_Alternative newAlternative = new AIM.Business.AimForm.Form_Alternative();
            db.Form_Alternatives.InsertOnSubmit(newAlternative);
            newAlternative.SiteID = siteID;
            newAlternative.FormID = fID;
            newAlternative.SectionID = sID;
            newAlternative.QuestionID=qID;
            newAlternative.QuestionType = qType;

            switch (qType)
            {
                case 1:
                case 6:
                    newAlternative.AlternativeType = 1;
                    break;
                case 3:
                    newAlternative.AlternativeType = 3;
                    break;

                default:
                    newAlternative.AlternativeType = 2;
                    break;
            }            
            newAlternative.AlternativeText = "Nytt Alternativ";

            db.SubmitChanges();

            Response.Redirect("EditQuestionStep2.aspx?" + Request.QueryString); //+ "&cID=" + catID);
        }

        protected void btnNew2_Click(object sender, EventArgs e)//Lägg till nytt alternativ, flerkategorifrågor eller fritextalternativ
        {
            SaveQuestion();
            
            qID = Convert.ToInt32(Request["qID"]);

            AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();
            
            int fID = 0;
            int sID = 0;
            int aType;
            int qType = theQuestion.QuestionType;
            int aID;
            int j = 0;
            string alternativeText;
            string childQuestionText;
            string interval = "";

            if (theQuestion.FormID.ToString() != "")
                fID = theQuestion.FormID.Value;

            if (theQuestion.SectionID.ToString() != "")
                sID = theQuestion.SectionID.Value;

            //Spara nytt alternativ

            AIM.Business.AimForm.Form_Alternative newAlternative = new AIM.Business.AimForm.Form_Alternative();
            db.Form_Alternatives.InsertOnSubmit(newAlternative); 
            newAlternative.SiteID = siteID;
            newAlternative.FormID = fID;
            newAlternative.SectionID = sID;
            newAlternative.QuestionID = qID;
            newAlternative.QuestionType = qType;
            newAlternative.AlternativeType = 3;
            newAlternative.AlternativeText = "Nytt Alternativ";

            db.SubmitChanges();            

            Response.Redirect("EditQuestionStep2.aspx?" + Request.QueryString);
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {   
            if (e.CommandName == "DeleteAlternative")
            {
                SaveQuestion();

                Hashtable hash = new Hashtable();
                hash.Add("@ID", e.CommandArgument);

                AIM.Business.AimForm.Form_Alternative theAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == Convert.ToInt32(e.CommandArgument) select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

                db.Form_Alternatives.DeleteOnSubmit(theAlternative);
                db.SubmitChanges();

                Response.Redirect("EditQuestionStep2.aspx?" + Request.QueryString); // + "&cID=" + catID);
            }
        }

        protected void Repeater3_ItemCommand(object source, RepeaterCommandEventArgs e)
        {           
            if (e.CommandName == "DeleteQuestion")
            {
                SaveQuestion();

                AIM.Business.AimForm.Form_Alternative theAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == Convert.ToInt32(e.CommandArgument) select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

                db.Form_Alternatives.DeleteOnSubmit(theAlternative);
                db.SubmitChanges();

                Response.Redirect("EditQuestionStep2.aspx?" + Request.QueryString);
            }
        }

        protected void ddlMaxAlternatives_SelectedIndexChanged(object sender, EventArgs e)
        {
            qID = Convert.ToInt32(Request["qID"]);
            AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();

            int qType = theQuestion.QuestionType;

            if (qType == 4)
            {
                List<AIM.Business.AimForm.Form_Question> childquestionList =
                (from o in db.Form_Questions
                 where o.MainQuestionID == theQuestion.QuestionID
                 orderby o.QuestionID ascending
                 select o).ToList<AIM.Business.AimForm.Form_Question>();

                foreach (AIM.Business.AimForm.Form_Question q in childquestionList)
                {
                    AIM.Business.AimForm.Form_Question theChildQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == q.QuestionID select linq_question).First<AIM.Business.AimForm.Form_Question>();
                    theChildQuestion.AlternativesMax = Convert.ToInt32(ddlMaxAlternatives.SelectedValue);
                    db.SubmitChanges();
                }
            }
            else
            {
                theQuestion.AlternativesMax = Convert.ToInt32(ddlMaxAlternatives.SelectedValue);
                db.SubmitChanges();
            }
        }

        protected void ddlJump_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dsQuestion = get...

            qID = Convert.ToInt32(Request["qID"]);
            AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();
            DropDownList ddlJump = sender as DropDownList;
            //DropDownList ddlJump = (DropDownList) e.Item.FindControl("ddlJ");
            string clientID = ddlJump.ClientID;
            int altID = 0;

            foreach (RepeaterItem item in Repeater1.Items)
            {
                DropDownList ddl = (DropDownList)item.FindControl("ddlJ");
                Label lbl = (Label)item.FindControl("lbl");
                if (ddl.ClientID == clientID)
                    altID = Convert.ToInt32(lbl.Text);

            }

            ////if jump existed before; delete
            //dsAlternatives = AIM.DBcom.ComTrans.GetAlternatives(qID);
            //fID = Convert.ToInt32(dsQuestion.Tables[0].Rows[0]["FormID"]);

            AIM.Business.AimForm.Form_Alternative theAlt = (from linq_alt in db.Form_Alternatives where linq_alt.ID == altID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

            theAlt.Jump = Convert.ToInt32(ddlJump.SelectedValue);
        }

        protected void SaveQuestion()
        {   
            DataSet dsQ;
            qID = Convert.ToInt32(Request["qID"]);
            fID = Convert.ToInt32(Request["ID"]);

            AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();
            
            int qType = theQuestion.QuestionType;
            int aType;
            int aID;
            int cID = Convert.ToInt32(Request["cID"]);
            int MaxAlt;
            int j = 0;
            string alternativeText;
            string childQuestionText;
            string interval = "";

            if (theQuestion.FormID.ToString() != "")
                fID = theQuestion.FormID.Value;

            if (theQuestion.SectionID.ToString() != "")
                sID = theQuestion.SectionID.Value;

            //Nedan sparas all inskriven text, befintliga roller sätts och allt kopplas mot rätt ID
            foreach (RepeaterItem item in Repeater1.Items)
            {
                TextBox Answer = (TextBox)item.FindControl("tbRepeat");
                RadioButtonList rblC = (RadioButtonList)item.FindControl("rblCorr");

                if (rblC == null)
                    rblC = new RadioButtonList();

                Label Label = (Label)item.FindControl("lbl");
                alternativeText = Answer.Text;
                aID = Convert.ToInt32((Label.Text));
                correct = rblC.SelectedValue;
                                
                //DBcom.ComTrans.SaveAlternativeCorrect(aID, correct);
                //dsAlternative = AIM.DBcom.ComTrans.GetAlternative(aID);
                AIM.Business.AimForm.Form_Alternative theAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == aID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

                theAlternative.Correct = correct;

                if (alternativeText != "")
                    alternativeText = alternativeText.ToString().Replace("\r", "<br>");
                else
                    alternativeText = theAlternative.AlternativeText;
                
                aType = Convert.ToInt32(theAlternative.AlternativeType);
                
                if (aType == 1 && qType == 2)
                    aType = 2;
                else if (aType == 2 && qType == 1)
                    aType = 1;

                theAlternative.AlternativeType = aType;
                theAlternative.AlternativeText = alternativeText;

                db.SubmitChanges();
                //AIM.DBcom.ComTrans.SaveAlternative(Convert.ToInt32(siteID_cookie.Value), fID, sID, qID, aID, aType, alternativeText);

                j++;
            }

            theQuestion.Interval = interval;
            db.SubmitChanges();
            //DBcom.ComTrans.SaveInterval(qID, interval);

            foreach (RepeaterItem item in Repeater3.Items)
            {
                TextBox Answer2 = (TextBox)item.FindControl("tbRepeat");
                TextBox tbPoint2 = (TextBox)item.FindControl("tbP2");
                TextBox tbInterval = (TextBox)item.FindControl("tbI3");
                TextBox tbInterval2 = (TextBox)item.FindControl("tbI4");
                Label Label2 = (Label)item.FindControl("lbl");
                alternativeText = Answer2.Text;
                aID = Convert.ToInt32(Label2.Text);

                if (alternativeText != null && alternativeText != "")
                {
                    try
                    {
                        alternativeText = alternativeText.ToString().Replace("\r", "<br>");
                    

                    AIM.Business.AimForm.Form_Alternative theAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == aID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();
                    aType = theAlternative.AlternativeType;

                    string gKey = theAlternative.GroupKey.ToString();

                    if (gKey != "")
                    {
                        List<AIM.Business.AimForm.Form_Alternative> alternativeList =
                        (from o in db.Form_Alternatives
                         where o.GroupKey == gKey
                         orderby o.ID ascending
                         select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                        foreach (AIM.Business.AimForm.Form_Alternative a in alternativeList)
                        {
                            AIM.Business.AimForm.Form_Alternative theTempAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == a.ID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

                            theTempAlternative.QuestionID = aID;
                            theTempAlternative.AlternativeType = aType;
                            theTempAlternative.AlternativeText = alternativeText;
                            db.SubmitChanges();
                            //AIM.DBcom.ComTrans.SaveAlternative(Convert.ToInt32(siteID_cookie.Value), fID, sID, aID, gAID, aType, alternativeText);
                        }
                    }

                    theAlternative.AlternativeType = aType;
                    theAlternative.AlternativeText = alternativeText;
                    db.SubmitChanges();
                    }
                    catch
                    {

                    }
                    //AIM.DBcom.ComTrans.SaveAlternative(Convert.ToInt32(siteID_cookie.Value), fID, sID, qID, aID, aType, alternativeText);
                }
            }

            j = 0;

            foreach (RepeaterItem item in Repeater2.Items)
            {
                TextBox Answer2 = (TextBox)item.FindControl("tbRepeat");
                TextBox tbPoint2 = (TextBox)item.FindControl("tbP2");
                TextBox tbInterval = (TextBox)item.FindControl("tbI3");
                TextBox tbInterval2 = (TextBox)item.FindControl("tbI4");
                Label Label2 = (Label)item.FindControl("lbl");
                alternativeText = Answer2.Text;
                aID = Convert.ToInt32(Label2.Text);

                /*  if (tbInterval.Visible == true)
                      interval = interval + j.ToString() + "," + tbInterval.Text + ";";

                  if (tbInterval2.Visible == true && j == Repeater2.Items.Count - 1)
                      interval = interval + Repeater2.Items.Count + "," + tbInterval2.Text + ";";

                  DBcom.ComTrans.SaveInterval(qID, interval);*/


                if (alternativeText != "")
                {
                    alternativeText = alternativeText.ToString().Replace("\r", "<br>");
                    AIM.Business.AimForm.Form_Alternative theAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == aID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();
                    aType = theAlternative.AlternativeType;

                    string gKey = theAlternative.GroupKey;

                    if (gKey != "")
                    {
                        List<AIM.Business.AimForm.Form_Alternative> alternativeList =
                        (from o in db.Form_Alternatives
                         where o.GroupKey == gKey
                         orderby o.ID ascending
                         select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                        foreach (AIM.Business.AimForm.Form_Alternative a in alternativeList)
                        {
                            AIM.Business.AimForm.Form_Alternative theTempAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == a.ID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

                            theTempAlternative.QuestionID = aID;
                            theTempAlternative.AlternativeType = aType;
                            theTempAlternative.AlternativeText = alternativeText;
                            db.SubmitChanges();
                        }
                    }
                    theAlternative.AlternativeType = aType;
                    theAlternative.AlternativeText = alternativeText;
                    db.SubmitChanges();
                }
                j++;

            }

            if (qType == 5)
            {
                List<AIM.Business.AimForm.Form_Question> childquestionList =
                (from o in db.Form_Questions
                 where o.MainQuestionID == qID
                 orderby o.QuestionID ascending
                 select o).ToList<AIM.Business.AimForm.Form_Question>();

                foreach (AIM.Business.AimForm.Form_Question q in childquestionList)
                {
                    List<AIM.Business.AimForm.Form_Alternative> childquestionalternativeList =
                (from o in db.Form_Alternatives
                 where o.QuestionID == q.QuestionID
                 orderby o.QuestionID ascending
                 select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                    countAlt = childquestionalternativeList.Count();
                    if (countAlt >= 3)
                    {
                        theQuestion.AlternativesMax = Convert.ToInt32(ddlMaxAlternatives.SelectedItem.Text);
                        //AIM.DBcom.ComTrans.SaveAlternativesMax(qID, Convert.ToInt32(ddlMaxAlternatives.SelectedItem.Text));

                        q.AlternativesMax = Convert.ToInt32(ddlMaxAlternatives.SelectedItem.Text);
                        //AIM.DBcom.ComTrans.SaveAlternativesMax(cID, Convert.ToInt32(ddlMaxAlternatives.SelectedItem.Text));
                    }

                    db.SubmitChanges();
                }

            }

            else if (qType == 2 || qType == 7)
            {
                //dsAlternatives = AIM.DBcom.ComTrans.GetAlternatives(qID);
                List<AIM.Business.AimForm.Form_Alternative> alternativeList =
                (from o in db.Form_Alternatives
                 where o.QuestionID == qID
                 orderby o.QuestionID ascending
                 select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                //dsFreeAlternatives = AIM.DBcom.ComTrans.GetFreeAlternatives(qID);
                List<AIM.Business.AimForm.Form_Alternative> freealternativeList =
                (from o in db.Form_Alternatives
                 where o.QuestionID == qID && o.AlternativeType == 3
                 orderby o.QuestionID ascending
                 select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                countAlt = alternativeList.Count();
                countAlt = countAlt + freealternativeList.Count();
                if (countAlt >= 3)
                {
                    theQuestion.AlternativesMax = Convert.ToInt32(ddlMaxAlternatives.SelectedItem.Text);
                    db.SubmitChanges();
                    //AIM.DBcom.ComTrans.SaveAlternativesMax(qID, Convert.ToInt32(ddlMaxAlternatives.SelectedItem.Text));
                }
            }
        }

        protected void btnNew3_Click(object sender, EventArgs e)
        {            
            qID = Convert.ToInt32(Request["qID"]);
            int cID;
            int aID;
            int aType;
            int j = 0;
            string childQuestionText;
            string alternativeText;
            string interval = "";

            //dsQuestion = AIM.DBcom.ComTrans.GetQuestion(qID);
            AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();
            
            int fID = 0;
            int sID = 0;
            bool newQ = false;
            int qType = theQuestion.QuestionType;

            if (theQuestion.FormID.ToString() != "")
                fID = theQuestion.FormID.Value;

            if (theQuestion.SectionID.ToString() != "")
                sID = theQuestion.SectionID.Value;
            
            //Spara befintliga frågor
            foreach (RepeaterItem item in Repeater3.Items)
            {
                TextBox Answer = (TextBox)item.FindControl("tbRepeat");
                Label Label = (Label)item.FindControl("lbl");
                childQuestionText = Answer.Text;
                cID = Convert.ToInt32((Label.Text));

                if (childQuestionText != "")
                {
                    //AIM.DBcom.ComTrans.SaveChildQuestion(Convert.ToInt32(siteID_cookie.Value), fID, sID, cID, qType, childQuestionText, qID);

                    AIM.Business.AimForm.Form_Question thechildQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == cID select linq_question).First<AIM.Business.AimForm.Form_Question>();
                    thechildQuestion.SiteID = config.siteID;
                    thechildQuestion.FormID = fID;
                    thechildQuestion.SectionID = sID;
                    thechildQuestion.QuestionID = cID;
                    thechildQuestion.QuestionType = qType;
                    thechildQuestion.QuestionText = childQuestionText;
                    thechildQuestion.MainQuestionID = qID;
                    db.SubmitChanges();
                }
            }
            foreach (RepeaterItem item in Repeater2.Items)
            {
                TextBox Answer2 = (TextBox)item.FindControl("tbRepeat");
                Label Label2 = (Label)item.FindControl("lbl");
                TextBox tbInterval = (TextBox)item.FindControl("tbI3");
                TextBox tbInterval2 = (TextBox)item.FindControl("tbI4");
                alternativeText = Answer2.Text;
                aID = Convert.ToInt32((Label2.Text));

                //Save Interval questiontype 5
                if (tbInterval.Visible == true)
                    interval = interval + j.ToString() + "," + tbInterval.Text + ";";

                if (tbInterval2.Visible == true && j == Repeater2.Items.Count - 1)
                    interval = interval + Repeater2.Items.Count + "," + tbInterval2.Text + ";";
                
                //
                if (alternativeText != "")
                {
                    alternativeText = alternativeText.ToString().Replace("\r", "<br>");
                    
                    //dsAlternatives = AIM.DBcom.ComTrans.GetAlternative(aID);
                    AIM.Business.AimForm.Form_Alternative theAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == aID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();
                    
                    aType = theAlternative.AlternativeType;
                    theAlternative.AlternativeText = alternativeText;

                    db.SubmitChanges();

                    //AIM.DBcom.ComTrans.SaveAlternative(Convert.ToInt32(siteID_cookie.Value), fID, sID, qID, aID, aType, alternativeText);

                }
                j++;
            }

            //DBcom.ComTrans.SaveInterval(qID, interval);

            theQuestion.Interval = interval;
            db.SubmitChanges();

            if (Request["cID"].ToString() != "")
                catID = Convert.ToInt32(Request["cID"]);

            Response.Redirect("EditQuestionStep2.aspx?" + Request.QueryString); // + "&cID=" + catID);

        }//NOT IN USE
        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)//NOT IN USE
        {
            HttpCookie siteID_cookie = Request.Cookies["SiteID"];
            DataSet dsQ;
            qID = Convert.ToInt32(Request["qID"]);
            //dsQuestion = AIM.DBcom.ComTrans.GetQuestion(qID);
            AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == qID select linq_question).First<AIM.Business.AimForm.Form_Question>();

            int qType = theQuestion.QuestionType;
            int aType;
            int aID;
            int cID = Convert.ToInt32(Request["cID"]);
            int MaxAlt;
            int j = 0;
            string alternativeText;
            string childQuestionText;
            string interval = "";

            if (e.CommandName == "DeleteAlternative")
            {
                foreach (RepeaterItem item in Repeater2.Items)
                {
                    TextBox Answer2 = (TextBox)item.FindControl("tbRepeat");
                    TextBox tbPoint2 = (TextBox)item.FindControl("tbP2");
                    Label Label2 = (Label)item.FindControl("lbl");
                    alternativeText = Answer2.Text;
                    aID = Convert.ToInt32(Label2.Text);

                    if (tbPoint2.Visible == true) //if  point alternative
                    {
                        //dsAlternatives = AIM.DBcom.ComTrans.GetAlternative(aID);
                        AIM.Business.AimForm.Form_Alternative theAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == aID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();
                    
                        string gKey = theAlternative.GroupKey;
                        //DataSet dsAlternativeGroup = AIM.DBcom.ComTrans.GetAlternativeGroup(gKey);
                        //foreach (DataRow dr in dsAlternativeGroup.Tables[0].Rows)
                        List<AIM.Business.AimForm.Form_Alternative> alternativeList =
                       (from o in db.Form_Alternatives
                        where o.GroupKey == gKey
                        select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                        foreach (AIM.Business.AimForm.Form_Alternative qa in alternativeList)//drA
                        {
                            //int gAID = Convert.ToInt32(dr["ID"]);
                            //point = Convert.ToInt32(tbPoint2.Text);
                            AIM.Business.AimForm.Form_Alternative theAlternativetbPoint2 = (from linq_alt in db.Form_Alternatives where linq_alt.ID == qa.ID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

                            theAlternativetbPoint2.Point = point;
                            db.SubmitChanges();
                            //DBcom.ComTrans.SaveAlternativePoint(gAID, point);
                        }

                    }

                    if (alternativeText != "")
                    {
                        alternativeText = alternativeText.ToString().Replace("\r", "<br>");
                        //dsAlternatives = AIM.DBcom.ComTrans.GetAlternative(aID);
                        AIM.Business.AimForm.Form_Alternative theAlternative = (from linq_alt in db.Form_Alternatives where linq_alt.ID == aID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

                        aType = theAlternative.AlternativeType;

                        string gKey = theAlternative.GroupKey;

                        if (gKey != "")
                        {
                            //DataSet dsAlternativeGroup = AIM.DBcom.ComTrans.GetAlternativeGroup(gKey);
                            //foreach (DataRow dr in dsAlternativeGroup.Tables[0].Rows)
                            List<AIM.Business.AimForm.Form_Alternative> alternativeList =
                           (from o in db.Form_Alternatives
                            where o.GroupKey == gKey
                            select o).ToList<AIM.Business.AimForm.Form_Alternative>();

                            foreach (AIM.Business.AimForm.Form_Alternative qa in alternativeList)//drA
                            {
                                //int gAID = Convert.ToInt32(dr["ID"]);
                                //AIM.DBcom.ComTrans.SaveAlternative(Convert.ToInt32(siteID_cookie.Value), fID, sID, aID, gAID, aType, alternativeText);
                                AIM.Business.AimForm.Form_Alternative theAlternativegKey = (from linq_alt in db.Form_Alternatives where linq_alt.ID == qa.ID select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();
                                theAlternativegKey.Point = point;
                                theAlternativegKey.SiteID = config.siteID;
                                theAlternativegKey.FormID = fID;
                                theAlternativegKey.QuestionID = aID;
                                theAlternativegKey.ID = qa.ID;
                                theAlternativegKey.AlternativeType = aType;
                                theAlternativegKey.AlternativeText = alternativeText;

                                db.SubmitChanges();
                            }
                        }
                        
                        //AIM.DBcom.ComTrans.SaveAlternative(Convert.ToInt32(siteID_cookie.Value), fID, sID, qID, aID, aType, alternativeText);
                        theAlternative.AlternativeType = aType;
                        theAlternative.AlternativeText = alternativeText;
                        db.SubmitChanges();

                    }
                }
                
                qType = theQuestion.QuestionType;
                Hashtable hash = new Hashtable();
                //hash.Add("@ID", e.CommandArgument);
                //AIM.DBcom.ComClass.ExecuteStoredProcedure("usp_Form_DeleteAlternative", hash);
                
                AIM.Business.AimForm.Form_Alternative theAlternativeDelete = (from linq_alt in db.Form_Alternatives where linq_alt.ID == Convert.ToInt32(e.CommandArgument) select linq_alt).First<AIM.Business.AimForm.Form_Alternative>();

                db.Form_Alternatives.DeleteOnSubmit(theAlternativeDelete);
                db.SubmitChanges();
                if (Request["cID"].ToString() != "")
                    catID = Convert.ToInt32(Request["cID"]);

                Response.Redirect("EditQuestionStep2.aspx?" + Request.QueryString); // + "&cID=" + catID);
            }


        }
    }
}
