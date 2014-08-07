using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ImageResizer;
using System.Drawing;
using BBICMS;
using System.IO;
using BBICMS.Store;
using BBICMS.UI;

partial class Admin_AddEditProduct : AdminPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = string.Format(this.Title, Helpers.Settings.SiteName);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ProductId > 0)
            {
                BindProduct();
            }
            else
            {
                ClearItems();
            }
        }
    }

    protected void BindDepartments()
    {
        BindDepartments(0);
    }

    protected void BindDepartments(int vDepartmentId)
    {
        using (DepartmentRepository lDepartmentrpt = new DepartmentRepository())
        {
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataTextField = "Title";
            ddlDepartment.DataSource = lDepartmentrpt.GetDepartments();
            ddlDepartment.DataBind();

            ListItem li = new ListItem("--请选择分类--", "");
            ddlDepartment.Items.Insert(0,li);
          //  rfvDepartment.InitialValue = vDepartmentId.ToString();

            ddlDepartment.SelectedValue = vDepartmentId.ToString();
        }
    }

    protected void ClearItems()
    {
        ltlProductID.Text = string.Empty;
        ddlDepartment.ClearSelection();
        txtTitle.Text = string.Empty;
        txtDescription.Value = string.Empty;
        txtSKU.Text = string.Empty;
        txtUnitPrice.Text = "0";
        txtDiscountPercentage.Text = "0";
        txtUnitsInStock.Text = string.Empty;
        iThumbnail.Visible = false;
        ltlVotes.Text = string.Empty;
        ltlTotalRating.Text = string.Empty;
        ltlUpdatedDate.Text = string.Empty;

        BindDepartments();


        ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "添加商品";
    }

    protected void BindProduct()
    {
        using (ProductsRepository Productrpt = new ProductsRepository())
        {
            Product lProduct = Productrpt.GetProductById(ProductId);

            if ((lProduct != null))
            {
                ltlProductID.Text = lProduct.ProductID.ToString();
                ltlAddedDate.Text = lProduct.AddedDate.ToString();
                BindDepartments(lProduct.DepartmentID);
                txtTitle.Text = lProduct.Title;
                txtDescription.Value = lProduct.Description;
                txtSKU.Text = lProduct.SKU;
                txtUnitPrice.Text = lProduct.UnitPrice.ToString();
                txtDiscountPercentage.Text = lProduct.DiscountPercentage.ToString();
                txtUnitsInStock.Text = lProduct.UnitsInStock.ToString();

                if (string.IsNullOrEmpty(lProduct.SmallImageUrl) == false)
                {
                    iThumbnail.Visible = true;
                    iThumbnail.ImageUrl = StoreHelper.GetProductThumbUrl(lProduct.SmallImageUrl);
                    HiddenField1.Value = lProduct.SmallImageUrl;
                }
                else
                {
                    iThumbnail.Visible = false;
                }

                ltlVotes.Text = lProduct.Votes.ToString();
                ltlTotalRating.Text = lProduct.TotalRating.ToString();
                ltlUpdatedDate.Text = lProduct.UpdatedDate.ToShortDateString();

                cbActive.Checked = lProduct.Active;
                ltlAddedBy.Text = lProduct.AddedBy;
                ltlUpdatedBy.Text = lProduct.UpdatedBy;

                ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "修改商品";
            }
            else
            {
                ProductId = 0;
                ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "添加商品";
            }
        }
    }

    protected void ManageProducts()
    {
        Response.Redirect("ManageProducts.aspx");
    }

    protected void UpdateProduct()
    {
        using (ProductsRepository lProductrpt = new ProductsRepository())
        {
            Product lProduct = new Product();

            if (ProductId > 0)
            {
                lProduct = lProductrpt.GetProductById(ProductId);
            }
            else
            {
                lProduct = new Product();
            }

            lProduct.ProductID = ProductId;

            lProduct.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
            lProduct.Title = txtTitle.Text;
            lProduct.Description = txtDescription.Value;
            lProduct.SKU = txtSKU.Text;
            lProduct.UnitPrice = decimal.Parse(txtUnitPrice.Text);
            lProduct.DiscountPercentage = int.Parse(txtDiscountPercentage.Text);
            lProduct.UnitsInStock = int.Parse(txtUnitsInStock.Text);
          //  lProduct.Votes = int.Parse(ltlVotes.Text);
          //  lProduct.TotalRating = int.Parse(ltlTotalRating.Text);

            if (fThumbnail.HasFile)
            {
                // Path.GetFileName(fThumbnail.FileName);
                if (ProductId > 0)
                {
                    StoreHelper.DeleteProductThumb(lProduct.SmallImageUrl);
                }


                // 上传大图
                ResizeSettings rg = new ResizeSettings();
                rg.Width = lStoreSettings.ThumbWidth;
                rg.Height = lStoreSettings.ThumbHeight;
                //rg.CropTopLeft = new Point(0,0);
                //rg.CropBottomRight = new Point(rg.Width, rg.Height);
                rg.Mode = FitMode.Crop;
               

                ImageJob i = new ImageJob(fThumbnail.PostedFile, StoreHelper.GetProductThumbDirectory() + "<guid>.<ext>", rg);
                i.CreateParentDirectory = true; //Auto-create the uploads directory.
                i.Build();
                string[] ipath = i.FinalPath.ToString().Split('\\');

                HiddenField1.Value = ipath[ipath.Length - 1];

                iThumbnail.ImageUrl = StoreHelper.GetProductThumbUrl(HiddenField1.Value);
                iThumbnail.Visible = true;

            }

            lProduct.SmallImageUrl = HiddenField1.Value;

     
            lProduct.UpdatedDate = DateTime.Now;
            lProduct.UpdatedBy = UserName;

            if (lProduct.ProductID > 0)
            {
                

                if ((lProductrpt.AddProduct(lProduct) != null))
                {
                    IndicateUpdated(lProductrpt, "Product", ltlStatus, cmdDelete);
                }
                else
                {
                    IndicateNotUpdated(lProductrpt, "Product", ltlStatus);
                }
            }
            else
            {
                lProduct.Active = cbActive.Checked;
                lProduct.AddedBy = UserName;
                lProduct.AddedDate = DateTime.Now;

                  lProduct.Votes = 0;
                  lProduct.TotalRating =0;

                if ((lProductrpt.AddProduct(lProduct) != null))
                {
                    IndicateUpdated(lProductrpt, "Product", ltlStatus, cmdDelete);
                }
                else
                {
                    IndicateNotUpdated(lProductrpt, "Product", ltlStatus);
                }
            }
        }
    }

    protected void GoToProductList()
    {
        Response.Redirect("ManageProducts.aspx");
    }

    protected void DeleteProduct()
    {
        using (ProductsRepository lProductrpt = new ProductsRepository())
        {
            lProductrpt.DeleteProduct(lProductrpt.GetProductById(ProductId));
        }
        GoToProductList();
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        DeleteProduct();
    }

    protected void cmdUpdate_Click(object sender, System.EventArgs e)
    {
        UpdateProduct();
    }

 
}