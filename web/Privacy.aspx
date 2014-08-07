<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true"
    CodeFile="Privacy.aspx.cs" Inherits="Privacy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:NewsletterBox ID="NewsletterBox1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CenterColumn" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Privacy Policy Statement</h1>
        </div>
        <div id="ContentBody">
            This is the web site of <b>The Beer House</b>.<br>
            <p>
                Our postal address is
                <br>
                <b>123 Main<br>
                    Raleigh, NC 27607</b></p>
            We can be reached via e-mail at <a href="mailto:info@thebeerhouse.com">info@thebeerhouse.com</a><br>
            or you can reach us by telephone at 919-555-1212<br>
            <p>
                For each visitor to our Web page, our Web server automatically recognizes no information
                regarding the domain or e-mail address.</p>
            <p>
                We collect the e-mail addresses of those who post messages to our bulletin board,
                the e-mail addresses of those who communicate with us via e-mail, information volunteered
                by the consumer, such as survey information and/or site registrations, fax number.</p>
            <p>
                The information we collect is used to customize the content and/or layout of our
                page for each individual visitor, used to notify consumers about updates to our
                Web site, .</p>
            <p>
                With respect to cookies: We use cookies to store visitors preferences, record session
                information, such as items that consumers add to their shopping cart.</p>
            <p>
                If you do not want to receive e-mail from us in the future, please let us know by
                sending us e-mail at the above address.</p>
            <p>
                With respect to Ad Servers: We do not partner with or have special relationships
                with any ad server companies.</p>
            <p>
                If you feel that this site is not following its stated information policy, you may
                contact us at the above addresses or phone number.</p>
        </div>
    </div>
</asp:Content>
