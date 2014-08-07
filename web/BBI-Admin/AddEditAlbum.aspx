<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditAlbum.aspx.cs" Inherits="Admin_AddEditAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <div class="sectiontitle">
                    <h1>
                        <asp:Literal runat="server" ID="ltlStatus" Text="Create or Edit an Album" /></h1>
                </div>
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
                <div runat="server" id="dAlbumForm">
                    <table id="Table2" cellspacing="0" cellpadding="0" width="400" align="center" border="0">
                        <tr>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table cellspacing="0" cellpadding="0" align="center" border="0">
                                    <tr>
                                        <td align="left">
                                            <font size="2">Name</font>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAlbumName" runat="server" CssClass="formField" Width="90%"></asp:TextBox><span
                                                class="RequiredField">*</span>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAlbumName"
                                                Display="Dynamic" ErrorMessage="You must enter an Album Name"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <font size="2">Description</font>
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:TextBox ID="txtAlbumDesc" runat="server" TextMode="MultiLine" Rows="5" Columns="50"
                                                CssClass="formField"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <font size="2">Parent Album</font>
                                        </td>
                                        <td colspan="2" align="left">
                                            <asp:DropDownList ID="ddlAlbums" CssClass="formStyleDropDown" DataTextField="AlbumName"
                                                DataValueField="AlbumId" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 36px">
                                            <table width="95%" align="center" border="0" cellspacing="1" cellpadding="1">
                                                <tr>
                                                    <td align="center" style="height: 20px">
                                                        &nbsp;<asp:HyperLink ID="hlnkCancel" runat="server" CssClass="AdminButton">Cancel</asp:HyperLink>
                                                    </td>
                                                    <td align="center" style="height: 20px">
                                                        <asp:HyperLink ID="hlNewPicture" runat="server" CssClass="AdminButton">Add New Picture</asp:HyperLink>
                                                    </td>
                                                    <td align="center" style="height: 20px">
                                                        <asp:HyperLink ID="hlnkViewAlbums" runat="server" CssClass="AdminButton">View All Albums</asp:HyperLink>
                                                    </td>
                                                    <td align="center" style="height: 20px">
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="AdminButton">Delete Album</asp:LinkButton>
                                                    </td>
                                                    <td align="center" style="height: 20px">
                                                        <asp:LinkButton ID="lbSubmit" runat="server" CssClass="AdminButton">Submit</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3">
                                            <asp:Button ID="Button1" runat="server" Text="Update Order"></asp:Button>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:ListView ID="lvPictures" runat="server" GroupItemCount="5" GroupPlaceholderID="groupPlaceHolder"
                                                ItemPlaceholderID="itemPlaceHolder">
                                                <LayoutTemplate>
                                                    <table id="tblPictures" runat="server" cellspacing="0" cellpadding="2">
                                                        <tr runat="server" id="groupPlaceholder" />
                                                    </table>
                                                </LayoutTemplate>
                                                <GroupTemplate>
                                                    <tr runat="server" id="ContactsRow" style="background-color: #FFFFFF">
                                                        <td runat="server" id="itemPlaceholder" />
                                                    </tr>
                                                </GroupTemplate>
                                                <ItemTemplate>
                                                    <td>
                                                        <div style="height: 90px; width: 85px;">
                                                            <a href="AddEditPhoto.aspx?albumid=<%#Eval("AlbumId") %>&pictureid=<%#Eval("PictureId") %>">
                                                                <img runat="server" id="iThumb" border="0" width="50" /></a>
                                                        </div>
                                                        <div>
                                                            <asp:Label EnableViewState="false" runat="server" ID='lblTitle' CssClass="PhotoTitle"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox ID="txtOrder" runat="server" Columns="3" CssClass="formField"></asp:TextBox>
                                                        </div>
                                                        <div>
                                                            <asp:ImageButton ImageAlign="Middle" ID="btnPicture" AlternateText="Delete This Picture?"
                                                                CommandName="DeletePicture" ImageUrl="~/images/delete.gif" CausesValidation="False"
                                                                runat="server" CommandArgument='<%# Eval("PictureId") %>' />
                                                        </div>
                                                    </td>
                                                </ItemTemplate>
                                                <ItemSeparatorTemplate>
                                                    <td id="Td1" runat="server" style="border-right: 1px solid #00C0C0">
                                                        &nbsp;
                                                    </td>
                                                </ItemSeparatorTemplate>
                                            </asp:ListView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3">
                                            <asp:Button ID="btnOrderUpdate1" runat="server" Text="Update Order"></asp:Button>
                                        </td>
                                        <td>
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
</asp:Content>
