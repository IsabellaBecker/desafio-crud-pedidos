using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PedidosAPI.DataBase;
using PedidosAPI.Entidades;
using PedidosAPI.Services;

namespace ProdutoAPITests
{
    public class Tests
    {
        private Mock<DataContext> _mockContext;
        private List<Pedido> pedidos;
        private List<ItemPedido> itensPedido;
        private List<Produto> produtos;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<DataContext>();

            // Arrange: Preparar os dados simulados
            this.pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, NomeCliente = "Cliente 1", ItensPedido = new List<ItemPedido>(), ValorTotal = 0 },
                new Pedido { Id = 2, NomeCliente = "Cliente 2", ItensPedido = new List<ItemPedido>(), ValorTotal = 0 }
            };

            this.itensPedido = new List<ItemPedido>
            {
                new ItemPedido { Id = 1, IdPedido = 1, IdProduto = 1, Quantidade = 2 },
                new ItemPedido { Id = 2, IdPedido = 1, IdProduto = 2, Quantidade = 3 },
                new ItemPedido { Id = 3, IdPedido = 2, IdProduto = 1, Quantidade = 1 }
            };

            this.produtos = new List<Produto>
            {
                new Produto { Id = 1, NomeProduto = "Produto 1", Valor = 10 },
                new Produto { Id = 2, NomeProduto = "Produto 2", Valor = 20 }
            };
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task GetAllPedidosRetornaValoresCalculadosCorretos()
        {
            mocks();

            var pedidoService = new PedidoService(_mockContext.Object); // Supondo que PedidoService usa DataContext
            var result = await pedidoService.GetAllPedidos();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));

            var pedido1 = result.First(p => p.Id == 1);
            Assert.That(pedido1.ValorTotal, Is.EqualTo(70)); 

            var pedido2 = result.First(p => p.Id == 2);
            Assert.That(pedido2.ValorTotal, Is.EqualTo(10));
        }

        private void mocks() {
            var mockPedidosDbSet = GetQueryableMockDbSet(pedidos);
            _mockContext.Setup(c => c.Pedidos).Returns(mockPedidosDbSet.Object);

            var mockItensPedidoDbSet = GetQueryableMockDbSet(itensPedido);
            _mockContext.Setup(c => c.ItensPedido).Returns(mockItensPedidoDbSet.Object);

            var mockProdutosDbSet = GetQueryableMockDbSet(produtos);
            _mockContext.Setup(c => c.Produtos).Returns(mockProdutosDbSet.Object);
        }



        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSet;
        }
    }
}