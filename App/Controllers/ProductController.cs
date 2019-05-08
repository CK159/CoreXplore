using System.Collections.Generic;
using System.Linq;
using App.Models;
using App.Services;
using App.Util;
using Db;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
	[Route("api/Product")]
	public class ProductController : Controller
	{
		private readonly DbCore _db;
		private readonly ProductManager _mgr;

		public ProductController(DbCore db, ProductManager mgr)
		{
			_db = db;
			_mgr = mgr;
		}
		
		[Route("Preview")]
		public PagedResult<IQueryable<ProductPreviewDto>> ProductPreview(
			int? currentPage,
			int? pageSize,
			string productName,
			string productDesc,
			int? catalogId,
			bool? active)
		{
			// 0. Load data / data source

			// 1. Project
			var qry = from p in _db.Products
				from catProd in _db.CatalogProducts.Where(e => e.Product == p).DefaultIfEmpty()
				group catProd.Catalog by p
				into grp
				select new
				{
					Product = grp.Key,
					Catalogs = grp
				};

			// 2. Filter
			if (!string.IsNullOrEmpty(productName))
				qry = qry.Where(e => e.Product.ProductName.Contains(productName));

			if (!string.IsNullOrEmpty(productDesc))
				qry = qry.Where(e =>
					e.Product.ProductDesc.Contains(productDesc) || e.Product.ProductRichDesc.Contains(productDesc));

			if (catalogId != null)
				qry = qry.Where(e => e.Catalogs.Where(w => w != null).Any(f => f.CatalogId == catalogId));

			if (active != null)
				qry = qry.Where(e => e.Product.Active == active);

			// 3. Select to DTO
			var items = qry.Select(q => new ProductPreviewDto
			{
				ProductId = q.Product.ProductId,
				ProductName = q.Product.ProductName,
				Active = q.Product.Active,
				Catalogs = q.Catalogs
					.Where(w => w != null)
					.Select(c => c.CatalogName)
					.ToList()
			});

			// 4. Sort
			items = items.OrderBy(o => o.ProductName);

			// 5. Page
			return PagedResult<PreviewDetailDTO>.AutoPage(items, currentPage, pageSize);
		}

		[Route("Load")]
		public ProductDto ProductLoad(int productId)
		{
			return _mgr.GetProductDto().FirstOrDefault(e => e.ProductId == productId);
		}

		[Route("Save")]
		public ProductDto ProductSave([FromBody]ProductDto dto)
		{
			Product product = _mgr.ProductDto2Product(dto);
			_mgr.ProductDto2Resources(dto, product);
			_mgr.ProductDto2Catalogs(dto, product);
			_db.SaveChanges();

			return _mgr.GetProductDto().FirstOrDefault(e => e.ProductId == product.ProductId);
		}

		[Route("Delete")]
		public void ProductSave(int productId)
		{
			Product p = _db.Products.Find(productId);

			_db.ProductResources.RemoveRange(_db.ProductResources.Where(e => e.Product.ProductId == p.ProductId));
			_db.CatalogProducts.RemoveRange(_db.CatalogProducts.Where(e => e.Product.ProductId == p.ProductId));
			_db.Products.Remove(p);
			_db.SaveChanges();
		}

		//TODO: way of annotating default value on DTOs themselves and having generic method to return default
		[Route("New")]
		public Dictionary<string, object> ProductNew()
		{
			List<ProductCatalogDto> defaultCatalogs = _mgr.GetDefaultCatalogs();
			Dictionary<string, object> dict = new Dictionary<string, object>();
			
			dict.Add("product", new ProductDto
			{
				Active = true,
				//ProductTypeId = db.ProductTypes.FirstOrDefault(x => x.ProductTypeCode == "PHYS")?.ProductTypeId ?? 0,
				Resources = new List<ProductResourceDto>(),
				Catalogs = defaultCatalogs
			});
			dict.Add("resource", new ProductResourceDto
			{
				File = new UploadableFileDto()
			});
			dict.Add("catalog", defaultCatalogs);
			
			return dict;
		}
	}
}
