using System.Collections.Generic;
using BBICMS.Polls;

namespace BBICMS.BLL
{
    public class PollOptionList : List<PollOption>
    {

        public double Percentage(int lVotes)
        {
            double totalVotes = TotalVotes();

            return (lVotes * 100) / totalVotes > 0 ? totalVotes : 1;
        }

        public int TotalVotes()
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