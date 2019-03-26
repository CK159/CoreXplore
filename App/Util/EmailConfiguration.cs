using System.Net;
using Serilog.Sinks.Email;

namespace App.Util
{
	public class EmailConfiguration
	{
		public string From { get; set; }
		public string To { get; set; }
		public string Server { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool EnableSsl { get; set; }
		public int Port { get; set; }
		public string Subject { get; set; }

		public EmailConnectionInfo ToEmailConnectionInfo()
		{
			return new EmailConnectionInfo
			{
				FromEmail = From,
				ToEmail = To,
				MailServer = Server,
				NetworkCredentials = new NetworkCredential {
					UserName = Username,
					Password = Password
				},
				EnableSsl = EnableSsl,
				Port = Port,
				EmailSubject = Subject
			};
		}
	}
}
