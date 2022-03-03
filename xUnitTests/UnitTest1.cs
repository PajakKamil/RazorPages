using Xunit;

namespace xUnitTests
{
	public class UnitTest1
	{
		public object MenuOrderController { get; private set; }

		[Fact]
		public void Test1()
		{
			Assert.Equal(1, 1);
			Assert.True(true);
		}

		[Fact]
		public void RazorTest()
		{
		}
	}
}