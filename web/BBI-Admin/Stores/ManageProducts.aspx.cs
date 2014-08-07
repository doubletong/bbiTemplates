using System.Collections.Generic;
using System.Web.UI.WebControls;
using System;
using BBICMS.Store;
using BBICMS;
using BBICMS.UI;

partial class Admin_ManageProducts : AdminPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = string.Format(this.Title, Helpers.Settings.SiteName);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
          //  BindDepartmentsToListControl(ddlDepartments, true);
            BindProducts();
            
        }
    }
    
    protected void BindProducts()
    {
        using (ProductsRepository lProductsrpt = new ProductsRepository()) {
            
            List<Product> lProducts;
            
            if (DepartmentId > 0) {
                lProducts = lProductsrpt.GetProductsByDepartment(DepartmentId);
            }
            else {
                lProducts = lProductsrpt.GetProducts();
            }
            
            lvProducts.DataSource = lProducts;
            lvProducts.DataBind();
            
            //DataPager pagerBottom = (DataPager)lvProducts.FindControl("pagerBottom");
            
            //pagerBottom.PageSize = int.Parse(ddlProductsPerPage.SelectedValue);
            
            //if ((pagerBottom != null)) {
            //    if (lProducts.Count <= pagerBottom.PageSize) {
            //        pagerBottom.Visible = false;
            //    }
            //    else {
            //        pagerBottom.Visible = true;
            //    }
            //}
        }
    }


    protected void lvProducts_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int vProductID = int.Parse(lvProducts.DataKeys[e.ItemIndex].Value.ToString());
        PhotoRepository phototry = new PhotoRepository();
        if (phototry.GetPhotosByProduct(vProductID).Count > 0)
        {
            Helpers.ShowMsg("删除产品", "请先删除组图");
        }
        else
        {
            using (ProductsRepository Productrty = new ProductsRepository())
            {
                Product vProduct = Productrty.GetProductById(vProductID);
                StoreHelper.DeleteProductThumb(vProduct.SmallImageUrl);
                Productrty.RemoveProduct(vProduct);
                BindProducts();
            }
        }



    }

   

   
}