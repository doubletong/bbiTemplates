<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsletterBox.ascx.cs" Inherits="Controls_NewsletterBox" %>
<div class="SideBarbox">
    <div class="SideBarTitle">
        <asp:Image ID="imgArrow" runat="server" ImageUrl="~/images/arrowr.gif" GenerateEmptyAlternateText="True" meta:resourcekey="imgArrowResource1" />
        Newsletter
    </div>
    <div class="SideBarContent">
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <asp:Localize runat="server" ID="locSubscribe1" meta:resourcekey="locSubscribe1Resource1"
                    Text="
         &lt;b&gt;Register to the site for free&lt;/b&gt;, and subscribe to the newsletter. Every month you will receive new
         articles and special content not available elsewhere on the site, right into your e-mail box!
         "></asp:Localize>
                <br />
                <br />
                <input id="NewsletterEmail" style="width: 140px" type="text" value="E-mail here"
                    onfocus="javascript:this.value = '';" />
                <input type="button" value="OK" onclick="javascript:SubscribeToNewsletter();" />
            </AnonymousTemplate>
            <LoggedInTemplate>
                <asp:Localize runat="server" ID="locSubscribe2" meta:resourcekey="locSubscribe2Resource1"
                    Text="
         You can change your subscription type (plain-text, HTML or no newsletter) from your"></asp:Localize>
                <asp:HyperLink runat="server" ID="lnkProfile" NavigateUrl="~/EditProfile.aspx" Text="profile"
                    meta:resourcekey="lnkProfileResource1" />
                <asp:Localize runat="server" ID="locSubscribe3" meta:resourcekey="locSubscribe3Resource1"
                    Text="page. Click the link below to read the newsletters run in the past."></asp:Localize>
            </LoggedInTemplate>
        </asp:LoginView>
        <br />
        <asp:HyperLink runat="server" ID="lnkArchive" NavigateUrl="~/ArchivedNewsletters.aspx"
            Text="Archived Newsletters" meta:resourcekey="lnkArchiveResource1" />
    </div>
</div>
