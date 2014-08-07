<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true" CodeFile="BrowseAlbums.aspx.cs" Inherits="BrowseAlbums" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div id="GalleryMain">
        <div id="GalleryTitle">
            <h2 id="h2GalleryTitle">
                Photo Gallery
            </h2>
        </div>
        <div id="GalleryContent">
            <asp:ListView ID="lvPictures" runat="server" GroupItemCount="5" GroupPlaceholderID="groupPlaceHolder"
                ItemPlaceholderID="itemPlaceHolder">
                <LayoutTemplate>
                    <table id="tblPictures" runat="server" class="AlbumsTable">
                        <tr runat="server" id="groupPlaceholder" />
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr runat="server" id="ContactsRow">
                        <td runat="server" id="itemPlaceholder" />
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td class="AlbumPhoto">
                        <div class="AlbumShadow">
                            <a href="ShowAlbum.aspx?albumid=<%#Eval("AlbumId") %>">
                                <img runat="server" id="iThumb" border="0" width="50" align="middle" /></a>
                        </div>
                        <div class="PhotoTitle">
                            <a href='<%# String.Format("AddEditAlbum.aspx?Albumid={0}", Eval("AlbumId")) %>'>
                                <%# Eval("AlbumName") %></a>
                        </div>
                    </td>
                </ItemTemplate>
                <ItemSeparatorTemplate>
                    <td class="AlbumSeparator">
                        &nbsp;
                    </td>
                </ItemSeparatorTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="RightColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>
