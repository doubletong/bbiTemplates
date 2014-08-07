using System;
using System.Web.Security;
using BBICMS.UI;

partial class Admin_AddEditRole : RoleAdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            
            MultiView1.ActiveViewIndex = 0;
            
            if (string.IsNullOrEmpty(RoleName) == false) {
                BindRoleInfo();
            }
            else {
                ClearRoleInfo();
                
            }
            
        }
    }
    
    
    protected void ClearRoleInfo()
    {
        
        txtRole.Text = string.Empty;
        flUser.Visible = false;
        lvUsers.Items.Clear();
            
        btnDelete.Visible = false;
    }
    
    protected void BindRoleInfo()
    {
        BindRoleInfo(RoleName);
    }
    
    protected void BindRoleInfo(string sRole)
    {
        
        if (Roles.RoleExists(sRole) == false) {
            txtRole.Text = string.Empty;
            
            return;
        }
        
        txtRole.Text = sRole;
        
        lvUsers.DataSource = Roles.GetUsersInRole(sRole);
            
            //MultiView1.ActiveViewIndex = 1
            
        lvUsers.DataBind();
    }
    
    protected void MigrateRole()
    {
        Roles.CreateRole(txtRole.Text);
        
        //To Do tHis we will need to migrate all the users to the new role and delete the old role.
        string[] sUsers = Roles.GetUsersInRole(RoleName);
        
        if (sUsers.Length > 0) {
            Roles.AddUsersToRole(sUsers, txtRole.Text);
            Roles.RemoveUsersFromRole(sUsers, RoleName);
        }
        
        Roles.DeleteRole(RoleName);
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(RoleName) == false) {
            if (string.IsNullOrEmpty(txtRole.Text) == false) {
                if (Roles.RoleExists(txtRole.Text) == false) {
                    Roles.CreateRole(txtRole.Text);
                    flUser.Visible = true;
                    btnDelete.Visible = true;
                    
                }
            }
        }
        else {
            //Change the name of the role
            if (txtRole.Text.ToLower() != RoleName.ToLower()) {
                MigrateRole();
            }
            
            //Do something here to indicate the action has completed.
            
        }
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteRole(txtRole.Text);
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageRoles.aspx");
    }
    
    
    protected void DeleteRole(string sRole)
    {
        
        if (Roles.GetUsersInRole(sRole).Length > 0) {
            Roles.RemoveUsersFromRole(Roles.GetUsersInRole(sRole), sRole);
        }
        
            
        Roles.DeleteRole(sRole);
    }
    
    protected void btnAddUserToRole_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ltlRole.Text = txtRole.Text;
    }
    
    protected void btnCancelAddUserToRole_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    
    protected void btnAddUserToRole2_Click(object sender, EventArgs e)
    {
        
        if ((Membership.GetUser(txtUser.Text) != null)) {
            Roles.AddUserToRole(txtUser.Text, ltlRole.Text);
            BindRoleInfo(ltlRole.Text);
            ltlStatus.Text = string.Format("{0} has been aded to the role {1}.", txtUser.Text, ltlRole.Text);
        }
        else {
            ltlStatus.Text = "Sorry, that user does not exist.";
            
        }
    }
    
    protected void lvUsers_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
    {
        Response.Write(e.CommandArgument);
    }
    
    protected void lvUsers_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
    {
        DeleteUserFromRole(lvUsers.Items[e.ItemIndex].DataItem.ToString());
    }
    
    protected void DeleteUserFromRole(string vUserName)
    {
        if (string.IsNullOrEmpty(RoleName) == false) {
            Roles.RemoveUserFromRole(vUserName, RoleName);
            ltlStatus.Text = string.Format("The user, {0} has been removed from {1}.", vUserName, RoleName);
        }
    }
    
}