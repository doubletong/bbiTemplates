<%@ Page Language="C#" MasterPageFile="~/LCMaster.master" AutoEventWireup="true"
    CodeFile="ShowAlbum.aspx.cs" Inherits="ShowAlbum" %>

<asp:Content ID="headContent" ContentPlaceHolderID="cphMainHeader" runat="Server">
    <link rel="stylesheet" href="lightbox.css" type="text/css" media="screen" />

    <script type="text/javascript" src="js/prototype.js"></script>

    <script type="text/javascript" src="js/scriptaculous.js?load=effects,builder"></script>

    <script type="text/javascript" src="js/lightbox.js"></script>

</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <div id="ContentMain">
        <h1 runat="server" id="h2GalleryTitle">
        </h1>
        <div id="ContentBody">
            <asp:ListView ID="lvPictures" runat="server" GroupItemCount="5" GroupPlaceholderID="groupPlaceHolder"
                ItemPlaceholderID="itemPlaceHolder" OnItemDataBound="lvPictures_ItemDataBound"
                OnPagePropertiesChanged="lvPictures_PagePropertiesChanged">
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
                    <td class="AlbumPhoto">
                        <div class="AlbumShadow">
                            <a href='<%#GetDisplayPicturePath(Eval("AlbumName").ToString(), Eval("PictureFileName").ToString())%>'
                                rel="lightbox[<%#Eval("AlbumName") %>img]" title='<%#Eval("PictureTitle")%>'>
                                <img runat="server" id="iThumb" border="0" width="50" src='' alt='<%#Eval("PictureTitle")%>' /></a>
                        </div>
                        <div class="PhotoTitle">
                            <a href='<%#GetDisplayPicturePath(Eval("AlbumName").ToString(), Eval("PictureFileName").ToString())%>'
                                rel="lightbox[<%#Eval("AlbumName") %>title]" title='<%#Eval("PictureTitle")%>'>
                                <%#Eval("PictureTitle")%></a>
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
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>
