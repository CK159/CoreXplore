using System.IO;
using System.Linq;
using Db;
using Microsoft.AspNetCore.Mvc;
using File = Db.File;

namespace App.Controllers
{
	public class FileController : Controller
	{
		private readonly DbCore _db;
		
		public FileController(DbCore db)
		{
			_db = db;
		}
		
		[Route("api/File/{fileId}")]
		public IActionResult ProductLoad(int fileId)
		{
			File f = _db.Files.FirstOrDefault(x => x.FileId == fileId);

			if (f?.Content == null)
				return NotFound();
			
			Stream stream = new MemoryStream(f.Content);
			return new FileStreamResult(stream, f.MimeType ?? "application/octet-stream");
		}
	}
}
