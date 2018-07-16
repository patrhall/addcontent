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
using AIM.Business.AimForm;

/*
 * Ta bort svaren om något fält inte är ifyllt 
 */


namespace AimForm
{
    public partial class Form : AIM.Base.Modules.Page.AimForm
    {
        public int formID;
        public int questionID = 0;
        public int sortNo = 0;
        public int objectID = 0;
        public DataSet dsForm;
        public AIM.Business.AimForm.Form_Section Section;
        public List<AIM.Business.AimForm.Form_Section> listSections;
                

        protected override void Page_Load()
        {            
            config.GetConfig();            

            if (Request["ID"] != null)
                formID = Convert.ToInt32(Request["ID"]);
            else
                formID = 0;

            configureEditor();

            if (!IsPostBack)
            {
                if (formID != 0)
                {
                    fillEditor(formID);
                    //Används externalconnection
                    if (Convert.ToBoolean(Preference.getValue("aimform", "externalconnection", "used")))
                        initExternalConnectionPanel(formID);
                }
                
                
            }

            if (formID != 0)
            {
                fillQuestions(formID);
            }
            
        }

        private void initExternalConnectionPanel(int formID)
        {
            lblExternalConnectionTitle.Text = Language.getValue("lblExternalConnectionTitle");
            lblExternalConnectionDescription.Text = Language.getValue("lblExternalConnectionDescription");
            pnlExternalConnection.Visible = true;
            
            Form_Form theForm = (from f in db.Form_Forms
                                 where f.FormID == formID
                                 select f).Single<Form_Form>();

            List<Form_ExternalConnection> ExternalConnections = (from e in db.Form_ExternalConnections
                                                                 where e.Site_FK == config.siteID
                                                                 select e).ToList<Form_ExternalConnection>();

            cbExternalConnection.Checked = theForm.ExternalConnectionEnabled;
            ddlExternalConnections.DataSource = ExternalConnections;
            ddlExternalConnections.DataValueField = "ID";
            ddlExternalConnections.DataTextField = "Name";
            ddlExternalConnections.DataBind();

            ListItem li = new ListItem(Language.getValue("emptyListItem"), "0");
            ddlExternalConnections.Items.Insert(0, li);

            if (theForm.ExternalConnectionEnabled)
            {
                ddlExternalConnections.SelectedValue = theForm.ExternalConnection_FK.ToString();
                initExternalConnectionsProperties(theForm.ExternalConnection_FK.Value, formID);
            }
        }

        private void initExternalConnectionsProperties(int externalConnectionID, int FormID)
        {
            var Props = (from p in db.Form_ExternalConnections_Properties
                                                             where p.ExternalConnection_FK == externalConnectionID
                                                             orderby p.ID
                                                             select p);

            rptExternalConnectionsAttributes.DataSource = Props;
            rptExternalConnectionsAttributes.DataBind();


            //HtmlTable propTable = new HtmlTable();
            //int id = 0;
            //foreach (Form_ExternalConnections_Property prop in Props)
            //{
            //    Label lblPropTitle = new Label();
            //    lblPropTitle.Text = prop.PropertyName;

            //    HtmlTableRow propRow = new HtmlTableRow();
            //    HtmlTableCell propCell = new HtmlTableCell();
            //    propCell.InnerText = prop.PropertyName;
            //    propRow.Cells.Add(propCell);
            //    propTable.Rows.Add(propRow);

            //    propRow = new HtmlTableRow();
            //    propCell = new HtmlTableCell();
            //    TextBox tbPropValue = new TextBox();
            //    tbPropValue.Text = GetExternalConnectionPropertyValue(prop.ID, FormID);
            //    tbPropValue.ID = "tbPropValue_" + id;
            //    propCell.Controls.Add(tbPropValue);
            //    propRow.Cells.Add(propCell);
            //    propTable.Rows.Add(propRow);
            //    id++;
            //}
            //phExternalConnectionsAttributes.Controls.Add(propTable);
                        
        }

        protected void pung(object sender, RepeaterItemEventArgs e)
        {            
            var prop = (Form_ExternalConnections_Property)e.Item.DataItem;

            ((HiddenField)e.Item.FindControl("hfPropId")).Value = prop.ID.ToString();
            ((TextBox)e.Item.FindControl("tbPropertyValue")).Text = GetExternalConnectionPropertyValue(prop.ID, formID);
            ((Label)e.Item.FindControl("lblPropertyName")).Text = prop.PropertyName;
        }

        private string GetExternalConnectionPropertyValue(int PropertyID, int FormID)
        {
            var Values = from v in db.Form_ExternalConnections_Values
                         where v.ExternalConnection_Property_FK == PropertyID && v.Form_Form_FK == FormID
                         select v;
            foreach (Form_ExternalConnections_Value propvalue in Values)
                return propvalue.Value;
            return "";
        }
        protected void UpdateProperties(object sender, EventArgs e)
        {
            initExternalConnectionsProperties(int.Parse(ddlExternalConnections.SelectedValue), formID);
        }

        protected void fillQuestions(int formID)
        {
            int lastSort = 0;
            int firstsort = 0;

            List<AIM.Business.AimForm.Form_Question> questionList =
            (from o in db.Form_Questions
             where o.FormID == formID && o.Deleted != true
             orderby o.Sort ascending
             select o).ToList<AIM.Business.AimForm.Form_Question>();

            lastSort = questionList.Count();
            try
            {
                firstsort = questionList.Min(w => w.Sort).Value;
            }
            catch
            {
                firstsort = 1;
            }

            foreach (AIM.Business.AimForm.Form_Question q in questionList)
            {
                questionID = q.QuestionID;

                int qType = q.QuestionType;

                switch (qType)
                {

                    case 1:
                    case 2:
                    case 5:
                    case 6:
                    case 7:
                        GenerateQuestions(q);
                        if (qType == 6 || qType == 7)
                            GenerateFreeTextAlternatives(questionID);

                        //Generate sort buttons                        
                        Table tblButton = new Table();
                        TableRow tblRow = new TableRow();
                        TableCell tblCell1 = new TableCell();
                        TableCell tblCell2 = new TableCell();

                        if (q.Sort.ToString() != "")
                            sortNo = q.Sort.Value;

                        Label lblSpace = new Label();

                        if (sortNo != firstsort)
                        {
                            //Button btnMoveUp = new Button();
                            //btnMoveUp.Text = "Flytta upp";
                            //btnMoveUp.CssClass = "inputs";
                            //btnMoveUp.ID = "btnMoveup" + questionID.ToString();
                            //btnMoveUp.Click += new EventHandler(btnMoveUp_Click);
                            //phForm.Controls.Add(btnMoveUp);

                            ImageButton imgbtnMoveUp = new ImageButton();
                            //btnMoveUp.Text = "Flytta upp";
                            imgbtnMoveUp.ImageUrl = "/Images/arrow_up.png";
                            imgbtnMoveUp.CssClass = "inputs";
                            imgbtnMoveUp.ID = "btnMoveup" + questionID.ToString();
                            imgbtnMoveUp.Click += new ImageClickEventHandler(btnMoveUp_Click);                            
                            phForm.Controls.Add(imgbtnMoveUp);

                        }
                        else
                            lblSpace.Text = "<span style=\"width:147px;\"></span>";


                        if (sortNo != lastSort)
                        {
                            ImageButton btnMoveDown = new ImageButton();
                            btnMoveDown.ImageUrl = "/Images/arrow_down.png";
                            btnMoveDown.CssClass = "inputs";
                            btnMoveDown.ID = "btnMoveDown" + questionID.ToString();
                            btnMoveDown.Click += new ImageClickEventHandler(btnMoveDown_Click);
                            phForm.Controls.Add(btnMoveDown);
                        }
                        else
                            lblSpace.Text = "<span style=\"width:142px;\"></span>";

                        if ((sortNo != firstsort && sortNo != lastSort))
                            lblSpace.Text = "<span style=\"width:63px;\"></span>";

                        phForm.Controls.Add(lblSpace);

                        //Create Edit & Delete button
                        ImageButton btnEdit = new ImageButton();
                        btnEdit.ImageUrl = "/Images/Edit.gif";
                        btnEdit.CssClass = "inputs";
                        btnEdit.ID = "btnEdit" + questionID.ToString();
                        btnEdit.Click += new ImageClickEventHandler(btnEdit_Click);
                        phForm.Controls.Add(btnEdit);

                        Label lblSpace2 = new Label();
                        lblSpace2.Text = "<span style=\"width:30px;\"></span>";
                        phForm.Controls.Add(lblSpace2);

                        ImageButton btnDelete = new ImageButton();
                        btnDelete.ImageUrl = "/Images/delete.gif";
                        btnDelete.CssClass = "inputs";
                        btnDelete.ID = "btnDelete" + questionID.ToString();

                        btnDelete.Attributes.Add("onclick", "javascript:return confirm('Är du säker på att du vill ta bort frågan och ALLA svar till denna fråga?')");
                        btnDelete.Click += new ImageClickEventHandler(btnDelete_Click);

                        phForm.Controls.Add(btnDelete);
                        break;

                    /* case 2:
                         GenerateQuestions(questionID, qType);
                         //Generate sort buttons
                         dsQuestion = AIM.DBcom.ComTrans.GetQuestion(questionID);

                         if (dsQuestion.Tables[0].Rows[0]["Sort"].ToString() != "")
                             sortNo = Convert.ToInt32(dsQuestion.Tables[0].Rows[0]["Sort"]);

                         if (sortNo != 1)
                         {
                             Button btnMoveUp = new Button();
                             btnMoveUp.Text = "Flytta upp";
                             btnMoveUp.CssClass = "inputs";
                             btnMoveUp.ID = "btnMoveup" + questionID.ToString();
                             btnMoveUp.Click += new EventHandler(btnMoveUp_Click);
                             phForm.Controls.Add(btnMoveUp);
                         }

                         if (sortNo != lastSort)
                         {
                             Button btnMoveDown = new Button();
                             btnMoveDown.Text = "Flytta ner";
                             btnMoveDown.CssClass = "inputs";
                             btnMoveDown.ID = "btnMoveDown" + questionID.ToString();
                             btnMoveDown.Click += new EventHandler(btnMoveDown_Click);
                             phForm.Controls.Add(btnMoveDown);
                         }

                         break;*/

                    case 3:
                    case 4:
                        GenerateFreeTextQuestions(q);
                        //Generate sort buttons

                        if (q.Sort.ToString() != "")
                            sortNo = q.Sort.Value;

                        Label lblSpace3 = new Label();

                        if (sortNo != firstsort)
                        {
                            ImageButton btnMoveUp = new ImageButton();
                            btnMoveUp.ImageUrl = "/Images/arrow_up.png";
                            btnMoveUp.CssClass = "inputs";
                            btnMoveUp.ID = "btnMoveup" + questionID.ToString();
                            btnMoveUp.Click += new ImageClickEventHandler(btnMoveUp_Click);
                            phForm.Controls.Add(btnMoveUp);
                        }
                        else
                            lblSpace3.Text = "<span style=\"width:147px;\"></span>";

                        if (sortNo != lastSort)
                        {
                            ImageButton btnMoveDown = new ImageButton();
                            btnMoveDown.ImageUrl = "/Images/arrow_down.png";
                            btnMoveDown.CssClass = "inputs";
                            btnMoveDown.ID = "btnMoveDown" + questionID.ToString();
                            btnMoveDown.Click += new ImageClickEventHandler(btnMoveDown_Click);
                            phForm.Controls.Add(btnMoveDown);
                        }
                        else
                            lblSpace3.Text = "<span style=\"width:142px;\"></span>";

                        if ((sortNo != firstsort && sortNo != lastSort))
                            lblSpace3.Text = "<span style=\"width:63px;\"></span>";

                        phForm.Controls.Add(lblSpace3);

                        //Create Edit & Delete button
                        ImageButton btnEdit2 = new ImageButton();
                        btnEdit2.ImageUrl = "/Images/edit.gif";
                        btnEdit2.CssClass = "inputs";
                        btnEdit2.ID = "btnEdit" + questionID.ToString();
                        btnEdit2.Click += new ImageClickEventHandler(btnEdit_Click);
                        phForm.Controls.Add(btnEdit2);

                        Label lblSpace4 = new Label();
                        lblSpace4.Text = "<span style=\"width:30px;\"></span>";
                        phForm.Controls.Add(lblSpace4);

                        ImageButton btnDelete2 = new ImageButton();
                        btnDelete2.ImageUrl = "/Images/delete.gif";
                        btnDelete2.CssClass = "inputs";
                        btnDelete2.ID = "btnDelete" + questionID.ToString();
                        btnDelete2.Click += new ImageClickEventHandler(btnDelete_Click);
                        btnDelete2.Attributes.Add("onclick", "javascript:return confirm('Är du säker på att du vill ta bort frågan och ALLA svar till denna fråga')");
                        phForm.Controls.Add(btnDelete2);
                        break;
                }

                Label lblLine = new Label();
                lblLine.Text = "<hr>";
                lblLine.CssClass = "normal";
                phForm.Controls.Add(lblLine);
            }
        }

        protected void fillEditor(int formID)
        {
            AIM.Business.AimForm.Form_Form theItems = (from linq_form in db.Form_Forms where linq_form.FormID == formID && linq_form.SiteID == config.siteID select linq_form).First<AIM.Business.AimForm.Form_Form>();

            //DataRow drForm = dsForm.Tables[0].Rows[0];
            tbFormName.Text = theItems.FormName;

            editor1.Content = theItems.FormText;

            if (theItems.ObjectID.HasValue)
            {
                objectID = theItems.ObjectID.Value;
            }
        }

        protected void configureEditor()
        {
            AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref editor1, config);

            //editor1.Editable = rad;
            //editor1.HasPermission = true;
            //editor1.Scheme = config.editorscheme_config;


            //string[] images = new string[1];
            //string[] documents = new string[1];
            //string[] flash = new string[1];
            //string[] media = new string[1];
            //string[] imagefilters = new string[1];
            //string[] documentfilters = new string[1];
            //string[] templates = new string[1];
            //string[] css = new string[1];

            //imagefilters[0] = config.imagefilters_config;
            //documentfilters[0] = config.documentfilters_config;
            //templates[0] = config.templatefolder_config;
            //css[0] = config.customerpath + config.cssfile_editor_config;

            //if (config.secureEditing != null && config.secureEditing != "")
            //{
            //    documents[0] = config.secureEditingDocument;
            //    images[0] = config.secureEditingImages;
            //    flash[0] = config.secureEditingFlash;
            //    media[0] = config.secureEditingMedia;
            //}
            //else
            //{
            //    documents[0] = config.documentfolder_config;
            //    images[0] = config.imagefolder_config;
            //    flash[0] = config.flashfolder_config;
            //    media[0] = config.mediafolder_config;
            //}

            //this.editor1.ImagesPaths = images;
            //this.editor1.UploadImagesPaths = images;
            //this.editor1.DeleteImagesPaths = images;
            //this.editor1.DocumentsPaths = documents;
            //this.editor1.UploadDocumentsPaths = documents;
            //this.editor1.DeleteDocumentsPaths = documents;
            //this.editor1.TemplatePaths = templates;
            //this.editor1.UploadTemplatePaths = templates;
            //this.editor1.DeleteTemplatePaths = templates;

            //this.editor1.FlashPaths = flash;
            //this.editor1.UploadFlashPaths = flash;
            //this.editor1.DeleteFlashPaths = flash;

            //this.editor1.CssFiles = css;
            //this.editor1.AllowThumbGeneration = true;
            ////this.editor1.UseSession = true;
            //this.editor1.MaxDocumentSize = config.maxuploadbytesize;
            //this.editor1.MaxFlashSize = config.maxuploadbytesize;
            //this.editor1.MaxImageSize = config.maxuploadbytesize;
            //this.editor1.MaxMediaSize = config.maxuploadbytesize;
            //this.editor1.MaxTemplateSize = config.maxuploadbytesize;

            ////Strippa bort absolut sökväg
            //this.editor1.StripAbsoluteAnchorPaths = true;
            //this.editor1.StripAbsoluteImagesPaths = true;
            //this.editor1.ImagesPathToStrip = config.imagespathstostrip;
            //this.editor1.AnchorPathToStrip = config.anchorpathstostrip;
        }

        #region Generate Questions

        //private void GenerateQuestions(int questionID, int qType)
        private void GenerateQuestions(AIM.Business.AimForm.Form_Question q)
        {
            //Write Question
            Label lblQuestion = new Label();

            string qText = "<br><b>" + q.QuestionText + "</b>";
            if (q.Required == "T")
                qText += " *<br>";
            else
                qText += "<br>";

            lblQuestion.Text = qText;
            lblQuestion.CssClass = "normal";
            phForm.Controls.Add(lblQuestion);

            RadioButtonList rblQuestion = new RadioButtonList();
            rblQuestion.CssClass = "normal";
            CheckBoxList cblQuestion = new CheckBoxList();
            cblQuestion.CssClass = "normal";

            //dsAlternatives = AIM.DBcom.ComTrans.GetAlternatives(questionID);

            List<AIM.Business.AimForm.Form_Alternative> alternativeList =
            (from o in db.Form_Alternatives
             where o.QuestionID == q.QuestionID
             select o).ToList<AIM.Business.AimForm.Form_Alternative>();

            int countAlternatives = alternativeList.Count();

            if (countAlternatives >= 7 && countAlternatives <= 12)
            {
                rblQuestion.RepeatColumns = 2;
                cblQuestion.RepeatColumns = 2;
            }
            else if (countAlternatives >= 13 && countAlternatives <= 18)
            {
                rblQuestion.RepeatColumns = 3;
                cblQuestion.RepeatColumns = 3;
            }
            else if (countAlternatives >= 19)
            {
                rblQuestion.RepeatColumns = 4;
                cblQuestion.RepeatColumns = 4;
            }

            //foreach (DataRow drA in dsAlternatives.Tables[0].Rows)
            foreach (AIM.Business.AimForm.Form_Alternative qAlt in alternativeList)
            {
                ListItem AltItem = new ListItem();
                AltItem.Text = qAlt.AlternativeText;
                AltItem.Value = qAlt.ID.ToString();

                if (q.QuestionType == 1 || q.QuestionType == 6)
                    rblQuestion.Items.Add(AltItem);
                else
                    cblQuestion.Items.Add(AltItem);
            }

            if (q.QuestionType == 1 || q.QuestionType == 6)
                phForm.Controls.Add(rblQuestion);
            else
                phForm.Controls.Add(cblQuestion);
        }

        private void GenerateFreeTextQuestions(AIM.Business.AimForm.Form_Question q)
        {
            try
            {
                //dsAlternatives = AIM.DBcom.ComTrans.GetFreeAlternatives(questionID);
                Table tblFree = new Table();
                TableRow tblRow = new TableRow();
                TableCell tblCell1 = new TableCell();
                TableCell tblCell2 = new TableCell();
                //Label lblFree = new Label();
                // lblFree.Text = drA["AlternativeText"].ToString();
                //lblFree.ID = drA["ID"].ToString();
                //lblFree.CssClass = "normal";
                //tblCell1.Controls.Add(lblFree);

                //Write Question
                Label lblQuestion = new Label();
                lblQuestion.Text = "<b>" + q.QuestionText + "</b><br>";
                lblQuestion.CssClass = "normal";
                tblCell1.Controls.Add(lblQuestion);
                tblRow.Controls.Add(tblCell1);

                Label lblStar = new Label();
                lblStar.Text = " *";
                lblStar.CssClass = "normal";

                //TextBox tbFree = new TextBox();
                //tbFree.CssClass = "FreeTextClass";

                //if (q.QuestionType == 4)
                //{
                //    tbFree.TextMode = TextBoxMode.MultiLine;
                //    tbFree.Rows = 3;
                //}
                //tblCell2.Controls.Add(tbFree);

                TextBox mumu = new TextBox();
                mumu.CssClass = "FreeTextClass";

                if (q.QuestionType == 4)
                {
                    mumu.TextMode = TextBoxMode.MultiLine;
                    mumu.Rows = 3;
                }
                tblCell2.Controls.Add(mumu);

                //check if answer required
                if (q.Required == "T")
                    tblCell2.Controls.Add(lblStar);

                tblRow.Controls.Add(tblCell2);
                tblFree.Rows.Add(tblRow);
                phForm.Controls.Add(tblFree);
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void GenerateFreeTextAlternatives(int questionID)
        {
            //dsAlternatives = AIM.DBcom.ComTrans.GetFreeAlternatives(questionID);
            Table tblFree = new Table();

            List<AIM.Business.AimForm.Form_Alternative> alternativeList =
           (from o in db.Form_Alternatives
            where o.QuestionID == questionID && o.AlternativeType == 3
            select o).ToList<AIM.Business.AimForm.Form_Alternative>();

            foreach (AIM.Business.AimForm.Form_Alternative qa in alternativeList)//drA
            {
                TableRow tblRow = new TableRow();

                TableCell tblCell1 = new TableCell();

                Label lblFree = new Label();
                lblFree.Text = qa.AlternativeText;
                lblFree.ID = qa.ID.ToString();
                lblFree.CssClass = "normal";

                tblCell1.Controls.Add(lblFree);
                tblRow.Controls.Add(tblCell1);

                TableCell tblCell2 = new TableCell();

                TextBox tbFree = new TextBox();
                tbFree.CssClass = "FreeTextClass";

                tblCell2.Controls.Add(tbFree);
                tblRow.Controls.Add(tblCell2);

                tblFree.Rows.Add(tblRow);
            }

            phForm.Controls.Add(tblFree);
        }

        private void GenerateCombAlternatives(int ChildQuestionID)
        {
            //dsAlternatives = AIM.DBcom.ComTrans.GetAlternatives(ChildQuestionID);
            //Table tblComb = new Table();
            ////Show Alternatives
            //TableRow tblCombRow = new TableRow();
            //TableCell tblMarginCell = new TableCell();
            //tblMarginCell.Text = "<div style=\"width:130px\">&nbsp;</div>";

            //tblMarginCell.CssClass = "tableClass";
            //tblCombRow.Controls.Add(tblMarginCell);

            //foreach (DataRow drA in dsAlternatives.Tables[0].Rows)
            //{
            //    TableCell tblCombCell = new TableCell();
            //    Label lblAlt = new Label();
            //    lblAlt.Text = "<div style=\"width:100px\">" + drA["AlternativeText"].ToString() + "</div>";
            //    lblAlt.CssClass = "normal";

            //    tblCombCell.Controls.Add(lblAlt);
            //    tblCombRow.Controls.Add(tblCombCell);
            //}

            //tblComb.Controls.Add(tblCombRow);
            //phForm.Controls.Add(tblComb);
        }

        private void GenerateCombQuestions(int questionID, int questionType)
        {
            //int countAlternatives = Convert.ToInt32(dsAlternatives.Tables[0].Rows.Count);
            //dsChildQuestions = AIM.DBcom.ComTrans.GetChildQuestions(questionID);

            //Table tblAComb = new Table();

            //foreach (DataRow drCQ in dsChildQuestions.Tables[0].Rows)
            //{

            //    TableRow tblCombQRow = new TableRow();
            //    Label lblCQ = new Label();
            //    TableCell tblCombQCell = new TableCell();


            //    lblCQ.Text = "<div style=\"width:125px\">" + drCQ["QuestionText"].ToString() + "</div><br>";
            //    lblCQ.CssClass = "fet";
            //    tblCombQCell.Controls.Add(lblCQ);
            //    tblCombQRow.Controls.Add(tblCombQCell);
            //    tblAComb.Controls.Add(tblCombQRow);
            //    //   tblCombQRow.Controls.Add(tblCombQCell);


            //    TableCell tblCombACell = new TableCell();
            //    CheckBoxList cbl = new CheckBoxList();
            //    RadioButtonList rbl = new RadioButtonList();

            //    rbl.RepeatColumns = countAlternatives;
            //    cbl.RepeatColumns = countAlternatives;
            //    //rbl.CssClass = "listClass";
            //    rbl.ID = drCQ["QuestionID"].ToString();
            //    cbl.ID = drCQ["QuestionID"].ToString();
            //    //rbl.Text = drCQ["QuestionText"].ToString(); 

            //    foreach (DataRow drA in dsAlternatives.Tables[0].Rows)
            //    {

            //        ListItem cqItem = new ListItem();
            //        cqItem.Value = drA["ID"].ToString();
            //        cqItem.Text = "<div style=\"width:100px\">&nbsp;</div>";
            //        rbl.Items.Add(cqItem);
            //        cbl.Items.Add(cqItem);
            //    }

            //    if (questionType == 4)
            //        tblCombACell.Controls.Add(rbl);
            //    else
            //        tblCombACell.Controls.Add(cbl);

            //    tblCombQRow.Controls.Add(tblCombACell);
            //    tblAComb.Controls.Add(tblCombQRow);

            //}

            //phForm.Controls.Add(tblAComb);
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            formID = SaveObject();

            Response.Redirect("EditQuestion.aspx?ID=" + formID);

        }
        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            formID = SaveObject();

            ImageButton btnEdit = (ImageButton)sender;
            string strID = btnEdit.ID;
            int ID = Convert.ToInt32(strID.Substring(7));


            Response.Redirect("EditQuestion.aspx?ID=" + formID + "&qID=" + ID);
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            int formID = SaveObject();

            //formID = Convert.ToInt32(Request["ID"]);

            ImageButton btnDel = (ImageButton)sender;
            string strID = btnDel.ID;
            int ID = Convert.ToInt32(strID.Substring(9));


            //DataSet dsQuestion = DBcom.ComTrans.GetQuestion(ID);
            AIM.Business.AimForm.Form_Question theQuestion = (from linq_question in db.Form_Questions where linq_question.QuestionID == ID select linq_question).First<AIM.Business.AimForm.Form_Question>();

            int questionType = theQuestion.QuestionType;
            formID = theQuestion.FormID.Value;

            //Change sort order
            bool after = false;

            if (formID != 0)
            {
                List<AIM.Business.AimForm.Form_Question> questionList =
                (from o in db.Form_Questions
                 where o.FormID == formID
                 orderby o.Sort ascending
                 select o).ToList<AIM.Business.AimForm.Form_Question>();

                foreach (AIM.Business.AimForm.Form_Question q in questionList)
                {
                    if (q.QuestionID == ID)
                    {
                        after = true;
                    }

                    //Ändra till Linq
                    if (after)
                    {
                        q.Sort = q.Sort - 1;
                    }//DBcom.ComTrans.SortQuestion(Convert.ToInt32(drQ["QuestionID"]), Convert.ToInt32(drQ["Sort"]) - 1);

                }
            }
            //if (questionType == 1 || questionType == 2)
            //{

            //100415 deletequestion
           // List<AIM.Business.AimForm.Form_Alternative> alternativeList =
           //(from o in db.Form_Alternatives
           // where o.QuestionID == theQuestion.QuestionID
           // select o).ToList<AIM.Business.AimForm.Form_Alternative>();

           // foreach (AIM.Business.AimForm.Form_Alternative alt in alternativeList)
           // {
           //     db.Form_Alternatives.DeleteOnSubmit(alt);
           //     db.SubmitChanges();
           // }

           // //DBcom.ComClass.ExecuteStoredProcedure("usp_Form_DeleteQuestionAlternatives", hash);

           // db.SubmitChanges();
            //}

            //db.Form_Questions.DeleteOnSubmit(theQuestion);


            List<AIM.Business.AimForm.Form_Answer> questionAnswers =
           (from o in db.Form_Answers
            where o.QuestionID == theQuestion.QuestionID
            select o).ToList<AIM.Business.AimForm.Form_Answer>();

            foreach (AIM.Business.AimForm.Form_Answer answ in questionAnswers)
            {
                answ.Deleted = true;
                answ.DeletedDate = DateTime.Now;
                db.SubmitChanges();
            }

            theQuestion.Deleted = true;
            theQuestion.DeletedDate = DateTime.Now;
            theQuestion.Sort = 0;

            db.SubmitChanges();
            //Hashtable hash = new Hashtable();
            //hash.Add("@QuestionID", ID);
            //DBcom.ComClass.ExecuteStoredProcedure("usp_Form_DeleteQuestion", hash);
            
            Response.Redirect("Form.aspx?ID=" + formID);
        }
        protected void btnMail_Click(object sender, EventArgs e)
        {
            formID = SaveObject();

            Response.Redirect("EditMail.aspx?ID=" + formID);
        }

        protected void editor1_Submit(object sender, System.EventArgs e)
        {
            formID = SaveObject();

            Page.RegisterClientScriptBlock("", "<script language='javascript'>if (alert('Sida sparad')){document.location=\"default.aspx\";}else{document.location=\"Form.aspx?ID=" + formID + "\";}</script>");

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveObject();
            if (Convert.ToBoolean(Preference.getValue("aimform", "externalconnection", "used")) && formID != 0 && ddlExternalConnections.SelectedValue != "0")
                SaveExternalConnection();

            Response.Redirect("Formlist.aspx");
        }

        private void SaveExternalConnection()
        {
            foreach (RepeaterItem item in rptExternalConnectionsAttributes.Items)
            {
                TextBox tbPropertyValue = (TextBox)item.FindControl("tbPropertyValue");
                HiddenField hfPropId = (HiddenField)item.FindControl("hfPropId");
                bool exists = false;
                int propID = int.Parse(hfPropId.Value);
                string value = tbPropertyValue.Text;

                var Values = from v in db.Form_ExternalConnections_Values
                            where v.ExternalConnection_Property_FK == propID && v.Form_Form_FK == formID
                            select v;

                foreach (Form_ExternalConnections_Value v in Values)
                {
                    v.Value = value;
                    
                    exists = true;
                }
                if (!exists)
                {
                    Form_ExternalConnections_Value v = new Form_ExternalConnections_Value();
                    v.Value = value;
                    v.Form_Form_FK = formID;
                    v.ExternalConnection_Property_FK = propID;
                    db.Form_ExternalConnections_Values.InsertOnSubmit(v);
                }
                db.SubmitChanges();

            }

            Form_Form theForm = (from f in db.Form_Forms
                                 where f.FormID == formID
                                 select f).Single<Form_Form>();
            theForm.ExternalConnectionEnabled = cbExternalConnection.Checked;
            theForm.ExternalConnection_FK = int.Parse(ddlExternalConnections.SelectedValue);


            db.SubmitChanges();
        }

        private void saveRoles()
        {
            int objectID = Convert.ToInt32(Request["ID"]);

            //AIM.DBcom.ComTrans.deleteObjectRoles(objectID);

            for (int i = 0; i < cblRoleProperty.Items.Count; i++)
            {
                if (cblRoleProperty.Items[i].Selected == true)
                {
                    string applicationName = config.applicationname;

                    //DataSet dsSiteRoleID = AIM.DBcom.ComTrans.getRoleID(cblRoleProperty.Items[i].Text.ToString(), applicationName);
                    //string siteRoleID = Convert.ToString(dsSiteRoleID.Tables[0].Rows[0]["RoleId"]);

                    //AIM.DBcom.ComTrans.saveObjectRoles(objectID, siteRoleID);
                    //Response.Write(siteRoleID);

                }
            }

        }

        private int SaveObject()
        {
            formID = Convert.ToInt32(Request["ID"]);

            AIM.Business.AimForm.Form_Form theForm;

            if (formID != 0)
            {
                theForm = (from linq_form in db.Form_Forms where linq_form.FormID == formID && linq_form.SiteID == config.siteID select linq_form).First<AIM.Business.AimForm.Form_Form>();
                //objectID = theForm.ObjectID.Value;
            }
            else
            {
                theForm = new AIM.Business.AimForm.Form_Form();
                db.Form_Forms.InsertOnSubmit(theForm);
            }

            theForm.SiteID = config.siteID;
            theForm.FormName = tbFormName.Text;
            theForm.FormText = editor1.Content;
            theForm.ObjectID = objectID;

            db.SubmitChanges();

            //if (formID != 0)
            //    DBcom.ComTrans.SaveForm(formID, siteID, tbFormName.Text, editor1.Content, objectID);
            //else
            //    formID = Convert.ToInt32(DBcom.ComTrans.SaveFormAndReturnID(siteID, tbFormName.Text, editor1.Content.Replace(Environment.NewLine, "<br />"), objectID));

            return theForm.FormID;
        }


        #region Sort
        protected void btnMoveUp_Click(object sender, ImageClickEventArgs e)
        {
            formID = SaveObject();

            int sortNo = 0;
            //int i = 0;
            int previousQuestionID = 0;
            // formID = Convert.ToInt32(Request["ID"]);
            //int PageIndex = Convert.ToInt32(Request["pID"]);

            ImageButton btnUp = (ImageButton)sender;
            string strID = btnUp.ID;
            int ID = Convert.ToInt32(strID.Substring(9));

            //dsQuestion = AIM.DBcom.ComTrans.GetQuestion(ID);
            //dsQuestions = AIM.DBcom.ComTrans.GetFormQuestions(formID);

            List<AIM.Business.AimForm.Form_Question> questionList =
            (from o in db.Form_Questions
             where o.FormID == formID && o.Deleted != true
             orderby o.Sort ascending
             select o).ToList<AIM.Business.AimForm.Form_Question>();

            bool gate = true;
            int sort = 0;

            foreach (AIM.Business.AimForm.Form_Question q in questionList)//drQ
            {
                if (q.QuestionID == ID)
                {
                    sortNo = q.Sort.Value;                    
                    q.Sort = sort;                   
                    gate = false;
                }
                else
                {
                    if (gate)
                    {
                        previousQuestionID = q.QuestionID;
                        sort = q.Sort.Value;
                    }
                }
            }
            //To Do: Vet ej om den kommer att uppdatera den ny
            AIM.Business.AimForm.Form_Question thepreviousQuestion = questionList.Find(p => p.QuestionID == previousQuestionID);//(from linq_question in db.Form_Questions where linq_question.QuestionID == previousQuestionID select linq_question).First<AIM.Business.AimForm.Form_Question>();
            thepreviousQuestion.Sort = sortNo;

            db.SubmitChanges();

            Response.Redirect("Form.aspx?ID=" + formID);
        }

        protected void btnMoveDown_Click(object sender, ImageClickEventArgs e)
        {

            formID = SaveObject();

            int sortNo = 0;
            //int i = 0;
            //int previousQuestionID = 0;
            //formID = Convert.ToInt32(Request["ID"]);
            //int PageIndex = Convert.ToInt32(Request["pID"]);
            
            List<AIM.Business.AimForm.Form_Question> questionList =
            (from o in db.Form_Questions
             where o.FormID == formID && o.Deleted != true
             orderby o.Sort ascending
             select o).ToList<AIM.Business.AimForm.Form_Question>();

            ImageButton btnDown = (ImageButton)sender;
            string strID = btnDown.ID;
            int ID = Convert.ToInt32(strID.Substring(11));
            bool moveNext = false;
            int sort = 1;
            foreach (AIM.Business.AimForm.Form_Question q in questionList)//drQ
            {
                if (q.QuestionID == ID)
                {
                    sortNo = q.Sort.Value;
                    //q.Sort = sortNo + 1;
                    moveNext = true;
                }
                else
                {
                    if (moveNext)
                    {
                        sort = q.Sort.Value;
                        q.Sort = sortNo;
                        sortNo = sort;
                        moveNext = false;
                    }
                }
            }

            AIM.Business.AimForm.Form_Question thepreviousQuestion = questionList.Find(p => p.QuestionID == ID);//(from linq_question in db.Form_Questions where linq_question.QuestionID == previousQuestionID select linq_question).First<AIM.Business.AimForm.Form_Question>();
            thepreviousQuestion.Sort = sortNo;

            db.SubmitChanges();
            Response.Redirect("Form.aspx?ID=" + formID);
        }

        protected void btnMoveUpSection_Click(object sender, EventArgs e)
        {

            formID = SaveObject();

            int sortNo = 0;
            //int i = 0;
            int previousSectionID = 0;
            int objectID = Convert.ToInt32(Request["ID"]);
            int PageIndex = Convert.ToInt32(Request["pID"]);

            Button btnUp = (Button)sender;
            string strID = btnUp.ID;

            int ID = Convert.ToInt32(strID.Substring(16));

            Section = (from linq_section in db.Form_Sections where linq_section.SectionID == ID select linq_section).First<AIM.Business.AimForm.Form_Section>();
            formID = Section.FormID.Value;

            listSections = (from o in db.Form_Sections
                            where o.FormID == formID
                            orderby o.Sort ascending
                            select o).ToList<AIM.Business.AimForm.Form_Section>();

            bool gate = true;

            foreach (AIM.Business.AimForm.Form_Section section in listSections)
            {
                if (section.SectionID == ID)
                {
                    sortNo = section.Sort.Value;
                    section.Sort = sortNo - 1;
                    gate = false;
                    //DBcom.ComTrans.SortSection(ID, sortNo - 1);
                    //DBcom.ComTrans.SortSection(previousSectionID, sortNo);
                }
                if (gate)
                    previousSectionID = section.SectionID;
                    
            }

            AIM.Business.AimForm.Form_Section thepreviousSection = listSections.Find(p => p.SectionID == previousSectionID);//(from linq_question in db.Form_Questions where linq_question.QuestionID == previousQuestionID select linq_question).First<AIM.Business.AimForm.Form_Question>();
            thepreviousSection.Sort = sortNo;

            db.SubmitChanges();

            //Generate_pages();
            Response.Redirect("SurveyPreview.aspx?ID=" + objectID);

        }

        protected void btnMoveDownSection_Click(object sender, EventArgs e)
        {

            formID = SaveObject();

            int sortNo = 0;
            int objectID = Convert.ToInt32(Request["ID"]);
            int PageIndex = Convert.ToInt32(Request["pID"]);
            bool moveNext = false;

            Button btnDown = (Button)sender;
            string strID = btnDown.ID;

            int ID = Convert.ToInt32(strID.Substring(18));

            Section = (from linq_section in db.Form_Sections where linq_section.SectionID == ID select linq_section).First<AIM.Business.AimForm.Form_Section>();
            formID = Section.FormID.Value;

            listSections = (from o in db.Form_Sections
                            where o.FormID == formID
                            orderby o.Sort ascending
                            select o).ToList<AIM.Business.AimForm.Form_Section>();

            foreach (AIM.Business.AimForm.Form_Section section in listSections)
            {
                //Response.Write(drS["SectionID"].ToString());
                if (moveNext)
                {
                    section.Sort = sortNo;
                    moveNext = false;
                }

                else if (section.SectionID == ID)
                {
                    sortNo = section.Sort.Value;
                    section.Sort = sortNo + 1;
                    moveNext = true;
                }
            }

            db.SubmitChanges();
            //Generate_pages();
            Response.Redirect("SurveyPreview.aspx?ID=" + objectID);
        }
        #endregion
    }
}
