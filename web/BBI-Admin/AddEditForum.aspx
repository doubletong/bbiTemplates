<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditForum.aspx.cs" Inherits="Admin_AddEditForum" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Forums</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManageForums.aspx"><span>Forums</span></a></li>
                        <li><a href="AddEditForum.aspx"><span>New Forum</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div runat="server" id="dEditForm" style="width: 90%;">
                    <div class="sectiontitle">
                        <h1>
                            <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit a Forum" /></h1>
                    </div>
                    <table cellspacing="1" cellpadding="1" width="97%" align="center" border="0">
                        <tr>
                            <td>
                                Forum ID
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlForumID"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Title
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="formField" Columns="60"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Moderated
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="cbModerated"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Importance
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtImportance" CssClass="formField"></asp:TextBox>
                                <asp:NumericUpDownExtender ID="txtImportance_NumericUpDownExtender" runat="server"
                                    Maximum="100" Minimum="0" TargetControlID="txtImportance" Width="55" Step="5">
                                </asp:NumericUpDownExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="formField" ID="txtDescription" TextMode="MultiLine"
                                    Rows="4" Columns="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Image
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
                                Active
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlActive"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Added Date
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltlAddedDate" />
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
                                <asp:Literal runat="server" ID="ltlUpdatedDate" />
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
                            <td align="center" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                    <tr>
                                        <td align="center" style="height: 16px">
                                            <asp:Button ID="cmdCancel" runat="server" BorderStyle="None" Text="Cancel" />
                                        </td>
                                        <td align="center" style="height: 16px">
                                            <asp:Button ID="cmdDelete" Visible="False" runat="server" BorderStyle="None" CausesValidation="False"
                                                Text="Delete" OnClientClick="return confirm('Warning: This will delete the Forum from the database.');" />
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
            </td>
        </tr>
    </table>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BehaviorID="RoundedCornersBehavior1"
        TargetControlID="dEditForm" Radius="6" BorderColor="Black" Color="White" Corners="All" />
</asp:Content>

