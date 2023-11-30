using System;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        

        public AdminWindowContext AdminWindowContext { get; set; }

        public AdminView()
        {
            InitializeComponent();
            AdminWindowContext = new AdminWindowContext();
            DataContext = AdminWindowContext;

            foreach (var product in ProductManager.Products)
            {
                AdminWindowContext.ProductList.Add(product as BaseProduct);
            }
            

            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            ProductManager.ProductListChanged += ProductManager_ProductListChanged;
        }

        private void ProductManager_ProductListChanged()
        {
            AdminWindowContext.ProductList.Clear();
            
            foreach (var product in ProductManager.Products)
            {
                AdminWindowContext.ProductList.Add(product as BaseProduct);
            }
        }

        private void UserManager_CurrentUserChanged()
        {
            
        }
        
        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProdList.SelectedItem is Product selectedItem)
            {
                AdminWindowContext.ProdName = selectedItem.Name;
                AdminWindowContext.ProdPrice = selectedItem.Price.ToString();
            }
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            var productName = AdminWindowContext.ProdName;
            var productPrice = AdminWindowContext.ProdPrice;

            if (!AdminWindowContext.IsFood && !AdminWindowContext.IsToy)
            {
                MessageBox.Show("Choose which product type!");
                return;
            }

            

            if (!string.IsNullOrEmpty(productName) || !string.IsNullOrEmpty(productPrice))
            {
                if (AdminWindowContext.IsToy)
                {
                    
                    var toyProduct = new Toy(productName, double.Parse(productPrice));
                    ProductManager.AddProduct(toyProduct);
                }

                if (AdminWindowContext.IsFood)
                {
                    var foodProduct = new Food(productName, double.Parse(productPrice));
                    ProductManager.AddProduct(foodProduct);
                }
            }

            AdminWindowContext.ProdName = string.Empty;
            AdminWindowContext.ProdPrice = string.Empty;

        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ProdList.SelectedItem is BaseProduct selectedItem)
            {
                var selectedProd = AdminWindowContext.ProductList.FirstOrDefault(p => p.Name == selectedItem.Name);

                if (selectedProd is null)
                {
                    return;
                }

                ProductManager.RemoveProduct(selectedItem);
                AdminWindowContext.ProductList.Remove(selectedItem);
            }

            AdminWindowContext.ProdName = string.Empty;
            AdminWindowContext.ProdPrice = string.Empty;
        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.LogOut();
        }
    }
}
