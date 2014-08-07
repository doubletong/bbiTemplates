namespace TheBeerHouse
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Configuration;

    public class ContactFormElement : ConfigurationElement
    {
        [ConfigurationProperty("mailCC")]
        public string MailCC
        {
            get
            {
                return Conversions.ToString(this["mailCC"]);
            }
            set
            {
                this["mailCC"] = value;
            }
        }

        [ConfigurationProperty("mailSubject", DefaultValue="Mail from TheBeerHouse: {0}")]
        public string MailSubject
        {
            get
            {
                return Conversions.ToString(this["mailSubject"]);
            }
            set
            {
                this["mailSubject"] = value;
            }
        }

        [ConfigurationProperty("mailTo", IsRequired=true)]
        public string MailTo
        {
            get
            {
                return Conversions.ToString(this["mailTo"]);
            }
            set
            {
                this["mailTo"] = value;
            }
        }
    }
}

