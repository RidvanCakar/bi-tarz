public class ShoppingCardDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
}