<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentTreeView.ascx.cs" Inherits="WebSolution.LeftMenuControl.ContentTreeView" %>


<script type="text/javascript" language="javascript"> 
    var clickCalledAfterRadconfirm = false;
    var lastClickedItem = null;                   
    
    function onClientContextMenuItemClicked(sender, args)
    {
        var menuItem = args.get_menuItem();
        var treeNode = args.get_node();

        switch(menuItem.get_text())
        {            
            case "Byt namn":
            case "Rename":
                treeNode.get_treeView()._startEdit(treeNode);
                break;            
            
        }
    }    
</script>               
    
    <rad:RadComboBox ID="ddLanguage" runat="server" Width="230px"  Skin="Vista"  
        MarkFirstMatch="false" HighlightTemplatedItems="True" ShowToggleImage="True" 
        AllowCustomText="false" EnableLoadOnDemand="false" AutoPostBack="true" 
        onselectedindexchanged="ddLanguage_SelectedIndexChanged">
        <ItemTemplate>
            <table class="ddLanguage_table">
                <tr>
                    <td class="ddLanguage_img">
                        <img src='<%# DataBinder.Eval(Container, "Attributes['ImagePath']") %>' alt="<%# DataBinder.Eval(Container, "Text") %>" />
                    </td>
                    <td class="ddLanguage_text">
                        <%# DataBinder.Eval(Container, "Text") %>
                    </td>
                </tr>                                                                                
            </table>
        </ItemTemplate>                    
    </rad:RadComboBox>

      
    <rad:RadTreeView 
        ID="RadTreeView1"                   
        Runat="server"                    
        Width="100%" 
        AllowNodeEditing="false"           
        DragAndDrop="true" 
        onnodedrop="RadTree1_NodeDrop" 
        EnableDragAndDrop="true" 
        EnableDragAndDropBetweenNodes="false"
        OnClientContextMenuItemClicked="onClientContextMenuItemClicked"
        oncontextmenuitemclick="RadTreeView1_ContextMenuItemClick"   
        OnNodeEdit="RadTreeView1_NodeEdit"                               
    >      
        
        <CollapseAnimation Type="OutQuint" Duration="100" />
        <ExpandAnimation Duration="100" />   
    </rad:RadTreeView>    