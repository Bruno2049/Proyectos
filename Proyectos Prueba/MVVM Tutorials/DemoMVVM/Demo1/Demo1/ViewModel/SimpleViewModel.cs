using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.ViewModel
{
    public class SimpleViewModel : VMBase
    {
        private string _sampleText;

        public string SampleText
        {
            get { return _sampleText; }
            set
            {
                _sampleText = value;
                RaisePropertyChanged("SampleText");
            }
        }

        public SimpleViewModel()
        {
            SampleText = "Muestra de texto";
        }

    }
}
