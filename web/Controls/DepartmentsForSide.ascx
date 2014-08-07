<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentsForSide.ascx.cs" Inherits="Controls_DepartmentsForSide" %>
<h3>模板分类</h3>
<asp:Repeater ID="rpDeparts" runat="server">
    <HeaderTemplate><ul></HeaderTemplate>
    <ItemTemplate>
        <li>
            <a href='/Department_<%# Eval("DepartmentID") %>/'><i class="icon-angle-right"></i> <%# Eval("Title") %></a>
        </li>
    </ItemTemplate>
    <FooterTemplate></ul></FooterTemplate>

</asp:Repeater>
