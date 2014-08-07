<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditNewsLetter.aspx.cs" 
Inherits="Admin_AddEditNewsLetter" Title="Untitled Page" ValidateRequest="false" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/GetNewsLetterStatus.asmx" />
        </Services>
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/Newsletter.js" />
        </Scripts>
    </asp:ScriptManagerProxy>

     <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-envelope-alt"></i>
            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
          </h3>
          <h5>
            已发送的邮件存档
          </h5>
        </div>

         <bbi:Statistics runat="server" id="SimpleStatistics" />
      </div>
    </div>
  </div>



    <div class="container-fluid padded">
    <div class="row-fluid">

      <!-- Breadcrumb line -->

      <div id="breadcrumbs">
        <div class="breadcrumb-button blue">
          <span class="breadcrumb-label"><i class="icon-home"></i> 首页</span>
          <span class="breadcrumb-arrow"><span></span></span>
        </div>

        <div class="breadcrumb-button">
              <span class="breadcrumb-label">
                <i class="icon-envelope"></i> 邮件列表
              </span>
              <span class="breadcrumb-arrow"><span></span></span>
            </div>
           

        <div class="breadcrumb-button">
          <span class="breadcrumb-label">
            <i class="icon-envelope"></i> <asp:Literal runat="server" ID="ltlTitle1"></asp:Literal>
          </span>
          <span class="breadcrumb-arrow"><span></span></span>
        </div>
      </div>
    </div>
  </div>



    <div id="dAdmin">
       
        <br />
        <asp:Panel runat="server" ID="panSend">
            <asp:Literal runat="server" ID="ltlAddInstruction"></asp:Literal>
            Fill the fields below with the newsletter's subject, the body in plain-text and
            HTML format. Only the plain-text body is compulsory. If you don't specify the HTML
            version, the plain-text body will be used for HTML subscriptions as well.
            <asp:Literal runat="server" ID="ltlDateSent"></asp:Literal>
            <br />
            <br />
            <small><b>-12
                <asp:Literal runat="server" ID="lblTitle" Text="Subject:" /></b></small><br />
            <asp:Literal ID="ltlSubject" runat="server"></asp:Literal>
            <asp:TextBox ID="txtSubject" runat="server" MaxLength="256" Width="90%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valRequireSubject" runat="server" ControlToValidate="txtSubject"
                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                ValidationGroup="Newsletter">The Subject field is required.</asp:RequiredFieldValidator>
            <br />
            <br />
            <small><b>
                <asp:Literal runat="server" ID="lblPlainTextBody" Text="Plain-text Body:" /></b></small><br />
            <asp:Literal ID="ltlPlainTextBody" runat="server"></asp:Literal>
            <asp:TextBox ID="txtPlainTextBody" runat="server" Rows="14" TextMode="MultiLine"
                Width="90%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valRequirePlainTextBody" runat="server" ControlToValidate="txtPlainTextBody"
                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                ValidationGroup="Newsletter">The plain-text body is required.</asp:RequiredFieldValidator>
            <br />
            <br />
            <small><b>
                <asp:Literal runat="server" ID="lblHtmlBody" Text="HTML Body:" /></b></small><br />
            <asp:Literal ID="ltlHtmlBody" runat="server"></asp:Literal>
            <FCKeditorV2:FCKeditor ID="txtHtmlBody" runat="server" Height="400px" ToolbarSet="BBICMS">
            </FCKeditorV2:FCKeditor>
            <br />
            <br />
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"/>
            <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" OnClientClick="if (confirm('Are you sure you want to send the newsletter?') == false) return false;"
                ValidationGroup="Newsletter" />
        </asp:Panel>
        <asp:Panel ID="panWait" runat="server" Visible="false">
            <p>
                Another newsletter is currently being sent. Please wait until it completes before
                compiling and sending a new one.</p>
            <div>
                <div id="dIsSending">
                    Checking to see if sending a NewsLetter....
                </div>
                <div id="dProgress">
                </div>
                <br />
                <div id="progressbarcontainer">
                    <div id="progressbar">
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
