using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using BBICMS;
using BBICMS.Gallery;
using BBICMS.UI;

partial class Admin_ManageAlbums : AdminPage
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack) {
            BindData();
        }
    }
    
    protected void BindData()
    {
        
        using (AlbumRepository lAlbumrpt = new AlbumRepository()) {
            
            lvAlbums.DataSource = lAlbumrpt.GetActiveAlbums();
            lvAlbums.DataBind();
            
            DataPager pagerBottom = (DataPager)lvAlbums.FindControl("pagerBottom");
            
            if ((pagerBottom != null)) {
                
                if (lvAlbums.Items.Count > pagerBottom.PageSize) {
                    pagerBottom.Visible = true;
                }
                else {
                    pagerBottom.Visible = false;
                    
                }
                
            }
            
        }
    }
    
    
    protected void DeleteAlbum(int vAlbumid)
    {
        using (AlbumRepository lAlbumrpt = new AlbumRepository()) {
            lAlbumrpt.DeleteAlbum(vAlbumid);
        }
        
            
        BindData();
    }
    
    protected void lvAlbums_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        
        ListViewDataItem lvdi = (ListViewDataItem)e.Item;
        
        if (lvdi.ItemType == ListViewItemType.DataItem) {
            
            HtmlImage iThumb = (HtmlImage)lvdi.FindControl("iThumb");
            Album lAlbum = (Album)lvdi.DataItem;
            
            if ((lAlbum != null) & (iThumb != null)) {

                //http://blogs.msdn.com/bethmassi/archive/2009/08/14/auto-access-to-non-indexed-collections-or-how-i-learned-to-stop-worrying-and-love-the-vb-compiler.aspx
                if ((lAlbum.Pictures != null) && lAlbum.Pictures.Count > 0) {

                    //TODO:But seriously, ElementAtOrDefault does not want to compile, so here is some duct tape!
                    Picture lp = null; // = (Picture)lAlbum.Pictures.ElementAtOrDefault(0);

                    using (IEnumerator<Picture> picts = lAlbum.Pictures.GetEnumerator())
                    {
                        while (picts.MoveNext() )
                        {
                            lp = picts.Current;
                            break;
                        }
                    }
                    
                    iThumb.Src = string.Format("~/Photos/{0}/thumbnails/{1}", 
                        Helpers.SEOFriendlyURL(lAlbum.AlbumName, ""), lp.PictureFileName);
                }
                else {
                    iThumb.Src = "~/Images/generic.jpg";
                }
                
                iThumb.Alt = lAlbum.AlbumName;
                
            }
            
        }
    }
    
    protected void lvAlbums_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        DeleteAlbum(int.Parse( lvAlbums.DataKeys[e.ItemIndex].Value.ToString()));
    }
    
    protected void lvAlbums_PagePropertiesChanged(object sender, System.EventArgs e)
    {
        BindData();
    }
    
}