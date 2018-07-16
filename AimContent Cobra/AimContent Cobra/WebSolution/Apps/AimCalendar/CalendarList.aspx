<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true"
    CodeBehind="CalendarList.aspx.cs" Inherits="AimCalendar.CalendarList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <table>
        <tr>
            <td>
                Kalender
            </td>
            <td>
                <asp:DropDownList ID="ddCalendarGroups" DataTextField="Name" DataValueField="CalendarGroupID"
                    runat="server" OnSelectedIndexChanged="ddCalendarGroups_Changed" AutoPostBack="true" />
                <asp:Label ID="lblCalendarName" runat="server" CssClass="title" Visible="true"></asp:Label>    
            </td>
        </tr>
        <tr>
            <td>
                Kategori
            </td>
            <td>
                <asp:DropDownList ID="ddTroop" DataTextField="Name" DataValueField="TroopID" runat="server"
                    OnSelectedIndexChanged="ddTroop_Changed" AutoPostBack="true" />&nbsp;<asp:HyperLink ID="hlNewAd" ToolTip="Skapa nytt event för valt lag" Text="&lt;img src='/Images/newobject.gif' alt='Skapa ny annons i vald grupp' /&gt;" runat="server" ></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadCalendar ID="rgCalendar" BackColor="Transparent" 
                HeaderStyle-Font-Bold="true" CalendarTableStyle-Width="275px" runat="server" 
                AutoPostBack="True" EnableMultiSelect="False" CalendarTableStyle-BorderWidth="0px"
                onselectionchanged="rgCalendar_SelectionChanged">                
            </telerik:RadCalendar>
            </td>
        </tr>
    </table>
    <div>
        <br />
        <asp:Label ID="lblrepHorizontalHeader" Text="Dagens händelser" CssClass="title" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblEmtyRepHorizontal" Text="Inga händelser." CssClass="normal" runat="server" ></asp:Label>
        <br />
        <asp:Repeater ID="repHorizontal" DataMember="CalendarID" runat="server" OnItemCommand="repHorizontal_OnItemCommand">
            <ItemTemplate>
                <div style="background-color: #FFFFFF;">                    
                    <table>
                        <tr>
                            <td align="left" style="width:125px;">
                                <a href='CalendarEditItem.aspx?calgroupID=<%# Eval("CalendarGroupID_FK") %>&calID=<%# Eval("CalendarID") %>&troopid=<%# Eval("TroopID_FK") %>&date=<%# Eval("Date") %>'>
                                    <%# Eval("Date")%>
                                </a>
                            </td>
                            <td align="left" style="width:300px;">
                                <span style="font-weight:bold;"><%# Eval("Titel")%></span>
                            </td>
                            <td align="left">
                                <a href='CalendarEditItem.aspx?calgroupID=<%# Eval("CalendarGroupID_FK") %>&calID=<%# Eval("CalendarID") %>&troopid=<%# Eval("TroopID_FK") %>'>
                                    <img src='/Images/Edit.gif' alt='Redigera' />
                                </a>
                                <asp:LinkButton ID="lbtnrepHorizontalDelete" CommandName="delete" Text="&lt;img src='/Images/delete.gif' alt='Ta bort' /&gt;" OnClientClick="return confirm('Är du säker på att du vill ta bort informationen?');"
                                CommandArgument='<%# Eval("CalendarID") %>' runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" style="width:500px;">
                                <%# Eval("Description")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                <div style="height: 1px;  background-color: #000; font-size: 0px;"></div>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
