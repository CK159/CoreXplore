using System;
using System.Collections.Generic;

namespace App.Models
{
	public class ProductPreviewDto
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public bool Active { get; set; }
		public List<string> Catalogs { get; set; }
	}

	public class ProductResourceDto
	{
		public int ProductResourceId { get; set; }
		public int ProductId { get; set; }
		public UploadableFileDto File { get; set; }
		public int SortOrder { get; set; }
		public bool Active { get; set; }
		/*public string ResourceName { get; set; }
		public string ResourceInfo { get; set; }
		public string ResourceTypeName { get; set; }
		public string ResourceTypeCode { get; set; }*/
		public DateTime DateCreated { get; set; }
	}

	public class ProductDto
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDesc { get; set; }
		public string ProductRichDesc { get; set; }
		//public int? ProductTypeId { get; set; }
		public bool Active { get; set; }
		public List<ProductResourceDto> Resources { get; set; }
		public List<ProductCatalogDto> Catalogs { get; set; }
	}
		
	public class ProductCatalogDto
	{
		public bool Assigned { get; set; }
		public int CatalogId { get; set; }
		public string CatalogName { get; set; }
		public bool Active { get; set; }
		public int StoreId { get; set; }
		public string StoreName { get; set; }
	}
}
