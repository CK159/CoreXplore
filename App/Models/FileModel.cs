using System;

namespace App.Models
{
	public class UploadableFileDto
	{
		public int FileId { get; set; }
		public string FileName { get; set; }
		public string MimeType { get; set; }
		public byte[] Content { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
