using Db;
using TestSupport.EfSchemeCompare;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Test
{
	public class SchemaCompare
	{
		[Fact]
		public void DbCoreSchemaCheck()
		{
			//SETUP
			using (DbCore context = Util.BuildContext())
			{
				CompareEfSql comparer = new CompareEfSql();

				//ATTEMPT
				//This will compare EF Core model of the database with the database that the context's connection points to
				bool hasErrors = comparer.CompareEfWithDb(context); 

				//VERIFY
				//The CompareEfWithDb method returns true if there were errors. 
				//The comparer.GetAllErrors property returns a string, with each error on a separate line
				hasErrors.ShouldBeFalse(comparer.GetAllErrors);
			}
		}
	}
}
