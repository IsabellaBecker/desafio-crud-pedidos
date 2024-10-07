using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PedidosAPI.DataBase;
using PedidosAPI.Entidades;
using System.Collections.Generic;

namespace PedidosAPI.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly DataContext _dataContext;

        public PedidoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Pedido> AddPedido(Pedido pedido)
        {
            _dataContext.Pedidos.Add(pedido);
            await _dataContext.SaveChangesAsync();

            return pedido;
        }

        public async Task<bool> DeletePedido(int id)
        {
            var pedido = await _dataContext.Pedidos.FindAsync(id);

            if (pedido is null) return false;

            var itensPedido = await _dataContext.ItensPedido.ToListAsync();

            foreach (var item in itensPedido)
            {
                if (item.IdPedido == pedido.Id)
                {
                    _dataContext.ItensPedido.Remove(item);
                    await _dataContext.SaveChangesAsync();
                }
            }

            _dataContext.Pedidos.Remove(pedido);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidos()
        {
            var allPedidos = await _dataContext.Pedidos.ToListAsync();            

            foreach (var pedido in allPedidos) 
            {
                var itensPedido = await _dataContext.ItensPedido.ToListAsync();
                pedido.ItensPedido = new List<ItemPedido>();

                foreach (var item in itensPedido) 
                {
                    if (item.IdPedido == pedido.Id)
                    {
                        var produto = await _dataContext.Produtos.FindAsync(item.IdProduto);
                        pedido.ValorTotal = pedido.ValorTotal + produto.Valor * item.Quantidade;
                        item.ValorUnitario = produto.Valor;
                        pedido.ItensPedido.Add(item);
                    }
                }
            }
            return allPedidos;
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            var pedido = await _dataContext.Pedidos.FindAsync(id);

            var itensPedido = await _dataContext.ItensPedido.ToListAsync();
            pedido.ItensPedido = new List<ItemPedido>();

            foreach (var item in itensPedido)
            {
                if (item.IdPedido == pedido.Id)
                {
                    var produto = await _dataContext.Produtos.FindAsync(item.IdProduto);
                    pedido.ValorTotal = pedido.ValorTotal + produto.Valor * item.Quantidade;
                    item.ValorUnitario = produto.Valor;
                    pedido.ItensPedido.Add(item);
                }
            }
  
            return pedido;
        }

        public async Task<Pedido> UpdatePedido(Pedido pedido)
        {
            var dbPedido = await _dataContext.Pedidos.FindAsync(pedido.Id);

            if (dbPedido is null) return null;

            dbPedido.NomeCliente = pedido.EmailCliente;
            dbPedido.EmailCliente = pedido.EmailCliente;
            dbPedido.ItensPedido = pedido.ItensPedido;
            dbPedido.Pago = pedido.Pago;
            dbPedido.DataCriacao = pedido.DataCriacao;

            if (pedido is not null)
            {
                foreach (var item in pedido.ItensPedido)
                {
                    var dbItem = dbPedido.ItensPedido.FirstOrDefault(i => i.Id == item.Id);

                    if (dbItem is not null)
                    {
                        dbItem.Quantidade = item.Quantidade;
                        dbItem.IdProduto = item.IdProduto;
                    }
                    else
                    {
                        dbPedido.ItensPedido.Add(item);
                    }

                    var itensToRemove = dbPedido.ItensPedido.Where(dbP => !pedido.ItensPedido.Any(p => p.Id == dbP.Id)).ToList();

                    foreach (var itemToRemove in itensToRemove)
                    {
                        dbPedido.ItensPedido.Remove(itemToRemove);
                        _dataContext.ItensPedido.Remove(itemToRemove);
                        await _dataContext.SaveChangesAsync();
                    }
                }
            }

            _dataContext.Pedidos.Update(dbPedido);
            await _dataContext.SaveChangesAsync();

            return dbPedido;
        }
    }
}
