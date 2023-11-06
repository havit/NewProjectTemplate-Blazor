using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Havit.NewProjectTemplate.Services.Mailing;

[Service]
public class MailingService : IMailingService
{
	private readonly MailingOptions _options;

	public MailingService(
		IOptions<MailingOptions> options)
	{
		_options = options.Value;
	}

	public async Task VerifyHealthAsync(CancellationToken cancellationToken)
	{
		using SmtpClient smtpClient = await CreateConnectedSmtpClientAsync(cancellationToken);
	}

	public async Task SendAsync(MimeMessage mailMessage, CancellationToken cancellationToken)
	{
		using (SmtpClient smtpClient = await CreateConnectedSmtpClientAsync(cancellationToken))
		{
			if (!mailMessage.From.Any())
			{
				mailMessage.From.Add(InternetAddress.Parse(_options.From));
			}

			await smtpClient.SendAsync(mailMessage, cancellationToken);
		}
	}

	private async Task<SmtpClient> CreateConnectedSmtpClientAsync(CancellationToken cancellationToken)
	{
		var smtpClient = new SmtpClient();
		await smtpClient.ConnectAsync(_options.SmtpServer, _options.SmtpPort ?? 0, _options.UseSsl, cancellationToken);

		if (_options.HasCredentials())
		{
			await smtpClient.AuthenticateAsync(_options.SmtpUsername, _options.SmtpPassword, cancellationToken);
		}

		return smtpClient;
	}
}
