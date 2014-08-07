<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ArchivedNewsletters.aspx.cs" Inherits="Admin_ArchivedNewsletters" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Newsletters</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ArchivedNewsletters.aspx"><span>Newsletters</span></a></li>
                        <li><a href="ShowNewsletter.aspx"><span>New Newsletter</span></a></li>
                        <li><a href="SendNewsletter.aspx"><span>Send Newsletter</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="sectiontitle">
                    <h1>
                        <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit a Newsletter" /></h1>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

