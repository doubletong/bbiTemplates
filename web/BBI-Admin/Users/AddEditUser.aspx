<%@ Page Title="编辑会员_后台管理_{0}" Language="C#" MasterPageFile="~/bbi-Admin/Admin.master" AutoEventWireup="true" CodeFile="AddEditUser.aspx.cs" Inherits="Admin_Users_AddEditUser" %>
<%@ Register Src="~/bbi-Admin/Controls/UserProfile.ascx" TagPrefix="bbi" TagName="UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" Runat="Server">
    
     <div class="container-fluid">
    <div class="row-fluid">

      <div class="area-top clearfix">
        <div class="pull-left header">
          <h3 class="title">
            <i class="icon-user"></i>
            会员管理
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
                <i class="icon-user"></i> 会员中心
              </span>
              <span class="breadcrumb-arrow"><span></span></span>
            </div>

            <div class="breadcrumb-button">
              <span class="breadcrumb-label">
                <i class="icon-edit"></i> 编辑会员
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
        <ul class="nav nav-tabs nav-tabs-left">
          <li class="active"><a href="#home" data-toggle="tab"><i class="icon-user"></i> <span>用户信息</span></a></li>
          <li><a href="#messages" data-toggle="tab"><i class="icon-key"></i> <span>角色信息</span></a></li>
          <li><a href="#settings" data-toggle="tab"><i class="icon-info-sign"></i> <span>个人资料</span></a></li>
        </ul>

        <div class="title">Settings</div>
      </div>

      <div class="box-content">
        <div class="tab-content">
          <div class="tab-pane active" id="home">
            <ul class="box-list">

              <li>
                <span><i class="icon-user"></i> 用户名：</span>
                <span class="pull-right">
                    <asp:Literal runat="server" ID="ltlUserName"></asp:Literal>
                                
                </span>
              </li>

              <li>
                <span><i class="icon-money"></i> E-Mail：</span>
                <span class="pull-right">
                  <asp:Literal runat="server" ID="ltlEMail"></asp:Literal>
                </span>
              </li>

              <li>
                <span><i class="icon-upload"></i> 注册时间：</span>
                <span class="pull-right">
                  <asp:Literal runat="server" ID="lblRegistered" />
                </span>
              </li>

                 <li>
                <span><i class="icon-upload"></i> 最后登录：</span>
                <span class="pull-right">
                  <asp:Literal runat="server" ID="lblLastLogin" />
                </span>
              </li>
                
              <li>
                <span><i class="icon-upload"></i> 最后激活：</span>
                <span class="pull-right">
                  <asp:Literal runat="server" ID="lblLastActivity" />
                </span>
              </li>
                 <li>
                <span><i class="icon-upload"></i> 是否在线</span>
                <span class="pull-right">
                  <asp:CheckBox runat="server" CssClass="iButton-icons-tab" OnCheckedChanged="chkOnlineNow_CheckedChanged" ID="chkOnlineNow" Enabled="false" />
                </span>
              </li>
                
                 <li>
                <span><i class="icon-upload"></i> 审核</span>
                <span class="pull-right">
                  <asp:CheckBox runat="server" CssClass="iButton-icons-tab" ID="chkApproved" OnCheckedChanged="chkApproved_CheckedChanged" AutoPostBack="true" />
                </span>
              </li>

                 <li>
                <span><i class="icon-upload"></i> 锁定</span>
                <span class="pull-right">
                  <asp:CheckBox runat="server" CssClass="iButton-icons-tab" ID="chkLockedOut" OnCheckedChanged="chkLockedOut_CheckedChanged" AutoPostBack="true" />
                </span>
              </li>

            </ul>
          </div>

          <div class="tab-pane" id="messages">
              <asp:CheckBoxList ID="cblRoles" CssClass="box-list" RepeatLayout="UnorderedList" runat="server">
                        </asp:CheckBoxList>
           
              <div class="box-footer flat padded">
    <!-- chat send message module. you can use it with or without a form/submit button or just a link -->
    
      <div class="input-append">
        <asp:TextBox ID="txtNewRole" placeholder="新角色" runat="server"></asp:TextBox> 
        <ul class="add-on">
          <li>
            <a href="#"><i class="icon-user"></i></a>
          </li>
          <li>
            <asp:Button ID="btnNewRole" class="btn btn-blue" runat="server" OnClick="btnNewRole_Click"  Text="添加角色" />
            
          </li>
        </ul>
      </div>
  
  </div>
              <div class="box-footer flat padded">
                  <asp:Button ID="btnUpdateRoles" runat="server" Text="更新角色" OnClick="btnUpdateRoles_Click" CssClass="btn btn-primary" Visible="False" />
                        <br />
                        <asp:Label ID="lblRolesFeedbackOK" runat="server" SkinID="FeedbackOK" Text="Roles updated successfully"
                            Visible="False"></asp:Label>
                  </div>
                          
          </div>

          <div class="tab-pane" id="settings">
              <bbi:UserProfile runat="server" id="UserProfile1" />

          </div>
        </div>

        <div class="box-footer padded">
          <span class="pull-right">
       
              <asp:LinkButton ID="lbUpdate" CssClass="btn btn-primary btn-small" OnClick="lbUpdate_Click" runat="server">保存</asp:LinkButton>
              <asp:LinkButton ID="lbDelete" CssClass="btn btn-danger btn-small" OnClick="lbDelete_Click" runat="server" CausesValidation="false">删除</asp:LinkButton>
              <asp:LinkButton ID="lbCancel" CssClass="btn  btn-small" runat="server" OnClick="lbCancel_Click" CausesValidation="false">返回</asp:LinkButton>
                          
              <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
              <asp:Literal runat="server" ID="ltlStatus"></asp:Literal>
          </span>
        </div>
      </div>

    </div>


            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" Runat="Server">
</asp:Content>

