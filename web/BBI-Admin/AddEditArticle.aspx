<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" 
CodeFile="AddEditArticle.aspx.cs" Inherits="Admin_AddEditArticle" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="../Controls/FileUploader.ascx" TagName="FileUploader" TagPrefix="mb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Manage Article</h1>
        </div>
        <div id="dAdminHeader">
            <ul>
                <li><a href="ManageArticles.aspx"><span>Articles</span></a></li>
                <li><a href="AddEditArticle.aspx"><span>New Article</span></a></li>
                <li><a href="AddEditCategory.aspx"><span>New Category</span></a></li>
            </ul>
        </div>
        <div id="sectiontitle">
            <h2>
                <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit an Article" /></h2>
        </div>
        <div id="ContentBody">
            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        ArticleID :
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlArticleID"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Title:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" Width="200" CssClass="formField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Abstract:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtAbstract" TextMode="MultiLine" Rows="3" Columns="40"
                            CssClass="formField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Keywords:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtKeywords" TextMode="MultiLine" Rows="2" Columns="20"
                            CssClass="formField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Body :
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <FCKeditorV2:FCKeditor ID="txtBody" runat="server" ToolbarSet="BBICMS" Height="400px"
                            Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Country:
                    </td>
                    <td>
                        <asp:CountryDropDownList ID="ddlCountry" runat="server" CssClass="formField">
                        </asp:CountryDropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        State :
                    </td>
                    <td>
                        <asp:StateDropDownList ID="ddlState" runat="server" CssClass="formField">
                        </asp:StateDropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        City :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCity" CssClass="formField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Release Date :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtReleaseDate" Width="70" CssClass="formField"></asp:TextBox>
                        <asp:Image runat="Server" ID="iReleaseDate" ImageUrl="~/images/Calendar.png" /><br />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReleaseDate"
                            PopupButtonID="iReleaseDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtReleaseDate"
                            Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txtReleaseDate" IsValidEmpty="True" EmptyValueMessage="Date is required"
                            InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic"
                            TooltipMessage="Input a Date" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Expire Date :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtExpireDate" Width="70" CssClass="formField"></asp:TextBox>
                        <asp:Image runat="Server" ID="iExpireDate" ImageUrl="~/images/Calendar.png" /><br />
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtExpireDate"
                            PopupButtonID="iExpireDate">
                        </asp:CalendarExtender>
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtExpireDate"
                            Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                        <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txtExpireDate" IsValidEmpty="True" EmptyValueMessage="Date is required"
                            InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic"
                            TooltipMessage="Input a Date" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Approved:
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="cbApproved"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Listed:
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="cbListed"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Comments Enabled:
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="cbCommentsEnabled"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Only For Members:
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="cbOnlyForMembers"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Categories:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategories" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        View Count :
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlViewCount"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Votes :
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlVotes"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Rating :
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlRating"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Added Date:
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlAddedDate"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Added By:
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlAddedBy"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Updated Date:
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlUpdatedDate"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Updated By:
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltlUpdateBy"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <table cellspacing="0" cellpadding="0" width="500" align="center" border="0">
                            <tr>
                                <td align="center" style="height: 16px">
                                    <button onclick="window.location.assign('ManageArticles.aspx');" value="">
                                        Cancel</button>
                                </td>
                                <td align="center" style="height: 16px">
                                    <asp:Button ID="cmdDelete" Visible="False" runat="server" BorderStyle="None" CausesValidation="False"
                                        Text="Delete" />
                                </td>
                                <td align="center" style="height: 16px">
                                    <asp:Button runat="server" ID="btnSubmit" Text="Submit" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <mb:FileUploader ID="FileUploader1" runat="server" />
        </div>
    </div>
</asp:Content>
