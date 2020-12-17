using System;
using System.Windows;
using ATM.ViewModels;

namespace ATM.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var atmViewModel = new ATMViewModel();
            atmViewModel.AuthorizationSucceeded += () => Dispatcher.Invoke(() => Frame.NavigationService.Navigate(new MainMenuPage(atmViewModel)));
            atmViewModel.AuthorizationFailed += message => MessageBox.Show(message);
            atmViewModel.TransferSucceeded += () => Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Успешный перевод");
                Frame.NavigationService.Navigate(new MainMenuPage(atmViewModel));
            });
            atmViewModel.TransferFailed += message => Dispatcher.Invoke(() =>
            {
                MessageBox.Show(message);
                Frame.NavigationService.Navigate(new MainMenuPage(atmViewModel));
            });
            Frame.NavigationService.Navigate(new StartPage(atmViewModel));
            Closing += (s, e) => Environment.Exit(0);
        }
    }
}