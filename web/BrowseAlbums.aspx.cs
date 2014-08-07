using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using BBICMS;
using BBICMS.Gallery;
using BBICMS.UI;

public partial class BrowseAlbums : BasePage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        
        if (!IsPostBack) {
            BindAlbums();
        }
    }
    
    protected void BindAlbums()
    {
        
        using (AlbumRepository lAlbumsrpt = new AlbumRepository()) {
            
            lvPictures.DataSource = lAlbumsrpt.GetActiveAlbums();
                
            lvPictures.DataBind();
            
        }
    }
    
    protected void lvPictures_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindAlbums();
    }
    
    
    protected void lvPictures_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
    {
        
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;
        
        if (lvdi.ItemType == ListViewItemType.DataItem) {
            
            HtmlImage iThumb = (HtmlImage)lvdi.FindControl("iThumb");
            Album lAlbum = (Album)lvdi.DataItem;
            
            if ((lAlbum != null) & (iThumb != null)) {
                if ((lAlbum.Pictures != null) && lAlbum.Pictures.Count > 0) {
                    //TODO:But seriously, ElementAtOrDefault does not want to compile, so here is some duct tape!
                    Picture lPicture = null; // = (Picture)lAlbum.Pictures.ElementAtOrDefault(0);

                    using (IEnumerator<Picture> picts = lAlbum.Pictures.GetEnumerator())
                    {
                        while (picts.MoveNext())
                        {
                            lPicture = picts.Current;
                            break;
                        }
                    }

                    iThumb.Src = string.Format("~/Photos/{0}/thumbnails/{1}", Helpers.FormatSpacesForURL(lAlbum.AlbumName), lPicture.PictureFileName);
                    iThumb.Alt = lPicture.PictureCaption;
                }
                else {
                    iThumb.Src = "~/Images/generic.jpg";
                    iThumb.Alt = "No Pictues Yet.";
                    
                }
                
            }
            
        }
    }
    
    public string GetPicturePath(string vAlbumName, string vPicture)
    {
        return string.Format("~/Photos/{0}/thumbnails/{1}", Helpers.FormatSpacesForURL(vAlbumName), vPicture);
    }
    
}