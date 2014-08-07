namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    public class ForumsElement : ConfigurationElement
    {
        [ConfigurationProperty("bronzePosterDescription", DefaultValue="Bronze Poster")]
        public string BronzePosterDescription
        {
            get
            {
                return Conversions.ToString(this["bronzePosterDescription"]);
            }
            set
            {
                this["bronzePosterDescription"] = value;
            }
        }

        [ConfigurationProperty("bronzePosterPosts", DefaultValue="100")]
        public int BronzePosterPosts
        {
            get
            {
                return Conversions.ToInteger(this["bronzePosterPosts"]);
            }
            set
            {
                this["bronzePosterPosts"] = value;
            }
        }

        [ConfigurationProperty("cacheDuration")]
        public int CacheDuration
        {
            get
            {
                int duration = Conversions.ToInteger(this["cacheDuration"]);
                if (duration > 0)
                {
                    return duration;
                }
                return Globals.Settings.DefaultCacheDuration;
            }
            set
            {
                this["cacheDuration"] = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                string connStringName;
                if (string.IsNullOrEmpty(this.ConnectionStringName))
                {
                    connStringName = Globals.Settings.DefaultConnectionStringName;
                }
                else
                {
                    connStringName = this.ConnectionStringName;
                }
                return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            }
        }

        [ConfigurationProperty("connectionStringName")]
        public string ConnectionStringName
        {
            get
            {
                return Conversions.ToString(this["connectionStringName"]);
            }
            set
            {
                this["connectionStringName"] = value;
            }
        }

        [ConfigurationProperty("enableCaching", DefaultValue="true")]
        public bool EnableCaching
        {
            get
            {
                return Conversions.ToBoolean(this["enableCaching"]);
            }
            set
            {
                this["enableCaching"] = value;
            }
        }

        [ConfigurationProperty("goldPosterDescription", DefaultValue="Gold Poster")]
        public string GoldPosterDescription
        {
            get
            {
                return Conversions.ToString(this["goldPosterDescription"]);
            }
            set
            {
                this["goldPosterDescription"] = value;
            }
        }

        [ConfigurationProperty("goldPosterPosts", DefaultValue="1000")]
        public int GoldPosterPosts
        {
            get
            {
                return Conversions.ToInteger(this["goldPosterPosts"]);
            }
            set
            {
                this["goldPosterPosts"] = value;
            }
        }

        [ConfigurationProperty("hotThreadPosts", DefaultValue="25")]
        public int HotThreadPosts
        {
            get
            {
                return Conversions.ToInteger(this["hotThreadPosts"]);
            }
            set
            {
                this["hotThreadPosts"] = value;
            }
        }

        [ConfigurationProperty("postsPageSize", DefaultValue="10")]
        public int PostsPageSize
        {
            get
            {
                return Conversions.ToInteger(this["postsPageSize"]);
            }
            set
            {
                this["postsPageSize"] = value;
            }
        }

        [Obsolete("No longer a factor with the Entity Framework.")]
        public string Providertype
        {
            get
            {
                return Conversions.ToString(this["providerType"]);
            }
            set
            {
                this["providerType"] = value;
            }
        }

        [ConfigurationProperty("rssItems", DefaultValue="5")]
        public int RssItems
        {
            get
            {
                return Conversions.ToInteger(this["rssItems"]);
            }
            set
            {
                this["rssItems"] = value;
            }
        }

        [ConfigurationProperty("silverPosterDescription", DefaultValue="Silver Poster")]
        public string SilverPosterDescription
        {
            get
            {
                return Conversions.ToString(this["silverPosterDescription"]);
            }
            set
            {
                this["silverPosterDescription"] = value;
            }
        }

        [ConfigurationProperty("silverPosterPosts", DefaultValue="500")]
        public int SilverPosterPosts
        {
            get
            {
                return Conversions.ToInteger(this["silverPosterPosts"]);
            }
            set
            {
                this["silverPosterPosts"] = value;
            }
        }

        [ConfigurationProperty("threadsPageSize", DefaultValue="25")]
        public int ThreadsPageSize
        {
            get
            {
                return Conversions.ToInteger(this["threadsPageSize"]);
            }
            set
            {
                this["threadsPageSize"] = value;
            }
        }

        [ConfigurationProperty("urlIndicator")]
        public string URLIndicator
        {
            get
            {
                string lurlIndicator = this["urlIndicator"].ToString();
                if (string.IsNullOrEmpty(lurlIndicator))
                {
                    lurlIndicator = "Forum";
                }
                return lurlIndicator;
            }
            set
            {
                this["urlIndicator"] = value;
            }
        }
    }
}

