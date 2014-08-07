namespace TheBeerHouse.BLL.Store
{
    using System;

    public enum StatusCode
    {
        Canceled = 5,
        Confirmed = 2,
        Shipped = 4,
        Verified = 3,
        WaitingForPayment = 1
    }
}

