using System.Collections.Specialized;
using System.Web.Security;
using System.Web.Services;

/// <summary>
/// Summary description for MembersService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MembersService : System.Web.Services.WebService
{

     [WebMethod]
    public string[] SearchUsersByName(string prefixText , int count )
     {
         if (count == 0)
         {
             count = 10;
         }

         StringCollection UserNames = new StringCollection();

         MembershipUserCollection users = Membership.FindUsersByName(prefixText + "%");

         foreach (MembershipUser mu in users)
         {
             UserNames.Add(mu.UserName);    
         }

         string[] saUsers = new string[UserNames.Count - 1];
         UserNames.CopyTo(saUsers, 0);

         return saUsers;
     }

}

