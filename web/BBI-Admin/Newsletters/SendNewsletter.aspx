<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="SendNewsletter.aspx.cs" Inherits="Admin_SendNewsletter" Title="Untitled Page" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/NewsLetterService.asmx" />
        </Services>
        <Scripts>
            <asp:ScriptReference Path="~/TBHComments.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <div class="sectiontitle">
        <asp:Literal runat="server" ID="lblSendNewsletter" Text="Create & Send Newsletter" />
    </div>
    <br />
    <asp:Panel runat="server" ID="panSend">
        Fill the fields below with the newsletter's subject, the body in plain-text and
        HTML format. Only the plain-text body is compulsory. If you don't specify the HTML
        version, the plain-text body will be used for HTML subscriptions as well.
        <br />
        <small><b>
            <asp:Literal runat="server" ID="lblTitle" Text="Subject:" /></b></small><br />
        <asp:TextBox ID="txtSubject" runat="server" MaxLength="256" Width="99%"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valRequireSubject" runat="server" ControlToValidate="txtSubject"
            Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
            ValidationGroup="Newsletter">The Subject field is required.</asp:RequiredFieldValidator>
        <br />
        <small><b>
            <asp:Literal runat="server" ID="lblPlainTextBody" Text="Plain-text Body:" /></b></small><br />
        <asp:TextBox ID="txtPlainTextBody" runat="server" Rows="14" TextMode="MultiLine"
            Width="99%"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valRequirePlainTextBody" runat="server" ControlToValidate="txtPlainTextBody"
            Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
            ValidationGroup="Newsletter">The plain-text body is required.</asp:RequiredFieldValidator>
        <br />
        <small><b>
            <asp:Literal runat="server" ID="lblHtmlBody" Text="HTML Body:" /></b></small><br />
        <FCKeditorV2:FCKeditor ID="txtHtmlBody" runat="server" Height="400px" ToolbarSet="BBICMS"
            Width="99%">
        </FCKeditorV2:FCKeditor>
        <br />
        <asp:Button ID="btnSend" runat="server" Text="Send" OnClientClick="if (confirm('Are you sure you want to send the newsletter?') == false) return false;"
            ValidationGroup="Newsletter" />
    </asp:Panel>
    <asp:Panel ID="panWait" runat="server" Visible="false">
        
      <p>Another newsletter is currently being sent. Please wait until it completes
      before compiling and sending a new one.</p>
            <div>
        <div id="dIsSending">
            Checking to see if sending a NewsLetter....
        </div>
        <br />
        <div class="progressbarcontainer">
            <div class="progressbar" id="progressbar">
                dfsdfsdf
            </div>
        </div>
    </div>
    </asp:Panel>
</asp:Content>
