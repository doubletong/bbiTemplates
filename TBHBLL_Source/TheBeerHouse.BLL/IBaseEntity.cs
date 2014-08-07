namespace TheBeerHouse.BLL
{
    using System;

    /// <summary>
    /// </summary>
    /// <remarks></remarks>
    public interface IBaseEntity
    {
        bool CanAdd { get; }

        bool CanDelete { get; }

        bool CanEdit { get; }

        bool CanRead { get; }

        bool IsDirty { get; set; }

        bool IsValid { get; }

        string SetName { get; set; }
    }
}

