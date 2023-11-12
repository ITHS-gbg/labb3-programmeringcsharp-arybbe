using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;
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
            
        }

        private void LoginBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (UserManager.Users is List<User> users)
            {
                var foundUser = users.FirstOrDefault(user => user.Name == LoginName.Text);

                if (foundUser != null)
                {
                    var userType = foundUser.Type;

                    UserManager.ChangeCurrentUser(LoginName.Text, LoginPwd.Password, userType);
                }
                else
                {
                    MessageBox.Show("User doesn't exist, try something else or register!");
                }
            }
        }

        private void RegisterAdminBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string username = RegisterName.Text;
            string password = RegisterPwd.Password;

            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                var admin = new Admin(username, password);
                UserManager.AddAdmin(admin);

                RegisterName.Text = string.Empty;
                RegisterPwd.Password = string.Empty;
            }
        }

        private void RegisterCustomerBtn_OnClickmerBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = RegisterName.Text;
            string password = RegisterPwd.Password;

            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                var customer = new Customer(username, password);
                UserManager.AddCustomer(customer);

                RegisterName.Text = string.Empty;
                RegisterPwd.Password = string.Empty;
            }
        }
        
    }
}
