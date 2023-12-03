using MediatR;

namespace BargainIt.Application.Requests.Products.Queries.GetAllProducts; 

public class GetAllProductsQuery : IRequest<ProductDto[]> {
	//todo czy potrzebuje tu cos wspisywac? jesli nie, to czemu bylo tu co w getalerts w wildalert?
}