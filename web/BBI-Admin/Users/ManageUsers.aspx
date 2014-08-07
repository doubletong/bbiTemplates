<%@ Page Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" 
    Inherits="Admin_ManageUsers" Title="会员管理_后台管理_{0}" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    
     <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-user"></i>
            会员中心
          </h3>
          <h5>
            会员管理中心
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
                <i class="icon-user"></i> 会员管理
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
          <a href="AddEditUser.aspx" class="btn btn-primary rightbtn"><i class="icon-edit"></i>创建会员</a>
          <span class="title">查找会员</span>

      </div>
      <div class="box-content">
        <div class="padded">
        <p>
                           
                            总注册会员：
                                <asp:Literal runat="server" ID="lblTotUsers" />，
                                在线会员：
                                <asp:Literal runat="server" ID="lblOnlineUsers" />
                          
                            </p>
            <p class="text text-info">
                根据会员名首字母查找会员：
            </p>
            <asp:ListView runat="server" ID="lvAlphabet" OnItemCommand="lvAlphabet_ItemCommand">
                                <LayoutTemplate>
                                    <p>
                                    <span runat="server" id="itemPlaceHolder" />
                                        </p>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <span>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Container.DataItem %>'
                                            CommandArgument='<%# Container.DataItem %>' />&nbsp;&nbsp;</span>
                                </ItemTemplate>
                            </asp:ListView>
            <p>根据用户名或email来搜索用户：</p>
            <p><asp:DropDownList runat="server" ID="ddlSearchTypes" CssClass="formStyleDropDown">
                                <asp:ListItem Text="UserName" Selected="true" />
                                <asp:ListItem Text="E-mail" />
                            </asp:DropDownList>
                            
                            <asp:TextBox runat="server" ID="txtSearchText" CssClass="formField" />
                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="搜索" />
                <asp:Literal runat="server" ID="ltlstatus" />

            </p>

             <asp:ListView ID="lvUsers" OnItemDeleting="lvUsers_ItemDeleting" 
                                DataKeyNames="UserName" OnItemCreated="lvUsers_ItemCreated" OnItemDataBound="lvUsers_ItemDataBound"
                                 runat="server">
                                <LayoutTemplate>
                                    <table class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>
                                                用户名
                                            </th>
                                            <th>
                                                Email
                                            </th>
                                            <th>
                                                创建日期
                                            </th>
                                            <th>
                                                最后活动
                                            </th>
                                            <th>
                                                激活
                                            </th>
                                            <th>
                                                操作
                                            </th>
                                        </tr>
                                    </thead>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </table>
                                </LayoutTemplate>
                                <EmptyDataTemplate>
                                    <tr>
                                        <td colspan="3">
                                            <p>
                                                Sorry there are no Users available at this time.</p>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                    <td>
                                        <%# Eval("UserName")%>
                                    </td>
                                    <td>
                                        <a href='<%# Eval("Email","mailto:{0}")%>'>
                                            <%# Eval("Email")%></a>
                                    </td>
                                    <td>
                                        <%# Eval("CreationDate", "{0:yyyy/MM/dd h:mm tt}")%>
                                    </td>
                                    <td>
                                        <%# Eval("LastActivityDate", "{0:yyyy/MM/dd h:mm tt}")%>
                                    </td>
                                    <td>
                                     
                                        <input type="checkbox" id="ckApproved" checked='<%# Eval("IsApproved") %>' disabled="disabled" runat="server" />
                                    </td>
                                    <td>
                                        <a class="btn btn-mini" href="<%# Eval("UserName","AddEditUser.aspx?CurrentUserName={0}")%>"><i
                                            class="icon-edit"></i> 编辑</a>
                                        <asp:LinkButton runat="server" ID="btnDelete" CssClass="btn btn-mini btn-danger" CommandName="Delete"
                                            AlternateText="删除管理员"><i class="icon-trash"></i> 删除</asp:LinkButton>
                                    </td>
                                </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="tblFooter">
                     
                          
                                <div id="pagination">
                                <asp:DataPager ID="pdPager" runat="server" PageSize="15" QueryStringField="Page" PagedControlID="lvUsers">
                                    <Fields>
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                <span class="mr30">页：
                                                    <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.TotalRowCount>0 ? (Container.StartRowIndex / Container.PageSize) + 1 : 0 %>" />
                                                    /
                                                    <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Math.Ceiling ((double)Container.TotalRowCount / Container.PageSize) %>" />，
                                                    总记录：<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                                    条 </span>
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                        <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="首页" ShowFirstPageButton="true"
                                            ShowNextPageButton="false" ShowPreviousPageButton="false" />
                                        <asp:NumericPagerField PreviousPageText="&lt; 前10页" NextPageText="后10页 &gt;" ButtonCount="10" />
                                        <asp:NextPreviousPagerField ButtonType="Link" LastPageText="尾页" ShowLastPageButton="true"
                                            ShowNextPageButton="false" ShowPreviousPageButton="false" />
                                    </Fields>
                                </asp:DataPager>
                                </div>
                                 
                                
                                 

                            </div>
                   
                        <!-- end .tblFooter -->
 </div>
                    
        
       </div>
    </div>
  </div>
</div>
</div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="/Content/Admin/js/jquery.sparkline.min.js"></script>
   
    <script>

        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            var cli = $(".primary-sidebar ul.nav >li").eq(8);
            cli.addClass('active').find('a').eq(0).removeClass('collapsed');
            cli.find('ul').addClass('in').find('li').eq(0).addClass('active');


        });
	</script>
</asp:Content>
