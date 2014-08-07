<%@ Page Language="C#" MasterPageFile="~/LCMaster.master" AutoEventWireup="true" CodeFile="ArchivedNewsletters.aspx.cs" Inherits="ArchivedNewsletters" %>


<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <div id="ContentTitle">
            <h1>
                Archived Newsletters</h1>
        </div>
        <div id="ContentBody">
            <p>
                Here is the list of archived newsletters sent in the past. Click on the newsletter's
                subject to read the whole content. If you want to receive future newsletters right
                into your e-mail box, <a href="Register.aspx">register now</a> to the site, if you
                haven't done so yet.</p>
            <asp:UpdatePanel runat="server" ID="upOrders">
                <ContentTemplate>
                    <asp:ListView runat="server" ID="lvNewsLetters" DataKeyNames="NewsLetterId">
                        <LayoutTemplate>
                            <ul class="NewsletterList">
                                <li runat="server" id="itemPlaceHolder" />
                            </ul>
                            <div class="pager">
                                <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvNewsLetters">
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
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="lnkNewsletter" runat="server" NavigateUrl='<%# "ShowNewsletter.aspx?NewsletterID=" + Eval("NewsletterID").ToString() %>'
                                    Text='<%# Eval("Subject") %>'></asp:HyperLink><small> - (sent on
                                        <%#Eval("DateSent", "{0:d}")%>
                                        )</small> </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="LeftColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>
