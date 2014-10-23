using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_CustomException
{
    public class CarIsDeadException : ApplicationException
    {
        private string messageDetails = String.Empty;
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }
        
        public CarIsDeadException() 
        {
        }
        
        public CarIsDeadException(string message, string cause, DateTime time)
        {
            messageDetails = message;
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }

        // Override the Exception.Message property.
        public override string Message
        {
            get
            {
                return string.Format("Car Error Message: {0}", messageDetails);
            }
        }
    }
}
