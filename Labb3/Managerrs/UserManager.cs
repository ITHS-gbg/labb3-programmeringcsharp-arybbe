﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.Managerrs;

public static class UserManager
{
    private static readonly IEnumerable<User>? _users = new List<User>();
    private static User _currentUser;

    public static IEnumerable<User>? Users => _users;

    public static User CurrentUser  
    {
        get => _currentUser;
        set
        { 
            _currentUser = value;
            CurrentUserChanged?.Invoke();
        }
    }


    public static event Action CurrentUserChanged;

    // Skicka detta efter att användarlistan ändrats eller lästs in
    public static event Action UserListChanged;

    public static event Action UserLoggedOut;

    public static bool IsAdminLoggedIn => CurrentUser.Type is UserTypes.Admin;

    

    public static void ChangeCurrentUser(string name, string password, UserTypes type)
    {
        User user = Users.FirstOrDefault(u => u.Name == name && u.Password == password && u.Type == type);

        if (user != null)
        {
            CurrentUser = user;

        }

    }

    public static void LogOut()
    {
        _ = CurrentUser.Type is UserTypes.MainMenu;
        UserLoggedOut.Invoke();
        MessageBox.Show("User logged out!");
    }

    public static bool AddCustomer(Customer customer)
    {
        if (_users is List<User> users)
        {
            
            if (users.Any(existingUser => existingUser.Name == customer.Name))
            {
                MessageBox.Show("Username already taken!");
                return false;
            }
            users.Add(customer);
        }
        return true;
    }

    public static bool AddAdmin(Admin admin)
    {
        if (_users is List<User> users)
        {
            if (users.Any(existingUser => existingUser.Name == admin.Name))
            {
                MessageBox.Show("Username already taken!");
                return false;
            }
            users.Add(admin);
        }
        return true;
    }

    public static async Task SaveUsersToFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
        Directory.CreateDirectory(directory);
        var usersFilePath = Path.Combine(directory, "UsersList.json");
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;

        
        var distinctUsers = Users.GroupBy(u => u.Name).Select(g => g.First()).ToList();
        var json = JsonSerializer.Serialize(distinctUsers, jsonOptions);

        using var sw = new StreamWriter(usersFilePath);
        sw.WriteLine(json);

        UserListChanged.Invoke();
        await ProductManager.SaveProductsToFile();
    }


    public static async Task LoadUsersFromFile()
    {
        
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
            Directory.CreateDirectory(directory);
            var usersListsFilePath = Path.Combine(directory, "UsersList.json");

            if (File.Exists(usersListsFilePath))
            {
                var jsonContent = File.ReadAllText(usersListsFilePath);
                using var jsonDoc = JsonDocument.Parse(jsonContent);

                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                    {
                        if (jsonElement.TryGetProperty("Type", out var typeProperty) && typeProperty.ValueKind == JsonValueKind.Number)
                        {
                            int userTypeValue = typeProperty.GetInt32();
                            User a;

                            UserTypes userType = (UserTypes)userTypeValue;

                            switch (userType)
                            {
                                case UserTypes.Admin:
                                    a = jsonElement.Deserialize<Admin>();
                                    if (_users is List<User> userAdmin)
                                    {
                                        userAdmin.Add(a);
                                    }
                                    break;
                                case UserTypes.Customer:
                                    a = jsonElement.Deserialize<Customer>();
                                    if (_users is List<User> userCustomer)
                                    {
                                        userCustomer.Add(a);
                                    }
                                    break;
                                default:
                                    MessageBox.Show($"Unknown user type: {userType}");
                                    continue; // Skip to the next iteration
                            }

                            // Check if the current user has a "Cart" property
                            if (jsonElement.TryGetProperty("Cart", out var cartProperty) && cartProperty.ValueKind == JsonValueKind.Array)
                            {
                                // Deserialize the cart as a List<Product>
                                var products = new List<Product>();
                                foreach (var productElement in cartProperty.EnumerateArray())
                                {
                                    var product = productElement.Deserialize<Product>();
                                    products.Add(product);
                                }

                                // Assign the cart to the user
                                a.Cart = products;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Type property not found or not a number in JSON element");
                        }
                    }
                }
            }
            UserListChanged.Invoke();
        

    }


    public static void Test()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
        Directory.CreateDirectory(directory);
        var usersListsFilePath = Path.Combine(directory, "UsersList.json");

        if (File.Exists(usersListsFilePath))
        {
            var jsonContent = File.ReadAllText(usersListsFilePath);
            using var jsonDoc = JsonDocument.Parse(jsonContent);

            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                {
                    if (jsonElement.TryGetProperty("Type", out var typeProperty) && typeProperty.ValueKind == JsonValueKind.Number)
                    {
                        int userTypeValue = typeProperty.GetInt32();
                        User a;

                        UserTypes userType = (UserTypes)userTypeValue;

                        switch (userType)
                        {
                            case UserTypes.Admin:
                                a = jsonElement.Deserialize<Admin>();
                                if (_users is List<User> userAdmin)
                                {
                                    userAdmin.Add(a);
                                }
                                break;
                            case UserTypes.Customer:
                                a = jsonElement.Deserialize<Customer>();
                                if (_users is List<User> userCustomer)
                                {
                                    userCustomer.Add(a);
                                }
                                break;
                            default:
                                MessageBox.Show($"Unknown user type: {userType}");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Type property not found or not a number in JSON element");
                    }
                }
            }
        }
        UserListChanged.Invoke();
    }
}