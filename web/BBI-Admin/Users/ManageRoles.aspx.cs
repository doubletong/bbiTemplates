using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using BBICMS.UI;

partial class Admin_ManageRoles : RoleAdminPage
{
    protected void Page_Load1(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRoles();
        }
    }

    protected void BindRoles()
    {
        string[] sRoles = Roles.GetAllRoles();

        lvRoles.DataSource = sRoles;

        lvRoles.DataBind();
    }

    protected void DeleteRole(string sRole)
    {
        if (Roles.GetUsersInRole(sRole).Length > 0)
        {
            Roles.RemoveUsersFromRole(Roles.GetUsersInRole(sRole), sRole);
        }


        Roles.DeleteRole(sRole);
    }


    protected void btnNewUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditRole.aspx");
    }


    protected void lvRoles_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        DeleteRole(lvRoles.Items[e.ItemIndex].DataItem.ToString());
    }

    protected void DeleteRoles(string vRole)
    {
        if (Roles.GetUsersInRole(vRole).Length > 0)
        {
            Roles.RemoveUsersFromRole(Roles.GetUsersInRole(vRole), vRole);
        }
        Roles.DeleteRole(vRole);
        BindRoles();
    }

    protected void lvRoles_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        BindRoles();
    }
}