using MimeKit;

namespace Havit.NewProjectTemplate.Services.Mailing;

public interface IMailingService
{
	void Send(MimeMessage mailMessage);
}
