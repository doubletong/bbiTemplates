using System;
using System.Collections.Generic;

using System.Web.UI.WebControls;
using BBICMS.Store;
using BBICMS;
using BBICMS.UI;

partial class Admin_ManageDepartments : AdminPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = string.Format(this.Title, Helpers.Settings.SiteName);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack) {
            BindDepartments();
            
        }
    }
    
    protected void BindDepartments()
    {
        using (DepartmentRepository lDepartmentsrpt = new DepartmentRepository()) {
            
            List<Department> lDepartments = lDepartmentsrpt.GetDepartments();
            lvDepartments.DataSource = lDepartments;
            lvDepartments.DataBind();
            
            //DataPager pagerBottom = (DataPager)lvDepartments.FindControl("pagerBottom");
            
            //if ((pagerBottom != null)) {
            //    if (lDepartments.Count <= pagerBottom.PageSize) {
            //        pagerBottom.Visible = false;
            //    }
            //    else {
            //        pagerBottom.Visible = true;
            //    }
            //}
        }
    }
    
    protected void lvDepartments_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindDepartments();
    }

    protected void lvDepartments_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Show":

                UnDeleteCategory(Int32.Parse(e.CommandArgument.ToString()));
                // Response.Write(e.CommandArgument.ToString());
                break;

            case "Hide":

                DeleteCategory(Int32.Parse(e.CommandArgument.ToString()));
                break;

        }
    }

    protected void DeleteCategory(int vDepartmentId)
    {
        using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
        {
            lDepartmentrpt.DeleteDepartment(lDepartmentrpt.GetDepartmentById(vDepartmentId));
        }
        BindDepartments();
    }

    protected void UnDeleteCategory(int vDepartmentId)
    {
        using (DepartmentRepository lCategoryrpt = new DepartmentRepository())
        {
            lCategoryrpt.UnDeleteDepartment(lCategoryrpt.GetDepartmentById(vDepartmentId));
        }
        BindDepartments();
    }


    protected void lvDepartments_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        RomeveDepartment(Int32.Parse(lvDepartments.DataKeys[e.ItemIndex].Value.ToString()));
        BindDepartments();
    }

    protected void RomeveDepartment(int vDepartmentID)
    {

        using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
        {
            Department vDepartment = lDepartmentrpt.GetDepartmentById(vDepartmentID);

            if (vDepartment.Products.Count > 0)
            {
                Helpers.ShowMsg("删除分类", "分类下面还有商品，请先删除。");
            }
            else
            {
                lDepartmentrpt.RemoveDepartment(vDepartmentID);
            }

        }
    }
    
}