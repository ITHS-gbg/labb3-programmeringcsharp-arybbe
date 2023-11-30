using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        

        public StoreWindowContext StoreWindowContext { get; set; }

        public AdminView()
        {
            InitializeComponent();
            StoreWindowContext = new StoreWindowContext();
            DataContext = StoreWindowContext;
            
            foreach (var product in ProductManager.Products)
            {
                ProdList.Items.Add(product);
            }

            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
        }

        private void UserManager_CurrentUserChanged()
        {
            
        }
        
        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProdList.SelectedItem is Product selectedItem)
            {
                StoreWindowContext.ProdName = selectedItem.Name;
                StoreWindowContext.ProdPrice = selectedItem.Price.ToString();
            }
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var productList = (ObservableCollection<Product>)ProdList.ItemsSource;
            
            var productName = StoreWindowContext.ProdName;
            var productPrice = StoreWindowContext.ProdPrice;

            if (!StoreWindowContext.IsFood && !StoreWindowContext.IsToy)
            {
                MessageBox.Show("Choose which product type!");
                return;
            }

            if (!string.IsNullOrEmpty(productName) || !string.IsNullOrEmpty(productPrice))
            {
                if (StoreWindowContext.IsToy)
                {
                    var toyProduct = new Toy(productName, double.Parse(productPrice));
                    productList.Add(toyProduct);
                    ProductManager.AddProduct(toyProduct);
                }

                if (StoreWindowContext.IsFood)
                {
                    var foodProduct = new Food(productName, double.Parse(productPrice));
                    productList.Add(foodProduct);
                    ProductManager.AddProduct(foodProduct);
                }
            }

            StoreWindowContext.ProdName = string.Empty;
            StoreWindowContext.ProdPrice = string.Empty;

        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.LogOut();
        }
    }
}
