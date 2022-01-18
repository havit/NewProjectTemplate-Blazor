namespace Havit.NewProjectTemplate.Contracts.Infrastructure;

public class MailingOptions
{
	public string SmtpServer { get; set; }
	public int? SmtpPort { get; set; }
	public bool UseSsl { get; set; }

	public string SmtpUsername { get; set; }
	public string SmtpPassword { get; set; }
	public string From { get; set; }

	public bool HasCredentials()
	{
		return !String.IsNullOrEmpty(SmtpUsername);
	}
}
