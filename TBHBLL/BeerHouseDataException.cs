using System;

public class BeerHouseDataException : Exception
{

    #region " Properties "

    private string _propertyName;
    public string PropertyName
    {
        get { return _propertyName; }
        set { _propertyName = value; }
    }

    private string _propertyValue;
    public string PropertyValue
    {
        get { return _propertyValue; }
        set { _propertyValue = value; }
    }

    #endregion


    #region " Constructors "

    public BeerHouseDataException(string Message, Exception InnerException, string PropName, string PropValue)
        : base(Message, InnerException)
    {
        PropertyName = PropName;
        PropertyValue = PropValue;
    }

    public BeerHouseDataException(string Message, string PropName, string PropValue)
        : this(Message, null, PropName, PropValue)
    {
    }

    public BeerHouseDataException(string PropName, string PropValue)
        : this("A property of an object was inproperly set", null, PropName, PropValue)
    {
    }

    #endregion

}