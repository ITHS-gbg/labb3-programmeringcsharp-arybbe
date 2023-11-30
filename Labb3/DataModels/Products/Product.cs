using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public abstract class Product
{
    public string Name { get; set; }

    public double Price { get; set; }

    public abstract ProductTypes Type { get; set; }

    protected Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public abstract void Update(Product product);
}