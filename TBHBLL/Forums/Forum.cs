using System;
using BLL;

namespace BBICMS.Forums
{
    public partial class Forum : IBaseEntity
    {
        #region " On Changing Methods "

        partial void OnAddedDateChanging(DateTime value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The AddedDate cannot be less than 0.")
            // End If

            // If value <> AddedDate Then
            // IsDirty = True
            // End If
        }

        partial void OnTitleChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The Title cannot be less than 0.")
            // End If

            // If value <> Title Then
            // IsDirty = True
            // End If
        }

        partial void OnModeratedChanging(bool value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The Moderated cannot be less than 0.")
            // End If

            // If value <> Moderated Then
            // IsDirty = True
            // End If
        }

        partial void OnImportanceChanging(int value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The Importance cannot be less than 0.")
            // End If

            // If value <> Importance Then
            // IsDirty = True
            // End If
        }

        partial void OnDescriptionChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The Description cannot be less than 0.")
            // End If

            // If value <> Description Then
            // IsDirty = True
            // End If
        }

        partial void OnImageUrlChanging(string value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The ImageUrl cannot be less than 0.")
            // End If

            // If value <> ImageUrl Then
            // IsDirty = True
            // End If
        }

        partial void OnUpdatedDateChanging(DateTime value)
        {
            // If value < 0 Then
            // Throw New ArgumentException("The UpdatedDate cannot be less than 0.")
            // End If

            // If value <> UpdatedDate Then
            // IsDirty = True
            // End If
        }

        #endregion

        private string _postBody;


        private string _setName = "Forums";
        private bool bIsDirty = false;

        public string PostBody
        {
            get { return _postBody; }
            set { _postBody = value; }
        }

        #region IBaseEntity Members

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Title) == false & string.IsNullOrEmpty(Description) == false)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsDirty
        {
            get { return bIsDirty; }
            set { bIsDirty = value; }
        }


        public bool CanAdd
        {
            get { return true; }
        }

        public bool CanDelete
        {
            get { return true; }
        }

        public bool CanEdit
        {
            get { return true; }
        }

        public bool CanRead
        {
            get { return true; }
        }

        #endregion

        partial void OnAddedByChanging(string value)
        {
            if (string.IsNullOrEmpty(AddedBy) == false)
            {
                throw new ArgumentException("Cannot change the AddedBy property once it has been set.");
            }
        }
    }
}