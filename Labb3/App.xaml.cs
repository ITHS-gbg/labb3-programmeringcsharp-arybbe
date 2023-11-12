using System.Windows;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            UserManager.LoadUsersFromFile();
            ProductManager.LoadProductsFromFile();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            UserManager.SaveUsersToFile();
            ProductManager.SaveProductsToFile();
        }
    }
}
