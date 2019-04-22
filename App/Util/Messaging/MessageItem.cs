using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.Util
{
	public class MessageItem
	{
		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }
		
		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(PropertyName = "type")]
		public MessageType Type { get; set; }

		public MessageItem()
		{
		}

		public MessageItem(string message, MessageType type = MessageType.None)
		{
			Message = message;
			Type = type;
		}
	}

	public enum MessageType
	{
		None,
		Info,
		Success,
		Warning,
		Error
	}
}
