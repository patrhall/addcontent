using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Linq;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace AimEdit
{
    public partial class TrashCan : AIM.Base.Modules.Page.AimEdit
    {
        protected override void Page_Load()
        {
            setLanguage();

            if (!IsPostBack)
                fillGrid();
        }

        private void setLanguage()
        {
            gvTrash.EmptyDataText = Language.getValue("gvTrash_EmptyDataText");
            gvTrash.Columns[0].HeaderText = Language.getValue("gvTrash_Title");
            gvTrash.Columns[1].HeaderText = Language.getValue("gvTrash_ObjectType");
            gvTrash.Columns[2].HeaderText = Language.getValue("gvTrash_TrashedDate");

            ButtonField bf = (ButtonField)gvTrash.Columns[3];
            bf.Text = Language.getValue("gvTrash_MoveToMenu");

            bf = (ButtonField)gvTrash.Columns[4];
            bf.Text = Language.getValue("gvTrash_delete");
        }

        /// <summary>
        /// Fill trashed object for site
        /// </summary>
        private void fillGrid()
        {            
            gvTrash.DataSource = db.usp_GetTrashedObjects(config.siteID);
            gvTrash.DataBind();
        }
        
        protected void gvTrash_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int objectID = Convert.ToInt32(gvTrash.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

            if (e.CommandName == "GET")
            {                
                //Put into tree                
                db.usp_EditTree(objectID, config.rootID, null);
                db.usp_ObjectTrash_Set(objectID, 0);                

                Response.Redirect("/Apps/AimEdit/TrashCan.aspx");
            }
            else if (e.CommandName == "ERASE")
            {
                try
                {
                    AIM.Business.AimEdit.usp_GetObjectResult siteObject = db.usp_GetObject(null, objectID).First<AIM.Business.AimEdit.usp_GetObjectResult>();

                    switch (siteObject.ObjectTypeID.Value)
                    {
                        case 4: //Kommentarer
                            db.usp_DeleteAllMessages(objectID);
                            break;
                        case 6://Forum
                            List<AIM.Business.AimEdit.usp_Forum_GetAllThreadsResult> threads = db.usp_Forum_GetAllThreads(objectID).ToList < AIM.Business.AimEdit.usp_Forum_GetAllThreadsResult>();
                            
                            //Delete all thread messages
                            foreach(AIM.Business.AimEdit.usp_Forum_GetAllThreadsResult thread in threads)
                                db.usp_Forum_DeleteAllMessages(thread.ThreadID);

                            db.usp_Forum_DeleteAllThreads(objectID);
                            break;
                        case 7: //Kolla om kalender och ta bort alla kategorier och events
                            db.usp_Calendar_getAllEventCategories(objectID);
                            db.usp_Calendar_DeleteAllEvents(objectID);
                            break;
                    }

                    db.usp_DeleteObject(objectID);                    
                }
                catch { }

                Response.Redirect("/Apps/AimEdit/TrashCan.aspx");
            }
        }
    }

}