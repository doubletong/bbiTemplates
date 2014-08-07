using System;
using BBICMS;
using BBICMS.Store;
using BBICMS.UI;


partial class Admin_AddEditShippingMethod : AdminPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = string.Format(this.Title, Helpers.Settings.SiteName);
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            
            if (ShippingMethodId > 0) {
                BindShippingMethods();
            }
            else {
                ClearItems();

                plnote.Visible = false;
            }
            
        }
    }
    
    protected void ClearItems()
    {
        
        ltlShippingMethodID.Text = string.Empty;
        ltlAddedDate.Text = string.Empty;
        txtTitle.Text = string.Empty;
        txtPrice.Text = string.Empty;
        ltlUpdatedDate.Text = string.Empty;
        
            
        ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "创建物流方式";
    }
    
    protected void BindShippingMethods()
    {
        
        using (ShippingMethodsRepository lShippingMethodsrpt = new ShippingMethodsRepository()) {
            
            ShippingMethod lShippingMethods = lShippingMethodsrpt.GetShippingMethodById(ShippingMethodId);
            
            if ((lShippingMethods != null)) {
                
                ltlShippingMethodID.Text = lShippingMethods.ShippingMethodID.ToString();
                txtTitle.Text = lShippingMethods.Title;
                txtPrice.Text = lShippingMethods.Price.ToString();

                ActiveCheckBox.Checked = lShippingMethods.Active;
                ltlAddedBy.Text = lShippingMethods.AddedBy;
                ltlAddedDate.Text = lShippingMethods.AddedDate.ToShortDateString();
                ltlUpdatedBy.Text = lShippingMethods.UpdatedBy;
                ltlUpdatedDate.Text = lShippingMethods.UpdatedDate.ToShortDateString();

                ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "编辑物流方式";
            }
            else {
                ShippingMethodId = 0;
                ltlTitle.Text = ltlTitle2.Text = ltlTitle3.Text = "创建物流方式";
                
            }
            
        }
    }
    
    protected void ManageShippingMethodss()
    {
        Response.Redirect("ManageShippingMethodss.aspx");
    }
    
    protected void UpdateShippingMethods()
    {
        
        using (ShippingMethodsRepository lShippingMethodsrpt = new ShippingMethodsRepository()) {
            
            ShippingMethod lShippingMethods;
            
            if (ShippingMethodId > 0) {
                lShippingMethods = lShippingMethodsrpt.GetShippingMethodById(ShippingMethodId);
            }
            else {
                lShippingMethods = new ShippingMethod();
            }
            
            lShippingMethods.Title = txtTitle.Text;
            lShippingMethods.Price = decimal.Parse(txtPrice.Text);
            
            lShippingMethods.UpdatedDate = DateTime.Now;
            lShippingMethods.UpdatedBy = UserName;
            lShippingMethods.Active = ActiveCheckBox.Checked;
            
            if (lShippingMethods.ShippingMethodID > 0) {
                if ((lShippingMethodsrpt.AddShippingMethod(lShippingMethods) != null)) {
                    IndicateUpdated(lShippingMethodsrpt, "Shipping Method", ltlStatus, cmdDelete);
                }
                else {
                    IndicateNotUpdated(lShippingMethodsrpt, "Shipping Method", ltlStatus);
                }
            }
            else {
                
                lShippingMethods.Active = true;
                lShippingMethods.AddedBy = UserName;
                lShippingMethods.AddedDate = DateTime.Now;
                if ((lShippingMethodsrpt.AddShippingMethod(lShippingMethods) != null)) {
                    IndicateUpdated(lShippingMethodsrpt, "Shipping Method", ltlStatus, cmdDelete);
                }
                else {
                    IndicateNotUpdated(lShippingMethodsrpt, "Shipping Method", ltlStatus);
                    
                }
                
            }
            
        }
    }
    
    
    protected void GoToShippingMethodsList()
    {
        Response.Redirect("ManageShippingMethods.aspx");
    }
    
    protected void DeleteShippingMethods()
    {
        using (ShippingMethodsRepository lShippingMethodsrpt = new ShippingMethodsRepository()) {
            lShippingMethodsrpt.DeleteShippingMethod(lShippingMethodsrpt.GetShippingMethodById(ShippingMethodId));
        }
        GoToShippingMethodsList();
    }
    
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        DeleteShippingMethods();
    }
    
    protected void cmdUpdate_Click(object sender, System.EventArgs e)
    {
        UpdateShippingMethods();
    }
    
    protected void cmdCancel_Click(object sender, System.EventArgs e)
    {
        GoToShippingMethodsList();
    }
    
}