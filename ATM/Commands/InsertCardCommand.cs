using System;
using System.Windows.Input;
using BankLibrary;

namespace ATM.Commands
{
    public class InsertCardCommand : Command
    {
        public InsertCardCommand(BankLibrary.ATM atm) : base(atm)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public override void Execute(object parameter)
        {
            var card = (PhysicalCard) parameter;
            atm.AcceptCard(card);
        }

        public override event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}