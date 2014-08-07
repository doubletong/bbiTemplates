namespace TheBeerHouse
{
    using System;

    public interface ICacheItemExpiration
    {
        bool HasExpired();
        void Notify();
    }
}

