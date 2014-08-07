<%@ Page Language="C#" MasterPageFile="~/LCMaster.master" AutoEventWireup="true" CodeFile="AddEditPost.aspx.cs" Inherits="AddEditPost" %>


<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div class="sectiontitle">
        <asp:Literal runat="server" ID="lblNewThread" Text="Create New Thread" Visible="false" />
        <asp:Literal runat="server" ID="lblNewReply" Text="Reply to Thread: {0}" Visible="false" />
        <asp:Literal runat="server" ID="lblEditPost" Text="Edit Post" Visible="false" />
        <asp:Literal ID="ltlStatus" runat="server" Text="Sorry your post did not save." 
            Visible="False"></asp:Literal>
    </div>
    <p>
    </p>
    <asp:Panel ID="panInput" runat="server">
        <asp:Panel ID="panTitle" runat="server">
            <small><b>
                <asp:Literal runat="server" ID="lblTitle" Text="Title:" /></b></small><br />
            <asp:TextBox ID="txtTitle" runat="server" Width="99%" MaxLength="256" />
            <asp:RequiredFieldValidator ID="valRequireTitle" runat="server" ControlToValidate="txtTitle"
                SetFocusOnError="true" Text="The Title field is required." ToolTip="The Title field is required."
                Display="Dynamic"></asp:RequiredFieldValidator>
            <p>
            </p>
        </asp:Panel>
        <small><b>
            <asp:Literal runat="server" ID="lblBody" Text="Body:" /></b></small><br />
        <FCKeditorV2:FCKeditor ID="txtBody" runat="server" ToolbarSet="BBICMS_Simple"
            Height="400px" Width="99%"  BasePath ="~/fckeditor/"/>
        <p>
        </p>
        <asp:CheckBox ID="chkClosed" runat="server" Text="Do NOT allow replies to this thread" />
        <p>
        </p>
        <asp:Button runat="server" ID="btnSubmit" Text="Post" />
    </asp:Panel>
    <asp:Panel runat="server" ID="panFeedback" Visible="false">
        <asp:Label runat="server" ID="lblConfirmation" SkinID="FeedbackOK">
         Your post has been submitted successfully, and is already visible on the forum.
         Thank your for contributing to the community! 
        </asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="panApprovalRequired" Visible="false">
        <asp:Label runat="server" ID="lblApprovalRequired" SkinID="FeedbackOK">
         Thank you for your post. The forum you posted to is moderated, so your post will
         not appear online immediately. Our team will review your post very soon,
         and consider it for approval.
        </asp:Label>
    </asp:Panel>
    <br />
    <asp:HyperLink runat="server" ID="lnkThreadList" NavigateUrl="BrowseThreads.aspx?ForumID={0}"
        Visible="False">Back to thread list</asp:HyperLink>
    <asp:HyperLink runat="server" ID="lnkThreadPage" NavigateUrl="ShowThread.aspx?ThreadID={0}">Back to thread page</asp:HyperLink>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="LeftColumn" runat="Server">
</asp:Content>
