using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using BBICMS;
using BBICMS.Gallery;

public partial class ShowAlbum : GalleryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPictures();
        }
    }

    protected void BindPictures()
    {
        using (PicturesRepository lPicturesrpt = new PicturesRepository())
        {
            List<Picture> lPictures = lPicturesrpt.GetActivePicturesByAlbum(AlbumId);
            lvPictures.DataSource = lPictures;
            lvPictures.DataBind();

            if (lPictures.Count > 0)
            {
                h2GalleryTitle.InnerText = lPictures[0].AlbumName;
            }
            else
            {
                h2GalleryTitle.InnerText = "Photo Galery";
            }
        }
    }

    protected void lvPictures_PagePropertiesChanged(object sender, EventArgs e)
    {
        BindPictures();
    }


    protected void lvPictures_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;

        if (lvdi.ItemType == ListViewItemType.DataItem)
        {
            HtmlImage iThumb = (HtmlImage)lvdi.FindControl("iThumb");
            Picture lPicture = (Picture)lvdi.DataItem;

            if ((lPicture != null) & (iThumb != null))
            {
                iThumb.Src = string.Format("~/Photos/{0}/thumbnails/{1}", Helpers.FormatSpacesForURL(lPicture.AlbumName),
                                           lPicture.PictureFileName);
                iThumb.Alt = lPicture.PictureCaption;
            }
        }
    }

    public string GetPicturePath(string vAlbumName, string vPicture)
    {
        return string.Format("~/Photos/{0}/thumbnails/{1}", Helpers.FormatSpacesForURL(vAlbumName), vPicture);
    }

    public string GetDisplayPicturePath(string vAlbumName, string vPicture)
    {
        return
            VirtualPathUtility.ToAbsolute(string.Format("~/Photos/{0}/Display/{1}",
                                                        Helpers.FormatSpacesForURL(vAlbumName), vPicture));
    }
}