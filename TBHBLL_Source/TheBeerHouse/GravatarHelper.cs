namespace TheBeerHouse
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    public class GravatarHelper
    {
        private string _localAvatarPath = string.Empty;

        private bool CheckForAvatar(string sAvatar)
        {
            string lAvatarPath = this.get_LocalAvatarPath(sAvatar);
            return (!lAvatarPath.Contains("default.jpg") && System.IO.File.Exists(lAvatarPath));
        }

        private void DownloadGravatar(string sGravatarHash)
        {
            this.DownloadGravatar(sGravatarHash, 80);
        }

        private void DownloadGravatar(string sGravatarHash, int width)
        {
            string sGravatar = string.Format("http://www.gravatar.com/avatar/{0}.jpg?d=wavatar&s={1}", sGravatarHash, width);
            this.writeImage(sGravatar, sGravatarHash);
        }

        public string GetAvatar(string eMail)
        {
            return this.GetAvatar(eMail, 80);
        }

        public string GetAvatar(string eMail, int width)
        {
            string gravatarHash = GetGravatarHash(eMail);
            if (!this.CheckForAvatar(gravatarHash))
            {
                this.DownloadGravatar(gravatarHash, width);
            }
            return (gravatarHash + ".jpg");
        }

        public static string GetGravatarHash(string sEmail)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.Default.GetBytes(sEmail));
            StringBuilder sBuilder = new StringBuilder();
            int VB$t_i4$L0 = data.Length - 1;
            for (int i = 0; i <= VB$t_i4$L0; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private void writeImage(string imageFilePath, string sGravatarHash)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(imageFilePath);
            request.Timeout = 0x1388;
            request.ReadWriteTimeout = 0x4e20;
            Image img = Image.FromStream(((HttpWebResponse) request.GetResponse()).GetResponseStream());
            using (img)
            {
                img.Save(this.get_LocalAvatarPath(sGravatarHash + ".jpg"), ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sAvatar"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string this[string sAvatar]
        {
            get
            {
                if (this.CheckForAvatar(sAvatar))
                {
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sAvatar"></param>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string this[string sAvatar]
        {
            get
            {
                if (string.IsNullOrEmpty(this._localAvatarPath))
                {
                    this._localAvatarPath = Path.Combine(Path.Combine(this.Request.PhysicalApplicationPath, "Avatars"), sAvatar + ".jpg");
                }
                return this._localAvatarPath;
            }
        }

        public HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
    }
}

