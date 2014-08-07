<%@ Page Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" CodeFile="AddEditProduct.aspx.cs" 
Inherits="Admin_AddEditProduct" Title="添加或修改商品_商铺_后台管理_{0}" ValidateRequest ="false" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Content/Admin/Plugins/bootstrap-fileupload/bootstrap-fileupload.min.css" rel="stylesheet" />
    <link href="/Content/Admin/Plugins/iCheck/skins/flat/blue.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="AdminContent" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-edit"></i>
             <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
          </h3>
          <h5>
            商品按品种分类
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
            <i class="icon-edit"></i> <asp:Literal runat="server" ID="ltlTitle2"></asp:Literal>
          </span>
          <span class="breadcrumb-arrow"><span></span></span>
        </div>
      </div>
    </div>
  </div>

<!-- 表单 -->
  <div class="container-fluid padded">
    <div class="row-fluid">
  <div class="span12">

    <div class="box">
      <div class="box-header">
        <span class="title"><i class="icon-edit"></i> <asp:Literal runat="server" ID="ltlTitle3"></asp:Literal></span>
      </div>

      <div class="box-content">

        <section class="form-horizontal fill-up validatable">
            
          <div class="padded">
              <div class="edit_thumb"><asp:Image runat="server" ID="iThumbnail" /></div>


              

              <div class="control-group">
              <label class="control-label">商品名称</label>
              <div class="controls">
                  <asp:TextBox runat="server" CssClass="required input-xlarge" ID="txtTitle"></asp:TextBox>
               
              </div>
            </div>

            <div class="control-group">
              <label class="control-label">简短名称</label>
              <div class="controls">
                   <asp:TextBox runat="server" CssClass="required" ID="txtSKU"></asp:TextBox>               
              </div>
            </div>


              <div class="control-group">
              <label class="control-label">商品分类</label>
              <div class="controls">                  
                <asp:DropDownList runat="server" ID="ddlDepartment" title="请选择商品分类" required=""></asp:DropDownList>
              </div>
              </div>

               <div class="control-group">
              <label class="control-label">单价</label>
              <div class="controls">                  
                <asp:TextBox runat="server" CssClass="required number" min="0" ID="txtUnitPrice"></asp:TextBox>
              </div>
              </div>

              <div class="control-group">
              <label class="control-label">商品折扣</label>
              <div class="controls">                  
                <asp:TextBox runat="server" CssClass="required number" range="0,100" ID="txtDiscountPercentage"></asp:TextBox>
              </div>
              </div>

              <div class="control-group">
              <label class="control-label">库存</label>
              <div class="controls">                  
                <asp:TextBox runat="server" CssClass="number" min="1" ID="txtUnitsInStock"></asp:TextBox>
              </div>
              </div>

              <div class="control-group">
              <label class="control-label">照片</label>
              <div class="controls">                  
                
                   <div class="fileupload fileupload-new" data-provides="fileupload">
                                                    <div class="input-append">
                                                        <div class="uneditable-input span3">
                                                            <i class="icon-file fileupload-exists"></i><span class="fileupload-preview"></span>
                                                        </div>
                                                        <span class="btn btn-file"><span class="fileupload-new">选择文件</span><span class="fileupload-exists">重新选择</span><asp:FileUpload runat="server" ID="fThumbnail" accept="image/*" /></span><a href="#" class="btn fileupload-exists"
                                                                data-dismiss="fileupload">移除</a>
                                                    </div>
                                                </div>
                  <asp:HiddenField ID="HiddenField1" runat="server" />
              </div>
              </div>


              <div class="control-group">
              <label class="control-label">商品描述</label>
              <div class="controls">
                  <FCKeditorV2:FCKeditor ID="txtDescription" runat="server" ToolbarSet="BBICMS"
                                    Height="400px" Width="100%" BasePath="~/fckeditor/" />
               
              </div>
            </div>

             


              <div class="control-group">
											<label class="control-label">显示</label>
											<div class="controls">
												<asp:CheckBox ID="cbActive" Checked="true" runat="server"  />     
											</div>
										</div>

</div>
              <div class="form-actions">
                  <div class="pull-right">
                      <asp:Literal runat="server" ID="ltlStatus" />
                  </div>
                  <asp:Button ID="cmdUpdate" runat="server" OnClick="cmdUpdate_Click" Text="保存" CssClass="btn btn-primary" />
                  <asp:Button ID="cmdDelete" Visible="False" runat="server" OnClick="cmdDelete_Click" CausesValidation="False" CssClass="btn btn-danger"
                                                Text="删除" OnClientClick="return confirm('Warning: This will delete the Products from the database.');" />
                  <a href="ManageProducts.aspx" class="btn" ><i class="icon-chevron-sign-left icon-fixed-width"></i>返回</a>
                   <asp:Panel ID="plnote" runat ="server">
                       <hr />
                       ID：<asp:Literal runat="server" ID="ltlProductID"></asp:Literal>；
                           票数：<asp:Literal runat="server" ID="ltlVotes"></asp:Literal>；
                            评分：<asp:Literal runat="server" ID="ltlTotalRating"></asp:Literal>；
                            发布日期：<asp:Literal runat="server" ID="ltlAddedDate"></asp:Literal>；
                            发布者：<asp:Literal runat="server" ID="ltlAddedBy"></asp:Literal>；
                            修改日期：<asp:Literal runat="server" ID="ltlUpdatedDate"></asp:Literal>；
                            修改者：<asp:Literal runat="server" ID="ltlUpdatedBy"></asp:Literal>
                   </asp:Panel>
              </div>

              </section>

      </div>
    </div>

  </div>
</div>
</div>

                    
                 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="/Content/Admin/js/jquery.sparkline.min.js"></script>

    <script src="/Content/Admin/Plugins/jquery-validation/jquery.validate.js"></script>
    <script src="/Content/Admin/Plugins/jquery-validation/additional-methods.js"></script>
    <script src="/Content/Admin/Plugins/jquery-validation/localization/messages_zh.js"></script>
    <script src="/Content/Admin/Plugins/bootstrap-fileupload/bootstrap-fileupload.min.js"></script>
    <script src="/Content/Admin/Plugins/iCheck/jquery.icheck.min.js"></script>

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

            $('input').iCheck({
                checkboxClass: 'icheckbox_flat-blue'
                //  radioClass: 'iradio_flat-blue'
            });

            var cli = $(".primary-sidebar ul.nav").find('li').eq(1);
            cli.addClass('active').find('a').eq(0).removeClass('collapsed');
            cli.find('ul').addClass('in').find('li').eq(0).addClass('active');
        });
	</script>
</asp:Content>
