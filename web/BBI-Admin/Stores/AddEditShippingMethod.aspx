<%@ Page Language="C#" MasterPageFile="~/BBI-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditShippingMethod.aspx.cs" Inherits="Admin_AddEditShippingMethod" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Content/Admin/Plugins/iCheck/skins/flat/blue.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="Server">
    <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-edit"></i>
             <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
          </h3>
          <h5>
            物流方式
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



    <div class="container-fluid padded">
    <div class="row-fluid">
  <div class="span12">

    <div class="box">
      <div class="box-header">
        <span class="title"><i class="icon-edit"></i> <asp:Literal runat="server" ID="ltlTitle3"></asp:Literal></span>
      </div>

      <div class="box-content">

        <section class="form-horizontal fill-up validatable pt20">

            <div class="control-group">
              <label class="control-label">主题</label>
              <div class="controls">
                  <asp:TextBox runat="server" CssClass="formField" ID="txtTitle"></asp:TextBox>
               
              </div>
            </div>


             <div class="control-group">
              <label class="control-label">价格</label>
              <div class="controls">
                  <asp:TextBox runat="server"  ID="txtPrice"></asp:TextBox>
               
              </div>
            </div>

             <div class="control-group">
											<label class="control-label">显示</label>
											<div class="controls">
												<asp:CheckBox ID="ActiveCheckBox" Checked="true" runat="server"  />     
											</div>
										</div>

             <div class="form-actions">
                  <asp:Button ID="cmdUpdate" runat="server"   CssClass="btn btn-primary"  Text="保存" OnClick="cmdUpdate_Click" />
                                            <asp:Button ID="cmdCancel" runat="server"  CssClass="btn"  OnClick="cmdCancel_Click" Text="返回" />
                                       
                                            <asp:Button ID="cmdDelete" Visible="False" runat="server" CssClass="btn btn-danger" CausesValidation="False" OnClick="cmdDelete_Click"
                                                Text="删除" OnClientClick="return confirm('Warning: This will delete the ShippingMethods from the database.');" />
                                        
                            <div class="pull-right">
              <asp:Literal runat="server" ID="ltlStatus"></asp:Literal>
            </div>
             

              <asp:Panel ID="plnote" runat ="server">
                   <hr />
                               ID：<asp:Literal runat="server" ID="ltlShippingMethodID"></asp:Literal>；
                           
                       发布日期：<asp:Literal runat="server" ID="ltlAddedDate"></asp:Literal>；
                           发布人：<asp:Literal runat="server" ID="ltlAddedBy"></asp:Literal>；
                            修改日期：<asp:Literal runat="server" ID="ltlUpdatedDate"></asp:Literal>；
                           修改人：<asp:Literal runat="server" ID="ltlUpdatedBy"></asp:Literal>
                           
                    
              </asp:Panel>               
                                       
          </div>

              
                       
                  
                  
                    
                      
                    
                        
                      
                
                              
           </section>

      </div>
    </div>

  </div>
</div>
</div>
</asp:Content>

