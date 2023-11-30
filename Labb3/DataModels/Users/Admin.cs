using System.Collections.Generic;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Users;

public class Admin : User
{
    public override UserTypes Type { get; }
    public override List<BaseProduct> Cart { get; set; } = new();

    public Admin(string name, string password) : base(name, password)
    {
        Type = UserTypes.Admin;
    }

}