using System;
using System.Threading;
using BLL;

namespace BBICMS.Newsletters
{
    public partial class Newsletter : IBaseEntity
    {
        private static bool _isSending = false;
        private static int _sentMails = 0;
        private static int _totalMails = -1;
        public static ReaderWriterLock Lock = new ReaderWriterLock();
        private string _setName = "tbh_Newsletterss";
        private bool bIsDirty = false;

        public static bool IsSending
        {
            get { return _isSending; }
            set { _isSending = value; }
        }

        public static double PercentageCompleted
        {
            get
            {
                if (TotalMails == 0)
                {
                    return 0;
                }
                return (double) SentMails*100/(double) TotalMails;
            }
        }

        public static int TotalMails
        {
            get { return _totalMails; }
            set { _totalMails = value; }
        }

        public static int SentMails
        {
            get { return _sentMails; }
            set { _sentMails = value; }
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
                if (string.IsNullOrEmpty(Subject) == false &
                    (string.IsNullOrEmpty(PlainTextBody) == false | string.IsNullOrEmpty(HtmlBody) == false))
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