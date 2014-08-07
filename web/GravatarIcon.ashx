<%@ WebHandler Language="C#" Class="GravatarIcon" %>
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Drawing;
using System.IO;
using System.Net;

public class GravatarIcon : IHttpHandler
{
    
    private HttpContext _context;
    public HttpContext CurrentContext {
        get { return _context; }
        set { _context = value; }
    }
    
    public void ProcessRequest(HttpContext context)
    {
        
        CurrentContext = context;
        
        string gravatarHash = "none";
        
        if ((CurrentContext.Request.QueryString["email"] != null)) {
            gravatarHash = CurrentContext.Request.QueryString["email"];
        }
        
        if (CheckForAvatar(gravatarHash) == false) {
            DownloadGravatar(gravatarHash);
        }
        
        Image Gravatar = Image.FromFile(LocalAvatarPath(gravatarHash));
        
        using (Gravatar) {
            
            context.Response.ContentType = "image/jpeg";
            Gravatar.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
            
        
    }
    
    protected void DownloadGravatar(string sGravatarHash)
    {
        
        string sGravatar = string.Format("http://www.gravatar.com/avatar/{0}.jpg?d=wavatar&s=32", sGravatarHash);
            
        writeImage(sGravatar, sGravatarHash);
    }
    
    protected void writeImage(string imageFilePath, string sGravatarHash)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(imageFilePath);
        request.Timeout = 5000;
        request.ReadWriteTimeout = 20000;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream());
        
        using (img) {
            
            img.Save(LocalAvatarPath(sGravatarHash + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
        }
            
        
    }
    
    
    private string _localAvatarPath = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sAvatar"></param>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public string LocalAvatarPath(string sAvatar) {

            if (string.IsNullOrEmpty(_localAvatarPath)) {
                
                string lAvatarPath = Path.Combine(CurrentContext.Request.PhysicalApplicationPath, "Avatars");
                
                if (Directory.Exists(lAvatarPath) == false) {
                    Directory.CreateDirectory(lAvatarPath);
                }
                
                _localAvatarPath = Path.Combine(lAvatarPath, sAvatar + ".jpg");
            }
            return _localAvatarPath;

    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sAvatar"></param>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public string AvatarPath (string sAvatar) {
        
            if (CheckForAvatar(sAvatar)) {
            }
            
                //TODO: Add default avatar here.
            return string.Empty;
        
    }
    
    private bool CheckForAvatar(string sAvatar)
    {
        string lAvatarPath = LocalAvatarPath(sAvatar);
        if (lAvatarPath.Contains("default.jpg") == false) {
            return File.Exists(lAvatarPath);
        }
        return false;
    }
    
    private string GetGravatarHash(string sEmail)
    {
        
        MD5 md5Hasher = MD5.Create();
        
        //Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(sEmail));
        
        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();
        
        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        int i = 0;
        for (i = 0; i <= data.Length - 1; i++) {
            sBuilder.Append(data[i].ToString("x2"));
        }
        
        // Return the hexadecimal string.
            
            
        return sBuilder.ToString();
    }
    
    public bool IsReusable {
        get { return false; }
    }
    
}