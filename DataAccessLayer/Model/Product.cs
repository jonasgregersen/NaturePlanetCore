namespace DataAccessLayer.Model;

public class Product
{
    public string ProductID { get; set; }
    public string? Name { get; set; }
    public double? EAN { get; set; }
    public string? ERP_Source { get; set; }
    public bool Active { get; set; }
    public int? Purchase_quantity_step { get; set; }
    public decimal? Weight { get; set; }
    public string? Segment { get; set; }
    public string? Product_Category_1 { get; set; }
    public string? Product_Category_2 { get; set; }
    public string? Product_Category_3 { get; set; }

    public Product(string name, double? ean, string erpSource, bool active, int? quantityInBag, decimal? weight,
        string segment, string productCategory1, string productCategory2, string productCategory3)
    {
        Name = name;
        EAN = ean ?? 0;
        ERP_Source = erpSource;
        Active = active;
        Purchase_quantity_step = quantityInBag ?? 0;
        Weight = weight ?? 0;
        Segment = segment;
        Product_Category_1 = productCategory1;
        Product_Category_2 = productCategory2;
        Product_Category_3 = productCategory3;
    }

    public Product() { }

    
}