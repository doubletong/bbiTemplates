namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Configuration;

    public class TheBeerHouseSection : ConfigurationSection
    {
        [ConfigurationProperty("articles", IsRequired=true)]
        public ArticlesElement Articles
        {
            get
            {
                return (ArticlesElement) this["articles"];
            }
        }

        [ConfigurationProperty("calendar", IsRequired=true)]
        public CalendarElement Calendar
        {
            get
            {
                return (CalendarElement) this["calendar"];
            }
        }

        [ConfigurationProperty("contactForm", IsRequired=true)]
        public ContactFormElement ContactForm
        {
            get
            {
                return (ContactFormElement) this["contactForm"];
            }
        }

        [ConfigurationProperty("defaultCacheDuration", DefaultValue="600")]
        public int DefaultCacheDuration
        {
            get
            {
                return Conversions.ToInteger(this["defaultCacheDuration"]);
            }
            set
            {
                this["defaultCacheDuration"] = value;
            }
        }

        [ConfigurationProperty("defaultConnectionStringName", DefaultValue="LocalSqlServer")]
        public string DefaultConnectionStringName
        {
            get
            {
                return Conversions.ToString(this["defaultConnectionStringName"]);
            }
            set
            {
                this["DefaultConnectionStringName"] = value;
            }
        }

        [ConfigurationProperty("forums", IsRequired=true)]
        public ForumsElement Forums
        {
            get
            {
                return (ForumsElement) this["forums"];
            }
        }

        [ConfigurationProperty("gallery", IsRequired=true)]
        public GalleryElement Gallery
        {
            get
            {
                return (GalleryElement) this["gallery"];
            }
        }

        [ConfigurationProperty("newsletters", IsRequired=true)]
        public NewslettersElement Newsletters
        {
            get
            {
                return (NewslettersElement) this["newsletters"];
            }
        }

        [ConfigurationProperty("polls", IsRequired=true)]
        public PollsElement Polls
        {
            get
            {
                return (PollsElement) this["polls"];
            }
        }

        [ConfigurationProperty("siteDomainName", DefaultValue="localhost")]
        public string SiteDomainName
        {
            get
            {
                return Conversions.ToString(this["siteDomainName"]);
            }
            set
            {
                this["siteDomainName"] = value;
            }
        }

        [ConfigurationProperty("store", IsRequired=true)]
        public StoreElement Store
        {
            get
            {
                return (StoreElement) this["store"];
            }
        }
    }
}

