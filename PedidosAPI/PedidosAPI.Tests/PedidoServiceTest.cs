using Moq;
using PedidosAPI.DataBase;

namespace PedidosAPI.Tests
{
    public class PedidoServiceTest
    {
        private readonly Mock<DataContext> _mockContext;

        public PedidoServiceTest()
        {
            _mockContext = new Mock<DataContext>();
        }

        [Fact]
        public void Test()
        {

        }
    }
}