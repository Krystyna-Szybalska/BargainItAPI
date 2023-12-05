namespace BargainIt.Application.Requests.Products;

public class ProductDto {
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public required decimal Price { get; set; }
}