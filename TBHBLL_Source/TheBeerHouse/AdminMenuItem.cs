namespace TheBeerHouse
{
    using System;

    public class AdminMenuItem
    {
        private string _imageURL;
        private string _name;
        private string _uRL;

        public AdminMenuItem(string vName, string vURL)
        {
            this.MenuName = vName;
            this.URL = vURL;
        }

        public AdminMenuItem(string vName, string vImageURL, string vURL)
        {
            this.MenuName = vName;
            this.ImageURL = vImageURL;
            this.URL = vURL;
        }

        public string ImageURL
        {
            get
            {
                return this._imageURL;
            }
            set
            {
                this._imageURL = value;
            }
        }

        public string MenuName
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string URL
        {
            get
            {
                return this._uRL;
            }
            set
            {
                this._uRL = value;
            }
        }
    }
}

