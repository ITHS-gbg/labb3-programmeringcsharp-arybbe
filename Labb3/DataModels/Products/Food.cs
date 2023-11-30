using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class Food : BaseProduct
{
    public override ProductTypes Type { get; set; }

    public Food(string name, double price) : base(name, price)
    {
        Type = ProductTypes.Food;
    }

    
}