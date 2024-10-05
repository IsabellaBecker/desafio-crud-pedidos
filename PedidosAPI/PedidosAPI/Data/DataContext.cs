using Microsoft.EntityFrameworkCore;
using PedidosAPI.Entidades;

namespace PedidosAPI.DataBase
{
    public class DataContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        }
    }
}