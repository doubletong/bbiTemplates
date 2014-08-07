<%@ Page Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageShippingMethods.aspx.cs" 
    Inherits="Admin_ManageShippingMethods" Title="物流方式_商铺_{0}" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">

    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-truck"></i>
            物流方式
          </h3>
          <h5>
            快递，邮递
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
                <i class="icon-shopping-cart"></i> 电子商务
              </span>
              <span class="breadcrumb-arrow"><span></span></span>
            </div>
        

        <div class="breadcrumb-button">
          <span class="breadcrumb-label">
            <i class="icon-truck"></i> 物流方式
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
          <a href="AddEditShippingMethod.aspx" class="btn btn-primary rightbtn"><i class="icon-edit"></i>添加物流方式</a>
          <span class="title">物流方式</span>

      </div>
      <div class="box-content">

        <!-- find me in partials/data_tables_custom -->

    <div id="dataTables">


                        <asp:ListView ID="lvShippingMethods" runat="server">
                            <LayoutTemplate>
                                <table class="dTable responsive">
<thead>
                                    <tr>
                                        <td>
                                            物流方式
                                        </td>
                                        <td>
                                            价格
                                        </td>
                                        <td>
                                            操作
                                        </td>
                                    </tr>
    </thead>
                                    <tbody>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                               
                                        <p>
                                            Sorry there are no Shipping Methods available at this time.</p>                                
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("Title") %>
                                    </td>
                                    <td>
                                        <%# Eval("Price") %>
                                    </td>
                                    <td>
                                        <a href="<%# String.Format("AddEditShippingMethod.aspx?ShippingMethodid={0}", Eval("ShippingMethodid")) %>" class="btn btn-primary btn-mini">
                                            <i class="icon-edit"></i> 编辑</a>
                                        <asp:LinkButton runat="server" ID="btnDelete" CommandArgument='<%# Eval("ShippingMethodid").ToString() %>'
                                            CommandName="Delete"  AlternateText="Delete" CssClass="btn btn-danger btn-mini"
                                            OnClientClick="return confirm('Warning: This will delete the Shipping Method from the database.');">
                                            <i class="icon-remove"></i> 删除
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
            cli.find('ul').addClass('in').find('li').eq(4).addClass('active');


        });
	</script>
</asp:Content>