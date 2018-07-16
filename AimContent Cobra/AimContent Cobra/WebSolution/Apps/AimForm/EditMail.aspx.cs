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
    public partial class EditMail : AIM.Base.Modules.Page.AimForm
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

        protected override void Page_Load()
        {
            //hämta configvariabler
            config.GetConfig();                       
            //this.linkStyle.Attributes.Add("href", config.customerpath + config.cssfile_config);

            if (!IsPostBack)
            {
                int formID = Convert.ToInt32(Request["ID"]);

                //DataSet dsMail = AIM.DBcom.ComTrans.GetFormMail(formID);
                List<AIM.Business.AimForm.Form_Mail> fromMails = (from o in db.Form_Mails
                where o.FormID == formID
                select o).ToList<AIM.Business.AimForm.Form_Mail>();

                Repeater1.DataSource = fromMails;
                Repeater1.DataBind();

                if (fromMails.Count() != 0)
                {
                    var fmail = fromMails.First();
                    tbFrommail.Text = fmail.FromMail;
                }

                /* else
                 {
                     tbFrommail.Visible = false;
                     lblFromMail.Visible = false;
                 }*/
            }
        }

        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            int formID = Convert.ToInt32(Request["ID"]);

            //Save mailadresses
            foreach (RepeaterItem item in Repeater1.Items)
            {
                TextBox Mail = (TextBox)item.FindControl("tbRepeat");
                Label Label = (Label)item.FindControl("lbl");

                string mail = Mail.Text;
                int ID = Convert.ToInt32((Label.Text));

                //DBcom.ComTrans.SaveFormMail(ID, mail, tbFrommail.Text);
                AIM.Business.AimForm.Form_Mail tmpFromMail = (from linq_frommail in db.Form_Mails where linq_frommail.ID == ID select linq_frommail).First<AIM.Business.AimForm.Form_Mail>();
                //DBcom.ComTrans.SaveFormMail(ID, mail, tbFrommail.Text);
                tmpFromMail.Mail = mail;
                tmpFromMail.FromMail = tbFrommail.Text;
                db.SubmitChanges();
            }


            Response.Redirect("Form.aspx?" + Request.QueryString);
            /*if (Convert.ToInt32(WebConfigurationManager.AppSettings["Skarp"]) == 1)
                Response.Redirect("/Form.aspx?" + Request.QueryString);
            else
                Response.Redirect("/aim2/Form.aspx?" + Request.QueryString);*/
        }


        protected void btnNew_Click(object sender, EventArgs e) //Lägg till nytt alternativ enval, flerval, kombination.
        {
            int formID = Convert.ToInt32(Request["ID"]);

            //Save current mail adresses
            foreach (RepeaterItem item in Repeater1.Items)
            {
                TextBox Mail = (TextBox)item.FindControl("tbRepeat");
                Label Label = (Label)item.FindControl("lbl");

                string mail = Mail.Text;
                int ID = Convert.ToInt32((Label.Text));

                AIM.Business.AimForm.Form_Mail tmpFromMail = (from linq_frommail in db.Form_Mails where linq_frommail.ID == ID select linq_frommail).First<AIM.Business.AimForm.Form_Mail>(); 
                //DBcom.ComTrans.SaveFormMail(ID, mail, tbFrommail.Text);
                tmpFromMail.Mail = mail;
                tmpFromMail.FromMail = tbFrommail.Text;
                db.SubmitChanges();
            }

            //AIM.DBcom.ComTrans.CreateFormMail(formID, "", tbFrommail.Text);
            AIM.Business.AimForm.Form_Mail newFromMail = new AIM.Business.AimForm.Form_Mail();

            db.Form_Mails.InsertOnSubmit(newFromMail);
            newFromMail.FormID = formID;
            newFromMail.FromMail = "";
            newFromMail.FromMail = tbFrommail.Text;

            db.SubmitChanges();

            Response.Redirect("EditMail.aspx?" + Request.QueryString);
        }


        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAlternative")
            {
                //Save Current
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    TextBox Mail = (TextBox)item.FindControl("tbRepeat");
                    Label Label = (Label)item.FindControl("lbl");

                    string mail = Mail.Text;
                    int ID = Convert.ToInt32((Label.Text));

                    //DBcom.ComTrans.SaveFormMail(ID, mail, tbFrommail.Text);
                    AIM.Business.AimForm.Form_Mail tmpFromMail = (from linq_frommail in db.Form_Mails where linq_frommail.ID == ID select linq_frommail).First<AIM.Business.AimForm.Form_Mail>();
                    //DBcom.ComTrans.SaveFormMail(ID, mail, tbFrommail.Text);
                    tmpFromMail.Mail = mail;
                    tmpFromMail.FromMail = tbFrommail.Text;
                    db.SubmitChanges();
                }

                //AIM.DBcom.ComClass.ExecuteStoredProcedure("usp_Form_DeleteFormMail", hash);
                AIM.Business.AimForm.Form_Mail deleteFromMail = (from linq_frommail in db.Form_Mails where linq_frommail.ID == Convert.ToInt32(e.CommandArgument) select linq_frommail).First<AIM.Business.AimForm.Form_Mail>();

                db.Form_Mails.DeleteOnSubmit(deleteFromMail);
                db.SubmitChanges();
            }

            Response.Redirect("EditMail.aspx?" + Request.QueryString);
        }

    }
}