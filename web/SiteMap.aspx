<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="SiteMap" %>

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
                The Beer House Site Map</h1>
        </div>
        <div id="ContentBody">
            <asp:TreeView runat="server" ID="tvSiteMap" DataSourceID="smds1" 
                ImageSet="Simple" ShowLines="True">
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="False" ForeColor="Red" />
                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
                <LeafNodeStyle ForeColor="#CC0066" />
            </asp:TreeView>
            <asp:SiteMapDataSource runat="server" ID="smds1" SiteMapProvider="TBHSiteMapProvider" />
        </div>
    </div>
</asp:Content>
