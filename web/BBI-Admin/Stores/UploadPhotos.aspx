<%@ Page Title="上传组图_电子商务_{0}" Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" AutoEventWireup="true" CodeFile="UploadPhotos.aspx.cs" Inherits="BBI_Admin_Stores_UploadPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/Content/Admin/Plugins/bootstrap-fileupload/bootstrap-fileupload.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-edit"></i>
             组图上传
          </h3>
          <h5>
            全方位展示产品
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
                <i class="icon-th"></i> 产品管理
              </span>
              <span class="breadcrumb-arrow"><span></span></span>
            </div>
        

        <div class="breadcrumb-button">
          <span class="breadcrumb-label">
            <i class="icon-upload"></i> 上传组图
          </span>
          <span class="breadcrumb-arrow"><span></span></span>
        </div>
      </div>
    </div>
  </div>

     <div class="container-fluid padded">
    <div class="row-fluid">
  <div class="span12">

    <div class="box">
      <div class="box-header">
        <span class="title"><i class="icon-edit"></i> 上传组图</span>
      </div>

      <div class="box-content">

          <asp:ListView ID="lvPhotos" runat="server"  DataKeyNames="PhotoID" GroupItemCount="5"
     GroupPlaceholderID="groupPlaceHolder" ItemPlaceholderID="itemPlaceHolder"
     OnItemDeleting="lvPhotos_ItemDeleting">
        <LayoutTemplate>
            <table class="table table-bordered table-striped">                               
                   <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>                
            </table>
        </LayoutTemplate>
        <EmptyDataTemplate>
                <div class="text text-info padded">
                <i class="icon-info-sign"></i> 没有找到任何图片。
            </div>
        </EmptyDataTemplate>
        <GroupTemplate>
            <tr>
                 <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <td>
                <p><a href="<%# BBICMS.StoreHelper.GetPhotosUrl(Eval("OriginalPic").ToString())%>" class="thickbox" title="<%# Eval("ProductID")%>">
                    <img src='<%# BBICMS.StoreHelper.GetPhotosThumbUrl(Eval("Thumbnail").ToString())%>'
                        alt='<%# Eval("ProductID")%>' /></a></p>
                               
                    <asp:LinkButton runat="server" ID="lbtnDelete" CommandArgument='<%# Eval("PhotoID").ToString() %>'
                        CommandName="Delete" Visible='<%# Eval("CanDelete")%>' CssClass="btn btn-mini btn-danger"
                        ToolTip="删除" CausesValidation="false" OnClientClick="return confirm('警告: 确定要删除这条记录吗？');">
                        <i class="icon-trash"></i> 删除
                        </asp:LinkButton>
            </td>
        </ItemTemplate>
    </asp:ListView>
           <div class="form-horizontal">
                                        <div class="control-group">
                                            <label class="control-label">
                                                上传组图</label>
                                            <div class="controls">
                                                <div class="fileupload fileupload-new" data-provides="fileupload">
                                                    <div class="input-append">
                                                        <div class="uneditable-input span3">
                                                            <i class="icon-file fileupload-exists"></i><span class="fileupload-preview"></span>
                                                        </div>
                                                        <span class="btn btn-file"><span class="fileupload-new">选择文件</span><span class="fileupload-exists">重新选择</span><asp:FileUpload
                                                            ID="FileUpload1" accept="image/*"  runat="server" /></span><a href="#" class="btn fileupload-exists"
                                                                data-dismiss="fileupload">移除</a>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>
                                        </div>

                                   

                                       
                                     <div class="form-actions">
                                         <div class="mb10">
                                            <asp:Button ID="btnUpload" runat="server" Text="上传照片" CssClass="btn btn-primary"
        onclick="btnUpload_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="全部删除" CssClass="btn btn-danger" 
                onclick="btnDelete_Click" />
                                             <a href="ManageProducts.aspx" class="btn"><i class="icon-reply"></i> 返回</a>
                                         </div>
                                         <asp:Panel ID="plStatus" Visible="false" runat="server">
                                             <button type="button" class="close" data-dismiss="alert">
                                                 ×</button>
                                             <asp:Literal ID="ltlStatus" runat="server"></asp:Literal>
                                         </asp:Panel>
                                     </div>

									
                                    </div>



      </div>
        <!-- end box-content -->

    </div>

  </div>
</div>
</div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" Runat="Server">
    <script src="/Content/Admin/js/jquery.sparkline.min.js"></script>

    <script src="/Content/Admin/Plugins/jquery-validation/jquery.validate.js"></script>
    <script src="/Content/Admin/Plugins/jquery-validation/additional-methods.js"></script>
    <script src="/Content/Admin/Plugins/jquery-validation/localization/messages_zh.js"></script>
    <script src="/Content/Admin/Plugins/bootstrap-fileupload/bootstrap-fileupload.min.js"></script>
   <script>

       jQuery(document).ready(function () {
           // binds form submission and fields to the validation engine
           jQuery("form").validate({
               errorClass: "help-inline",
               errorElement: "span",
               highlight: function (element, errorClass, validClass) {
                   $(element).parents('.control-group').addClass('error');
               },
               unhighlight: function (element, errorClass, validClass) {
                   $(element).parents('.control-group').removeClass('error');
                   $(element).parents('.control-group').addClass('success');
               }

           });

         
           var cli = $(".primary-sidebar ul.nav").find('li').eq(1);
           cli.addClass('active').find('a').eq(0).removeClass('collapsed');
           cli.find('ul').addClass('in').find('li').eq(0).addClass('active');
       });
	</script>
</asp:Content>

