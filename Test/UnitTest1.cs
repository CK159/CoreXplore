using System;
using Xunit;

namespace Test
{
	public class UnitTest1
	{
		[Fact]
		public void MyFirstTest()
		{
			//Math Check
			Assert.NotEqual(0.3d, 0.1d + 0.2d);
		}

		[Fact]
		public void MySecondTest()
		{
			//Math Check
			double a = 0;

			for (int i = 0; i < 100; i++)
			{
				a += 0.01;
			}
			
			Assert.Equal(1.0000000000000007d, a);
		}
	}
}
