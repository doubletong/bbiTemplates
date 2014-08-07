using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BBICMS;
using BBICMS.Gallery;
using BBICMS.UI;

partial class Admin_AddEditAlbum : AdminPage
{
    private const string STR_Adminalbumsadminaspx = "~/admin/managealbums.aspx";

    #region " Bind Drop Downs "

    protected void BindAlbums()
    {
        ddlAlbums.ClearSelection();

        using (AlbumRepository Albumrpt = new AlbumRepository())
        {
            ddlAlbums.DataSource = Albumrpt.GetActiveAlbums();

            ddlAlbums.DataBind();
        }

        ddlAlbums.Items.Insert(0, new ListItem("<Not Specified>", "0"));
    }

    #endregion

    protected bool NewItem
    {
        get
        {
            if ((Request["New"] != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    protected void Page_Load(object ser, EventArgs e)
    {
        if (!IsPostBack)
        {
            hlnkViewAlbums.NavigateUrl = STR_Adminalbumsadminaspx;
            hlnkCancel.NavigateUrl = STR_Adminalbumsadminaspx;

            btnOrderUpdate1.Visible = false;

            BindAlbums();

            if (NewItem)
            {
                ClearItems();
            }
            else
            {
                if (AlbumId > 0)
                {
                    hlNewPicture.Visible = true;
                    hlNewPicture.NavigateUrl = "addeditphoto.aspx?albumid=" + AlbumId;


                    BindData();
                }
                else
                {
                    hlNewPicture.Visible = false;
                    lbtnDelete.Visible = false;
                    btnOrderUpdate1.Visible = false;
                    lvPictures.Visible = false;

                    Button1.Visible = false;
                }
            }
        }
    }


    public string GetPicturePath(string vPicture)
    {
        return string.Format("~/Photos/{0}/thumbnails/{1}",
                             Helpers.SEOFriendlyURL(txtAlbumName.Text, Path.GetExtension(vPicture)), vPicture);
    }

    protected void BindData()
    {
        using (AlbumRepository lAlbumrpt = new AlbumRepository())
        {
            BindData(lAlbumrpt);
        }
    }

    protected void BindData(AlbumRepository lAlbumrpt)
    {
        Album lAlbum = lAlbumrpt.GetAlbumById(AlbumId);

        lbtnDelete.Attributes.Add("onclick", "return confirm('Warning: This will delete the Album from the database.');");

        lbSubmit.Text = "Update";

        txtAlbumName.Text = lAlbum.AlbumName;
        txtAlbumDesc.Text = lAlbum.AlbumDesc;

        BindPictures();

        if (lAlbum.ParentAlbumID > 0)
        {
            if ((ddlAlbums.Items.FindByValue(lAlbum.ParentAlbumID.ToString()) != null))
            {
                ddlAlbums.Items.FindByValue(lAlbum.ParentAlbumID.ToString()).Selected = true;
            }
        }
    }

    protected void BindPictures()
    {
        using (PicturesRepository lPicturerpt = new PicturesRepository())
        {
            List<Picture> aPics = lPicturerpt.GetActivePicturesByAlbum(AlbumId);

            if (aPics.Count > 0)
            {
                lvPictures.DataSource = aPics;
                lvPictures.DataBind();


                btnOrderUpdate1.Visible = true;
            }
            else
            {
                btnOrderUpdate1.Visible = false;
                lvPictures.Visible = false;

                Button1.Visible = false;
            }
        }
    }

    protected void ManageAlbums()
    {
        Response.Redirect("ManageAlbums.aspx");
    }

    protected void UpdateAlbum()
    {
        using (AlbumRepository lAlbumrpt = new AlbumRepository())
        {
            Album lAlbum;
            string lOldAlbumName = string.Empty;

            if (AlbumId > 0)
            {
                lAlbum = lAlbumrpt.GetAlbumById(AlbumId);
            }
            else
            {
                lAlbum = new Album();
            }

            lOldAlbumName = lAlbum.AlbumName;
            lAlbum.AlbumName = txtAlbumName.Text;
            lAlbum.AlbumDesc = txtAlbumDesc.Text;

            lAlbum.ParentAlbumID = int.Parse(ddlAlbums.SelectedValue);

            lAlbum.DateUpdated = DateTime.Now;
            lAlbum.UpdatedBy = UserName;

            if (lAlbum.AlbumID > 0 & lAlbum.CanEdit)
            {
                if ((lAlbumrpt.AddAlbum(lAlbum) != null))
                {
                    IndicateUpdated(lAlbumrpt, "Album", ltlStatus, lbtnDelete);
                    if (lOldAlbumName != lAlbum.AlbumName)
                    {
                        GalleryHelper.UpdateAlbumTree(lOldAlbumName, lAlbum.AlbumName);
                    }
                }
                else
                {
                    IndicateNotUpdated(lAlbumrpt, "Album", ltlStatus);
                }
            }
            else
            {
                if (lAlbum.CanAdd)
                {
                    lAlbum.Active = true;
                    lAlbum.AddedBy = UserName;
                    lAlbum.DateAdded = DateTime.Now;
                    lAlbum.AlbumCreateDate = DateTime.Now;
                    if ((lAlbumrpt.AddAlbum(lAlbum) != null))
                    {
                        IndicateUpdated(lAlbumrpt, "Album", ltlStatus, lbtnDelete);
                        GalleryHelper.CreateAlbumTree(lAlbum.AlbumName);
                    }
                    else
                    {
                        IndicateNotUpdated(lAlbumrpt, "Album", ltlStatus);
                    }
                }
            }
        }
    }

    protected void lbSubmit_Click(Object ser, EventArgs e)
    {
        UpdateAlbum();
    }

    protected void lbtnDelete_Click(Object ser, EventArgs e)
    {
        using (AlbumRepository lAlbumrpt = new AlbumRepository())
        {
            lAlbumrpt.DeleteAlbum(AlbumId);
        }


        ManageAlbums();
    }

    protected void Button1_Click(Object ser, EventArgs e)
    {
        UpdatePicOrder();
    }

    protected void btnOrderUpdate1_Click(Object ser, EventArgs e)
    {
        UpdatePicOrder();
    }

    protected void UpdatePicOrder()
    {
        using (PicturesRepository lPicturerpt = new PicturesRepository())
        {
            int i = 0;
            for (i = 0; i <= lvPictures.Items.Count - 1; i++)
            {
                TextBox txtOrder = (TextBox)lvPictures.Items[i].FindControl("txtOrder");
                ImageButton btnPicture = (ImageButton)lvPictures.Items[i].FindControl("btnPicture");

                if ((txtOrder == null) == false)
                {
                    lPicturerpt.UpdatePicOrder(Convert.ToInt32(btnPicture.CommandArgument),
                                               Convert.ToInt32(txtOrder.Text));
                }
            }
        }

        BindPictures();
    }

    protected void ClearItems()
    {
        lbtnDelete.Visible = false;
        Button1.Visible = false;

        lbSubmit.Text = "Add";

        txtAlbumName.Text = string.Empty;

        txtAlbumDesc.Text = string.Empty;
    }

    protected void lvPictures_ItemCommand1(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeletePicture":
                DeletePicture((int) e.CommandArgument);
                break;
        }
    }

    protected void DeletePicture(int vPictureId)
    {
        using (PicturesRepository lPicturerpt = new PicturesRepository())
        {
            lPicturerpt.DeletePicture(vPictureId);
        }

        BindData();
    }

    protected void lvPictures_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;

        if (lvdi.ItemType == ListViewItemType.DataItem)
        {
            HtmlImage iThumb = (HtmlImage)lvdi.FindControl("iThumb");
            TextBox txtOrder = (TextBox)lvdi.FindControl("txtOrder");
            Picture lPicture = (Picture)lvdi.DataItem;

            if ((lPicture != null) & (iThumb != null))
            {
                iThumb.Src = GetPicturePath(lPicture.PictureFileName);
                iThumb.Alt = lPicture.PictureCaption;
            }

            if ((lPicture != null) & (iThumb != null))
            {
                txtOrder.Text = lPicture.AlbumOrder.ToString();
            }
        }
    }
}