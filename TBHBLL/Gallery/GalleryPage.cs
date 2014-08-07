using BBICMS.UI;

namespace BBICMS
{
    public class GalleryPage : BasePage
    {

        #region " Properties "

        public int AlbumId
        {
            get { return PrimaryKeyId("AlbumId"); }
            set { ViewState["AlbumId"] = value; }
        }

        public int PictureId
        {
            get { return PrimaryKeyId("PictureId"); }
            set { ViewState["PictureId"] = value; }
        }

        #endregion

    }
}

