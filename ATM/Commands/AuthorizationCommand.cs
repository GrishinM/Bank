using System;
using System.Windows.Input;
using BankLibrary.Requests;

namespace ATM.Commands
{
    public class AuthorizationCommand : Command
    {
        public AuthorizationCommand(BankLibrary.ATM atm) : base(atm)
        {
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;
            var authorization = (Authorization) parameter;
            return authorization.Password.ToString().Length == 4;
        }

        public override void Execute(object parameter)
        {
            var authorization = (Authorization) parameter;
            atm.Authorization(authorization);
        }

        public override event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}