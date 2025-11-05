namespace DataAccessLayer.Model;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int EAN { get; set; }
    public string ErpSource { get; set; }
    public bool Active { get; set; }
    public int QuantityInBag { get; set; }
    public double Weight { get; set; }
    public string Segment { get; set; }
    public string ProductCategory1 { get; set; }
    public string ProductCategory2 { get; set; }
    public string ProductCategory3 { get; set; }
}