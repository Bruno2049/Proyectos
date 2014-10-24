using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;


namespace ViewModel.Commands
{
    public class FilterAsyncBehavior : Behavior<AutoCompleteBox>
    {
        public ICommand FilterAsyncCommand
        {
            get
            {
                return (ICommand)GetValue(FilterAsyncCommandProperty);
            }
            set
            {
                SetValue(FilterAsyncCommandProperty, value);
            }
        }

        public static readonly DependencyProperty FilterAsyncCommandProperty = DependencyProperty.Register("FilterAsyncCommand",
            typeof(ICommand),
            typeof(FilterAsyncBehavior),
            new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();

            // handle the populating event of the associated auto complete box
            AssociatedObject.Populating += AssociatedObject_Populating;
        }

        protected override void OnDetaching()
        {
            // detach the event handler
            AssociatedObject.Populating -= AssociatedObject_Populating;

            base.OnDetaching();
        }

        private void AssociatedObject_Populating(object sender, PopulatingEventArgs e)
        {
            // get the command
            ICommand filterCommand = FilterAsyncCommand;

            if (filterCommand != null)
            {
                // create the parameters for the command
                var parameters = new FilterAsyncParameters(AssociatedObject.PopulateComplete, e.Parameter);

                // execute command
                filterCommand.Execute(parameters);

                // cancel the population of the auto complete box
                e.Cancel = true;
            }
        }
    }
}
