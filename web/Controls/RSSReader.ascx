<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RSSReader.ascx.cs" Inherits="Controls_RSSReader" %>
<dl>
<asp:ListView runat="server" ID="lvRSSReader" ItemPlaceholderID="itemPlaceHolder">
    <LayoutTemplate>
        <dl runat="server" id="itemPlaceHolder">
        </dl>
    </LayoutTemplate>
    <ItemTemplate>
    
        <dt><a href="<%#Eval("Url")%>">
            <%#Eval("Title")%></a> </dt>
        <dd>
            <%#Eval("description")%>
            <a href="<%#Eval("Url")%>">...</a></dd>
    </ItemTemplate>
</asp:ListView>
</dl> 