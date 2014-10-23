using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Classes
{
    class TableClass
    {
        public TableClass(string tableNo, Int16 capacity, string state)
        {
            this._tableNo = tableNo;
            this._capacity = capacity;
            this._state = state;
        }

        private string _tableNo = "";
        public string TableNo
        {
            get { return _tableNo; }
            set { _tableNo = value; }
        }

        private Int16 _capacity;
        public Int16 Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        private string _state = "";
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
    }
}
