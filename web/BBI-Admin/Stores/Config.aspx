<%@ Page Title="配置_电子商务_{0}" Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" AutoEventWireup="true" CodeFile="Config.aspx.cs" Inherits="BBI_Admin_Stores_Config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-edit"></i>
             基础配置
          </h3>
          <h5>
            电子商铺基础配置
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
            <i class="icon-cog"></i> 基础配置
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
        <span class="title"><i class="icon-edit"></i> 基础配置</span>
      </div>

      <div class="box-content">

        <section class="form-horizontal fill-up validatable pt20">

            <div class="control-group">
                        <label class="control-label">
                            分页设置</label>
                        <div class="controls">
                            <asp:TextBox ID="txtPageSize" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            目录图片保存路径</label>
                        <div class="controls">
                            <asp:TextBox ID="txtIndexImg"  runat="server" CssClass="input-xxlarge"  Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            目录图片宽度</label>
                        <div class="controls">
                            <asp:TextBox ID="txtIWidth"  runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            目录图片高度</label>
                        <div class="controls">
                            <asp:TextBox ID="txtIHeight"  runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="control-group">
                        <label class="control-label">
                            组图缩略图保存路径</label>
                        <div class="controls">
                            <asp:TextBox ID="txtThumbUrl" runat="server" CssClass="input-xxlarge"  Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            组图缩略图宽度</label>
                        <div class="controls">
                            <asp:TextBox ID="txtThumbWidth"  runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            组图缩略图高度</label>
                        <div class="controls">
                            <asp:TextBox ID="txtThumbHeigh" runat="server" ></asp:TextBox>
                        </div>
                    </div>

                    <div class="control-group">
                        <label class="control-label">
                            组图保存路径</label>
                        <div class="controls">
                            <asp:TextBox ID="txtPhotoUrl" runat="server" CssClass="input-xxlarge" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            组图宽度</label>
                        <div class="controls">
                            <asp:TextBox ID="txtPWidth" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            组图高度</label>
                        <div class="controls">
                            <asp:TextBox ID="txtPHeight"  runat="server" ></asp:TextBox>
                        </div>
                    </div>

                                     <div class="form-actions">
                                         <div class="mb10">
                                             <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="btnSave_Click" />
                                            
                                             <a href="ManageProducts.aspx" class="btn"><i class="icon-reply"></i> 返回</a>
                                         </div>
                                         <asp:Panel ID="plStatus" Visible="false" runat="server">
                                             <button type="button" class="close" data-dismiss="alert">
                                                 ×</button>
                                             <asp:Literal ID="ltlStatus" runat="server"></asp:Literal>
                                         </asp:Panel>
                                     </div>


              </section>

      </div>
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
            cli.find('ul').addClass('in').find('li').eq(5).addClass('active');
        });
	</script>
</asp:Content>

