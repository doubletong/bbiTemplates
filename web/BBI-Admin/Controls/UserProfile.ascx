<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="Admin_Controls_UserProfile" %>
<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS.Web.UI" TagPrefix="cc1" %>

<section class="form-horizontal fill-up validatable">
            
         
<fieldset class="padded">
    <legend>
        基本信息
    </legend>

    <div class="control-group">
              <label class="control-label">邮件订阅</label>
              <div class="controls">
                  <asp:DropDownList runat="server" ID="ddlSubscriptions">
                <asp:ListItem Text="No subscription" Value="0" Selected="true" />
                <asp:ListItem Text="Subscribe to plain-text version" Value="1" />
                <asp:ListItem Text="Subscribe to HTML version" Value="2" />
            </asp:DropDownList>
              </div>
            </div>

    <div class="control-group">
              <label class="control-label">语言</label>
              <div class="controls">
                  <asp:DropDownList runat="server" ID="ddlLanguages">
                <asp:ListItem Text="中文" Value="zh-CN" Selected="true" />
                <asp:ListItem Text="English" Value="en-US" />
            </asp:DropDownList>
               
              </div>
            </div>
    </fieldset>


    <fieldset class="padded">
    <legend>
        论坛信息
    </legend>


    <div class="control-group">
              <label class="control-label">个人头像</label>
              <div class="controls">               
              
            <img alt="Avatar" src="<%= GetAvatarURL() %>" />      
            <a href="http://www.gravatar.com">More About Gravatar</a>
       
              </div>
            </div>

    <div class="control-group">
              <label class="control-label">头像URL</label>
              <div class="controls">
                  <asp:TextBox runat="server" ID="txtAvatarUrl" />
              </div>
            </div>

    <div class="control-group">
              <label class="control-label">签名</label>
              <div class="controls">
                  <asp:TextBox runat="server" ID="txtSignature"  MaxLength="500" TextMode="multiLine" CssClass="input-xxlarge" />
               
              </div>
            </div>
</fieldset>

    
    <fieldset class="padded">
    <legend>
        个人资料
    </legend>


    <div class="control-group">
              <label class="control-label">姓</label>
              <div class="controls">               
              
            <asp:TextBox ID="txtFullName" runat="server" ></asp:TextBox>
       
              </div>
            </div>



    <div class="control-group">
              <label class="control-label">性别</label>
              <div class="controls">
                  <asp:DropDownList runat="server" ID="ddlGenders">
                <asp:ListItem Text="Please select one..." Value="" Selected="True" />
                <asp:ListItem Text="Male" Value="M" />
                <asp:ListItem Text="Female" Value="F" />
            </asp:DropDownList>
               
              </div>
            </div>


        <div class="control-group">
              <label class="control-label">生日</label>
              <div class="controls">
                 <asp:TextBox ID="txtBirthDate" runat="server" Width="99%" Text='<%# DOB %>'></asp:TextBox>
            <asp:CompareValidator runat="server" ID="valBirthDateType" ControlToValidate="txtBirthDate"
                SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date"
                ErrorMessage="The format of the birth date is not valid." ToolTip="The format of the birth date is not valid."
                ValidationGroup="EditProfile">The format of the birth date is not valid.</asp:CompareValidator>
               
              </div>
            </div>




        <div class="control-group">
              <label class="control-label">职业</label>
              <div class="controls">
                  <asp:DropDownList ID="ddlOccupations" runat="server" Width="99%">
                <asp:ListItem Text="Please select one..." Value="" Selected="True" />
                <asp:ListItem Text="Academic" />
                <asp:ListItem Text="Accountant" />
                <asp:ListItem Text="Actor" />
                <asp:ListItem Text="Architect" />
                <asp:ListItem Text="Artist" />
                <asp:ListItem Text="Business Manager" />
                <asp:ListItem Text="Carpenter" />
                <asp:ListItem Text="Chief Executive" />
                <asp:ListItem Text="Cinematographer" />
                <asp:ListItem Text="Civil Servant" />
                <asp:ListItem Text="Coach" />
                <asp:ListItem Text="Composer" />
                <asp:ListItem Text="Computer programmer" />
                <asp:ListItem Text="Cook" />
                <asp:ListItem Text="Counsellor" />
                <asp:ListItem Text="Doctor" />
                <asp:ListItem Text="Driver" />
                <asp:ListItem Text="Economist" />
                <asp:ListItem Text="Editor" />
                <asp:ListItem Text="Electrician" />
                <asp:ListItem Text="Engineer" />
                <asp:ListItem Text="Executive Producer" />
                <asp:ListItem Text="Fixer" />
                <asp:ListItem Text="Graphic Designer" />
                <asp:ListItem Text="Hairdresser" />
                <asp:ListItem Text="Headhunter" />
                <asp:ListItem Text="HR - Recruitment" />
                <asp:ListItem Text="Information Officer" />
                <asp:ListItem Text="IT Consultant" />
                <asp:ListItem Text="Journalist" />
                <asp:ListItem Text="Lawyer / Solicitor" />
                <asp:ListItem Text="Lecturer" />
                <asp:ListItem Text="Librarian" />
                <asp:ListItem Text="Mechanic" />
                <asp:ListItem Text="Model" />
                <asp:ListItem Text="Musician" />
                <asp:ListItem Text="Office Worker" />
                <asp:ListItem Text="Performer" />
                <asp:ListItem Text="Photographer" />
                <asp:ListItem Text="Presenter" />
                <asp:ListItem Text="Producer / Director" />
                <asp:ListItem Text="Project Manager" />
                <asp:ListItem Text="Researcher" />
                <asp:ListItem Text="Salesman" />
                <asp:ListItem Text="Social Worker" />
                <asp:ListItem Text="Soldier" />
                <asp:ListItem Text="Sportsperson" />
                <asp:ListItem Text="Student" />
                <asp:ListItem Text="Teacher" />
                <asp:ListItem Text="Technical Crew" />
                <asp:ListItem Text="Technical Writer" />
                <asp:ListItem Text="Therapist" />
                <asp:ListItem Text="Translator" />
                <asp:ListItem Text="Waitress / Waiter" />
                <asp:ListItem Text="Web designer / author" />
                <asp:ListItem Text="Writer" />
                <asp:ListItem Text="Other" />
            </asp:DropDownList>
               
              </div>
            </div>

        <div class="control-group">
              <label class="control-label">主页</label>
              <div class="controls">
                   <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
               
              </div>
            </div>

       
</fieldset>
          
    
    
    <fieldset class="padded">
    <legend>
        联系方式
    </legend>


    <div class="control-group">
              <label class="control-label">街道</label>
              <div class="controls">               
              
          <asp:TextBox runat="server" ID="txtStreet" Width="99%" />
              </div>
            </div>

    <div class="control-group">
              <label class="control-label">邮政编码</label>
              <div class="controls">
                 <asp:TextBox runat="server" ID="txtPostalCode" Width="99%" Text='<%# PostCode %>' />
              </div>
            </div>

    <div class="control-group">
              <label class="control-label">城市</label>
              <div class="controls">
                   <asp:TextBox runat="server" ID="txtCity" Width="99%" />
               
              </div>
            </div>

        <div class="control-group">
              <label class="control-label">国家</label>
              <div class="controls">
                    <cc1:CountryDropDownList ID="ddlCountry" runat="server" Width="99%">
            </cc1:CountryDropDownList>
               
              </div>
            </div>

        <div class="control-group">
              <label class="control-label">地区</label>
              <div class="controls">
                  <cc1:StateDropDownList ID="ddlState" runat="server" Width="99%">
            </cc1:StateDropDownList>
               
              </div>
            </div>


        <div class="control-group">
              <label class="control-label">电话</label>
              <div class="controls">
                  <asp:TextBox runat="server" ID="txtPhone" Width="99%" />
              </div>
            </div>


        <div class="control-group">
              <label class="control-label">传真</label>
              <div class="controls">
                  <asp:TextBox runat="server" ID="txtFax" Width="99%" />
               
              </div>
            </div>


        
</fieldset>
     
</section>