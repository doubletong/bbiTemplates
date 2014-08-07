using System.Web.Services;
using System.Web.Script.Services;
using BBICMS.Articles;
using BBICMS.BLL.Articles;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService()]
public class CommentService : System.Web.Services.WebService
{

    [WebMethod()]
    [ScriptMethod()]
    public bool PostComment(Comment vComment)
    {

        bool bRet = false;

        using (CommentRepository Commentrps = new CommentRepository())
        {
            bRet = (Commentrps.AddComment(vComment) != null) ? true : false;
        }

        return bRet;
    }

}