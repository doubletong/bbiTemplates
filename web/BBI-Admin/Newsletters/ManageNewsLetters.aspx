<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageNewsLetters.aspx.cs"
     Inherits="Admin_ManageNewsLetters" Title="邮件列表_后台管理_{0}" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-envelope-alt"></i>
            邮件列表
          </h3>
          <h5>
            已发送的邮件存档
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
            <i class="icon-envelope"></i> 邮件列表
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
          <a href="AddEditNewsLetter.aspx" class="btn btn-primary rightbtn"><i class="icon-edit icon-fixed-width"></i>写邮件</a>
          <span class="title">归档邮件（已经发送过）</span>

      </div>
      <div class="box-content">

        <!-- find me in partials/data_tables_custom -->

<div id="dataTables">


        <asp:ListView runat="server" ID="lvNewsLetters" DataKeyNames="NewsLetterId" OnPagePropertiesChanged="lvNewsLetters_PagePropertiesChanged">
            <LayoutTemplate>
                <table class="dTable responsive">
<thead>
<tr>
  <th><div>主题</div></th>
  <th><div>发送日期</div></th>
  
  <th><div>操作</div></th>
</tr>
</thead>

<tbody>
                               
                                    
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
    </tbody>
                                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Subject") %></td>
                    <td><%#Eval("DateSent", "{0:d}")%></td>
                    <td>
                        <a href='<%# "AddEditNewsletter.aspx?NewsletterID=" + Eval("NewsletterID") %>' class="btn btn-mini">
                            <i class="icon-eye-open"></i>
                            查看
                        </a>
                         <asp:LinkButton runat="server" ID="btnDelete" CommandArgument='<%# Eval("NewsletterID").ToString() %>'
                                            CommandName="Delete" AlternateText="Delete" CssClass="btn btn-danger btn-mini"
                                            OnClientClick="return confirm('警告：您确定要删除些分类！');" >
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
             $(".primary-sidebar ul.nav>li").eq(7).addClass('active');
         
           


        });
	</script>
</asp:Content>