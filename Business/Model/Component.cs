namespace Business.Model;

public abstract class Component
{
    public List<Component> Children { get; set; }
    public abstract double getPrice();
}