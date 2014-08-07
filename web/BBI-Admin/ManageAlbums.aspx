<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageAlbums.aspx.cs" Inherits="Admin_ManageAlbums" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <table cellpadding="0" cellspacing="0" class="AdminLayout">
        <tr>
            <td>
                <h1>
                    Manage Albums</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dAdminHeader">
                    <ul>
                        <li><a href="ManageAlbums.aspx"><span>Manage Albums</span></a></li>
                        <li><a href="AddEditAlbum.aspx"><span>New Album</span></a></li>
                        <li><a href="AddEditPhoto.aspx"><span>New Photo</span></a></li>
                        <li><a href="AlbumOptions.aspx"><span>Album Options</span></a></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <asp:UpdatePanel runat="server" ID="uppnlAlbums">
                        <ContentTemplate>
                            <asp:ListView ID="lvAlbums" runat="server" DataKeyNames="AlbumId">
                                <LayoutTemplate>
                                    <table cellspacing="0" cellpadding="0" class="AdminList">
                                        <tr class="AdminListHeader">
                                            <td>
                                            </td>
                                            <td>
                                                Album Name
                                            </td>
                                            <td>
                                                Add Picture
                                            </td>
                                            <td>
                                                Edit
                                            </td>
                                            <td>
                                                Delete
                                            </td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <div class="pager">
                                                    <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvAlbums">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="<<" PreviousPageText="<"
                                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                                            <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                                                NextPreviousButtonCssClass="command" />
                                                            <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText=">>" NextPageText=">"
                                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <EmptyDataTemplate>
                                    <tr>
                                        <td colspan="5">
                                            <p>
                                                Sorry there are no Albums available at this time.</p>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <a href="AddEditAlbum.aspx?albumid=<%#Eval("Albumid") %>">
                                                <img runat="server" id="iThumb" border="0" width="50" /></a>
                                        </td>
                                        <td>
                                            <a href='<%# String.Format("AddEditAlbum.aspx?Albumid={0}", Eval("AlbumId")) %>'>
                                                <%# Eval("AlbumName") %></a>
                                        </td>
                                        <td align="center">
                                            <a href="<%# String.Format("AddEditPhoto.aspx?Albumid={0}", Eval("Albumid")) %>">
                                                <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                        </td>
                                        <td align="center">
                                            <a href="<%# String.Format("AddEditAlbum.aspx?Albumid={0}", Eval("Albumid")) %>">
                                                <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton runat="server" ID="btnDeleteAlbum" CommandArgument='<%# Eval("AlbumID").ToString() %>'
                                                CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                                OnClientClick="return confirm('Warning: This will delete the Album from the database.');" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
