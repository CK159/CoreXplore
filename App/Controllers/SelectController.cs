using System.Collections.Generic;
using System.Linq;
using App.Models;
using App.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace App.Controllers
{
	[Route("api/select")]
	public class ApiSelectController : Controller
	{
		private readonly LoaderService _loader;

		public ApiSelectController(LoaderService loader)
		{
			_loader = loader;
		}
		
		[HttpGet, HttpPost]
		[Route("categorySelect")]
		public SimpleSelect GetCategorySelect(int? CategoryID, bool? Single)
		{
			return _loader.LoadFromFile<List<CategoryEntity>>("category")
				.Where(e => (!Single.GetValueOrDefault() && e.Active) || e.CategoryId == CategoryID)
				.OrderBy(e => e.CategoryName)
				.ToSimpleSelect(a => a.CategoryId, a => a.CategoryName);
		}
		
		[HttpGet, HttpPost]
		[Route("codeSelect")]
		public SimpleSelect GetCodeSelect(int? CodeId, bool? Single)
		{
			return _loader.LoadFromFile<List<CodeEntry>>("code")
				.Where(e => !Single.GetValueOrDefault() || e.CodeId == CodeId)
				.OrderBy(e => e.CodeValue)
				.ToSimpleSelect(a => a.CodeId, a => a.CodeValue);
		}
		
		[HttpGet, HttpPost]
		[Route("codeAttributeSelect")]
		public SimpleSelect GetCodeAttributeSelect(int? CodeId, int? CodeAttributeId, bool? Single)
		{
			var x = (from c in _loader.LoadFromFile<List<CodeAttributeEntry>>("code-attribute")
				join a in _loader.LoadFromFile<List<AttributeEntry>>("attribute") on c.AttributeId equals a.AttributeId
				select new
				{
					c.CodeId,
					c.CodeAttributeId,
					a.AttributeName
				});
			
			return x.Where(e => 
					(CodeAttributeId != null && e.CodeAttributeId == CodeAttributeId)
					|| (!Single.GetValueOrDefault() && (CodeId == null || e.CodeId == CodeId))
				)
				.OrderBy(e => e.CodeAttributeId)
				.ToSimpleSelect(a => a.CodeAttributeId, a => a.AttributeName);
		}
		
		[HttpGet, HttpPost]
		[Route("codeAttributeValueSelect")]
		public SimpleSelect GetCodeAttributeValueSelect(int? CodeAttributeId, int? CodeAttributeValueId, bool? Single)
		{
			var x = (from cav in _loader.LoadFromFile<List<CodeAttributeValueEntry>>("code-attribute-value")
				join av in _loader.LoadFromFile<List<AttributeValueEntry>>("attribute-value") on cav.AttributeValueId equals av.AttributeValueId
				select new
				{
					cav.CodeAttributeId,
					cav.CodeAttributeValueId,
					av.ValueName
				});
			
			return x.Where(e => 
					(CodeAttributeValueId != null && e.CodeAttributeValueId == CodeAttributeValueId)
					|| (!Single.GetValueOrDefault() && (CodeAttributeId == null || e.CodeAttributeId == CodeAttributeId))
				)
				.OrderBy(e => e.CodeAttributeId)
				.ToSimpleSelect(a => a.CodeAttributeValueId, a => a.ValueName);
		}
		
		[HttpGet, HttpPost]
		[Route("attributeSelect")]
		public SimpleSelect GetAttributeSelect(int? AttributeId, bool? Single)
		{
			return _loader.LoadFromFile<List<AttributeEntry>>("attribute")
				.Where(e => !Single.GetValueOrDefault() || e.AttributeId == AttributeId)
				.OrderBy(e => e.AttributeName)
				.ToSimpleSelect(a => a.AttributeId, a => a.AttributeName);
		}
		
		[HttpGet, HttpPost]
		[Route("attributeValueSelect")]
		public SimpleSelect GetAttributeValueSelect(int? AttributeId, int? AttributeValueId, bool? Single)
		{
			return _loader.LoadFromFile<List<AttributeValueEntry>>("attribute-value")
				.Where(e => 
					(AttributeValueId != null && e.AttributeValueId == AttributeValueId)
					|| (!Single.GetValueOrDefault() && (AttributeId == null || e.AttributeId == AttributeId))
				)
				.OrderBy(e => e.ValueName)
				.ToSimpleSelect(a => a.AttributeValueId, a => a.ValueName);
		}
	}
}
