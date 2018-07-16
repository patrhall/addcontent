<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TreeView.ascx.cs" Inherits="RadControls_AimTemplates_TreeView" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadTreeView.Net2" %>

<!--AimTemplate TreeView-->
<rad:RadTreeView
            ID="treeview" 
            OnNodeContextClick="ContextClicked"
            OnNodeClick="NodeClicked"                                        
            Runat="server"
            Skin="AIM"
            ImagesBaseDir="~/RadControls/TreeView/Img/Outlook/"
            BeforeClientContextClick="ContextMenuClick"
            Width="100%"
            AllowNodeEditing="False"
            OnNodeEdit="HandleNodeEdit"
            OnNodeDrop="HandleDrop"
            DragAndDrop="true"
            
            />
<!--/AimTemplate TreeView-->