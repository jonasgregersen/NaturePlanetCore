namespace Business.Model;

public class Segment
{
    public string Name { get; set; }
    public List<Component> Children { get; set; } = new List<Component>();

    public void AddChild(Component child)
    {
        Children.Add(child);
    }

    public void RemoveChild(Component child)
    {
        Children.Remove(child);
    }

    public Component GetChild(int index)
    {
        if (index >= 0 && index < Children.Count)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }
        return Children[index];
    }

    public double GetPrice()
    {
        double total = 0;
        foreach (var child in Children)
        {
            total += child.getPrice();
        }
        return total;
    }
}