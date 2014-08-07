using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BBICMS.UI.Store;
using BBICMS.Store;

public partial class BrowseProducts : StoreAdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            //BindDepartmentsToListControl(ddlDepartments, true);
            //if (DepartmentId > 0) {
            //    BindProducts();
            //}
            
        }
    }
    
    //protected void BindProducts()
    //{
        
    //    using (ProductsRepository lProductsrpt = new ProductsRepository()) {
            
    //        List<Product> lProducts = new List<Product>();
            
    //        if (DepartmentId > 0) {
    //            lProducts = lProductsrpt.GetProductsByDepartment(DepartmentId);
    //        }
    //        else {
    //            lProducts = lProductsrpt.GetProducts();
    //        }
            
    //        lvProducts.DataSource = lProducts;
                
    //        lvProducts.DataBind();
            
    //    }
    //}
    
    //protected void lvProducts_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
        
    //    ListViewDataItem lvdi = (ListViewDataItem) e.Item;
        
    //    if ((lvdi != null)) {
            
    //        Product lProduct = (Product)lvdi.DataItem;
            
    //        if ((lProduct != null)) {
                
    //            Panel Panel1 = (Panel)lvdi.FindControl("Panel1");
    //            Panel1.Visible = lProduct.DiscountPercentage > 0 ? true : false;
                
    //            Literal ltlDiscountPercentage = (Literal)lvdi.FindControl("ltlDiscountPercentage");
    //            ltlDiscountPercentage.Text = string.Format("{0}% Off", lProduct.DiscountPercentage);
                
    //            Literal ltlUnitPrice = (Literal)lvdi.FindControl("ltlUnitPrice");
                    
    //            ltlUnitPrice.Text = string.Format("<strike>{0}</strike>", FormatPrice(lProduct.UnitPrice));
                
    //        }
            
    //    }
    //}
    
    //protected void ddlDepartments_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    DepartmentId = int.Parse( ddlDepartments.SelectedValue);
    //    BindProducts();
    //}



    protected void DataPager_PreRender(object sender, EventArgs e)
    {
        // DataPager pager = (DataPager)Page.FindControl("pagerBottom");
        // pager.Controls.Clear();

        int count = pdPager.TotalRowCount;
        int pageSize = pdPager.PageSize;
        int pagesCount = count / pageSize + (count % pageSize == 0 ? 0 : 1);
        int pageSelected = pdPager.StartRowIndex / pageSize + 1;

        if (pageSelected > 1)
        {
            // first page
            HyperLink img = new HyperLink();

            img.Text = "首页";
            img.NavigateUrl = "/Department_" + DepartmentId.ToString() + "/";
            pdPager.Controls.Add(img);
            // gap
            Literal space = new Literal();
            space.Text = " ";
            pdPager.Controls.Add(space);
        }


        for (int i = 1; i <= pagesCount; ++i)
        {
            if (pageSelected != i)
            {
                HyperLink link = new HyperLink();
                link.NavigateUrl = "/Department_" + DepartmentId.ToString() + "/Page_" + i.ToString() + "/";
                link.Text = i.ToString();
                pdPager.Controls.Add(link);


            }
            else
            {
                Literal lit = new Literal();
                lit.Text = "<strong>" + i.ToString() + "</strong>";
                pdPager.Controls.Add(lit);
            }

            Literal spaceb = new Literal();
            spaceb.Text = " ";
            pdPager.Controls.Add(spaceb);

        }
        if (pageSelected < pagesCount)
        {

            // last page
            HyperLink imgb = new HyperLink();

            imgb.Text = "尾页";
            imgb.NavigateUrl = "/Department_" + DepartmentId.ToString() + "/Page_" + pagesCount + "/";
            pdPager.Controls.Add(imgb);
        }

    }
    
}