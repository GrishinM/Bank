using System;

namespace ATM.Commands
{
    public class CheckBalanceCommand : Command
    {
        public CheckBalanceCommand(BankLibrary.ATM atm) : base(atm)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            atm.CheckBalance();
        }

        public override event EventHandler CanExecuteChanged;
    }
}