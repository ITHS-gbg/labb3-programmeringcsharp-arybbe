using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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

    public static bool IsAdminLoggedIn => CurrentUser.Type is UserTypes.Admin;

    public static bool IsCustomerLoggedIn => CurrentUser.Type is UserTypes.Customer;

    public static void ChangeCurrentUser(string name, string password, UserTypes type)
    {
        throw new NotImplementedException();
    }

    public static void LogOut()
    {
        throw new NotImplementedException();
    }

    public static async Task SaveUsersToFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
        Directory.CreateDirectory(directory);
        var usersFilePath = Path.Combine(directory, "UsersList.json");
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;

        var json = JsonSerializer.Serialize(Users, jsonOptions);

        using var sw = new StreamWriter(usersFilePath);
        sw.WriteLine(json);

    }

    public static async Task LoadUsersFromFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb 3 Listor");
        Directory.CreateDirectory(directory);
        var usersListsFilePath = Path.Combine(directory, "UsersList.json");

        using (var jsonDoc = JsonDocument.Parse(usersListsFilePath))
        {
            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                {
                    User a;
                    switch (jsonElement.GetProperty("Type").GetString())
                    {
                        case nameof(Admin):
                            a = jsonElement.Deserialize<Admin>();
                            Users.ToList().Add(a);
                            break;
                        case nameof(Customer):
                            a = jsonElement.Deserialize<Customer>();
                            Users.ToList().Add(a);
                            break;
                    }
                }
            }

        }
    }
}