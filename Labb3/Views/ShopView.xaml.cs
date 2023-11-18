using System;
using System.Windows;
using System.Windows.Controls;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {
        public ShopView()
        {
            InitializeComponent();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
        }

        private void UserManager_CurrentUserChanged()
        {
            
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (UserManager.CurrentUser.Type is UserTypes.Admin)
            {
                MessageBox.Show("You're not a customer! Log out from Admin view!");
                return;
            }
            UserManager.LogOut();
        }

        private void CheckoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
