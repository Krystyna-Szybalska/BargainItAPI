using BargainIt.Application.Services.Emails;

namespace BargainIt.Infrastructure.Services.Emails; 

public class EmailService : IEmailService {
	public Task SendEmail() {
		return Task.CompletedTask;
	}
}