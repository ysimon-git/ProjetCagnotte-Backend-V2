namespace ProjetCagnotte.API.Requests;

public class CreateProductRequest
{
    public string ProductName { get; set; } = string.Empty;

    public string ProductDescription { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public IFormFile Image { get; set; } = null!;
}