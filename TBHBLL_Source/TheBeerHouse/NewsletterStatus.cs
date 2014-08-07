namespace TheBeerHouse
{
    using System;

    public class NewsletterStatus
    {
        private bool _isSending = false;
        private int _sentMails = 0;
        private int _totalMails = -1;

        public bool IsSending
        {
            get
            {
                return this._isSending;
            }
            set
            {
                this._isSending = value;
            }
        }

        public double PercentageCompleted
        {
            get
            {
                if (this.TotalMails == 0)
                {
                    return 0.0;
                }
                return ((this.SentMails * 100.0) / ((double) this.TotalMails));
            }
        }

        public int SentMails
        {
            get
            {
                return this._sentMails;
            }
            set
            {
                this._sentMails = value;
            }
        }

        public int TotalMails
        {
            get
            {
                return this._totalMails;
            }
            set
            {
                this._totalMails = value;
            }
        }
    }
}

