namespace Business.Model;

public class ProductCategory: Component
{
    public string Name { get; set; }
    public Component Parent { get; set; }
    public List<ProductCategory> Children { get; set; } = new List<ProductCategory>();
    
    public override double getPrice()
    {
        return 0;
    }
}