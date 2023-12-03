using MediatR;

namespace BargainIt.Application.Requests.Products.Queries.GetAllProducts; 

public class GetAllProductsQuery : IRequest<ProductDto[]> {
}