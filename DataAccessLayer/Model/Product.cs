namespace DataAccessLayer.Model;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int EAN { get; set; }
    public string? ErpSource { get; set; }
    public bool Active { get; set; }
    public int QuantityInBag { get; set; }
    public double Weight { get; set; }
    public string? Segment { get; set; }
    public string? Product_Category_1 { get; set; }
    public string? Product_Category_2 { get; set; }
    public string? Product_Category_3 { get; set; }

    public Product(string name, int ean, string erpSource, bool active, int quantityInBag, double weight,
        string segment, string productCategory1, string productCategory2, string productCategory3)
    {
        Name = name;
        EAN = ean;
        ErpSource = erpSource;
        Active = active;
        QuantityInBag = quantityInBag;
        Weight = weight;
        Segment = segment;
        Product_Category_1 = productCategory1;
        Product_Category_2 = productCategory2;
        Product_Category_3 = productCategory3;
    }

    public Product() { }

    
}