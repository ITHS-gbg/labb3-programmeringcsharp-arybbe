using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.Managerrs;

public static class ProductManager
{
    private static readonly IEnumerable<Product>? _products = new List<Product>();
    public static IEnumerable<Product>? Products => _products;
    
    // Skicka detta efter att produktlistan ändrats eller lästs in
    public static event Action ProductListChanged;

    static ProductManager()
    {
        if (_products is List<Product> pr)
        {
            
        }
    }

    public static void AddProduct(Product product)
    {
        if (_products is List<Product> products)
        {
            Product existingProduct = products.FirstOrDefault(p => p.Name == product.Name);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Type = product.Type;
            }
            else
            {
                products.Add(product);
            }
            
            ProductListChanged?.Invoke();
        }
       
    }

    public static void RemoveProduct(Product product)
    {
        if (_products is List<Product> products)
        {
            products.Remove(product);
            ProductListChanged?.Invoke();
        }
    }

    

    public static async Task SaveProductsToFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
        Directory.CreateDirectory(directory);
        var productsFilePath = Path.Combine(directory, "ProductsList.json");
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;


        var distinctProducts = Products.GroupBy(p => p.Name).Select(g => g.First()).ToList();
        var json = JsonSerializer.Serialize(distinctProducts, jsonOptions);

        using var sw = new StreamWriter(productsFilePath);
        sw.WriteLine(json);
        
        ProductListChanged?.Invoke();
    }

    public static async Task LoadProductsFromFile()
    {
        
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
            Directory.CreateDirectory(directory);
            var productsFilePath = Path.Combine(directory, "ProductsList.json");

            if (File.Exists(productsFilePath))
            {
                var jsonContent = File.ReadAllText(productsFilePath);
                using var jsonDoc = JsonDocument.Parse(jsonContent);

                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                    {
                        if (jsonElement.TryGetProperty("Type", out var typeProperty) && typeProperty.ValueKind == JsonValueKind.Number)
                        {
                            int productTypeValue = typeProperty.GetInt32();
                            Product a;

                            ProductTypes productTypes = (ProductTypes)productTypeValue;

                            switch (productTypes)
                            {
                                case ProductTypes.Food:
                                    a = jsonElement.Deserialize<Food>();
                                    if (_products is List<Product> foodList)
                                    {
                                        foodList.Add(a);
                                    }
                                    break;
                                case ProductTypes.Toy:
                                    a = jsonElement.Deserialize<Toy>();
                                    if (_products is List<Product> toyList)
                                    {
                                        toyList.Add(a);
                                    }
                                    break;
                                default:
                                    MessageBox.Show($"Unknown product type: {productTypeValue}");
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Type property not found or not a string in JSON element");
                        }
                    }
                }
                

            }
            
            ProductListChanged?.Invoke();
        

    }
}
