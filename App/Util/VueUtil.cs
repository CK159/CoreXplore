using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Util
{
	public class PagedResult<T>
	{
		public T result { get; set; }
		public int currentPage { get; set; }
		public int pages { get; set; }
		public int recordCount { get; set; }

		public PagedResult(T result, int currentPage, int pages, int recordCount)
		{
			this.result = result;
			this.currentPage = currentPage;
			this.pages = pages;
			this.recordCount = recordCount;
		}

		public static PagedResult<IQueryable<U>> AutoPage<U>(IQueryable<U> q, int? currentPage, int? pageSize)
		{
			//TODO: Generic way to not evaluate query twice?
			//Like https://stackoverflow.com/questions/7767409/better-way-to-query-a-page-of-data-and-get-total-count-in-entity-framework-4-1
			int recordCount = q.Count();
			int pages = 1;

			if (pageSize != null)
			{
				int ps = pageSize.GetValueOrDefault();
				pages = (int) Math.Ceiling((double) recordCount / ps);
				//Paged result set
				q = q.Skip((currentPage ?? 0) * ps).Take(ps);
			}

			return new PagedResult<IQueryable<U>>(q, currentPage ?? 0, pages, recordCount);
		}
	}

	public class SimpleSelect : List<SimpleSelectItem>
	{
		public SimpleSelect()
		{
		}

		public SimpleSelect(List<SimpleSelectItem> src) : base(src)
		{
		}
	}

	public class SimpleSelectItem
	{
		public int? value { get; set; }
		public string name { get; set; }
	}

	public static class LINQExtension
	{
		//Based on System.Linq.ToDictionary()
		public static SimpleSelect ToSimpleSelect<T>(this IEnumerable<T> source,
			Func<T, int?> valueSelector, Func<T, string> nameSelector)
		{
			if (source == null)
				throw new ArgumentNullException(nameof (source));
			if (valueSelector == null)
				throw new ArgumentNullException(nameof (valueSelector));
			if (nameSelector == null)
				throw new ArgumentNullException(nameof (nameSelector));
			
			SimpleSelect s = new SimpleSelect();
			foreach (T item in source)
			{
				s.Add(new SimpleSelectItem
				{
					value = valueSelector(item),
					name = nameSelector(item)
				});
			}

			return s;
		}

		public static SimpleSelect ToSimpleSelect<T>(this IEnumerable<T> source,
			Func<T, int> valueSelector, Func<T, string> nameSelector)
		{
			Func<T, int?> valNull = (e => (int?)valueSelector(e));
			return source.ToSimpleSelect(valNull, nameSelector);
		}
	}
}
