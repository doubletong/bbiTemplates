<%@ Page Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageOrders.aspx.cs" 
    Inherits="Admin_ManageOrders" Title="订单管理_商铺_{0}" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Content/Admin/Plugins/datepicker/css/datepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">

    
    
    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-th"></i>
            订单管理
          </h3>
          <h5>
            订单查询
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
            <i class="icon-file"></i> 订单管理
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
        
          <span class="title">查找订单</span>

      </div>
      <div class="box-content">

        <div id="dOrderFilter" class="padded">
            <p>
              按订单状态查询：<asp:DropDownList ID="ddlOrderStatuses" runat="server" DataTextField="Title" DataValueField="OrderStatusID"
                                    AutoPostBack="True" /></p>
             <p>按订单日期查询：
                 <asp:TextBox ID="txtFromDate" runat="server" data-date-format="yyyy-mm-dd" CssClass="input-small" />                                
                               ~
                                <asp:TextBox ID="txtToDate" runat="server" data-date-format="yyyy-mm-dd" CssClass="input-small" />
                               
                                <asp:Button ID="btnListByStatus" runat="server" Text="查询" CssClass="btn btn-primary" ValidationGroup="ListByStatus" />
                                <span class="text text-error" id="alert"></span>
                 
                
                                
                                </p>
                                <p>按客户帐号查询：
                                <asp:TextBox ID="txtCustomerName" runat="server" />
                                <asp:Button ID="btnListByCustomer" runat="server" Text="查询"  CssClass="btn btn-primary" ValidationGroup="ListByCustomer" />
                                <asp:RequiredFieldValidator ID="valRequireCustomerName" runat="server" ControlToValidate="txtCustomerName"
                                    SetFocusOnError="true" ValidationGroup="ListByCustomer" Text="<br />The Customer Name field is required."
                                    ToolTip="The Customer Name field is required." Display="Dynamic"></asp:RequiredFieldValidator>                                
                                </p>
                                <p>
                                    按订单编号查询：
                                <asp:TextBox ID="txtOrderID" runat="server" />
                                <asp:Button ID="btnOrderLookup" runat="server" Text="查询"  CssClass="btn btn-primary" ValidationGroup="OrderLookup" />
                                <asp:Label runat="server" ID="lblOrderNotFound" SkinID="FeedbackKO" Text="Order not found!"
                                    Visible="false" />
                                <asp:RequiredFieldValidator ID="valRequireOrderID" runat="server" ControlToValidate="txtOrderID"
                                    SetFocusOnError="true" ValidationGroup="OrderLookup" Text="<br />The Order ID field is required."
                                    ToolTip="The Order ID field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                          
                        </p>
 </div>
                        <asp:ListView ID="lvOrders" runat="server">
                            <LayoutTemplate>
                                <table cellspacing="0" cellpadding="0" class="AdminList">
                                    <tr class="AdminListHeader">
                                        <td>
                                        </td>
                                        <td>
                                            Items
                                        </td>
                                        <td>
                                            Cost
                                        </td>
                                        <td>
                                            Edit
                                        </td>
                                        <td>
                                            Delete
                                        </td>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div class="pager">
                                                <asp:DataPager ID="pagerBottom" runat="server" PageSize="15" PagedControlID="lvOrders">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                                        <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                                            NextPreviousButtonCssClass="command" />
                                                        <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <tr>
                                    <td colspan="5">
                                        <p>
                                            Sorry there are no Orders available at this time.</p>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Eval("AddedBy")%>
                                        on
                                        <%#String.Format("{0:d}", Eval("AddedDate"))%><br />
                                        Shipping:
                                        <%#Eval("ShippingMethod")%>
                                    </td>
                                    <td>
                                        <small>
                                            <asp:Repeater runat="server" ID="repOrderItems" DataSource='<%# Eval("OrderItems") %>'>
                                                <ItemTemplate>
                                                    <img src="../Images/ArrowR3.gif" border="0" alt="" />
                                                    [<%# Eval("SKU") %>]
                                                    <asp:HyperLink runat="server" ID="lnkProduct" Text='<%# Eval("Title") %>' NavigateUrl='<%# "~/ShowProduct.aspx?productID=" + Eval("ProductID") %>' />
                                                    - (<%# Eval("Quantity") %>)
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </small>
                                    </td>
                                    <td>
                                        Sub Total:
                                        <%#String.Format("{0:C2}", Eval("SubTotal"))%><br />
                                        Shipping:
                                        <%#String.Format("{0:C2}", Eval("Shipping"))%><br />
                                        Grand Total:
                                        <%#String.Format("{0:C2}", Eval("GrandTotal"))%>
                                    </td>
                                    <td align="center">
                                        <a href="<%# String.Format("AddEditOrder.aspx?OrderId={0}", Eval("OrderId")) %>">
                                            <img src="../images/edit.gif" alt="" width="16" height="16" class="AdminImg" /></a>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton runat="server" ID="btnDelete" CommandArgument='<%# Eval("OrderId").ToString() %>'
                                            CommandName="Delete" ImageUrl="~/images/delete.gif" AlternateText="Delete" CssClass="AdminImg"
                                            OnClientClick="return confirm('Warning: This will delete the Product from the database.');" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <tr>
                                    <td colspan="5">
                                        <hr width="80%" />
                                    </td>
                                </tr>
                            </SelectedItemTemplate>
                        </asp:ListView>
          
    
              
    </div>
  </div>
</div>
</div>
         
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" Runat="Server">
      <script src="/Content/Admin/js/jquery.sparkline.min.js"></script>

    <script src="/Content/Admin/Plugins/datepicker/js/bootstrap-datepicker.js"></script>
  
   

    <script>

        jQuery(document).ready(function () {
          
            var startDate = new Date(Date.parse($('#txtFromDate').val().replace('-', '/').replace('-', '/')));

            var endDate = new Date(Date.parse($('#txtToDate').val().replace("-", "/").replace("-", "/")));
           // alert(startDate);
            $('#txtFromDate').datepicker()
				.on('changeDate', function (ev) {
				    if (ev.date.valueOf() > endDate.valueOf()) {
				        $('#alert').show().text('开始日期不能大于结束日期');
				    } else {
				        $('#alert').hide();
				        startDate = new Date(ev.date);
				      //  $('#startDate').text($('#txtFromDate').data('date'));
				    }
				    $('#txtFromDate').datepicker('hide');
				});
            $('#txtToDate').datepicker()
				.on('changeDate', function (ev) {
				    if (ev.date.valueOf() < startDate.valueOf()) {
				        $('#alert').show().text('结束日期不能小于开始日期');
				    } else {
				        $('#alert').hide();
				        endDate = new Date(ev.date);
				      //  $('#endDate').text($('#txtToDate').data('date'));
				    }
				    $('#txtToDate').datepicker('hide');
				});



            var cli = $(".primary-sidebar ul.nav").find('li').eq(1);
            cli.addClass('active').find('a').eq(0).removeClass('collapsed');
            cli.find('ul').addClass('in').find('li').eq(2).addClass('active');
        });
	</script>
</asp:Content>