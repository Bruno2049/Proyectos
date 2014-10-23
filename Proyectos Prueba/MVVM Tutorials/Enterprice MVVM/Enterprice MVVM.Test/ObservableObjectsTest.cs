using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enterprice_MVVM.Windows;

namespace Enterprice_MVVM.Test
{
    [TestClass]
    public class ObservableObjectsTest
    {
        [TestMethod]
        public void PropertyChangedEventHandlerIsRaised()
        {
            var obj = new StubObservableObject();

            var raised = false;

            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = true;
            };

            obj.ChangedProperty = "Some value";

            if(!raised)
                Assert.Fail("ChangedProperty was never invoked");
        }
    }

    class StubObservableObject : ObservableObject
    {
        private string _changedProperty;

        public string ChangedProperty
        {
            get
            {
                return _changedProperty;
            }

            set
            {
                _changedProperty = value;
                NotifyPropertyChanged();
            }
        }
    }
}
