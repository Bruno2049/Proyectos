using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities
{
    [Serializable()]
    public class ScheduledNotificationEntity
    {
        public int NotificationId { get; set; }
        public string ToEmail { get; set; }
        public string CCEmail { get; set; }
        public int SustitutionNumber { get; set; }
        public string CreditNumber { get; set; }
        public string FolioID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? LastSent { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
