namespace ShoppingApi.Data;

public class ShoppingItem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool Purchased { get; set; }
    public DateTime WhenAdded { get; set; }
    public bool Removed { get; set; }
}
