<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditCategory.aspx.cs" Inherits="Admin_AddEditCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%@ Register Src="../Controls/FileUploader.ascx" TagName="FileUploader" TagPrefix="mb" %>
<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS.Web.UI" TagPrefix="asp" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Manage Category</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="ManageCategories.aspx"><span>Categories</span></a></li>
                <li><a href="AddEditArticle.aspx"><span>New Article</span></a></li>
                <li><a href="AddEditCategory.aspx"><span>New Category</span></a></li>
            </ul>
        </div>
    </div>
    <div id="sectiontitle">
        <h1>
            <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit a Category" /></h1>
    </div>
    <div id="ContentBody">
        <table cellspacing="1" cellpadding="1" width="540" align="center" border="0">
            <tr>
                <td>
                    Category ID
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltlCategoryID"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    Title
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="formField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Importance
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtImportance" CssClass="formField"></asp:TextBox>
                    <asp:NumericUpDownExtender ID="txtImportance_NumericUpDownExtender" runat="server"
                        Maximum="100" Minimum="0" TargetControlID="txtImportance" Width="50" Step="5">
                    </asp:NumericUpDownExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Description
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="formField" TextMode="MultiLine"
                        Rows="3" Columns="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Image Url
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtImageUrl" CssClass="formField"></asp:TextBox>&nbsp;<asp:Image
                        runat="server" ID="iLogo"></asp:Image>
                    <br />
                    <asp:FileUpload ID="fuImageURL" runat="server" />
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
                    Added By
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltlAddedBy"></asp:Literal>
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
                <td>
                    Updated By
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltlUpdatedBy"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    Active
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbActive"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <mb:FileUploader ID="FileUploader1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table cellspacing="0" cellpadding="0" width="500" align="center" border="0">
                        <tr>
                            <td align="center" style="height: 16px">
                                <asp:Button ID="cmdCancel" runat="server" BorderStyle="None" Text="Cancel" />
                            </td>
                            <td align="center" style="height: 16px">
                                <asp:Button ID="cmdDelete" Visible="False" runat="server" BorderStyle="None" CausesValidation="False"
                                    Text="Delete" />
                            </td>
                            <td align="center" style="height: 16px">
                                <asp:Button ID="cmdUpdate" runat="server" BorderStyle="None" Text="Update" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
