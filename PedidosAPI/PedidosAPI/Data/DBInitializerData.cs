using PedidosAPI.DataBase;
using PedidosAPI.Entidades;
using System.Security;

namespace PedidosAPI.Data
{
    public static class DBInitializerData
    {
        public static void InitData(DataContext dataContext)
        {
            if (!dataContext.Produtos.Any())
            {
                var produto = new Produto
                {
                    NomeProduto = "Coca-cola Lata",
                    Valor = 3
                };
                var produto2 = new Produto
                {
                    NomeProduto = "Fanta Laranja 2L",
                    Valor = 5
                };
                var produto3 = new Produto
                {
                    NomeProduto = "Aua sem gas 600",
                    Valor = 2
                };

                dataContext.Produtos.Add(produto);
                dataContext.Produtos.Add(produto2);
                dataContext.Produtos.Add(produto3);
            }

            if (!dataContext.Pedidos.Any())
            {
                var pedido = new Pedido
                {
                    NomeCliente = "Ana Julia",
                    EmailCliente = "ana_julia@email.com",
                    Pago = false,
                    DataCriacao = new DateTime()
                };
                var pedido2 = new Pedido
                {
                    NomeCliente = "Gabriel Alves",
                    EmailCliente = "g_alves@email.com",
                    Pago = true,
                    DataCriacao = new DateTime()
                };
                var pedido3 = new Pedido
                {
                    NomeCliente = "Joao Pedro",
                    EmailCliente = "joao_p@email.com",
                    Pago = false,
                    DataCriacao = new DateTime()
                };

                dataContext.Pedidos.Add(pedido);
                dataContext.Pedidos.Add(pedido2);
                dataContext.Pedidos.Add(pedido3);
            }

            if (!dataContext.ItensPedido.Any())
            {
                var itensPedido = new ItemPedido
                {
                    IdPedido = 1,
                    IdProduto = 1,
                    Quantidade = 2
                    
                };
                var itensPedido2 = new ItemPedido
                {
                    IdPedido = 1,
                    IdProduto = 2,
                    Quantidade = 1
                };
                var itensPedido3 = new ItemPedido
                {
                    IdPedido = 2,
                    IdProduto = 3,
                    Quantidade = 1
                };
                var itensPedido4 = new ItemPedido
                {
                    IdPedido = 3,
                    IdProduto = 2,
                    Quantidade = 2
                };

                dataContext.ItensPedido.Add(itensPedido);
                dataContext.ItensPedido.Add(itensPedido2);
                dataContext.ItensPedido.Add(itensPedido3);
                dataContext.ItensPedido.Add(itensPedido4);
            }
            
            dataContext.SaveChanges();
        }
    }
}
