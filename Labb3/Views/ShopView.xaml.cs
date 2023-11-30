using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Labb3ProgTemplate.DataContext;
using Labb3ProgTemplate.DataModels.Products;
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
        public StoreWindowContext StoreWindowContext { get; set; }

        public ShopView()
        {
            InitializeComponent();
            StoreWindowContext = new StoreWindowContext();
            DataContext = StoreWindowContext;

            foreach (var product in ProductManager.Products)
            {
                StoreWindowContext.Products.Add(product);
            }

            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            ProductManager.ProductListChanged += ProductManager_ProductListChanged;
        }

        private void ProductManager_ProductListChanged()
        {
            StoreWindowContext.Products.Clear();

            foreach (var product in ProductManager.Products)
            {
                StoreWindowContext.Products.Add(product);
            }
        }

        private void UserManager_CurrentUserChanged()
        {
            
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CartList.SelectedItem is Product selectedItem)
            {
                var selectedProd = StoreWindowContext.Products.FirstOrDefault(p => p.Name == selectedItem.Name);

                if (selectedProd is null)
                {
                    return;
                }

                UserManager.CurrentUser.Cart.Remove(selectedItem);
            }

            StoreWindowContext.CartProducts.Clear();
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ProdList.SelectedItem is Product selectedItem)
            {
                var selectedProd = StoreWindowContext.Products.FirstOrDefault(p => p.Name == selectedItem.Name);

                if (selectedProd is null)
                {
                    return;
                }

                UserManager.CurrentUser.Cart.Add(selectedItem);
            }

            StoreWindowContext.CartProducts.Clear();
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
            UserManager.CurrentUser.Cart.Clear();
        }

        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
