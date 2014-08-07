namespace TheBeerHouse
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class GalleryImage
    {
        private static void CheckDirectory(string sPath)
        {
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
        }

        private static void CheckFileExists(string sFileName)
        {
            if (File.Exists(sFileName))
            {
                File.Delete(sFileName);
            }
        }

        public static string Clean(string strInput)
        {
            if (strInput == null)
            {
                return string.Empty;
            }
            return StripBadCharsDB(strInput).Replace("'s", "");
        }

        public static string CleanURLFileName(string strInput)
        {
            if (strInput == null)
            {
                return string.Empty;
            }
            return StripBadCharsDB(strInput).Replace(" ", "_");
        }

        private string GetEmptyImage(string CurImg)
        {
            if (string.IsNullOrEmpty(CurImg))
            {
                return "spacer.gif";
            }
            return CurImg;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void MakeThumbnail(ref string sSource, string sDestination, ref int MaxWidth, ref int MaxHeight, InterpolationMode nMode, [Optional, DefaultParameterValue(100)] int iJPGQuality)
        {
            double dblCoef;
            int iHeight;
            int iWidth;
            Image objImage = new Bitmap(sSource);
            int intOldWidth = objImage.Width;
            int intOldHeight = objImage.Height;
            if (intOldWidth > MaxWidth)
            {
                iWidth = MaxWidth;
                dblCoef = ((double) MaxWidth) / ((double) intOldWidth);
                if (MaxHeight <= Convert.ToInt32((double) (dblCoef * intOldHeight)))
                {
                    iHeight = MaxHeight;
                    dblCoef = ((double) MaxHeight) / ((double) intOldHeight);
                    iWidth = Convert.ToInt32((double) (dblCoef * intOldWidth));
                }
                else
                {
                    iHeight = Convert.ToInt32((double) (dblCoef * intOldHeight));
                }
            }
            else if (intOldHeight > MaxHeight)
            {
                iHeight = MaxHeight;
                dblCoef = ((double) MaxHeight) / ((double) intOldHeight);
                iWidth = Convert.ToInt32((double) (dblCoef * intOldWidth));
            }
            else
            {
                iWidth = intOldWidth;
                iHeight = intOldHeight;
            }
            Bitmap objBitmap = new Bitmap(iWidth, iHeight);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.InterpolationMode = nMode;
            objGraphics.DrawImage(objImage, 0, 0, iWidth, iHeight);
            EncoderParameters objEncoder = new EncoderParameters(1);
            objEncoder.Param[0] = new EncoderParameter(Encoder.Quality, (long) iJPGQuality);
            sDestination = sDestination.ToLower();
            if (Path.GetExtension(sDestination) == ".gif")
            {
                objBitmap.Save(sDestination, this.GetEncoder(ImageFormat.Gif), objEncoder);
            }
            else if (Path.GetExtension(sDestination) == ".jpg")
            {
                objBitmap.Save(sDestination, this.GetEncoder(ImageFormat.Jpeg), objEncoder);
            }
            else if (Path.GetExtension(sDestination) == ".png")
            {
                objBitmap.Save(sDestination, this.GetEncoder(ImageFormat.Png), objEncoder);
            }
            objBitmap.Dispose();
            objImage.Dispose();
        }

        public string StoreImage(string sDestinationPath, string sDestinationFileName, string ImgFile, int EleWidth, int EleHeight)
        {
            return this.StoreImage(sDestinationPath, sDestinationFileName, ImgFile, EleWidth, EleHeight, string.Empty);
        }

        public string StoreImage(string DestinationPath, string DestinationFileName, string ImageFileName, int ImageWidth, int ImageHeight, string CurrentImageFileName)
        {
            return this.StoreImage(DestinationPath, DestinationFileName, ImageFileName, ImageWidth, ImageHeight, CurrentImageFileName, 100);
        }

        public string StoreImage(string DestinationPath, string DestinationFileName, string ImageFileName, int ImageWidth, int ImageHeight, string CurrentImageFileName, int ImageQuality)
        {
            if (!string.IsNullOrEmpty(DestinationFileName))
            {
                DestinationFileName = Path.GetFileName(DestinationFileName).Replace("#", string.Empty);
                DestinationFileName = Path.Combine(DestinationPath, this.StoreImageExtracted(ref DestinationFileName));
                CheckFileExists(DestinationFileName);
                this.MakeThumbnail(ref ImageFileName, DestinationFileName, ref ImageWidth, ref ImageHeight, InterpolationMode.HighQualityBicubic, ImageQuality);
                return Path.GetFileName(DestinationFileName);
            }
            return string.Empty;
        }

        private string StoreImageExtracted(ref string sFileName)
        {
            if (sFileName.Length >= 0x31)
            {
                if (Path.GetExtension(sFileName) == ".jpg")
                {
                    sFileName = sFileName.Substring(0, 0x2d) + ".jpg";
                    return sFileName;
                }
                sFileName = sFileName.Substring(0, 0x2d) + ".gif";
            }
            return sFileName;
        }

        public static string StripBadChars(string strIn)
        {
            return Regex.Replace(strIn, @"(\s)|(,)|(;)|(:)|(')", "_");
        }

        public static string StripBadCharsDB(string strIn)
        {
            return Regex.Replace(strIn, "(,)|(;)|(:)|(')", "_");
        }
    }
}

