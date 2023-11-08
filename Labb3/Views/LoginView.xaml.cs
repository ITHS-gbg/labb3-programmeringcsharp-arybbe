using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
        }

        private void UserManager_CurrentUserChanged()
        {
            throw new NotImplementedException();
        }

        private void LoginBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RegisterAdminBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RegisterCustomerBtn_OnClickmerBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = RegisterName.Text;
            string password = RegisterPwd.Password;

            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                if (UserManager.Users is List<User> users)
                {
                    users.Add(new Customer(username, password));
                }
                UserManager.SaveUsersToFile();

                RegisterName.Text = string.Empty;
                RegisterPwd.Password = string.Empty;
            }
        }
        
    }
}
