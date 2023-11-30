using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class Toy : Product
{
    public override ProductTypes Type { get; }

    public Toy(string name, double price) : base(name, price)
    {
        Type = ProductTypes.Toy;
    }

}