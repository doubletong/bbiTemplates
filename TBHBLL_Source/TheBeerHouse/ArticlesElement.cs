namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    public class ArticlesElement : ConfigurationElement
    {
        [ConfigurationProperty("akismetKey")]
        public string AkismetKey
        {
            get
            {
                string lakismetKey = this["akismetKey"].ToString();
                if (string.IsNullOrEmpty(lakismetKey))
                {
                    lakismetKey = "";
                }
                return lakismetKey;
            }
            set
            {
                this["akismetKey"] = value;
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
                this["ConnectionStringName"] = value;
            }
        }

        [ConfigurationProperty("enableAkismet", DefaultValue="false")]
        public bool EnableAkismet
        {
            get
            {
                return Conversions.ToBoolean(this["enableAkismet"]);
            }
            set
            {
                this["enableAkismet"] = value;
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

        [ConfigurationProperty("enableTwitter", DefaultValue="false")]
        public bool EnableTwitter
        {
            get
            {
                return Conversions.ToBoolean(this["enableTwitter"]);
            }
            set
            {
                this["enableTwitter"] = value;
            }
        }

        [ConfigurationProperty("pageSize", DefaultValue="10")]
        public int PageSize
        {
            get
            {
                return Conversions.ToInteger(this["pageSize"]);
            }
            set
            {
                this["pageSize"] = value;
            }
        }

        [Obsolete("No longer a factor with the Entity Framework.")]
        public string ProviderType
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

        [ConfigurationProperty("ratingLockInterval", DefaultValue="15")]
        public int RatingLockInterval
        {
            get
            {
                return Conversions.ToInteger(this["ratingLockInterval"]);
            }
            set
            {
                this["ratingLockInterval"] = value;
            }
        }

        [ConfigurationProperty("reportAkismet", DefaultValue="false")]
        public bool ReportAkismet
        {
            get
            {
                return Conversions.ToBoolean(this["reportAkismet"]);
            }
            set
            {
                this["reportAkismet"] = value;
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

        [ConfigurationProperty("twitterPassword")]
        public string TwitterPassword
        {
            get
            {
                string lurlIndicator = this["twitterPassword"].ToString();
                if (string.IsNullOrEmpty(lurlIndicator))
                {
                    lurlIndicator = "TwitterPassword";
                }
                return lurlIndicator;
            }
            set
            {
                this["twitterPassword"] = value;
            }
        }

        [ConfigurationProperty("twitterUrserName")]
        public string TwitterUrserName
        {
            get
            {
                string lurlIndicator = this["twitterUrserName"].ToString();
                if (string.IsNullOrEmpty(lurlIndicator))
                {
                    lurlIndicator = "TwitterUserName";
                }
                return lurlIndicator;
            }
            set
            {
                this["twitterUrserName"] = value;
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
                    lurlIndicator = "Article";
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

