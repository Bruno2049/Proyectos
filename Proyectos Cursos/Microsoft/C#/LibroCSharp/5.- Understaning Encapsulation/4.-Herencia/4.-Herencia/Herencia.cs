using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._Herencia
{
    class Herencia
    {
    }

    public class WorkItem
    {
        private static int nextID;
        protected int ID { get; set; }
        protected TimeSpan jobLength { get; set; }
        protected string Title { get; set; }
        protected string Description { get; set; }
        // Default constructor
        public WorkItem()
        {
            ID = 0;
            Title = "Default title";
            Description = "Default description.";
            jobLength = new TimeSpan();
        }
        // Static constructor for static member.
        static WorkItem()
        {
            nextID = 0;
        }
        // Instance constructor.
        public WorkItem(string title, string desc, TimeSpan joblen)
        {
            this.ID = GetNextID();
            this.Title = title;
            this.Description = desc;
            this.jobLength = joblen;
        }
        protected int GetNextID()
        {
            return ++nextID;
        }
        public void Update(string title, TimeSpan joblen)
        {
            this.Title = title;
            this.jobLength = joblen;
        }

        // Virtual method override.
        public override string ToString()
        {
            return String.Format("{0} - {1}", this.ID, this.Title);
        }
    }

    // ChangeRequest derives from WorkItem and adds two of its own members.
    public class ChangeRequest : WorkItem
    {
        protected int originalItemID { get; set; }
        public ChangeRequest() { }
        public ChangeRequest(string title, string desc, TimeSpan jobLen, int originalID)
        {
            this.ID = GetNextID();
            this.Title = title;
            this.Description = desc;
            this.jobLength = jobLen;
            this.originalItemID = originalID;
        }
    }
}
