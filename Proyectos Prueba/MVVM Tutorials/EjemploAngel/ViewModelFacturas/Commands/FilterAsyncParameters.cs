using System;


namespace ViewModel.Commands
{
    public class FilterAsyncParameters
    {
        public Action FilterComplete
        {
            get;
            private set;
        }

        public string FilterCriteria
        {
            get;
            private set;
        }

        public FilterAsyncParameters(Action filterComplete, string filterCriteria)
        {
            FilterComplete = filterComplete;
            FilterCriteria = filterCriteria;
        }
    }
}
