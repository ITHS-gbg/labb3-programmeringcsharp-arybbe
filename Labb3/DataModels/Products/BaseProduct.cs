using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class BaseProduct : Product
{
    public BaseProduct(string name, double price) : base(name, price)
    {
    }

    public override ProductTypes Type { get; set; }
    
}