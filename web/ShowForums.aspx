<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true" CodeFile="ShowForums.aspx.cs" Inherits="ShowForums" %>

<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div id="MainContent">
        <div class="sectiontitle">
            Forums</div>
        <p>
        </p>
        Click on the title of the forum for which you want to browse the threads:
        <p>
        </p>
        <asp:ListView runat="server" ID="lvForums" GroupPlaceholderID="grpItemPlaceHolder"
            ItemPlaceholderID="itemPlaceHolder" GroupItemCount="2" DataKeyNames="ForumId">
            <LayoutTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="95%">
                    <tr runat="server" id="grpItemPlaceHolder">
                    </tr>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr>
                    <td runat="server" id="itemPlaceHolder">
                    </td>
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <td>
                    <table cellpadding="6" style="width: 100%;">
                        <tr>
                            <td style="width: 1px;">
                                <asp:HyperLink runat="server" ID="lnkForumImage" 
                                NavigateUrl='<%# "BrowseThreads.aspx?ForumID=" + Eval("ForumID") %>'>
                                    <asp:Image runat="server" ID="imgForum" BorderWidth="0px" AlternateText='<%# Eval("Title") %>'
                                        ImageUrl='<%# Eval("ImageUrl") %>' ImageAlign="left" />
                                </asp:HyperLink>
                            </td>
                            <td>
                                <div class="sectionsubtitle">
                                    <asp:HyperLink runat="server" ID="lnkForumRss" 
                                    NavigateUrl='<%# "forum.rss?ForumID=" + Eval("ForumID") %>'>
                     <img style="border-width: 0px;" src="Images/rss.gif" alt="Get the RSS for this forum"  /></asp:HyperLink>
                                    <asp:HyperLink runat="server" ID="lnkForumTitle" Text='<%# Eval("Title") %>' NavigateUrl='<%# "BrowseThreads.aspx?ForumID=" + Eval("ForumID") %>' />
                                </div>
                                <br />
                                <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Description") %>' />
                            </td>
                        </tr>
                    </table>
                </td>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="RightColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:NewsletterBox ID="NewsletterBox1" runat="server" />
</asp:Content>
