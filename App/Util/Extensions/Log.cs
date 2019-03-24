using Serilog;

namespace App.Util
{
	public static class AppLogging
	{
		public static ILogger ForCategory(string value)
		{
			return Log.Logger.ForContext("Category", value);
		}
	}
}
