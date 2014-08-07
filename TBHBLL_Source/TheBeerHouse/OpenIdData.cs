namespace TheBeerHouse
{
    using System;
    using System.Collections.Specialized;

    /// <summary> 
    /// The data store used for keeping state between OpenID requests. 
    /// </summary>
    public class OpenIdData
    {
        public string Identity;
        public bool IsSuccess;
        public NameValueCollection Parameters = new NameValueCollection();

        public OpenIdData(string identity__1)
        {
            this.Identity = identity__1;
        }
    }
}

