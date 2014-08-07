<%@ Page Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" 
    AutoEventWireup="true" CodeFile="ManageDepartments.aspx.cs" Inherits="Admin_ManageDepartments" Title="商品分类_商铺_后台管理_{0}" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
   
    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-th-large"></i>
            商品分类
          </h3>
          <h5>
            商品按品种分类
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
                <i class="icon-shopping-cart"></i> 电子商务
              </span>
              <span class="breadcrumb-arrow"><span></span></span>
            </div>
        

        <div class="breadcrumb-button">
          <span class="breadcrumb-label">
            <i class="icon-th-large"></i> 商品分类
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
          <a href="AddEditDepartment.aspx" class="btn btn-primary rightbtn"><i class="icon-edit"></i>添加分类</a>
          <span class="title">商品分类</span>

      </div>
      <div class="box-content">

        <!-- find me in partials/data_tables_custom -->

<div id="dataTables">


     <asp:ListView ID="lvDepartments" DataKeyNames="DepartmentID" runat="server" OnItemDeleting="lvDepartments_ItemDeleting"
          OnItemCommand="lvDepartments_ItemCommand">
                            <LayoutTemplate>
                                <table class="dTable responsive">
<thead>
<tr>
  <th><div>图标</div></th>
  <th><div>商品分类</div></th>
  <th><div>描述</div></th>
  <th><div>操作</div></th>
</tr>
</thead>

<tbody>
                               
                                    
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
    </tbody>
                                </table>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                
                                        <p>Sorry there are no Departmentss available at this time.</p>
                                 
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                    
                                            <img id="Img1" runat="server" src='<%# Eval("ImageUrl") %>' alt='<%# Eval("Title") %>' border="0" />
                                    </td>
                                    <td>
                                    <%# Eval("Title") %>
                                     </td>
                                    <td>
                                        <%# Eval("Description") %>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="ImageButton1" AlternateText="不显示" CssClass="btn btn-mini" Visible='<%# Eval("Active") %>'
                                CommandName="Hide" CausesValidation="False" runat="server" CommandArgument='<%# Eval("DepartmentID") %>'>
                                                                <i class="icon-eye-open"></i> 隐藏
                            </asp:LinkButton>
                            <asp:LinkButton  ID="ImageButton2" CssClass="btn btn-mini" AlternateText="显示"
                                Visible='<%# !(bool)Eval("Active") %>' CommandName="Show" CausesValidation="False"
                                runat="server" CommandArgument='<%# Eval("DepartmentID") %>'>
                                                                <i class="icon-eye-close"></i> 显示
                            </asp:LinkButton>


                                        <a href='<%# String.Format("manageproducts.aspx?Departmentid={0}", Eval("DepartmentID")) %>' class="btn btn-mini">
                                            <i class="icon-chevron-sign-right"></i>
                                            下属产品</a>
                                   
                                        <a href="<%# String.Format("AddEditDepartment.aspx?Departmentid={0}", Eval("DepartmentID")) %>" class="btn btn-primary  btn-mini">
                                            <i class="icon-edit"></i>
                                            编辑
                                        </a>
                                   
                                        <asp:LinkButton runat="server" ID="btnDelete" CommandArgument='<%# Eval("DepartmentID").ToString() %>'
                                            CommandName="Delete" AlternateText="Delete" CssClass="btn btn-danger btn-mini"
                                            OnClientClick="return confirm('警告：您确定要删除此分类吗？');" >
                                            <i class="icon-remove"></i>
                                            删除
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
            cli.find('ul').addClass('in').find('li').eq(1).addClass('active');
           
            
        });
	</script>
</asp:Content>
