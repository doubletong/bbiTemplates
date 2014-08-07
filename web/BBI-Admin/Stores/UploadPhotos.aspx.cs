using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ImageResizer;

using BBICMS.Store;
using BBICMS;
using BBICMS.UI.Store;

public partial class BBI_Admin_Stores_UploadPhoto : StoreAdminPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        PageTitle = string.Format(PageTitle, Helpers.Settings.SiteName);

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
          //  ltlTitle.Text = "添加照片";

            if (ProductId > 0)
            {
                BindPhotos();
            }

        }

    }






    protected void lvPhotos_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        using (PhotoRepository lPhotosrpt = new PhotoRepository())
        {
            lPhotosrpt.DeletePhoto(int.Parse(lvPhotos.DataKeys[e.ItemIndex].Value.ToString()));
            BindPhotos();
        }
    }


    protected void BindPhotos()
    {

        using (PhotoRepository lPhotosrpt = new PhotoRepository())
        {

            List<Photo> lPhotos = lPhotosrpt.GetPhotosByProduct(ProductId);
            lvPhotos.DataSource = lPhotos;
            lvPhotos.DataBind();

        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        using (PhotoRepository lPhotosrpt = new PhotoRepository())
        {
            Photo lPhoto;
            if (PhotoId > 0)
            {
                lPhoto = lPhotosrpt.GetPhotoById(PhotoId);
            }
            else
            {
                lPhoto = new Photo();
            }


            lPhoto.ProductID = ProductId;


            Boolean fileOk = true;
            //上传图片
            if (FileUpload1.HasFile)
            {

                //对上传文件的大小进行检测，限定文件最大不超过1M
                if (FileUpload1.PostedFile.ContentLength > 1024000)
                {

                    plStatus.Visible = true;
                    plStatus.CssClass = "alert alert-error";
                    //IndicateUpdated(ccyrpt, "产品分类", ltlStatus, btnDelete);
                    ltlStatus.Text = "文件大小超出1M,请缩小尺寸后再上传!";
                    fileOk = false;
                }
                //最后的结果
                if (fileOk)
                {
                    try
                    {


                        // 上传大图
                        ResizeSettings rg = new ResizeSettings();
                        rg.Width = lStoreSettings.DisplayWidth;
                        //rg.Height = lStoreSettings.DisplayHeight;
                        rg.Mode = FitMode.Max;

                        ImageJob i = new ImageJob(FileUpload1.PostedFile, StoreHelper.GetPhotosDirectory() + "<guid>.<ext>", rg);
                        i.CreateParentDirectory = true; //Auto-create the uploads directory.
                        i.Build();
                        string[] ipath = i.FinalPath.ToString().Split('\\');

                        HiddenField1.Value = ipath[ipath.Length - 1];
                        //    Helpers.ShowMsg("", @i.FinalPath);
                        //   Response.Write(ProductsHelper.GetPhotosDirectory());

                        //创建缩略图
                        rg.Width = lStoreSettings.PhotoThumbW;
                        rg.Height = lStoreSettings.PhotoThumbH;
                        rg.Mode = FitMode.Crop;

                        ImageJob ithumb = new ImageJob(i.FinalPath, StoreHelper.GetPhotosThumbDirectory() + "<guid>.<ext>", rg);
                        ithumb.CreateParentDirectory = true; //Auto-create the uploads directory.
                        ithumb.Build();
                        string[] ithumbpath = ithumb.FinalPath.ToString().Split('\\');

                        HiddenField2.Value = ithumbpath[ithumbpath.Length - 1];


                        // Helpers.MakeThumbnail(fullImage, smallImage, lStoresSettings.PhotoThumbW,lStoresSettings.PhotoThumbH, "Cut");  //缩略图生成

                    }
                    catch
                    {
                        plStatus.Visible = true;
                        plStatus.CssClass = "alert alert-error";
                        //IndicateUpdated(ccyrpt, "产品分类", ltlStatus, btnDelete);
                        ltlStatus.Text = "上传失败";
                    }
                }


            }



            lPhoto.OriginalPic = HiddenField1.Value;
            lPhoto.Thumbnail = HiddenField2.Value;



            if (lPhoto.PhotoID > 0 & lPhoto.CanEdit)
            {

                if ((lPhotosrpt.AddPhoto(lPhoto) != null))
                {
                    plStatus.Visible = true;
                    plStatus.CssClass = "alert alert-success";
                    IndicateUpdated(lPhotosrpt, "照片", ltlStatus, btnDelete);
                    //if (lOldCategoryName != lCategory.Title)
                    //{
                    //    ProductsHelper.UpdateCategoryTree(lOldCategoryName, lCategory.Title);
                    //}
                }
                else
                {

                    plStatus.Visible = true;
                    plStatus.CssClass = "alert alert-error";
                    IndicateNotUpdated(lPhotosrpt, "照片", ltlStatus);
                }
            }
            else
            {
                if (lPhoto.CanAdd)
                {
                    lPhoto.AddedBy = UserName;
                    lPhoto.AddedDate = DateTime.Now;
                    //   lCategory.CategoryCreateDate = DateTime.Now;
                    if ((lPhotosrpt.AddPhoto(lPhoto) != null))
                    {
                        plStatus.Visible = true;
                        plStatus.CssClass = "alert alert-success";
                        IndicateUpdated(lPhotosrpt, "照片", ltlStatus, btnDelete);
                        //ProductsHelper.CreateCategoryTree(lCategory.Title);
                    }
                    else
                    {
                        plStatus.Visible = true;
                        plStatus.CssClass = "alert alert-error";
                        IndicateNotUpdated(lPhotosrpt, "照片", ltlStatus);
                    }
                    HiddenField1.Value = "";
                }
            }

            BindPhotos();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeletePhotos();
    }

    protected void DeletePhotos()
    {
        using (PhotoRepository photorpy = new PhotoRepository())
        {
            int i = 0;
            for (i = 0; i <= lvPhotos.Items.Count - 1; i++)
            {
                LinkButton lbtnDelete = (LinkButton)lvPhotos.Items[i].FindControl("lbtnDelete");

                photorpy.DeletePhoto(Convert.ToInt32(lbtnDelete.CommandArgument));
            }

            BindPhotos();
        }
    }
}