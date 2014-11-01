using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entities
{
    public class ScheduleJobEntity
    {
        private int _Job_ID;
        public int Job_ID
        {
            get { return this._Job_ID; }
            set { this._Job_ID = value; }
        }

        private string _Credit_No;
        public string Credit_No
        {
            get { return this._Credit_No; }
            set { this._Credit_No = value; }
        }

        private string _Email_Title;
        public string Email_Title
        {
            get { return this._Email_Title; }
            set { this._Email_Title = value; }
        }

        private string _Email_Body;
        public string Email_Body
        {
            get { return this._Email_Body; }
            set { this._Email_Body = value; }
        }

        private string _Supplier_Name;
        public string Supplier_Name
        {
            get{return this._Supplier_Name;}
            set { this._Supplier_Name = value; }
        }

        private string _Supplier_Email;
        public string Supplier_Email
        {
            get { return this._Supplier_Email; }
            set { this._Supplier_Email = value; }
        }

        private object   _Warning_Date;
        public object Warning_Date
        {
            get { return this._Warning_Date; }
            set 
            {
                if (value == null)
                {
                    this._Warning_Date = DBNull.Value;
                }
                else
                {
                    this._Warning_Date = value;
                }
            }
        }

        private object _Canceled_Date;
        public object Canceled_Date
        {
            get { return this._Canceled_Date; }
            set
            {
                if (value == null)
                {
                    this._Canceled_Date = DBNull.Value;
                }
                else
                {
                    this._Canceled_Date = value;
                }
            }
        }

        private string _Job_Status;
        public string Job_Status
        {
            get { return this._Job_Status; }
            set { this._Job_Status = value; }
        }

        private object _Create_Date;
        public object Create_Date
        {
            get { return this._Create_Date; }
            set { this._Create_Date = value; }
        }
    }
}
