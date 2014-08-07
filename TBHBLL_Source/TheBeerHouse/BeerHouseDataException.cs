namespace TheBeerHouse
{
    using System;

    public class BeerHouseDataException : Exception
    {
        private string _propertyName;
        private string _propertyValue;

        public BeerHouseDataException(string PropName, string PropValue) : base("A property of an object was inproperly set")
        {
            this.PropertyName = PropName;
            this.PropertyValue = PropValue;
        }

        public BeerHouseDataException(string Message, string PropName, string PropValue) : base(Message, null)
        {
            this.PropertyName = PropName;
            this.PropertyValue = PropValue;
        }

        public BeerHouseDataException(string Message, Exception InnerException, string PropName, string PropValue) : base(Message, InnerException)
        {
            this.PropertyName = PropName;
            this.PropertyValue = PropValue;
        }

        public string PropertyName
        {
            get
            {
                return this._propertyName;
            }
            set
            {
                this._propertyName = value;
            }
        }

        public string PropertyValue
        {
            get
            {
                return this._propertyValue;
            }
            set
            {
                this._propertyValue = value;
            }
        }
    }
}

