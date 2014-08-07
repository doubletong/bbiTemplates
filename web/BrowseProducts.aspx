<%@ Page Language="C#" MasterPageFile="~/TBHMain.master" AutoEventWireup="true"  Theme="DarkBeer"
CodeFile="BrowseProducts.aspx.cs" Inherits="BrowseProducts" %>

<%@ Register Src="~/Controls/DepartmentsForSide.ascx" TagPrefix="bbi" TagName="DepartmentsForSide" %>

<%@ Register Src="~/Controls/RatingDisplay.ascx" TagPrefix="bbi" TagName="RatingDisplay" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="pure-g">
        <div class="pure-u-3-4">
            <h3>近期模板</h3>
        <section>
            <asp:ListView ID="lvProducts" runat="server" DataKeyNames="ProductID" DataSourceID="ObjectDataSource1">
                        <LayoutTemplate>
                            <ul  class="goodlist cf">
                                <asp:PlaceHolder runat="server" id="itemPlaceHolder"/>                                 
                            </ul>
                        </LayoutTemplate>
                        <EmptyDataTemplate>
                            <p class="text text-info"><i class="icon-info-sign"></i>
                                    没找到相关项
                                </p>
                        </EmptyDataTemplate>
                       
                        <ItemTemplate>
                            <li>
                                   
                                <div class="shadow">
                                    <a href='/Store/Product_<%# Eval("ProductId")%>/' title='<%# Eval("Title")%>'>
                                        <img src='<%# BBICMS.StoreHelper.GetProductThumbUrl(Eval("SmallImageUrl").ToString()) %>' alt='<%# Eval("Title")%>' /></a>

                                    <div class="ProductPrice">
                                        <span class="fr"><i class="icon-yen"></i><%# BBICMS.Helpers.FormatPrice(Eval("FinalUnitPrice")) %>   </span>

                                        <h3><a href='/Store/Product_<%# Eval("ProductId")%>/'
                                            title='<%# Eval("Title")%>'>
                                            <%# Eval("Title")%>
                                        </a></h3>
                                    </div>

                                </div>
                                                                       
                                 <%-- <bbi:RatingDisplay runat="server" ID="ratDisplay" Value='<%# decimal.Parse(Eval("AverageRating").ToString()) %>'
                                        Visible='<%# ((int)Eval("Votes") > 0) %>' />--%>
                                
                                                                  
                                   <%-- Availability:
                                    <asp:AvailabilityImage ID="AvailabilityImage1" runat="server" Value='<%# Eval("UnitsInStock") %>' />--%>
                                
                              
                            </li>
                        </ItemTemplate>
                    </asp:ListView>


            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetActiveProducts" EnablePaging="True" 
                    MaximumRowsParameterName="PageSize" 
                    StartRowIndexParameterName="StartRowIndex" SelectCountMethod="GetActiveProductsCount"
                TypeName="BBICMS.Store.ProductsRepository">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="vDepartmentId" QueryStringField="DepartmentId" Type="Int32" />                  
                </SelectParameters>
                
            </asp:ObjectDataSource>


            <!-- start #pagination -->                     
                          
                                <div id="pagination" class="pr50 mb50 cf">
                               
                                <asp:DataPager ID="pdPager" runat="server" QueryStringField="Page" PageSize="10" OnPreRender="DataPager_PreRender" PagedControlID="lvProducts">
                                    <Fields>
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                <span class="pull-right">页：
                                                    <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.TotalRowCount>0 ? (Container.StartRowIndex / Container.PageSize) + 1 : 0 %>" />
                                                    /
                                                    <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Math.Ceiling ((double)Container.TotalRowCount / Container.PageSize) %>" />，
                                                    总记录：<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                                    </span>
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                        
                                    </Fields>
                                </asp:DataPager>
                              
                                </div>
                   
                        
                        <!-- end #pagination --> 
            </section>

        </div>
        <div class="pure-u-1-5 sider">
            <aside class="ml30 departmentlist">
                <bbi:DepartmentsForSide runat="server" ID="DepartmentsForSide" />
            </aside>
            <%--<div id="dRSS">
                <a href="thebeerhouse.rss">Subscribe:
                <img id="Img1" runat="server" src="~/images/beercap.gif" border="0" alt="Follow The BeerHouse via RSS." /></a>
                <hr />
            </div>
                <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
    <uc1:NewsletterBox ID="NewsletterBox1" runat="server" />
            <uc1:PollBox ID="PollBox1" runat="server" />--%>
        </div>
    </div>
    
</asp:Content>
