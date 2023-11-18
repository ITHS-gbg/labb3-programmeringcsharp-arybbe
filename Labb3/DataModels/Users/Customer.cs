using System.Collections.Generic;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Users;

public class Customer : User
{
    public override UserTypes Type { get; }
    public override List<Product> Cart { get; set; } = new();

    public Customer(string name, string password) : base(name, password)
    {
        Type = UserTypes.Customer;
    }

    
}