<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditComment.aspx.cs" Inherits="Admin_AddEditComment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Comment</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManageComments.aspx"><span>Comments</span></a></li>
                        <li><a href="ManageArticles.aspx"><span>Articles</span></a></li>
                        <li><a href="AddEditArticle.aspx"><span>New Article</span></a></li>
                        <li><a href="ManageCategories.aspx"><span>Categories</span></a></li>
                        <li><a href="AddEditCategory.aspx"><span>New Category</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="sectiontitle">
                    <h1>
                        <asp:Literal runat="server" ID="ltlStatus" Text="Set Comment Aproval Status" /></h1>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" cellpadding="1" width="97%" align="center" border="0">
                    <tr>
                        <td width="165">
                            CommentID
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlCommentID"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Added Date
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlAddedDate"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Added By Email
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlAddedByEMail"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Added By IP
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlAddedByIP"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Article Title
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlArticleTitle"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Title
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Body
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlBody"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Commenter URL
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlCommenterURL"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Approved
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="cbApproved"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Updated Date
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltlUpdatedDate"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                <tr>
                                    <td align="center" style="height: 16px">
                                        <button onclick ="window.location.assign('ManageComments.aspx');" value="">Cancel</button>
                                    </td>
                                    <td align="center" style="height: 16px">
                                        <asp:Button ID="cmdDelete" runat="server" BorderStyle="None" CausesValidation="False"
                                            Text="Delete" OnClick ="cmdDelete_Click" />
                                    </td>
                                    <td align="center" style="height: 16px">
                                        <asp:Button ID="cmdUpdate" runat="server" BorderStyle="None" Text="Update" OnClick ="btnSubmit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
