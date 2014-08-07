<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUploader.ascx.cs" Inherits="Controls_FileUploader" %>

Upload a file:
<asp:FileUpload ID="filUpload" runat="server" />&nbsp;
<asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="false" OnClick="btnUpload_Click" /><br />
<asp:Label ID="lblFeedbackOK" SkinID="FeedbackOK" runat="server"></asp:Label>
<asp:Label ID="lblFeedbackKO" SkinID="FeedbackKO" runat="server"></asp:Label>