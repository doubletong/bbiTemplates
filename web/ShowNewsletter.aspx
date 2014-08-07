<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true" CodeFile="ShowNewsletter.aspx.cs" Inherits="ShowNewsletter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightColumn" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CenterColumn" Runat="Server">
   <div class="sectiontitle">Newsletter: <asp:Literal runat="server" ID="lblSubject" /></div>
   <p></p>
   <small><b>Plain-text Body:</b></small>
   <div style="border: dashed 1px black; overflow: auto; width: 98%; height: 300px; padding: 5px;">
      <asp:Literal runat="server" ID="lblPlaintextBody" />
   </div>
   <p></p>
   <small><b>HTML Body:</b></small>
   <div style="border: dashed 1px black; overflow: auto; width: 98%; height: 300px; padding: 5px;">
      <asp:Literal runat="server" ID="lblHtmlBody" />
   </div>
</asp:Content>

