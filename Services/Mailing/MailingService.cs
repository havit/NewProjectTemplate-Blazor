using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Havit.NewProjectTemplate.Services.Mailing;

[Service]
public class MailingService : IMailingService
{
	private readonly MailingOptions options;

	public MailingService(
		IOptions<MailingOptions> options)
	{
		this.options = options.Value;
	}

	public void Send(MimeMessage mailMessage)
	{
		using (SmtpClient smtpClient = new SmtpClient())
		{
			smtpClient.Connect(options.SmtpServer, options.SmtpPort ?? 0, options.UseSsl);

			if (options.HasCredentials())
			{
				smtpClient.Authenticate(options.SmtpUsername, options.SmtpPassword);
			}

			if (!mailMessage.From.Any())
			{
				mailMessage.From.Add(InternetAddress.Parse(options.From));
			}

			smtpClient.Send(mailMessage);
		}
	}
}
