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

	public async Task VerifyHealthAsync(CancellationToken cancellationToken)
	{
		// dostupnost ověříme připojením (a autentizací k SMTP serveru)
		using SmtpClient smtpClient = await CreateConnectedSmtpClientAsync(cancellationToken);
	}

	public async Task SendAsync(MimeMessage mailMessage, CancellationToken cancellationToken)
	{
		using (SmtpClient smtpClient = await CreateConnectedSmtpClientAsync(cancellationToken))
		{
			if (!mailMessage.From.Any())
			{
				mailMessage.From.Add(InternetAddress.Parse(options.From));
			}

			await smtpClient.SendAsync(mailMessage, cancellationToken);
		}
	}

	private async Task<SmtpClient> CreateConnectedSmtpClientAsync(CancellationToken cancellationToken)
	{
		var smtpClient = new SmtpClient();
		await smtpClient.ConnectAsync(options.SmtpServer, options.SmtpPort ?? 0, options.UseSsl, cancellationToken);

		if (options.HasCredentials())
		{
			await smtpClient.AuthenticateAsync(options.SmtpUsername, options.SmtpPassword, cancellationToken);
		}

		return smtpClient;
	}
}
