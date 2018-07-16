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

namespace WebSolution
{
    public partial class _Default : AIM.Base.PageBase
    {       
        protected override void Page_Load()
        {
            string content = Language.getValue("content");

            foreach (string ads in content.Split(new char[] { ';' }))
            {
                try
                {
                    string[] adContent = ads.Split(new char[] { '$' });

                    TableRow tr = new TableRow();                    

                    TableCell tc = new TableCell();
                    tc.Text = "<img src='/Images/Startpage/" + adContent[0] + "' alt='AimContent Module' width=\"185px\" height=\"34px\" /><br /><br />";
                    tc.VerticalAlign = VerticalAlign.Top;                    

                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Width = Unit.Pixel(400);
                    tc.Text = adContent[1] + "<br /><br />";
                    tc.VerticalAlign = VerticalAlign.Middle;

                    tr.Cells.Add(tc);

                    tblContent.Rows.Add(tr);
                }
                catch { Response.Write(ads); }
            }
        }
    }
}
