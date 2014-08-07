﻿namespace BLL
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public interface IBaseEntity
    {

        bool IsValid
        {
            get;
        }
        bool IsDirty
        {
            get;
            set;
        }

        string SetName
        {
            get;
            set;
        }

        bool CanEdit
        {
            get;
        }
        bool CanRead
        {
            get;
        }
        bool CanDelete
        {
            get;
        }
        bool CanAdd
        {
            get;
        }

    }
}