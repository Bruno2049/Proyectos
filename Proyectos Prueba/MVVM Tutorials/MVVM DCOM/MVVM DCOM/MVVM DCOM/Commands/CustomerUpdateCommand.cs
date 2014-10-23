namespace MVVM_DCOM.Commands
{
    using System.Windows.Input;
    using ViewModels;
    using System;

    internal class CustomerUpdateCommand : ICommand
    {
        public CustomerUpdateCommand(CustomerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private readonly CustomerViewModel _viewModel;
        
        public event EventHandler CanExecuteChanged
        {
            add 
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return String.IsNullOrWhiteSpace(_viewModel.Customer.Error);
        }

        public void Execute(object parameter)
        {
            _viewModel.SaveChanges();
        }
    }
}