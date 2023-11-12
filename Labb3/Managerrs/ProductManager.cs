using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.DataModels.Users;

namespace Labb3ProgTemplate.Managerrs;

public static class ProductManager
{
    private static readonly IEnumerable<Product>? _products = new List<Product>();
    public static IEnumerable<Product>? Products => _products;
    
    // Skicka detta efter att produktlistan ändrats eller lästs in
    public static event Action ProductListChanged;

    static ProductManager()
    {
        
    }

    public static void AddProduct(Product product)
    {
        if (_products is List<Product> products)
        {
            products.Add(product);
        }
    }

    public static void RemoveProduct(Product product)
    {
        if (_products is List<Product> products)
        {
            products.Remove(product);
        }
    }

    public static async Task SaveProductsToFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
        Directory.CreateDirectory(directory);
        var productsFilePath = Path.Combine(directory, "ProductsList.json");
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;

        var json = JsonSerializer.Serialize(Products, jsonOptions);

        using var sw = new StreamWriter(productsFilePath);
        sw.WriteLine(json);
    }

    public static async Task LoadProductsFromFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
        Directory.CreateDirectory(directory);
        var productsListsFilePath = Path.Combine(directory, "ProductsList.json");

        using (var jsonDoc = JsonDocument.Parse(productsListsFilePath))
        {
            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                {
                    Product a;
                    switch (jsonElement.GetProperty("Type").GetString())
                    {
                        case nameof(Food):
                            a = jsonElement.Deserialize<Food>();
                            Products.ToList().Add(a);
                            break;
                        case nameof(Electronic):
                            a = jsonElement.Deserialize<Electronic>();
                            Products.ToList().Add(a);
                            break;
                    }
                }
            }

        }
    }
}
