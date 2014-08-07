<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditPhoto.aspx.cs" Inherits="Admin_AddEditPhoto" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Photo Albums</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManageAlbums.aspx"><span>Albums</span></a></li>
                        <li><a href="AddEditAlbum.aspx"><span>New Album</span></a></li>
                        <li><a href="AddEditPhoto.aspx"><span>New Photo</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="sectiontitle">
                    <h3>
                        <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit a Photo" /></h3>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <table class="AlbumText" id="Table1" cellspacing="0" cellpadding="0" width="100%"
                        align="center" border="0">
                        <tr>
                            <td align="left" width="200">
                                Title
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="formField" Width="90%"></asp:TextBox><span
                                    class="RequiredField">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="You must enter a Title"></asp:RequiredFieldValidator>
                            </td>
                            <td valign="middle" align="center" width="120" rowspan="6">
                                <asp:Image ID="imgThumb" runat="server"></asp:Image>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Album
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlAlbums" runat="server">
                                </asp:DropDownList>
                                <span class="RequiredField">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="ddlAlbums" Display="Dynamic" ErrorMessage="You must select an Album"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Picture
                            </td>
                            <td align="left">
                                <asp:FileUpload ID="fPicture" runat="server" CssClass="formField" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Thumb Width
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtWidth" Width="50" />
                                <asp:NumericUpDownExtender ID="NUD1" runat="server" TargetControlID="txtWidth" Width="65"
                                    Maximum="750" Minimum="16" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Thumb Height
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtHeight" Width="50" />
                                <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="txtHeight"
                                    Width="65" Maximum="750" Minimum="16" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Order
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtOrder" Width="50" />
                                <asp:NumericUpDownExtender ID="NumericUpDownExtender2" runat="server" TargetControlID="txtOrder"
                                    Width="75" Minimum="0" Step="5" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Caption
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtCaption" runat="server" Columns="50" Rows="4" TextMode="MultiLine"
                                    CssClass="formField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Featured on the Home Page?
                            </td>
                            <td align="left" colspan="2">
                                <asp:RadioButton ID="rbHomePage" runat="server"></asp:RadioButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                        <tr>
                            <td align="center" style="height: 16px">
                                <asp:Button ID="cmdCancel" runat="server" BorderStyle="None" Text="Cancel" />
                            </td>
                            <td align="center" style="height: 16px">
                                <asp:Button ID="cmdDelete" Visible="False" runat="server" BorderStyle="None" CausesValidation="False"
                                    Text="Delete" OnClientClick="return confirm('Warning: This will delete the Pictures from the database.');" />
                            </td>
                            <td align="center" style="height: 16px">
                                <asp:Button ID="cmdUpdate" runat="server" BorderStyle="None" Text="Update" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
