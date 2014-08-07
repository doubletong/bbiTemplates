using System;
using BBICMS.Store;
using BBICMS.UI;

public partial class ShowDepartments : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDepartments();
        }
    }

    protected void BindDepartments()
    {
        using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
        {
            lvDepartments.DataSource = lDepartmentrpt.GetDepartments();

            lvDepartments.DataBind();
        }
    }
}