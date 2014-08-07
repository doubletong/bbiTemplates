using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;

using BBICMS.UI.Store;

public partial class BBI_Admin_Stores_Config : StoreAdminPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        PageTitle = string.Format(PageTitle, BBICMS.Helpers.Settings.SiteName);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    public void BindData()
    {

        //  Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
        //  BBICMSSection twotong = (BBICMSSection)config.GetSection("bbiconfig");
        txtPageSize.Text = lStoreSettings.PageSize.ToString();
        txtIndexImg.Text = lStoreSettings.ProductThumbDirectory;
        txtIWidth.Text = lStoreSettings.ThumbWidth.ToString();
        txtIHeight.Text = lStoreSettings.ThumbHeight.ToString();

        txtThumbUrl.Text = lStoreSettings.PhotosThumbDirectory;
        txtThumbWidth.Text = lStoreSettings.PhotoThumbW.ToString();
        txtThumbHeigh.Text = lStoreSettings.PhotoThumbH.ToString();
        txtPhotoUrl.Text = lStoreSettings.PhotosDirectory;
        txtPWidth.Text = lStoreSettings.DisplayWidth.ToString();
        txtPHeight.Text = lStoreSettings.DisplayHeight.ToString();


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            BBICMSSection twotong = (BBICMSSection)config.GetSection("theBeerHouse");

            twotong.Store.PageSize = Int32.Parse(txtPageSize.Text);

            twotong.Store.ProductThumbDirectory = txtIndexImg.Text;
            twotong.Store.ThumbWidth = Int32.Parse(txtIWidth.Text);
            twotong.Store.ThumbHeight = Int32.Parse(txtIHeight.Text);

            twotong.Store.PhotosThumbDirectory = txtThumbUrl.Text;
            twotong.Store.PhotoThumbW = Int32.Parse(txtThumbWidth.Text);
            twotong.Store.PhotoThumbH = Int32.Parse(txtThumbHeigh.Text);
            twotong.Store.PhotosDirectory = txtPhotoUrl.Text;
            twotong.Store.DisplayWidth = Int32.Parse(txtPWidth.Text);
            twotong.Store.DisplayHeight = Int32.Parse(txtPHeight.Text);

            config.Save();
            plStatus.Visible = true;
            plStatus.CssClass = "alert alert-success";
            ltlStatus.Text = "保存成功";
        }
        catch (System.Exception exc)
        {
            plStatus.Visible = true;
            plStatus.CssClass = "alert alert-error";
            ltlStatus.Text = (exc.Message + "<br />" + exc.StackTrace).Replace("\n", "<br />");
        }
    }
}