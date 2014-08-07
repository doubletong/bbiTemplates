<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditSiteMap.aspx.cs" Inherits="Admin_AddEditSiteMap" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Manage Site Maps</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="ManageSiteMap.aspx"><span>Albums</span></a></li>
                <li><a href="AddEditSiteMap.aspx"><span>New Album</span></a></li>
            </ul>
        </div>
    </div>
    <div id="sectiontitle">
        <h1>
            <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit an Site Map" /></h1>
    </div>
    <div id="ContentBody">
    
        {Site Map Stuff Here}
    
    </div>
</asp:Content>

