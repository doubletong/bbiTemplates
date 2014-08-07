using System.Collections.Generic;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Script.Services;
using BBICMS.Gallery;

//http://www.asp.net/learn/ajax/tutorial-05-cs.aspx

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
[ScriptService()]
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ToolboxItem(false)]
public class GalleryService : System.Web.Services.WebService
{

    [WebMethod()]
    [ScriptMethod()]
    public List<Picture> GetPicturesAroundPictureId(int Albumid, int PictureId)
    {

        List<Picture> lPicturesRet = new List<Picture>();

        using (PicturesRepository lPicturerpt = new PicturesRepository())
        {
            lPicturesRet = lPicturerpt.GetActivePicturesByAlbum(Albumid);
        }

        return lPicturesRet;
    }

}