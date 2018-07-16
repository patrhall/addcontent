<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Agency.aspx.cs" Inherits="AimBusiness.Customers.Mekpoint.Website.Agency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphDefault" runat="server">
    <asp:Panel ID="pnlOverview" runat="server">
        <asp:HiddenField ID="hfSearchFilter" runat="server" />
        <asp:HiddenField ID="hfLetterFilter" runat="server" />
        <asp:HiddenField ID="hfLatestFilter" runat="server" />
        <asp:TextBox ID="tbSearch" runat="server" />
        <asp:Button ID="btnSearchSubmit" runat="server" Text="Sök" OnClick="btnSearchSubmit_Click" />
        <asp:LinkButton ID="lbLatestFilter" runat="server" Text="Visa senaste" OnClick="lbLatestFilter_Click" />
        <br />
        <asp:Panel ID="pnlLetters" runat="server">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbLetter_Click" Text="A" /> -
            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbLetter_Click" Text="B" /> -
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lbLetter_Click" Text="C" /> -
            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lbLetter_Click" Text="D" /> -
            <asp:LinkButton ID="LinkButton5" runat="server" OnClick="lbLetter_Click" Text="E" /> -
            <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lbLetter_Click" Text="F" /> -
            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="lbLetter_Click" Text="G" /> -
            <asp:LinkButton ID="LinkButton8" runat="server" OnClick="lbLetter_Click" Text="H" /> -
            <asp:LinkButton ID="LinkButton9" runat="server" OnClick="lbLetter_Click" Text="I" /> -
            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="lbLetter_Click" Text="J" /> -
            <asp:LinkButton ID="LinkButton11" runat="server" OnClick="lbLetter_Click" Text="K" /> -
            <asp:LinkButton ID="LinkButton12" runat="server" OnClick="lbLetter_Click" Text="L" /> -
            <asp:LinkButton ID="LinkButton13" runat="server" OnClick="lbLetter_Click" Text="M" /> -
            <asp:LinkButton ID="LinkButton14" runat="server" OnClick="lbLetter_Click" Text="N" /> -
            <asp:LinkButton ID="LinkButton15" runat="server" OnClick="lbLetter_Click" Text="O" /> -
            <asp:LinkButton ID="LinkButton16" runat="server" OnClick="lbLetter_Click" Text="P" /> -
            <asp:LinkButton ID="LinkButton17" runat="server" OnClick="lbLetter_Click" Text="Q" /> -
            <asp:LinkButton ID="LinkButton18" runat="server" OnClick="lbLetter_Click" Text="R" /> -
            <asp:LinkButton ID="LinkButton19" runat="server" OnClick="lbLetter_Click" Text="S" /> -
            <asp:LinkButton ID="LinkButton20" runat="server" OnClick="lbLetter_Click" Text="T" /> -
            <asp:LinkButton ID="LinkButton21" runat="server" OnClick="lbLetter_Click" Text="U" /> -
            <asp:LinkButton ID="LinkButton22" runat="server" OnClick="lbLetter_Click" Text="V" /> -
            <asp:LinkButton ID="LinkButton23" runat="server" OnClick="lbLetter_Click" Text="W" /> -
            <asp:LinkButton ID="LinkButton24" runat="server" OnClick="lbLetter_Click" Text="X" /> -
            <asp:LinkButton ID="LinkButton25" runat="server" OnClick="lbLetter_Click" Text="Y" /> -
            <asp:LinkButton ID="LinkButton26" runat="server" OnClick="lbLetter_Click" Text="Z" /> -
            <asp:LinkButton ID="LinkButton27" runat="server" OnClick="lbLetter_Click" Text="Å" /> -
            <asp:LinkButton ID="LinkButton28" runat="server" OnClick="lbLetter_Click" Text="Ä" /> -
            <asp:LinkButton ID="LinkButton29" runat="server" OnClick="lbLetter_Click" Text="Ö" />
        </asp:Panel>
        <rad:RadGrid ID="rgAgency" runat="server" 
                     Width="100%" 
                     OnNeedDataSource="rgAgency_NeedDataSource"                 
                     OnItemDataBound="rgAgency_ItemDataBound"
                     OnItemCreated="rgAgency_ItemCreated"
                     OnItemCommand="rgAgency_ItemCommand"
                     AllowSorting="true" 
                     AllowPaging="true" 
                     AllowRowSelect="false"
                     GridLines="None" 
                     PageSize="30"
                     AutoGenerateColumns="false">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id">                    
                <CommandItemTemplate>
                    <asp:LinkButton ID="lbNew" runat="server" CommandName="newitem" />
                </CommandItemTemplate>
                <Columns>
                    <rad:GridBoundColumn DataField="Name" HeaderText="Namn" />                          
                    <rad:GridBoundColumn DataField="Country" HeaderText="Land" />
                    <rad:GridBoundColumn DataField="Company" HeaderText="Url" />              
                    <rad:GridButtonColumn ButtonType="ImageButton" ImageUrl="/Images/edit.gif" CommandName="edititem" >                    
                        <HeaderStyle Width="20px" />
                    </rad:GridButtonColumn>
                    <rad:GridButtonColumn ButtonType="ImageButton" ConfirmText="Är du säker?" ImageUrl="/Images/delete.gif" CommandName="deleteitem" >
                        <HeaderStyle Width="20px" />
                    </rad:GridButtonColumn>
                </Columns>
            </MasterTableView>        
        </rad:RadGrid>
    </asp:Panel>

    <asp:Panel ID="pnlDetail" runat="server">
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:Button ID="btnBack" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />
        <br /><br />
        Namn:<br />
        <asp:TextBox ID="tbName" runat="server" />
        <br /><br />Land:<br />
        <asp:TextBox ID="tbCountry" runat="server" />
        <br /><br />Länk:<br />
        <asp:TextBox ID="tbUrl" runat="server" />
        <br /><br />Företag:<br />
        <asp:TextBox ID="tbUrlText" runat="server" />
        <br /><br />Beskrivning:<br />
        <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="50" />
        <br /><br />
        <asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" />
    </asp:Panel>
</asp:Content>