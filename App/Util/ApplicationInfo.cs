using System;
using System.IO;
using System.Reflection;

namespace App.Util
{
	public class ApplicationInfo
	{
		public string ApplicationName { get; private set; }

		public string ApplicationVersion { get; private set; }

		public DateTime ApplicationBuildDate { get; private set; }
		
		public static ApplicationInfo BuildApplicationInfo()
		{
			return new ApplicationInfo
			{
				ApplicationName = "CoreXplore",
				ApplicationVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
					.InformationalVersion,
				
				//This is not the best - the file properties can be modified by external tooling but there's no default built-in date stamp any more
				ApplicationBuildDate = File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location)
			};
		}

		[Obsolete("This method will not work if deterministic builds are enabled for the assembly (the default in in .NET Core)")]
		public static DateTime RetrieveLinkerTimestamp(string filePath)
		{
			const int PeHeaderOffset = 60;
			const int LinkerTimestampOffset = 8;

			byte[] b = new byte[2048];
			Stream s = Stream.Null;
			try
			{
				s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
				s.Read(b, 0, 2048);
			}
			finally
			{
				if ((s != null)) s.Close();
			}

			int i = BitConverter.ToInt32(b, PeHeaderOffset);

			int secondsSince1970 = BitConverter.ToInt32(b, i + LinkerTimestampOffset);
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
			dt = dt.AddSeconds(secondsSince1970);
			dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
			return dt;
		}
	}
}
