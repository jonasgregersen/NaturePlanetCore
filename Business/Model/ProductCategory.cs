namespace Business.Model;

public class ProductCategory: Component
{
    public string Name { get; set; }
    public List<Component> children = new List<Component>();
    
    public override double getPrice()
    {
        return 0;
    }
}