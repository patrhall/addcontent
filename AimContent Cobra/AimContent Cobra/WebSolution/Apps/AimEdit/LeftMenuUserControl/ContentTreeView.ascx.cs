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
using Telerik.Web.UI;
using System.Security.Principal;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Data.Linq;

namespace WebSolution.LeftMenuControl
{
    public partial class ContentTreeView : AIM.Base.Modules.UserControl.AimEdit
    {
        public ContentTreeView()
        {
            UserControlVirtualPath = "/Apps/AimEdit/LeftMenuUserControl/ContentTreeView.ascx";
        }

        protected override void Page_Load()
        {
            if (RadTreeView1.IsEmpty)
            {
                initLanguage();
                GenerateTreeView();
            }
        }

        private void initLanguage()
        {
            List<AIM.Business.AimEdit.usp_GetLanguagesResult> languages = db.usp_GetLanguages(config.siteID, null).ToList<AIM.Business.AimEdit.usp_GetLanguagesResult>();

            if (languages.Count == 0)
            {
                ddLanguage.Visible = false;
                return;
            }

            string moduleimagespath = "/Images/flags/";

            ddLanguage.Items.Clear();

            RadComboBoxItem moduleitem;

            //Set height on dropdown, if to many languages, use default height
            if (languages.Count >= 4)
                ddLanguage.Height = Unit.Pixel(525);
            else
                ddLanguage.Height = Unit.Pixel((languages.Count * 55) + 55);

            //Add all languages
            foreach (AIM.Business.AimEdit.usp_GetLanguagesResult language in languages)
            {
                moduleitem = new RadComboBoxItem();
                moduleitem.Value = language.LangID.ToString();
                moduleitem.Text = Language.getValue("language" + language.LangID.ToString());

                //Flag image
                moduleitem.Attributes.Add("ImagePath", moduleimagespath + language.LangID.ToString() + ".gif");

                //select language
                if (language.LangID == menuLanguageId)
                    moduleitem.Selected = true;
                else
                    moduleitem.Selected = false;

                ddLanguage.Items.Add(moduleitem);
            }

            ddLanguage.DataBind();
        }

        private void GenerateTreeView()
        {
            var collObjects = (from o in db.Objects
                               where
                               o.SiteID == config.siteID &&
                               o.LangID == menuLanguageId &&
                               o.TreeStructure != null
                               orderby o.Sortorder
                               select new
                               {
                                   o,
                                   o.TreeStructure
                               });

            List<ObjectTreeStructure> tree = new List<ObjectTreeStructure>();

            foreach (var obj in collObjects)
            {
                ObjectTreeStructure item = new ObjectTreeStructure();
                item.Content = obj.o;
                item.ParentId = obj.TreeStructure.ParentObjectID;
                tree.Add(item);
            }

            var rootobject = tree.Single(p => !p.ParentId.HasValue).Content;

            RadTreeNode node = CreateNode(Language.getValue("rootobject"), true, rootobject.ObjectID.ToString());

            if (!config.useAdminRoles || db.AdminRoles.Any(p => p.AdminID == config.adminuserid && p.Role.RoleName.ToLower() == AIM.Base.Globals.Configuration.SuperAdminRole))
                node.ContextMenuID = "rContextMenuRoot";
            else
                node.ContextMenuID = "notused";

            RadTreeView1.Nodes.Add(node);

            RecursivelyPopulate(tree, node, rootobject.ObjectID);


            addStaticPuffs();

            var adminRole = (from o in db.AdminRoles
                             where o.AdminID == config.adminuserid
                             select o).First();

            // Only Superusers may see the TrashNode
            if (adminRole != null && adminRole.RoleID == 1)
                addTrashNode();

            addContextTreeMenu();
        }

        private void addStaticPuffs()
        {
            var puffs = (from o in db.Objects
                         where
                            o.SiteID == config.siteID &&
                            o.LangID == menuLanguageId &&
                            o.Puff != null
                         select new
                         {
                             o.ObjectID,
                             o.Title
                         }).ToList();

            if (puffs.Count == 0)
                return;

            //Root node for widgets
            RadTreeNode puffRootNode = new RadTreeNode();
            puffRootNode.Text = "<img src='/Images/widget.png' alt='' style='vertical-align:middle;' /> " + Language.getValue("rootWidgetNodeText");
            puffRootNode.Value = "-1";
            puffRootNode.EnableContextMenu = false;
            puffRootNode.Expanded = true;

            RadTreeView1.Nodes.Add(puffRootNode);



            foreach (var puff in puffs)
            {
                RadTreeNode puffNode = new RadTreeNode();
                puffNode.Text = puff.Title;
                puffNode.NavigateUrl = "/Apps/AimEdit/EditText.aspx?oID=" + puff.ObjectID;
                puffNode.Value = "-1";
                puffNode.EnableContextMenu = false;

                puffRootNode.Nodes.Add(puffNode);
            }
        }

        private void addTrashNode()
        {
            //Add a link to trash page
            RadTreeNode trashNode = new RadTreeNode();
            string trashNodeText = Language.getValue("trashNodeText");
            trashNode.Text = "<img src='/Images/delete.gif' alt='" + trashNodeText + "' style='vertical-align:middle;' /> " + trashNodeText;
            trashNode.NavigateUrl = "/Apps/AimEdit/TrashCan.aspx";
            trashNode.Value = "-1";
            trashNode.EnableContextMenu = false;

            RadTreeView1.Nodes.Add(trashNode);
        }

        private void addContextTreeMenu()
        {

            RadTreeView1.ContextMenus.Add(new Telerik.Web.UI.RadTreeViewContextMenu());
            RadTreeView1.ContextMenus.Add(new RadTreeViewContextMenu());
            RadTreeView1.ContextMenus.Add(new RadTreeViewContextMenu());
            RadTreeView1.ContextMenus[0].LoadContentFile("/App_Data/Language/" + Language.getLanguageName() + "/Apps/AimEdit/ContextTreeView/ContextMenuMain.xml");
            RadTreeView1.ContextMenus[0].ID = "rContextMenuMain";
            RadTreeView1.ContextMenus[1].LoadContentFile("/App_Data/Language/" + Language.getLanguageName() + "/Apps/AimEdit/ContextTreeView/ContextMenuRoot.xml");
            RadTreeView1.ContextMenus[1].ID = "rContextMenuRoot";
            RadTreeView1.ContextMenus[2].LoadContentFile("/App_Data/Language/" + Language.getLanguageName() + "/Apps/AimEdit/ContextTreeView/ContextMenuStartPage.xml");
            RadTreeView1.ContextMenus[2].ID = "rContextMenuStartPage";
        }

        private void RecursivelyPopulate(List<ObjectTreeStructure> tree, RadTreeNode parentNode, int parentId)
        {
            foreach (var childRow in tree.Where(p => p.ParentId.HasValue && p.ParentId.Value == parentId))
            {
                var objectItem = childRow.Content;

                RadTreeNode childNode = CreateNode(objectItem.Title, true, objectItem.ObjectID.ToString());

                if (config.useAdminRoles && !db.AdminRoles.Any(p => p.AdminID == config.adminuserid && p.Role.RoleName.ToLower() == AIM.Base.Globals.Configuration.SuperAdminRole))
                    childNode.ContextMenuID = "notused";
                else if (objectItem.ObjectID != config.startpageID)
                    childNode.ContextMenuID = "rContextMenuMain";
                else
                    childNode.ContextMenuID = "rContextMenuStartPage";

                childNode.NavigateUrl = getNodeLink(objectItem);
                parentNode.Nodes.Add(childNode);

                RecursivelyPopulate(tree, childNode, objectItem.ObjectID);

                //Set the selected node
                int objectID = 0;
                if (Request["oID"] != null)
                    if (!String.IsNullOrEmpty(Request["oID"].ToString()))
                        if (int.TryParse(Request["oID"].ToString(), out objectID))
                            if (objectID.ToString() == childNode.Value)
                            {
                                childNode.Selected = true;
                            }
            }
        }

        /// <summary>
        /// Created a node in the tree
        /// </summary>
        /// <param name="text"></param>
        /// <param name="expanded"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private RadTreeNode CreateNode(string text, bool expanded, string id)
        {
            RadTreeNode node = new RadTreeNode();
            node.Text = text;
            node.Expanded = true;
            node.Value = id;

            AIM.Business.AimEdit.usp_GetObjectResult siteObject = db.usp_GetObject(config.siteID, int.Parse(id)).First<AIM.Business.AimEdit.usp_GetObjectResult>();


            if (siteObject.PublishFrom != null && siteObject.PublishTo != null)
            {
                if ((DateTime.Now >= siteObject.PublishFrom && DateTime.Now <= siteObject.PublishTo) || (siteObject.PublishFrom == null && siteObject.PublishTo == null))
                    node.CssClass = "published";
                else
                    node.CssClass = "outdated";
            }

            return node;
        }

        /// <summary>
        /// Move nodes around by drag and drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadTree1_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            RadTreeNode sourceNode = e.SourceDragNode;
            RadTreeNode destNode = e.DestDragNode;

            RadTreeViewDropPosition dropPosition = e.DropPosition;
            if (destNode != null && destNode.Value != "-1")
            {
                if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                {
                    PerformDragAndDrop(dropPosition, sourceNode, destNode);
                }
                else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                {
                    //There is child nodes
                    foreach (RadTreeNode node in sourceNode.TreeView.SelectedNodes)
                    {
                        if (!PerformDragAndDrop(dropPosition, node, destNode))
                            break;
                    }
                }

                destNode.Expanded = true;
                sourceNode.TreeView.UnselectAllNodes();
            }
        }

        private bool PerformDragAndDrop(RadTreeViewDropPosition dropPosition, RadTreeNode sourceNode, RadTreeNode destNode)
        {
            switch (dropPosition)
            {
                case RadTreeViewDropPosition.Over:
                    // child
                    if (!sourceNode.IsAncestorOf(destNode))
                    {
                        sourceNode.Owner.Nodes.Remove(sourceNode);
                        destNode.Nodes.Add(sourceNode);
                        NodeDragAndDrop_SaveInDb(sourceNode);
                    }
                    break;
            }

            return true;
        }

        private void NodeDragAndDrop_SaveInDb(RadTreeNode movedNode)
        {
            RadTreeNode newParent = (RadTreeNode)movedNode.Parent;
            db.usp_EditTree(Convert.ToInt32(movedNode.Value), Convert.ToInt32(newParent.Value), null);
        }


        protected void RadTreeView1_NodeEdit(object sender, RadTreeNodeEditEventArgs e)
        {
            RadTreeNode nodeEdited = e.Node;
            nodeEdited.Text = e.Text;

            AIM.Business.AimEdit.Object theObject = (from linq_obj in db.Objects where linq_obj.ObjectID == int.Parse(nodeEdited.Value) select linq_obj).First<AIM.Business.AimEdit.Object>();
            theObject.Title = e.Text;

            db.SubmitChanges();
        }

        protected void RadTreeView1_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            switch (e.MenuItem.Value)
            {
                case "new":
                    NodeHandling_Create(e.Node);
                    break;
                case "moveup":
                    NodeHandling_MoveUp(e.Node, e.Node.Owner.Nodes);
                    break;
                case "movedown":
                    NodeHandling_MoveDown(e.Node, e.Node.Owner.Nodes);
                    break;
                case "changeObjectType":
                    NodeHandling_ChangeObjectType(e.Node);
                    break;
                case "erase":
                    NodeHandling_Erase(e.Node);
                    break;
                case "widgets":
                    NodeHandling_Widgets(e.Node);
                    break;
            }
        }

        private void NodeHandling_Widgets(RadTreeNode clickedNode)
        {
            //Redirect to widget admin
            Response.Redirect("/Apps/AimEdit/EditWidgets.aspx?oID=" + clickedNode.Value);
        }

        /// <summary>
        /// Redirect to change objecttype page
        /// </summary>
        /// <param name="clickedNode"></param>
        private void NodeHandling_ChangeObjectType(RadTreeNode clickedNode)
        {            
            //Redirect to change objecttypepage
            Response.Redirect(getAimContentUrl(Convert.ToInt32(clickedNode.Value),0));
        }

        /// <summary>
        /// Context menu create handler
        /// </summary>
        /// <param name="clickedNode"></param>
        private void NodeHandling_Create(RadTreeNode clickedNode)
        {
            string objectKey = AimResources.StringFunctions.GenerateRandomLowerCaseString(15);

            AIM.Business.AimEdit.usp_SaveObjectInTreeResult saveResult = db.usp_SaveObjectInTree(Language.getValue("newpage"), String.Empty, DateTime.Now, 0, 0, 1, config.siteID, menuLanguageId, int.Parse(clickedNode.Value), null, objectKey).First<AIM.Business.AimEdit.usp_SaveObjectInTreeResult>();

            //Get id...
            int ID = (from linq_object in db.Objects where linq_object.SiteID == config.siteID orderby linq_object.ObjectID descending select linq_object.ObjectID).Take(1).First<int>();

            db.usp_EditTree(ID, int.Parse(clickedNode.Value), null);

            RadTreeNode newNode = new RadTreeNode();
            newNode.Text = Language.getValue("newpage");
            newNode.ContextMenuID = "rContextMenuMain";
            newNode.Value = ID.ToString();

            newNode.NavigateUrl = "/Apps/AimEdit/EditObject.aspx?oID=" + ID;
            clickedNode.Nodes.Add(newNode);
        }

        /// <summary>
        /// Context menu move node down handler
        /// </summary>
        private void NodeHandling_MoveDown(RadTreeNode clickedNode, RadTreeNodeCollection nodesInTreeview)
        {
            RadTreeNode node = clickedNode;

            RadTreeNode last = RadTreeView1.Nodes[RadTreeView1.Nodes.Count - 1];
            int index = 0;
            if (node != last && node != nodesInTreeview[nodesInTreeview.Count - 1])
            {
                foreach (RadTreeNode current in nodesInTreeview)
                {
                    if (current.Value == node.Value)
                    {
                        node.Remove();
                        break;
                    }
                    index++;
                }

                nodesInTreeview.Insert(index + 1, node);
                int sortpos = 1;

                //Nu skriver vi trädets sortering till databasen
                foreach (RadTreeNode thenode in nodesInTreeview)
                {
                    db.usp_EditObjectPosition(int.Parse(thenode.Value), sortpos);
                    sortpos++;
                }
            }
        }

        /// <summary>
        /// Context menu move node up
        /// </summary>
        private void NodeHandling_MoveUp(RadTreeNode clickedNode, RadTreeNodeCollection nodesInTreeview)
        {
            RadTreeNode node = clickedNode;

            int index = 0;

            if (node != RadTreeView1.Nodes[0] && node != nodesInTreeview[0])
            {
                foreach (RadTreeNode current in nodesInTreeview)
                {
                    if (current.Next.Value == node.Value)
                    {
                        node.Remove();
                        break;
                    }
                    index++;
                }

                nodesInTreeview.Insert(index, node);

                int sortpos = 1;

                //Nu skriver vi trädets sortering till databasen
                foreach (RadTreeNode thenode in nodesInTreeview)
                {
                    db.usp_EditObjectPosition(int.Parse(thenode.Value), sortpos);
                    sortpos++;
                }
            }
        }

        /// <summary>
        /// Context menu erase node
        /// </summary>
        private void NodeHandling_Erase(RadTreeNode clickedNode)
        {
            RadTreeNode node = clickedNode;

            //Remove all childnodes
            List<string> childNodes = GetAllChildNodesId(node);

            for (int i = 0; i < childNodes.Count; i++)
            {
                db.usp_DeleteObjectRoles(int.Parse(childNodes[i]));
                db.usp_ObjectTrash_Set(int.Parse(childNodes[i]), 1);
            }

            //Remove the node
            db.usp_DeleteObjectRoles(int.Parse(clickedNode.Value));
            db.usp_ObjectTrash_Set(int.Parse(clickedNode.Value), 1);

            //Remove from treeview
            clickedNode.Remove();
        }

        protected void ddLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            menuLanguageId = int.Parse(ddLanguage.SelectedValue);

            RadTreeView1.Nodes.Clear();
            GenerateTreeView();
        }

        private List<string> GetAllChildNodesId(RadTreeNode parentNode)
        {
            List<string> nodeList = new List<string>();

            foreach (RadTreeNode node in parentNode.Nodes)
            {
                nodeList.Add(node.Value);
                GetAllChildNodesId(node);
            }

            return nodeList;
        }

        private string getNodeLink(AIM.Business.AimEdit.Object row)
        {
            return getAimContentUrl(row.ObjectID,row.ObjectTypeID.Value);
        }

        /// <summary>
        /// Language choosed for menu
        /// </summary>
        private int menuLanguageId
        {
            get
            {
                if (Session["WebSolution_LeftMenuControl_ContentTreeView"] != null)
                    return int.Parse(Session["WebSolution_LeftMenuControl_ContentTreeView"].ToString());
                else
                {
                    Session["WebSolution_LeftMenuControl_ContentTreeView"] = db.usp_GetObject(config.siteID, config.startpageID).First<AIM.Business.AimEdit.usp_GetObjectResult>().LangID;
                    return int.Parse(Session["WebSolution_LeftMenuControl_ContentTreeView"].ToString());
                }
            }

            set
            {
                Session["WebSolution_LeftMenuControl_ContentTreeView"] = value;
            }
        }



        private string getAimContentUrl(int objectId, int objectTypeId)
        {
            string url = String.Empty;

            try
            {
                if (!AdministratorAuthentication.HasAccessPermission(objectId))
                    return "/Apps/AimEdit/NotAllowed.aspx";

                //Get objecttype for object
                var objectType = (from linq_objecttype in db.ObjectTypes
                                  where linq_objecttype.ObjectTypeID == objectTypeId
                                  select linq_objecttype).Single();

                url = objectType.AimContentURL;

                //Add $-replacements here
                url = url.Replace("$ObjectID$", objectId.ToString());
            }
            catch
            {
                try
                {
                    // should not go here but to ease the error message
                    url = "/Apps/AimEdit/EditObject.aspx?oID=" + objectId;
                }
                catch { }
            }

            return url;
        }

        private class ObjectTreeStructure
        {
            public AIM.Business.AimEdit.Object Content { get; set; }
            public int? ParentId { get; set; }
            public int SortOrder { get; set; }
        }
    }
}