namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    public class StoreElement : ConfigurationElement
    {
        [ConfigurationProperty("businessEmail", IsRequired=true)]
        public string BusinessEmail
        {
            get
            {
                return Conversions.ToString(this["businessEmail"]);
            }
            set
            {
                this["businessEmail"] = value;
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
                string connStringName = this.ConnectionStringName;
                if (string.IsNullOrEmpty(this.ConnectionStringName))
                {
                    connStringName = Globals.Settings.DefaultConnectionStringName;
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

        [ConfigurationProperty("currencyCode", DefaultValue="USD")]
        public string CurrencyCode
        {
            get
            {
                return Conversions.ToString(this["currencyCode"]);
            }
            set
            {
                this["currencyCode"] = value;
            }
        }

        [ConfigurationProperty("defaultOrderListInterval", DefaultValue="7")]
        public int DefaultOrderListInterval
        {
            get
            {
                return Conversions.ToInteger(this["defaultOrderListInterval"]);
            }
            set
            {
                this["defaultOrderListInterval"] = value;
            }
        }

        [ConfigurationProperty("departmentURLIndicator", DefaultValue="Department")]
        public string DepartmentURLIndicator
        {
            get
            {
                string lurlIndicator = this["departmentURLIndicator"].ToString();
                if (string.IsNullOrEmpty(lurlIndicator))
                {
                    lurlIndicator = "Department";
                }
                return lurlIndicator;
            }
            set
            {
                this["DepartmentURLIndicator"] = value;
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

        [ConfigurationProperty("lowAvailability", DefaultValue="10")]
        public int LowAvailability
        {
            get
            {
                return Conversions.ToInteger(this["lowAvailability"]);
            }
            set
            {
                this["lowAvailability"] = value;
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

        [ConfigurationProperty("productURLIndicator", DefaultValue="Product")]
        public string ProductURLIndicator
        {
            get
            {
                string lurlIndicator = this["productURLIndicator"].ToString();
                if (string.IsNullOrEmpty(lurlIndicator))
                {
                    lurlIndicator = "Product";
                }
                return lurlIndicator;
            }
            set
            {
                this["productURLIndicator"] = value;
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

        [ConfigurationProperty("sandboxMode", DefaultValue="false")]
        public bool SandboxMode
        {
            get
            {
                return Conversions.ToBoolean(this["sandboxMode"]);
            }
            set
            {
                this["sandboxMode"] = value;
            }
        }
    }
}

