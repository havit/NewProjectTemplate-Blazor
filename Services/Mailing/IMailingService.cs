using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace Havit.NewProjectTemplate.Services.Mailing
{
	public interface IMailingService
	{
		void Send(MimeMessage mailMessage);
	}
}
