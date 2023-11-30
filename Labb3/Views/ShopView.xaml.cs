using System;
using System.Collections.Generic;
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
                StoreWindowContext.ProductList.Add(product as BaseProduct);
            }

            

            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            ProductManager.ProductListChanged += ProductManager_ProductListChanged;
            UserManager.CartChanged += UserManager_CartChanged;
        }

        private void UserManager_CartChanged()
        {
            StoreWindowContext.CartProducts.Clear();

            foreach (var product in UserManager.CurrentUser.Cart)
            {
                StoreWindowContext.CartProducts.Add(product);
            }
        }

        private void ProductManager_ProductListChanged()
        {
            StoreWindowContext.ProductList.Clear();

            foreach (var product in ProductManager.Products)
            {
                StoreWindowContext.ProductList.Add(product as BaseProduct);
            }
        }

        private void UserManager_CurrentUserChanged()
        {
            
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CartList.SelectedItem is BaseProduct selectedItem)
            {
                var selectedProd = StoreWindowContext.ProductList.FirstOrDefault(p => p.Name == selectedItem.Name);

                if (selectedProd is null)
                {
                    return;
                }

                UserManager.CurrentUser.Cart.Remove(selectedItem);
                StoreWindowContext.CartProducts.Remove(selectedItem);
            }
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ProdList.SelectedItem is BaseProduct selectedItem)
            {
                var selectedProd = StoreWindowContext.ProductList.FirstOrDefault(p => p.Name == selectedItem.Name);

                if (selectedProd is null)
                {
                    return;
                }

                UserManager.CurrentUser.Cart.Add(selectedItem);
                StoreWindowContext.CartProducts.Add(selectedItem);
            }
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
            UserManager.LogOut();
            UserManager.CurrentUser.Cart.Clear();
        }

        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CartList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
