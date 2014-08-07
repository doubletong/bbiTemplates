using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

using BBICMS;
using BBICMS.BLL;
using BBICMS.Gallery;
using BBICMS.UI;

partial class Admin_AddEditPhoto : AdminPage
{
    #region " Properties "

    private const string lEditAlbumURL = "addeditalbum.aspx?albumid=";
    private string _PhotoExtension = string.Empty;

    private string _UploadedFileName = string.Empty;

    private string UploadedFileName
    {
        get
        {
            if (string.IsNullOrEmpty(_UploadedFileName))
            {
                if (fPicture.HasFile)
                {
                    _UploadedFileName = HttpUtility.UrlDecode(Path.GetFileName(fPicture.FileName)).Replace(" ", "-");
                }
                else if ((ViewState["PictureFileName"] == null) == false)
                {
                    _UploadedFileName = ViewState["PictureFileName"].ToString();
                }
            }
            return _UploadedFileName;
        }
    }

    private string PhotoExtension
    {
        get
        {
            if (string.IsNullOrEmpty(_PhotoExtension))
            {
                _PhotoExtension = Path.GetExtension(fPicture.FileName);
            }
            return _PhotoExtension;
        }
    }

    #endregion

    protected void Page_Load(Object ser, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (PictureId > 0)
            {
                BindData();
            }
            else
            {
                ViewState["PictureFileName"] = string.Empty;
                imgThumb.Visible = false;
                cmdDelete.Visible = false;

                txtWidth.Text = lGallerySettings.ThumbWidth.ToString();
                txtHeight.Text = lGallerySettings.ThumbHeight.ToString();

                txtOrder.Text = "0";

                BindAlbums();

                ddlAlbums.SelectedValue = AlbumId.ToString();

            }
        }
    }

    protected void BindData()
    {
        BindAlbums();

        using (PicturesRepository lPicturerpt = new PicturesRepository())
        {
            Picture lPicture = lPicturerpt.GetPictureById(PictureId);
            cmdDelete.Attributes.Add("onclick",
                                     "return confirm('Warning: This will delete the Picture from the Album.')");

            txtTitle.Text = lPicture.PictureTitle;
            txtCaption.Text = lPicture.PictureCaption;
            //ddlAlbums.Items.FindByValue(lPicture.PictureAlbumID.ToString)
            ViewState["PictureFileName"] = lPicture.PictureFileName;
            txtOrder.Text = lPicture.AlbumOrder.ToString();

            txtWidth.Text = lPicture.ThumbWidth.ToString();
            txtHeight.Text = lPicture.ThumbHeight.ToString();

            imgThumb.ImageUrl = GalleryHelper.GetThumbnailURL(lPicture.AlbumName, lPicture.PictureFileName).ToString();

            imgThumb.AlternateText = lPicture.PictureTitle;

            ViewState["AlbumId"] = lPicture.AlbumId;
            ViewState["PictureCreateDate"] = lPicture.PictureCreateDate;


            ddlAlbums.SelectedValue = lPicture.AlbumId.ToString();
        }
    }

    protected void BindAlbums()
    {
        using (AlbumRepository lAlbumrpt = new AlbumRepository())
        {
            ddlAlbums.DataTextField = "AlbumName";
            ddlAlbums.DataValueField = "AlbumId";
            ddlAlbums.DataSource = lAlbumrpt.GetActiveAlbums();

            ddlAlbums.DataBind();
        }
    }

    protected void StorePics()
    {
        string sfName = ViewState["PictureFileName"].ToString();
        GalleryImage lgi = new GalleryImage();

        string lOriginalFileName = string.Empty;
        string lDisplayFileName = string.Empty;
        string lThumbnailFileName = string.Empty;


        if (fPicture.HasFile)
        {
            if (PhotoExtension.ToLower().Contains("jpg") == false &&
                PhotoExtension.ToLower().Contains("gif") == false &&
                PhotoExtension.ToLower().Contains("png") == false)
            {
                Response.Write("<B>You can Only Upload JPGs or GIFs</B>");
                return;
            }

            if (!GalleryHelper.EnsureAlbumTreeExist(Helpers.FormatSpacesForURL(ddlAlbums.SelectedItem.Text)))
            {
                GalleryHelper.CreateAlbumTree(ddlAlbums.SelectedItem.Text);
            }

            //Save Original
            lOriginalFileName = Path.Combine(GalleryHelper.GetAlbumOriginalsDirectory(ddlAlbums.SelectedItem.Text),
                                             UploadedFileName);

            if (File.Exists(lOriginalFileName))
            {
                File.Delete(lOriginalFileName);
            }

            fPicture.PostedFile.SaveAs(lOriginalFileName);

            lThumbnailFileName = Path.Combine(GalleryHelper.GetAlbumThumbNailsDirectory(ddlAlbums.SelectedItem.Text),
                                              UploadedFileName);

            //Save Thumbnails
            if (File.Exists(lThumbnailFileName))
            {
                File.Delete(lThumbnailFileName);
            }

            lgi.StoreImage(GalleryHelper.GetAlbumThumbNailsDirectory(ddlAlbums.SelectedItem.Text),
                           Path.GetFileName(lThumbnailFileName), lOriginalFileName, Convert.ToInt32(txtWidth.Text),
                           Convert.ToInt32(txtHeight.Text));

            //Save Display or Large Version
            lDisplayFileName = Path.Combine(GalleryHelper.GetAlbumDisplayDirectory(ddlAlbums.SelectedItem.Text),
                                            UploadedFileName);

            if (File.Exists(lDisplayFileName))
            {
                File.Delete(lDisplayFileName);
            }


            lgi.StoreImage(GalleryHelper.GetAlbumDisplayDirectory(ddlAlbums.SelectedItem.Text),
                           Path.GetFileName(lDisplayFileName), lOriginalFileName, lGallerySettings.DisplayWidth,
                           lGallerySettings.DisplayHeight);
        }
    }

    protected void ResizeThumbnail()
    {
        GalleryImage ei = new GalleryImage();

        if (File.Exists(GalleryHelper.GetAlbumOriginalsDirectory(ddlAlbums.SelectedItem.Text) + UploadedFileName))
        {
            ei.StoreImage(GalleryHelper.GetAlbumOriginalsDirectory(ddlAlbums.SelectedItem.Text), UploadedFileName,
                          GalleryHelper.GetAlbumThumbNailsDirectory(ddlAlbums.SelectedItem.Text) + UploadedFileName,
                          Convert.ToInt32(txtWidth.Text), Convert.ToInt32(txtHeight.Text));
        }
    }

    protected void UpdatePicture()
    {
        using (PicturesRepository lPicturerpt = new PicturesRepository())
        {

            Picture lPicture = new Picture();

            if (PictureId > 0)
            {
                lPicture = lPicturerpt.GetPictureById(PictureId);
            }
            else
            {
                lPicture = new Picture();
            }

            int pictid = 0;

            if (PictureId > 0 && lPicture.AlbumId != Convert.ToInt32(ddlAlbums.SelectedValue))
            {
                using (AlbumRepository lAlbumrpt = new AlbumRepository())
                {
                    string sOldAlbum = lPicture.AlbumName;
                    string sNewAlbum = lAlbumrpt.GetAlbumById(Convert.ToInt32(ddlAlbums.SelectedValue)).AlbumName;

                    if (fPicture.HasFile)
                    {
                        MovePhotosToNewAlbum(sOldAlbum, sNewAlbum, Path.GetFileName(fPicture.FileName));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(lPicture.PictureFileName) == false)
                        {
                            MovePhotosToNewAlbum(sOldAlbum, sNewAlbum, lPicture.PictureFileName);
                        }
                    }
                }
            }

            lPicture.AlbumId = Convert.ToInt32(ddlAlbums.SelectedValue);
            lPicture.PictureTitle = txtTitle.Text;
            lPicture.PictureCaption = txtCaption.Text;
            lPicture.PictureCreateDate = DateTime.Now;
            lPicture.AlbumOrder = int.Parse(txtOrder.Text);

            if (fPicture.HasFile)
            {
                lPicture.PictureFileName = Helpers.SEOFriendlyURL(Path.GetFileName(fPicture.FileName),
                                                                  Path.GetExtension(fPicture.FileName));
                StorePics();
            }
            else
            {
                if (lPicture.ThumbWidth != int.Parse(txtWidth.Text)
                    || lPicture.ThumbHeight != int.Parse(txtHeight.Text))
                {
                    ResizeThumbnail();
                }
            }

            lPicture.ThumbWidth = int.Parse(txtWidth.Text);
            lPicture.ThumbHeight = int.Parse(txtHeight.Text);

            lPicture.DateUpdated = DateTime.Now;
            lPicture.UpdatedBy = UserName;


            if (PictureId == 0)
            {
                lPicture.Active = true;
                lPicture.PictureCreateDate = DateTime.Now;
                lPicture.DateAdded = DateTime.Now;
                lPicture.AddedBy = UserName;

                if ((lPicturerpt.AddPicture(lPicture) != null))
                {
                    IndicateUpdated();
                }
                else
                {
                    IndicateNotUpdated(lPicturerpt);
                }
            }
            else
            {
                // lPicture.Active = Convert.ToBoolean(ViewState["Active"))
                // lPicture.PictureID = PictureId
                pictid = PictureId;
                lPicture.DateUpdated = DateTime.Now;
                lPicture.PictureCreateDate = (DateTime)ViewState["PictureCreateDate"];

                if ((lPicturerpt.AddPicture(lPicture) != null))
                {
                    IndicateUpdated();
                    BindData();
                }
                else
                {
                    IndicateNotUpdated(lPicturerpt);
                }
            }
        }
        //If rbHomePage.Checked And pictid > 0 Then
        // Site_Settings.AddSiteSetting("FeaturedPhoto", pictid)
        //End If
    }


    protected void cmdDelete_Click(object ser, EventArgs e)
    {
        DeletePicture();
    }

    protected void DeletePicture()
    {
        using (PicturesRepository lPicturesrpt = new PicturesRepository())
        {
            lPicturesrpt.DeletePicture(lPicturesrpt.GetPictureById(PictureId));
        }
        GoToPictureList();
    }

    protected void IndicateNotUpdated(BaseRepository vRepository)
    {
        ltlStatus.Text = string.Empty;
        if (vRepository.ActiveExceptions.Count > 0)
        {
            foreach (KeyValuePair<String, Exception> kv in vRepository.ActiveExceptions)
            {
                ltlStatus.Text += (kv.Value).Message + "<BR/>";
            }
        }
        else
        {
            ltlStatus.Text = "The Picture Has Not Been Updated.";
        }
    }

    protected void IndicateUpdated()
    {
        ltlStatus.Text = "The Picture Has Been Updated.";
        cmdDelete.Visible = true;
    }

    protected void GoToPictureList()
    {
        Response.Redirect(lEditAlbumURL + ViewState["AlbumId"]);
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        UpdatePicture();
    }

    #region " Move Picture Methods "

    protected void MovePhotosToNewAlbum(string vOldAlbum, string vNewAlbum, string vFileName)
    {
        //Move Original
        MoveFile(Path.Combine(GalleryHelper.GetAlbumOriginalsDirectory(vOldAlbum), vFileName),
                 Path.Combine(GalleryHelper.GetAlbumOriginalsDirectory(vNewAlbum), vFileName));

        //Move Display
        MoveFile(Path.Combine(GalleryHelper.GetAlbumDisplayDirectory(vOldAlbum), vFileName),
                 Path.Combine(GalleryHelper.GetAlbumDisplayDirectory(vNewAlbum), vFileName));

        //Move Thumbs

        MoveFile(Path.Combine(GalleryHelper.GetAlbumThumbNailsDirectory(vOldAlbum), vFileName),
                 Path.Combine(GalleryHelper.GetAlbumThumbNailsDirectory(vNewAlbum), vFileName));
    }

    protected void MoveFile(string sOriginal, string sNew)
    {
        if (File.Exists(sNew))
        {
            File.Delete(sNew);
        }

        if (File.Exists(sOriginal))
        {
            File.Move(sOriginal, sNew);
        }
        //File.Delete(sOriginal)
    }

    #endregion
}