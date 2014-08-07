<%@ Page Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" AutoEventWireup="true" 
    CodeFile="ManageProducts.aspx.cs" Inherits="Admin_ManageProducts" Title="商品列表_商铺_后台管理_{0}" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
 
    
    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-th"></i>
            商品列表
          </h3>
          <h5>
            所有商品陈列
          </h5>
        </div>

         <bbi:statistics runat="server" id="SimpleStatistics" />
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
                <i class="icon-shopping-cart"></i> 电子商铺
              </span>
              <span class="breadcrumb-arrow"><span></span></span>
            </div>
        

        <div class="breadcrumb-button">
          <span class="breadcrumb-label">
            <i class="icon-th"></i> 商品列表
          </span>
          <span class="breadcrumb-arrow"><span></span></span>
        </div>
      </div>
    </div>
  </div>


    
    <!-- 分类列表 -->
     <div class="container-fluid padded">
    <div class="row-fluid">
  <div class="span12">
    <div class="box">
      <div class="box-header">
          <a href="AddEditProduct.aspx" class="btn btn-primary rightbtn"><i class="icon-edit icon-fixed-width"></i>添加商品</a>
          <span class="title">商品列表</span>

      </div>
      <div class="box-content">

        <!-- find me in partials/data_tables_custom -->

<div id="dataTables">



               
                        <asp:ListView ID="lvProducts" OnItemDeleting="lvProducts_ItemDeleting" DataKeyNames="ProductID" runat="server">
                            <LayoutTemplate>
                                 <table class="dTable responsive">
<thead>
                                    <tr>
                                        <th>
                                            图片
                                        </th>
                                        <th>
                                            产品名称
                                        </th>
                                        <th>
                                            库存
                                        </th>
                                        <th>
                                           折扣
                                        </th>
                                        <th>
                                            价格
                                        </th>
                                        <th>
                                            操作
                                        </th>
                                    </tr>
    </thead>
                                     <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                 </tbody>
                                </table>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                              
                                        <p>
                                            Sorry there are no Productss available at this time.</p>
                                   
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <a href="<%# String.Format("AddEditProduct.aspx?ProductId={0}", Eval("ProductId")) %>">
                                            <img  src='<%#BBICMS.StoreHelper.GetProductThumbUrl(Eval("SmallImageUrl").ToString()) %>' style="height:30px;" alt='<%# Eval("Title") %>'
                                               />
                                          
                                           

                                        </a>
                                    </td>
                                   
                                    <td>
                                        <a href="<%# String.Format("AddEditProduct.aspx?ProductId={0}", Eval("ProductId")) %>">
                                            <%# Eval("Title") %></a>
                                        <br />
                                        <%# Eval("SKU") %>
                                    </td>
                                     <td><%# Eval("UnitsInStock") %></td>
                                     <td><%# Eval("DiscountPercentage") %>%</td>

                                    <td>
                                        <h4><%#FormatPrice(Eval("UnitPrice"))%></h4>
                                       
                                    </td>
                                    <td>
                                         <a href='<%# String.Format("UploadPhotos.aspx?ProductId={0}", Eval("ProductID")) %>' class="btn btn-mini btn-primary"><i class="icon-upload"></i> 上传组图</a>
                                        <a href="<%# String.Format("AddEditProduct.aspx?ProductId={0}", Eval("ProductId")) %>" class="btn btn-primary btn-mini">
                                            <i class="icon-edit"></i> 编辑</a>                                   
                                        <asp:LinkButton runat="server" ID="btnDelete" CommandArgument='<%# Eval("DepartmentID").ToString() %>'
                                            CommandName="Delete"  AlternateText="Delete" CssClass="btn btn-danger btn-mini"
                                            OnClientClick="return confirm('警告：您确定要删除些商品吗？');">
                                            <i class="icon-remove"></i> 删除</a>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                           
                            
                        </asp:ListView>
</div>
      </div>
    </div>
  </div>
</div>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="/Content/Admin/js/jquery.sparkline.min.js"></script>
    <script src="/Content/Admin/Plugins/DataTables-1.9.4/media/js/jquery.dataTables.min.js"></script>
    <script src="/Content/Admin/js/mydTable.js"></script>

    <script>

        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            var cli = $(".primary-sidebar ul.nav").find('li').eq(1);
            cli.addClass('active').find('a').eq(0).removeClass('collapsed');
            cli.find('ul').addClass('in').find('li').eq(0).addClass('active');

        });
	</script>
</asp:Content>