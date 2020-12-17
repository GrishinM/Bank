using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ATM.Commands;
using ATM.Properties;
using BankLibrary;

namespace ATM.ViewModels
{
    public sealed class ATMViewModel : INotifyPropertyChanged
    {
        private readonly BankLibrary.ATM atm;

        public int CardId => atm.CardId;

        public double Balance { get; set; }

        private readonly List<PhysicalCard> cards = new List<PhysicalCard>
        {
            new PhysicalCard {Number = 1234567891011121, Chip = new CardChip {CardId = 0}},
            new PhysicalCard {Number = 1121110987654321, Chip = new CardChip {CardId = 1}}
        };

        public ObservableCollection<PhysicalCard> Cards => new ObservableCollection<PhysicalCard>(cards);

        public event Action AuthorizationSucceeded;

        public event Action<string> AuthorizationFailed;

        public event Action TransferSucceeded;

        public event Action<string> TransferFailed;

        public ATMViewModel()
        {
            atm = new BankLibrary.ATM();
            atm.AuthorizationSucceeded += () => AuthorizationSucceeded();
            atm.AuthorizationFailed += message => AuthorizationFailed(message);
            atm.BalanceChanged += balance =>
            {
                Balance = balance;
                OnPropertyChanged(nameof(Balance));
            };
            atm.TransferSucceeded += () => TransferSucceeded();
            atm.TransferFailed += message => TransferFailed(message);
            atm.Error += e => MessageBox.Show(e.Message);
            AuthorizationCommand = new AuthorizationCommand(atm);
            CheckBalanceCommand = new CheckBalanceCommand(atm);
            TransferCommand = new TransferCommand(atm);
            InsertCardCommand = new InsertCardCommand(atm);
            ExitCommand = new ExitCommand(atm);
        }

        public AuthorizationCommand AuthorizationCommand { get; }

        public CheckBalanceCommand CheckBalanceCommand { get; }

        public TransferCommand TransferCommand { get; }

        public InsertCardCommand InsertCardCommand { get; }

        public ExitCommand ExitCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}