using System.Windows;
using ATM.ViewModels;

namespace ATM.Views
{
    public partial class StartPage
    {
        public StartPage(ATMViewModel atmViewModel)
        {
            InitializeComponent();
            DataContext = atmViewModel;
        }

        private void InsertClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage((ATMViewModel) DataContext));
        }
    }
}