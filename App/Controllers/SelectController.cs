using System.Collections.Generic;
using System.Linq;
using App.Models;
using App.Util;
using Db;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
	[Route("api/select")]
	public class ApiSelectController : Controller
	{
		private readonly LoaderService _loader;
		private readonly DbCore _db;

		public ApiSelectController(LoaderService loader, DbCore db)
		{
			_loader = loader;
			_db = db;
		}
		
		[Route("categorySelect")]
		public SimpleSelect GetCategorySelect(int? categoryId, bool? single)
		{
			return _loader.LoadFromFile<List<CategoryEntity>>("category")
				.Where(e => (!single.GetValueOrDefault() && e.Active) || e.CategoryId == categoryId)
				.OrderBy(e => e.CategoryName)
				.ToSimpleSelect(a => a.CategoryId, a => a.CategoryName);
		}
		
		[Route("codeSelect")]
		public SimpleSelect GetCodeSelect(int? codeId, bool? single)
		{
			return _loader.LoadFromFile<List<CodeEntry>>("code")
				.Where(e => !single.GetValueOrDefault() || e.CodeId == codeId)
				.OrderBy(e => e.CodeValue)
				.ToSimpleSelect(a => a.CodeId, a => a.CodeValue);
		}
		
		[Route("codeAttributeSelect")]
		public SimpleSelect GetCodeAttributeSelect(int? codeId, int? codeAttributeId, bool? single)
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
					(codeAttributeId != null && e.CodeAttributeId == codeAttributeId)
					|| (!single.GetValueOrDefault() && (codeId == null || e.CodeId == codeId))
				)
				.OrderBy(e => e.CodeAttributeId)
				.ToSimpleSelect(a => a.CodeAttributeId, a => a.AttributeName);
		}
		
		[Route("codeAttributeValueSelect")]
		public SimpleSelect GetCodeAttributeValueSelect(int? codeAttributeId, int? codeAttributeValueId, bool? single)
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
					(codeAttributeValueId != null && e.CodeAttributeValueId == codeAttributeValueId)
					|| (!single.GetValueOrDefault() && (codeAttributeId == null || e.CodeAttributeId == codeAttributeId))
				)
				.OrderBy(e => e.CodeAttributeId)
				.ToSimpleSelect(a => a.CodeAttributeValueId, a => a.ValueName);
		}
		
		[Route("attributeSelect")]
		public SimpleSelect GetAttributeSelect(int? attributeId, bool? single)
		{
			return _loader.LoadFromFile<List<AttributeEntry>>("attribute")
				.Where(e => !single.GetValueOrDefault() || e.AttributeId == attributeId)
				.OrderBy(e => e.AttributeName)
				.ToSimpleSelect(a => a.AttributeId, a => a.AttributeName);
		}
		
		[Route("attributeValueSelect")]
		public SimpleSelect GetAttributeValueSelect(int? attributeId, int? attributeValueId, bool? single)
		{
			return _loader.LoadFromFile<List<AttributeValueEntry>>("attribute-value")
				.Where(e => 
					(attributeValueId != null && e.AttributeValueId == attributeValueId)
					|| (!single.GetValueOrDefault() && (attributeId == null || e.AttributeId == attributeId))
				)
				.OrderBy(e => e.ValueName)
				.ToSimpleSelect(a => a.AttributeValueId, a => a.ValueName);
		}
		
		[Route("catalogSelect")]
		public SimpleSelect GetCatalogSelect(int? catalogId, bool? single)
		{
			return _db.Catalogs
				.Where(e => (!(single ?? false) && e.Active) || e.CatalogId == catalogId)
				.OrderBy(e => e.CatalogName)
				.ToSimpleSelect(a => a.CatalogId, a => a.CatalogName);
		}
		
		/*[Route("productTypeSelect")]
		public SimpleSelect GetProductTypeSelect(int? productTypeId, bool? single)
		{
			return _db.ProductTypes
				.Where(e => !(single ?? false) || e.ProductTypeId == productTypeId)
				.OrderBy(e => e.ProductTypeName)
				.ToSimpleSelect(a => a.ProductTypeId, a => a.ProductTypeName);
		}*/
	}
}
