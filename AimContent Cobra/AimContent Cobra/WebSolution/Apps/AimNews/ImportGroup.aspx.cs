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
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AimNews
{
    public partial class ImportGroup : AIM.Base.Modules.Page.AimNews
    {
        int groupID;
        List<string> groupusers = new List<string>();
        protected override void Page_Load()
        {
            initLanguage();
            aExcelLink.HRef = config.siteurl_config + config.customerpath + "templates/importTemplate.xls";//"http://publicutv.aimnews.se/seniornetaimnews_data/templates/importTemplate.xls";
           
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
           
            string fileName = "";
            groupID = Convert.ToInt32(Request["ID"].ToString());

            foreach (Telerik.Web.UI.UploadedFile file in ruImport.UploadedFiles)
            {
                groupusers = (from linq_users in db.AimNews_Users
                                           where linq_users.AimNewsID == AimNewsID
                                           from linq_groupusers in db.AimNews_GroupUsers
                                           where linq_groupusers.GroupID == groupID && linq_users.ID == linq_groupusers.UserID
                                           select linq_users.Email).ToList<string>();

                fileName = file.FileName.Substring(file.FileName.LastIndexOf(@"\") + 1);
                fileName = fileName.Replace(" ", "_");
                fileName = fileName.Replace("å", "a");
                fileName = fileName.Replace("ä", "a");
                fileName = fileName.Replace("ö", "o");
                string pathOrig = Server.MapPath(config.documentfolder_config + "/");                
                
                string fullName = pathOrig + fileName;

                try
                {                    
                    file.SaveAs(fullName, true);
                }
                catch 
                {
                    Response.Redirect("EditGroup.aspx?ID=" + groupID + "&S=3");                    
                }
                string datasource = fullName;                
                DataSet dsImport = CreateDataSource(datasource);

                foreach (DataRow dr in dsImport.Tables[0].Rows)
                {

                    if (!isMailDouble(dr["Email"].ToString()) && dr["Email"].ToString() != "")
                    {
                        groupusers.Add(dr["Email"].ToString());

                        AIM.Business.AimNews.AimNews_User newUser = new AIM.Business.AimNews.AimNews_User();
                        
                        newUser.AimNewsID = AimNewsID;
                        newUser.FirstName = dr["Firstname"].ToString();
                        newUser.LastName = dr["Lastname"].ToString();
                        
                        newUser.Email = dr["Email"].ToString();
                        //newUser.Email = Regex.Replace(dr["Email"].ToString(), @"\u00A0", " ").Trim();
                        
                        newUser.Company = dr["Company"].ToString();
                        newUser.Telephone = dr["Telephone"].ToString();
                        newUser.Mobile = dr["Mobile"].ToString();
                        newUser.Fax = dr["Fax"].ToString();
                        newUser.Address = dr["Address"].ToString();
                        newUser.Postal = dr["Postal"].ToString();
                        newUser.City = dr["City"].ToString();
                        newUser.Country = dr["Country"].ToString();
                        newUser.Orgnr = dr["Orgnr"].ToString();
                        db.AimNews_Users.InsertOnSubmit(newUser);
                        try
                        {
                            db.SubmitChanges();

                            saveUserInGroup(newUser.ID, groupID);
                        }
                        catch
                        {                
                            continue;                            
                        }
                    }
                    
                }
                
                Response.Redirect("EditGroup.aspx?ID=" + groupID + "&S=2");
            }

            Response.Redirect("EditGroup.aspx?ID=" + groupID + "&S=3");              
        }
        protected void btnTemplate_Click(object sender, EventArgs e)
        {
           Response.Write("<script type=\"text/javascript\">window.open('Excel.aspx');</script>");
        }
        protected void ibBack_OnClick(object sender, System.EventArgs e)
        {
            if (Request["ID"] != null)
                Response.Redirect("EditGroup.aspx?ID=" + Request["ID"].ToString());
            else
                Response.Redirect("Groups.aspx");
        }

        private void initLanguage()
        {
            lblTemplateText.Text = Language.getValue("templateText");
            btnImport.Text = Language.getValue("importbuttonText");
            ruImport.Localization.Select = Language.getValue("selectbuttonText");  
        }
       
        private DataSet CreateDataSource(string source)
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=" + source + "; Jet OLEDB:Engine Type=5;" +
                "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter myCommand = new OleDbDataAdapter(" SELECT * FROM [Blad1$] ", strConn);
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet);
            return myDataSet;
        }
        private void saveUserInGroup(int userID, int groupID)
        {
            AIM.Business.AimNews.AimNews_GroupUser newGroupUser = new AIM.Business.AimNews.AimNews_GroupUser();

            newGroupUser.GroupID = groupID;
            newGroupUser.UserID = userID;
            db.AimNews_GroupUsers.InsertOnSubmit(newGroupUser);

            db.SubmitChanges();
        }

        private bool isMailDouble(string Email)
        {
            if (groupusers.Contains(Email))
                return true;
            else
                return false;
        }
    }
}
