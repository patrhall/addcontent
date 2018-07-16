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
using System.Globalization;

namespace WebSolution
{
    
    public partial class SiteStyleSetting : AIM.Base.Modules.Page.WebSolution
    {
        protected override void Page_Load()
        {
            setLanguage();

            if (!IsPostBack)
            {
                getStyles();
                lblsuccessfulSave.Visible = false;
                lblerrorSave.Visible = false;
                lblmsgNoDifference.Visible = false;
                lblerrorSave.Visible = false;
            }
            

           // Response.Write(lblOldCss.Text + "sdasfa");
        }

        private void setLanguage()
        {
            /*
            lblMetaHeadTitle.Text = Language.getValue("lblMetaHeadTitle");
            lblMetaHeadDescription.Text = Language.getValue("lblMetaHeadDescription");
            lblMetaTitle.Text = Language.getValue("lblMetaTitle");
            lblMetaKeywords.Text = Language.getValue("lblMetaKeywords");
            lblMetaDescription.Text = Language.getValue("lblMetaDescription");
            * //*/
            btnSave.Text = Language.getValue("btnSave");
            lblsuccessfulSave.Text = Language.getValue("lblsuccessfulSave");
            lblerrorSave.Text = Language.getValue("lblerrorSave");
            lblmsgNoDifference.Text = Language.getValue("msgNoDifference");
        }

        public void getStyles()
        {   
            string filename = Server.MapPath(config.customerpath + config.cssfile_config);
            
            if(System.IO.File.Exists(filename) == true)
            {
	            System.IO.StreamReader objReader;
                objReader = new System.IO.StreamReader(filename);

                tbCSSStructure.Text = objReader.ReadToEnd();
                lblOldCss.Text = tbCSSStructure.Text;
                
                objReader.Close();
            }else
            {
                tbCSSStructure.Text = "finns ingen fil.";
                
	            // messagebox.show("No such file " + filename);
            }//*/
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblOldCss.Text != tbCSSStructure.Text)
                {
                    string serverPathOrg = Server.MapPath(config.customerpath) + config.cssfile_config.Replace('/', '\\');
                    string serverPathNew = Server.MapPath(config.customerpath);

                    if (System.IO.Directory.Exists(serverPathNew + "css\\backup\\"))
                    {
                        serverPathNew += "css\\backup\\";
                    }
                    else
                    {
                        System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(serverPathNew + "css\\backup\\");
                        dir1.Create();

                        serverPathNew += "css\\backup\\";
                    }

                    serverPathNew += config.cssfile_config.Replace("css/", "").Replace('/', '\\');
                    serverPathNew = serverPathNew.Replace(".css", "") + DateTime.Now.ToString().Trim().Replace(" ", "_").Replace(":", "") + ".css";

                    System.IO.File.WriteAllText(serverPathOrg, tbCSSStructure.Text);
                    System.IO.File.WriteAllText(serverPathNew, lblOldCss.Text);//*/

                    lblOldCss.Text = tbCSSStructure.Text;
                    lblerrorSave.Visible = false;
                    lblmsgNoDifference.Visible = false;
                    lblsuccessfulSave.Visible = true;
                }
                else
                {
                    lblerrorSave.Visible = false;
                    lblsuccessfulSave.Visible = false;

                    lblmsgNoDifference.Visible = true;
                }
            }
            catch
            {
                lblmsgNoDifference.Visible = false;
                lblsuccessfulSave.Visible = false;
                lblerrorSave.Visible=true;
            }
        }
    }
}

//tbCSSStructure.Text = serverPathOrg + "\n" + serverPathNew;//Server.MapPath(config.customerpath) + config.cssfile_config + "\n" + Server.MapPath(config.customerpath) + "css/Style" + DateTime.Now.ToString().Trim() + ".css";
//tbCSSStructure.Text = lblOldCss.Text + "*";

/*
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(config.customerpath + "css/Style" + DateTime.Now.ToString().Trim() + ".css"));
                sw.Write(oldCss);
                sw.Close()//*/

//System.IO.File.Create(Server.MapPath(config.customerpath + "css/Style" + DateTime.Now.ToString().Trim() + ".css"));