using System;
using System.Windows.Input;
using BankLibrary.Requests;

namespace ATM.Commands
{
    public class TransferCommand : Command
    {
        public TransferCommand(BankLibrary.ATM atm) : base(atm)
        {
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;
            var transfer = (Transfer) parameter;
            return transfer.CardToNumber.ToString().Length == 16 && transfer.Amount > 0;
        }

        public override void Execute(object parameter)
        {
            var transfer = (Transfer) parameter;
            atm.Transfer(transfer);
        }

        public override event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}