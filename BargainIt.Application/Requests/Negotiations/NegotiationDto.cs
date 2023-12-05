namespace BargainIt.Application.Requests.Negotiations;

public class NegotiationDto {
	public Guid Id { get; set; }

	public required decimal ProposedPrice { get; set; }
	public bool? IsAccepted { get; set; }

	public Guid ProductId { get; set; }
}