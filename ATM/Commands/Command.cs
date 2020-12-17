using System;
using System.Windows.Input;

namespace ATM.Commands
{
    public abstract class Command : ICommand
    {
        protected readonly BankLibrary.ATM atm;

        protected Command(BankLibrary.ATM atm)
        {
            this.atm = atm;
        }

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
        public abstract event EventHandler CanExecuteChanged;
    }
}