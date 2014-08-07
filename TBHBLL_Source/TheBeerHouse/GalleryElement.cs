namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Configuration;

    public class GalleryElement : ConfigurationElement
    {
        [ConfigurationProperty("albumDisplayDirectory", DefaultValue="Display")]
        public string AlbumDisplayDirectory
        {
            get
            {
                return Conversions.ToString(this["albumDisplayDirectory"]);
            }
            set
            {
                this["albumDisplayDirectory"] = value;
            }
        }

        [ConfigurationProperty("albumThumbNailsDirectory", DefaultValue="ThumbNails")]
        public string AlbumThumbNailsDirectory
        {
            get
            {
                return Conversions.ToString(this["albumThumbNailsDirectory"]);
            }
            set
            {
                this["albumThumbNailsDirectory"] = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [ConfigurationProperty("displayHeight", DefaultValue="500")]
        public int DisplayHeight
        {
            get
            {
                return Conversions.ToInteger(this["displayHeight"]);
            }
            set
            {
                this["displayHeight"] = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [ConfigurationProperty("displayWidth", DefaultValue="400")]
        public int DisplayWidth
        {
            get
            {
                return Conversions.ToInteger(this["displayWidth"]);
            }
            set
            {
                this["displayWidth"] = value;
            }
        }

        [ConfigurationProperty("originalsDirectory", DefaultValue="Originals")]
        public string OriginalsDirectory
        {
            get
            {
                return Conversions.ToString(this["originalsDirectory"]);
            }
            set
            {
                this["originalsDirectory"] = value;
            }
        }

        [ConfigurationProperty("photosDirectory", DefaultValue="Photos")]
        public string PhotosDirectory
        {
            get
            {
                return Conversions.ToString(this["photosDirectory"]);
            }
            set
            {
                this["photosDirectory"] = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [ConfigurationProperty("thumbHeight", DefaultValue="80")]
        public int ThumbHeight
        {
            get
            {
                return Conversions.ToInteger(this["thumbHeight"]);
            }
            set
            {
                this["thumbHeight"] = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [ConfigurationProperty("thumbWidth", DefaultValue="80")]
        public int ThumbWidth
        {
            get
            {
                return Conversions.ToInteger(this["thumbWidth"]);
            }
            set
            {
                this["thumbWidth"] = value;
            }
        }
    }
}

