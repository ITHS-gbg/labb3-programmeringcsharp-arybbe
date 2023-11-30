using System;
using System.IO;
using System.Windows;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AdminTab.Visibility = Visibility.Collapsed;
            ShopTab.Visibility = Visibility.Collapsed;
            LoginTab.Visibility = Visibility.Visible;
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            UserManager.UserLoggedOut += UserManager_UserLoggedOut;
        }

        

        private void UserManager_CurrentUserChanged()
        {
            if (UserManager.IsAdminLoggedIn)
            {
                AdminTab.IsSelected = true;
                AdminTab.Visibility = Visibility.Visible;
                ShopTab.Visibility = Visibility.Visible;
                LoginTab.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShopTab.IsSelected = true;
                ShopTab.Visibility = Visibility.Visible;
                AdminTab.Visibility = Visibility.Collapsed;
                LoginTab.Visibility = Visibility.Collapsed;
            }
        }

        private void UserManager_UserLoggedOut()
        {
            LoginTab.IsSelected = true;
            LoginTab.Visibility = Visibility.Visible;
            ShopTab.Visibility = Visibility.Collapsed;
            AdminTab.Visibility = Visibility.Collapsed;
        }
    }
}
