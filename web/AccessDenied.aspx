<%@ Page Language="C#" MasterPageFile="~/TBHMain.master"  Theme="DarkBeer" AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs" Inherits="AccessDenied" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="sitepath"><i class="icon-home"></i> <a href="/">首页</a> / 登录</div>


     <div id="ContentBody">
      
        <asp:Login runat="server" ID="adLogin" CreateUserUrl="~/Register.aspx" RenderOuterTable="false">
            <LayoutTemplate>
                <div class="pure-form">
                      <fieldset>
                    <legend>用户登录</legend>
                    <asp:TextBox ID="UserName" runat="server" placeholder="用户名" />
                     <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="密码"/>
                           <label for="remember">
                           <asp:CheckBox ID="RememberMe" runat="server" Text="记住我">
                            </asp:CheckBox>
                         </label>
                          <asp:LinkButton ID="Submit" runat="server" CommandName="Login" CssClass="pure-button pure-button-primary"
                                 ValidationGroup="Login" Text="登录"  />
                          <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Register.aspx" CssClass="pure-button">注册新帐号</asp:HyperLink>
                    <asp:HyperLink ID="lnkPasswordRecovery" runat="server" NavigateUrl="~/PasswordRecovery.aspx" CssClass="pure-button">忘记密码</asp:HyperLink>
                          </fieldset>
                </div>
               
                     
                            <asp:RequiredFieldValidator ID="valRequireUserName" runat="server" SetFocusOnError="True"
                                ControlToValidate="UserName" Text="*" ValidationGroup="Login" meta:resourcekey="valRequireUserNameResource1" />
                     
                            <asp:RequiredFieldValidator ID="valRequirePassword" runat="server" SetFocusOnError="True"
                                ControlToValidate="Password" Text="*" ValidationGroup="Login" meta:resourcekey="valRequirePasswordResource1" />
              
    
            </LayoutTemplate>
        </asp:Login>
         <div class="p15">
             <asp:Label runat="server" ID="lblLoginRequired" CssClass="text-info">
You must be a registered user to access this page. If you already have an account, please login with
your credentials in the box on the upper-right corner. Otherwise <a href="Register.aspx">click here</a> to register now for free.
             </asp:Label>
             <asp:Label runat="server" ID="lblInsufficientPermissions" CssClass="text-error">
Sorry, the account you are logged with does not have the permissions required to access this page.
             </asp:Label>
             <asp:Label runat="server" ID="lblInvalidCredentials" CssClass="text-info">
The submitted credentials are not valid. Please check they are correct and try again. 
If you forgot your password, <a href="PasswordRecovery.aspx">click here</a> to recover it.
             </asp:Label>
         </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHFooter" runat="Server">
   
</asp:Content>
