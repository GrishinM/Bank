using System.Windows;
using ATM.ViewModels;

namespace ATM.Views
{
    public partial class MainMenuPage
    {
        public MainMenuPage(ATMViewModel atmViewModel)
        {
            InitializeComponent();
            DataContext = atmViewModel;
        }

        private void CheckBalanceClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BalancePage((ATMViewModel) DataContext));
        }

        private void TransferClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TransferPage((ATMViewModel) DataContext));
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage((ATMViewModel) DataContext));
        }
    }
}