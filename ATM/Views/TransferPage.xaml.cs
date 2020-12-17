﻿using System.Windows;
using ATM.ViewModels;

namespace ATM.Views
{
    public partial class TransferPage
    {
        public TransferPage(ATMViewModel atmViewModel)
        {
            InitializeComponent();
            DataContext = atmViewModel;
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage((ATMViewModel) DataContext));
        }
    }
}