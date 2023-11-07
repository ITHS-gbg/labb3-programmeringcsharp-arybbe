using System;
using System.Windows.Controls;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        public AdminView()
        {
            InitializeComponent();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
        }

        private void UserManager_CurrentUserChanged()
        {
            throw new NotImplementedException();
        }
        
        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var productName = ProductName.Text;
            var productPrice = ProductPrice.Text;

            if (!string.IsNullOrEmpty(productName) || !string.IsNullOrEmpty(productPrice))
            {
                var product = new Fruit(productName, double.Parse(productPrice));
                ProductManager.AddProduct(product);
            }

            
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
