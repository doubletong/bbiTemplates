namespace TheBeerHouse.BLL
{
    using System;
    using System.Collections.Generic;

    public class PollOptionList : List<PollOption>
    {
        public double this[int lVotes]
        {
            get
            {
                return (((double) (lVotes * 100)) / ((this.TotalVotes > 0) ? ((double) this.TotalVotes) : ((double) 1)));
            }
        }

        public int TotalVotes
        {
            get
            {
                int lVotes = 0;
                foreach (PollOption lPollOption in this)
                {
                    lVotes += lPollOption.Votes;
                }
                return lVotes;
            }
        }
    }
}

