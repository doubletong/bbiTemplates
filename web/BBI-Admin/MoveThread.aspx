<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="MoveThread.aspx.cs" Inherits="Admin_MoveThread" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Move Thread</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManageForums.aspx"><span>Manage Forums</span></a></li>
                        <li><a href="AddEditForum.aspx"><span>New Forum</span></a></li>
                        <li><a href="ManageUnapprovedPosts.aspx"><span>Manage Unapproved Post</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="sectiontitle">
                    <h1>
                        <asp:Literal runat="server" ID="ltlStatus" Text="" /></h1>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="sectiontitle">
                    <asp:Literal runat="server" ID="lblPageTitle" Text="Move Forum Thread" /></div>
                <p>
                </p>
                Move thread
                <asp:Label runat="server" ID="lblThreadTitle" Font-Bold="True" /><br />
                from forum
                <asp:Label runat="server" ID="lblForumTitle" Font-Bold="True" />
                to forum
                <asp:DropDownList ID="ddlForums" runat="server" DataTextField="Title" DataValueField="ForumID">
                </asp:DropDownList>
                <asp:Button ID="btnSubmit" runat="server" Text="OK" />
            </td>
        </tr>
    </table>
</asp:Content>
