using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BBICMS.Store;

public partial class Controls_DepartmentsForSide : System.Web.UI.UserControl
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
            rpDeparts.DataSource = lDepartmentrpt.GetDepartments();

            rpDeparts.DataBind();
        }
    }
}