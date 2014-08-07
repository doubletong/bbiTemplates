namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    public class NewslettersElement : ConfigurationElement
    {
        [ConfigurationProperty("archiveIsPublic", DefaultValue="false")]
        public bool ArchiveIsPublic
        {
            get
            {
                return Conversions.ToBoolean(this["archiveIsPublic"]);
            }
            set
            {
                this["archiveIsPublic"] = value;
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

        [ConfigurationProperty("fromDisplayName", IsRequired=true)]
        public string FromDisplayName
        {
            get
            {
                return Conversions.ToString(this["fromDisplayName"]);
            }
            set
            {
                this["fromDisplayName"] = value;
            }
        }

        [ConfigurationProperty("fromEmail", IsRequired=true)]
        public string FromEmail
        {
            get
            {
                return Conversions.ToString(this["fromEmail"]);
            }
            set
            {
                this["fromEmail"] = value;
            }
        }

        [ConfigurationProperty("hideFromArchiveInterval", DefaultValue="15")]
        public int HideFromArchiveInterval
        {
            get
            {
                return Conversions.ToInteger(this["hideFromArchiveInterval"]);
            }
            set
            {
                this["hideFromArchiveInterval"] = value;
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

        [ConfigurationProperty("urlIndicator")]
        public string URLIndicator
        {
            get
            {
                string lurlIndicator = this["urlIndicator"].ToString();
                if (string.IsNullOrEmpty(lurlIndicator))
                {
                    lurlIndicator = "Newsletter";
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

