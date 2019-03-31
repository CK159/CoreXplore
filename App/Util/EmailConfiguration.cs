using System;
using System.Net;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace App.Util
{
	public class EmailConfiguration
	{
		public bool Enabled { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string Server { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool EnableSsl { get; set; } = true;
		public int Port { get; set; } = 465;
		public string Subject { get; set; } = "CoreXplore {0} Notification";
		public string OutputTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}{NewLine}[{Level}] {Message}{NewLine}{Exception}{NewLine}{Properties:j}";
		public int BatchPostingLimit { get; set; } = 10;
		public LogEventLevel RestrictedToMinimumLevel { get; set; } = LogEventLevel.Warning;

		public EmailConnectionInfo ToEmailConnectionInfo(string environment)
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
				EmailSubject = String.Format(Subject, environment)
			};
		}
	}
}
