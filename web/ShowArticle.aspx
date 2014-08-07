<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" Theme="DarkBeer" AutoEventWireup="true" CodeFile="ShowArticle.aspx.cs" Inherits="ShowArticle" %>

<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/CommentService.asmx" InlineScript="true" />
        </Services>
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/TBHComments.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <table cellpadding="0" cellspacing="0" class="AdminLayout" border="0" width="90%">
        <tr>
            <td colspan="3" id="dArticleHeader">
                <h1 class="articletitle">
                    <% =GetTitle%></h1>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table class="AdminLayout">
                    <tr>
                        <td>
                            <b>Posted by: </b>
                            <asp:Literal ID="lblAddedBy" runat="server"></asp:Literal>
                        </td>
                        <td>
                            Article Rating =
                            <% =GetAverageRating%>&nbsp;Beers&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <% =GetArticleDate%>
                        </td>
                        <td rowspan="3">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    Rate This Article:<br />
                                    <asp:Rating ID="ArticleRating" runat="server" BehaviorID="RatingBehavior1" CssClass="ArticleRating"
                                        StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                        EmptyStarCssClass="emptyRatingStar">
                                    </asp:Rating>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="lblLocation" Text="<br /><b>Location: </b>{0}" />
                            <br />
                            <b>Views: </b>
                            <asp:Literal runat="server" ID="lblViews" Text="this article has been read {0} times" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="lblCategory" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:HyperLink runat="server" ID="hlnkEdit" Visible="false" ForeColor="#C30000">Edit This Article?&nbsp;<img src="<%= ResolveUrl("Images/Edit.gif")%>" alt="Edit Article?" border="0" /><br /></asp:HyperLink>
                <asp:LinkButton runat="server" ID="lbtnApprove" Visible="false" ForeColor="#C30000">Approve This Article?&nbsp;<img src="<%= ResolveUrl("Images/checkmark.gif")%>" alt="Approve Article?"  border="0"/><br /></asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbtnDelete" Visible="false" ForeColor="#C30000">Delete This Article?&nbsp;<img src="<%= ResolveUrl("Images/Delete.gif")%>" alt="Delete Article?"  border="0"/></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="3" id="dArticleBody">
                <% =GetBody%>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="ArticleFooter">
                <asp:HyperLink runat="server" ID="hlnkArticles" NavigateUrl="~/BrowseArticles.aspx">Article List</asp:HyperLink>
            </td>
        </tr>
    </table>
    <input type="hidden" id="ArticleId" value="<%=ArticleId %>" />
    <input type="hidden" id="hUserIP" value="<%=Request.ServerVariables["remote_addr"]%>" />
    <div id="dComments">
        <asp:UpdatePanel runat="server" ID="uppnlComments">
            <ContentTemplate>
                <asp:ListView ID="lvComments" runat="server">
                    <LayoutTemplate>
                        <div>
                            <div id="itemPlaceholder" runat="server" />
                            <div class="pager">
                                <asp:DataPager ID="pagerBottom" runat="server" PageSize="6">
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
                        </div>
                    </LayoutTemplate>
                    <EmptyDataTemplate>
                        <p>
                            Sorry there are no comments available at this time. Be the first to make a comment!</p>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <div class="comment">
                            <div class="avatar">
                                <img src="<%# ResolveUrl(String.Format("GravatarIcon.ashx?email={0}", Eval("EMailHash"))) %>"
                                    alt="" />
                                <span class="comment-date">
                                    <%#Eval("AddedDate")%></span>
                            </div>
                            <!-- /avatar -->
                            <div class="info">
                                <strong>
                                    <%#Eval("Title")%></strong>
                            </div>
                            <!-- /info -->
                            <div>
                                <%#Eval("EncodedBody")%>'</div>
                            <div class="clear">
                            </div>
                        </div>
                        <!-- /comment -->
                    </ItemTemplate>
                </asp:ListView>
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dComment">
        <fieldset id="commentform">
            <legend>Leave a Comment</legend>
            <div id="dMakeComment">
                <div>
                    <label for="txtTitle">
                        Title</label><em>(required)</em></div>
                <div>
                    <input id="txtTitle" type="text" value="re: <% =GetTitle%>" size="40" /></div>
                <div>
                    <label for="txtEmail">
                        Your E-Mail</label>
                    <em>(will be kept private)</em></div>
                <div>
                    <input id="txtEmail" type="text" /></div>
                <div>
                    <label for="txtURL">
                        Your URL</label>
                    <em>(optional)</em></div>
                <div>
                    <input id="txtURL" type="text" /></div>
                <div>
                    <label for="txtComment">
                        Comments</label>
                    <em>(required)</em><span id="sCommentReq" style="color: Red; visibility: hidden;">*</span></div>
                <div>
                    <textarea rows="5" cols="25" id="txtComment"></textarea></div>
                <button name="btnStoreComment" id="btnStoreComment" onclick="StoreComment(); return false;">
                    Submit Me!</button>
            </div>
            <div id="dThinking">
                Thinking....</div>
            <div id="dCommentPosted">
                Your Comment has been submitted, check back later to see it listed....</div>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="RightColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>
