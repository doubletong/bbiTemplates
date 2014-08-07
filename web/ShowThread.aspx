<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true" CodeFile="ShowThread.aspx.cs" Inherits="ShowThread" %>

<%@ Import Namespace="BBICMS" %>
<%@ Import Namespace="BBICMS.BLL" %>
<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div class="sectiontitle">
        <asp:Literal runat="server" ID="lblPageTitle" Text="Thread: <a href='BrowseThreads.aspx?ForumID={0}'>{1}</a> / {2}" /></div>
    <br />
    <div class="ForumHeaderNav">
        <asp:HyperLink runat="server" ID="lnkNewReply1" NavigateUrl="AddEditPost.aspx?ForumID={0}&ThreadID={1}">Post Reply</asp:HyperLink>
        <asp:LinkButton runat="server" ID="btnCloseThread1" OnClientClick="if (confirm('Are you sure you want to close this thread?') == false) return false;"><br />Close Thread</asp:LinkButton>
    </div>
    <br />
    <asp:ListView runat="server" ID="lvPosts" ItemPlaceholderID="itemPlaceHolder" DataKeyNames="PostId">
        <LayoutTemplate>
            <table cellpadding="0" cellspacing="0" border="1" width="95%">
                <tr runat="server" id="itemPlaceHolder">
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="pager">
                            <asp:DataPager ID="pagerBottom" runat="server" PageSize="<%# Settings.Forums.PostsPageSize%>">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                        NextPreviousButtonCssClass="command" />
                                    <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td valign="top">
                    <div class="posttitle">
                        <asp:HyperLink runat="server" ID="lnkEditPost" ImageUrl="~/Images/Edit.gif" NavigateUrl="~/AddEditPost.aspx?ForumID={0}&ThreadID={1}&PostID={2}" />&nbsp;
                        <asp:ImageButton runat="server" ID="btnDeletePost" ImageUrl="~/Images/Delete.gif"
                            OnClientClick="if (confirm('Are you sure you want to delete this {0}?') == false) return false;" />&nbsp;&nbsp;
                    </div>
                    <asp:Literal ID="lblAddedDate" runat="server" Text='<%# Eval("AddedDate", "{0:D}<br/><br/>{0:T}") %>' />
                    <hr />
                    <asp:Literal ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>' /><br />
                    <br />
                    <small>
                        <asp:Literal ID="lblPosts" runat="server" Text='<%# "Posts: " + GetNoofPostForUser(Eval("AddedBy").ToString()).ToString() %>' />
                        <asp:Literal ID="lblPosterDescription" runat="server" Text='<%# "<br />" + GetPosterDescription(GetNoofPostForUser(Eval("AddedBy").ToString())) %>'
                            Visible='<%# GetNoofPostForUser(Eval("AddedBy").ToString()) >= Settings.Forums.BronzePosterPosts %>' /></small><br />
                    <br />
                    <asp:Panel runat="server" ID="panAvatar" Visible='<%# GetPosterAvatar(Eval("AddedBy").ToString()).Length > 0 %>'>
                        <asp:Image runat="server" ID="imgAvatar" ImageUrl='<%# GetPosterAvatar(Eval("AddedBy").ToString()) %>' />
                        <br />
                        <br />
                    </asp:Panel>
                </td>
                <td valign="top">
                    <div class="posttitle">
                        <asp:Literal ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' /></div>
                    <div class="postbody">
                        <asp:Literal ID="lblBody" runat="server" Text='<%# Eval("Body") %>' /><br />
                        <br />
                        <asp:Literal ID="lblSignature" runat="server" Text='<%# ConvertToHtml(GetPosterSignature(Eval("AddedBy").ToString())) %>' /><br />
                        <br />
                        <div style="text-align: right;">
                            <asp:HyperLink runat="server" ID="lnkQuotePost" NavigateUrl="~/AddEditPost.aspx?ForumID={0}&ThreadID={1}&QuotePostID={2}">Quote Post</asp:HyperLink>
                        </div>
                    </div>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <p>
    </p>
    <div style="text-align: center; font-weight: bold">
        <asp:HyperLink runat="server" ID="lnkNewReply2" NavigateUrl="AddEditPost.aspx?ForumID={0}&ThreadID={1}">Post Reply</asp:HyperLink>
        <asp:LinkButton runat="server" ID="btnCloseThread2" OnClientClick="if (confirm('Are you sure you want to close this thread?') == false) return false;"><br />Close Thread</asp:LinkButton>
        <asp:Panel runat="server" ID="panClosed" Visible="false">
            <asp:Image runat="server" ID="imgClosed" AlternateText="Thread Closed" ImageUrl="~/Images/LockSmall.gif" />
            <asp:Label runat="server" ID="lblClosed" Font-Bold="true">This thread has been closed</asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="RightColumn" runat="Server">
        <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
        <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
        <uc1:NewsletterBox ID="NewsletterBox1" runat="server" />
</asp:Content>
