namespace BBICMS.Newsletters
{
    public class NewsletterStatus
    {

        private bool _isSending = false;
        public bool IsSending
        {
            get { return _isSending; }
            set { _isSending = value; }
        }

        public double PercentageCompleted
        {
            get
            {
                if (TotalMails == 0)
                {
                    return 0;
                }
                return (double)SentMails * 100 / (double)TotalMails;
            }
        }

        private int _totalMails = -1;
        public int TotalMails
        {
            get { return _totalMails; }
            set { _totalMails = value; }
        }

        private int _sentMails = 0;
        public int SentMails
        {
            get { return _sentMails; }
            set { _sentMails = value; }
        }

    }
}