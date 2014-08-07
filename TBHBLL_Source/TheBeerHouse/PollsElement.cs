namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    public class PollsElement : ConfigurationElement
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
                if (duration <= 0)
                {
                    duration = Globals.Settings.DefaultCacheDuration;
                }
                return duration;
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
                    lurlIndicator = "Poll";
                }
                return lurlIndicator;
            }
            set
            {
                this["urlIndicator"] = value;
            }
        }

        [ConfigurationProperty("votingLockByCookie", DefaultValue="true")]
        public bool VotingLockByCookie
        {
            get
            {
                return Conversions.ToBoolean(this["votingLockByCookie"]);
            }
            set
            {
                this["votingLockByCookie"] = value;
            }
        }

        [ConfigurationProperty("votingLockByIP", DefaultValue="true")]
        public bool VotingLockByIP
        {
            get
            {
                return Conversions.ToBoolean(this["votingLockByIP"]);
            }
            set
            {
                this["votingLockByIP"] = value;
            }
        }

        [ConfigurationProperty("votingLockInterval", DefaultValue="15")]
        public int VotingLockInterval
        {
            get
            {
                return Conversions.ToInteger(this["votingLockInterval"]);
            }
            set
            {
                this["votingLockInterval"] = value;
            }
        }
    }
}

