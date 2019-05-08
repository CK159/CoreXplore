using System.Collections.Generic;
using System.Linq;
using App.Models;
using Db;

namespace App.Services
{
	public class ProductManager
	{
		private readonly DbCore _db;
		
		public ProductManager(DbCore db)
		{
			_db = db;
		}
		
		public IQueryable<ProductDto> GetProductDto()
		{
			IQueryable<ProductDto> qry = from p in _db.Products
				orderby p.ProductName
				select new ProductDto
				{
					ProductId = p.ProductId,
					ProductName = p.ProductName,
					ProductDesc = p.ProductDesc,
					ProductRichDesc = p.ProductRichDesc,
					//ProductTypeId = grp.Key.Type.ProductTypeId,
					Active = p.Active,
					Resources = p.ProductResources.Select(r => new ProductResourceDto
					{
						ProductResourceId = r.ProductResourceId,
						ProductId = p.ProductId,
						Active = r.Active,
						DateCreated = r.DateCreated,
						SortOrder = r.SortOrder,
						File = new UploadableFileDto
						{
							FileId = r.File.FileId,
							FileName = r.File.FileName,
							MimeType = r.File.MimeType,
							DateCreated = r.File.DateCreated
						}
					}).OrderBy(o => o.SortOrder).ToList(),
					Catalogs = _db.Catalogs.Select(c => new ProductCatalogDto
					{
						//Assigned = grp.Any(g => g.Catalog == c),
						Assigned = _db.CatalogProducts.Any(cp => cp.CatalogId == c.CatalogId && cp.ProductId == p.ProductId),
						CatalogId = c.CatalogId,
						CatalogName = c.CatalogName,
						Active = c.Active,
						StoreId = c.Store.StoreId,
						StoreName = c.Store.StoreName
					}).OrderBy(o => o.StoreName).ThenBy(o => o.CatalogName).ToList()
				};

			return qry;
		}

		public Product ProductDto2Product(ProductDto dto)
		{
			Product entity = _db.Products.Find(dto.ProductId);

			if (entity == null)
				_db.Products.Add(entity = new Product());

			entity.ProductName = dto.ProductName;
			entity.ProductDesc = dto.ProductDesc;
			entity.ProductRichDesc = dto.ProductRichDesc;
			//entity.Type = db.ProductTypes.Find(dto.ProductTypeId);
			entity.Active = dto.Active;

			return entity;
		}

		public void ProductDto2Resources(ProductDto dto, Product product)
		{
			foreach (ProductResourceDto resDto in dto.Resources)
			{
				//Resource file entity TODO: Move to FileManager?
				File file = _db.Files.Find(resDto.File.FileId);
				
				if (file == null)
					_db.Files.Add(file = new File());

				file.FileName = resDto.File.FileName;
				file.MimeType = resDto.File.MimeType;
				
				//Content is only provided when new file created or existing file changed
				if (resDto.File.Content != null)
					file.Content = resDto.File.Content;
				
				//Resource Entity
				ProductResource entity = _db.ProductResources.Find(resDto.ProductResourceId);
				
				if (entity == null)
					_db.ProductResources.Add(entity = new ProductResource());

				entity.Product = product;
				entity.SortOrder = resDto.SortOrder;
				entity.Active = resDto.Active;
				entity.File = file;
				entity.ResourceName = file.FileName;
			}
		}

		public void ProductDto2Catalogs(ProductDto dto, Product product)
		{
			foreach (ProductCatalogDto catDto in dto.Catalogs)
			{
				Catalog baseCat = _db.Catalogs.Find(catDto.CatalogId);
				
				//CatalogProduct doesn't have any navigation properties - find by Catalog and Product
				//TODO: Why doesn't (c => c.Catalog == baseCat && c.Product == product) work?
				CatalogProduct entity = _db.CatalogProducts.FirstOrDefault(c =>
					c.Catalog.CatalogId == baseCat.CatalogId && c.Product.ProductId == product.ProductId);

				//Catalog not assigned
				if (!catDto.Assigned)
				{
					//Remove existing assignment
					if (entity != null)
						_db.CatalogProducts.Remove(entity);
					
					continue;
				}
				
				//Catalog was assigned
				if (entity == null)
					_db.CatalogProducts.Add(entity = new CatalogProduct());

				entity.Product = product;
				entity.Catalog = baseCat;
			}
		}

		public List<ProductCatalogDto> GetDefaultCatalogs()
		{
			IOrderedQueryable<ProductCatalogDto> result = _db.Catalogs.Select(c => new ProductCatalogDto
			{
				//Assigned = grp.Any(g => g.Catalog == c),
				Assigned = false,
				CatalogId = c.CatalogId,
				CatalogName = c.CatalogName,
				Active = c.Active,
				StoreId = c.Store.StoreId,
				StoreName = c.Store.StoreName
			}).OrderBy(o => o.StoreName).ThenBy(o => o.CatalogName);

			return result.ToList();
		}
	}
}
