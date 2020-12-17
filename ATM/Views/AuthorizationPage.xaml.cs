using ATM.ViewModels;

namespace ATM.Views
{
    public partial class AuthorizationPage
    {
        public AuthorizationPage(ATMViewModel atmViewModel)
        {
            InitializeComponent();
            DataContext = atmViewModel;
            PasswordBox.Focus();
            // atmViewModel.AuthorizationSucceeded += () => Helper.Event(() => NavigationService.Navigate(new MainMenu(atmViewModel)));
            // atmViewModel.AuthorizationSucceeded += () => Dispatcher.BeginInvoke(new ThreadStart(() => NavigationService.Navigate(new MainMenuPage(atmViewModel))));
            // atmViewModel.AuthorizationSucceeded += () => Dispatcher.Invoke(() => NavigationService.Navigate(new MainMenuPage(atmViewModel)));
            // atmViewModel.AuthorizationSucceeded += () => NavigationService.Navigate(new MainMenuPage(atmViewModel));
            // atmViewModel.AuthorizationFailed += message => MessageBox.Show(message);
        }
    }
}