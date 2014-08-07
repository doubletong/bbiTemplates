using System.Linq;
using BLL;

namespace BBICMS.Polls
{

    public partial class Poll : IBaseEntity
    {

        private int _Votes = 0;
        public int Votes
        {
            get {
                if (this._Votes == 0) {
                    if ((PollOptions != null)) {
                        //PollOptions.IsLoaded = False 
                        this.PollOptions.Load();
                    }
                    
                    _Votes = (from po in PollOptions
                                select new {Votes = po.Votes}).Sum(p => p.Votes);
                }
                    
                return _Votes;
            }
            set { _Votes = value; }
        }


        public bool CanAdd
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanDelete
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanEdit
        {
            get
            {
                if (Helpers.CurrentUser.IsInRole("Administrator") | Helpers.CurrentUser.IsInRole("Editor"))
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanRead
        {
            get { return true; }
        }


        private bool _isDirty = false;
        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(QuestionText) == false)
                {
                    return true;
                }
                return false;
            }
        }

        private string _SetName;
        public string SetName
        {
            get { return _SetName; }
            set { _SetName = value; }
        }

    }
}