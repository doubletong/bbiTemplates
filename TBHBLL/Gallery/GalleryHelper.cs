using System.IO;

namespace BBICMS.Gallery
{
    public class GalleryHelper
    {
        public static void CreateAlbumDirectory(string AlbumName)
        {
            Helpers.MakeDirectory(GetAlbumDirectory(AlbumName));
        }

        public static void CreateAlbumDisplayDirectory(string AlbumName)
        {
            Helpers.MakeDirectory(GetAlbumDisplayDirectory(AlbumName));
        }

        public static void CreateAlbumOriginalsDirectory(string AlbumName)
        {
            Helpers.MakeDirectory(GetAlbumOriginalsDirectory(AlbumName));
        }

        public static void CreateAlbumThumbNailsDirectory(string AlbumName)
        {
            Helpers.MakeDirectory(GetAlbumThumbNailsDirectory(AlbumName));
        }

        public static void CreateAlbumTree(string AlbumName)
        {
            CreateAlbumDirectory(AlbumName);
            CreateAlbumOriginalsDirectory(AlbumName);
            CreateAlbumThumbNailsDirectory(AlbumName);
            CreateAlbumDisplayDirectory(AlbumName);
        }

        public static bool EnsureAlbumTreeExist(string AlbumName)
        {
            if (!Directory.Exists(GetAlbumDirectory(AlbumName)))
            {
                return false;
            }
            if ((!Directory.Exists(GetAlbumOriginalsDirectory(AlbumName)) |
                 !Directory.Exists(GetAlbumDisplayDirectory(AlbumName))) |
                !Directory.Exists(GetAlbumThumbNailsDirectory(AlbumName)))
            {
                return false;
            }
            return true;
        }

        public static string GetAlbumDirectory(string AlbumName)
        {
            return Path.Combine(GetPhotosDirectory(), Helpers.FormatSpacesForURL(AlbumName));
        }

        public static string GetAlbumDisplayDirectory(string AlbumName)
        {
            return Path.Combine(GetAlbumDirectory(AlbumName), Helpers.Settings.Gallery.AlbumDisplayDirectory);
        }

        public static string GetAlbumOriginalsDirectory(string AlbumName)
        {
            return Path.Combine(GetAlbumDirectory(AlbumName), Helpers.Settings.Gallery.OriginalsDirectory);
        }

        public static string GetAlbumThumbNailsDirectory(string AlbumName)
        {
            return Path.Combine(GetAlbumDirectory(AlbumName), Helpers.Settings.Gallery.AlbumThumbNailsDirectory);
        }

        public static object GetDispalyURL(string vAlbumName, string vFileName)
        {
            return GetPictureURL(vAlbumName, Helpers.Settings.Gallery.AlbumDisplayDirectory, vFileName);
        }

        public static object GetOriginalURL(string vAlbumName, string vFileName)
        {
            return GetPictureURL(vAlbumName, Helpers.Settings.Gallery.OriginalsDirectory, vFileName);
        }

        public static string GetPhotosDirectory()
        {
            return Helpers.GetPhyscialPath(Helpers.Settings.Gallery.PhotosDirectory);
        }

        private static string GetPictureURL(string vAlbumName, string vPictureType, string vFileName)
        {
            return Path.Combine(Helpers.WebRoot,
                                Path.Combine(Helpers.Settings.Gallery.PhotosDirectory,
                                             Path.Combine(Helpers.FormatSpacesForURL(vAlbumName),
                                                          Path.Combine(vPictureType, vFileName))));
        }

        public static object GetThumbnailURL(string vAlbumName, string vFileName)
        {
            return GetPictureURL(vAlbumName, Helpers.Settings.Gallery.AlbumThumbNailsDirectory, vFileName);
        }

        public static void UpdateAlbumTree(string OldAlbumName, string AlbumName)
        {
            if (Directory.Exists(GetAlbumDirectory(OldAlbumName)))
            {
                Directory.Move(GetAlbumDirectory(OldAlbumName), GetAlbumDirectory(AlbumName));
            }
            else
            {
                CreateAlbumTree(AlbumName);
            }
        }
    }
}