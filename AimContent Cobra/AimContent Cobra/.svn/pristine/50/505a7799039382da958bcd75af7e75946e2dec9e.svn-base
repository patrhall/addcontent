<%@ Page Language="C#" MasterPageFile="../../Site.Master" AutoEventWireup="true"
    CodeBehind="Ads.aspx.cs" Inherits="AimAds.Ads" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <div>
        Annonsgrupper<br />
        <rad:RadComboBox ID="rcbAdGroups" Width="300px" runat="server" DataTextField="Name"
            DataValueField="GroupID" AutoPostBack="true" OnSelectedIndexChanged="rcbAdGroups_Changed" />
    </div>
    <div>
        <p>
            <asp:HyperLink ID="hlNewAd" runat="server"><img src='/Images/newobject.gif' alt='Skapa ny annons i vald grupp' /> Skapa ny annons i grupp</asp:HyperLink>
        </p>
        <asp:Repeater ID="repHorizontal" DataMember="ID" runat="server" OnItemCommand="repHorizontal_OnItemCommand">
            <ItemTemplate>
                <div style="background-color: #FFFFFF; width: 320px;padding:5px;">
                    <div style="float: left; width: 150px;">
                        <p style="font-weight: bold; font-size: 13px; margin:0; padding: 0;">
                            <%# Eval("Title") %></p>
                        <asp:LinkButton ID="lbtnrepHorizontal" CommandName="up" Text="Flytta upp" CommandArgument='<%# Eval("ID") %>'
                            runat="server"></asp:LinkButton><br />
                        <asp:LinkButton ID="lbtnrepHorizontalDown" CommandName="down" Text="Flytta ner" CommandArgument='<%# Eval("ID") %>'
                            runat="server"></asp:LinkButton>
                        <p>
                            <a title='<%# Eval("Title") %>' href='EditAd.aspx?GroupID=<%# Eval("AimAds_Group_FK") %>&ADID=<%# Eval("ID") %>'>
                                <img src='/Images/Edit.gif' alt='Redigera' />
                            </a>
                            <asp:LinkButton ID="lbtnrepHorizontalDelete" CommandName="delete" Text="&lt;img src='/Images/delete.gif' alt='Ta bort' /&gt;"
                                CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('Är du säker på att du vill ta bort annonsen?');"></asp:LinkButton>
                        </p>
                    </div>
                    <div style="float: right; width: 160px;">
                        <img style="max-width: 150px; max-height: 150px; border: solid 1px black; background-color: #88A0B8;"
                            src='<%# imgPath %><%# Eval("ImageFilename") %>' alt='<%# Eval("ImageFilename") %>'
                            title='<%# Eval("Href") %>' />
                    </div>
                    <div style="clear: both; margin-bottom: 7px;">
                    </div>
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div style="background-color: #EEEEEE; width: 320px;padding:5px;">
                    <div style="float: left; width: 150px;">
                        <p style="font-weight: bold; font-size: 13px; margin:0; padding: 0;">
                            <%# Eval("Title") %></p>
                        <asp:LinkButton ID="lbtnrepHorizontal" CommandName="up" Text="Flytta upp" CommandArgument='<%# Eval("ID") %>'
                            runat="server"></asp:LinkButton><br />
                        <asp:LinkButton ID="lbtnrepHorizontalDown" CommandName="down" Text="Flytta ner" CommandArgument='<%# Eval("ID") %>'
                            runat="server"></asp:LinkButton>
                        <p>
                            <a title='<%# Eval("Title") %>' href='EditAd.aspx?GroupID=<%# Eval("AimAds_Group_FK") %>&ADID=<%# Eval("ID") %>'>
                                <img src='/Images/Edit.gif' alt='Redigera' />
                            </a>
                            <asp:LinkButton ID="lbtnrepHorizontalDelete" CommandName="delete" Text="&lt;img src='/Images/delete.gif' alt='Ta bort' /&gt;"
                                CommandArgument='<%# Eval("ID") %>' runat="server" OnClientClick="return confirm('Är du säker på att du vill ta bort annonsen?');"></asp:LinkButton>
                        </p>
                    </div>
                    <div style="float: right; width: 160px;">
                        <img style="max-width: 150px; max-height: 150px; border: solid 1px black; background-color: #88A0B8;"
                            src='<%# imgPath %><%# Eval("ImageFilename") %>' alt='<%# Eval("ImageFilename") %>'
                            title='<%# Eval("Href") %>' />
                    </div>
                    <div style="clear: both; margin-bottom: 7px;">
                    </div>
                </div>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
