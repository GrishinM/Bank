using System;

namespace ATM.Commands
{
    public class ExitCommand : Command
    {
        public ExitCommand(BankLibrary.ATM atm) : base(atm)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            atm.ThrowCard();
        }

        public override event EventHandler CanExecuteChanged;
    }
}