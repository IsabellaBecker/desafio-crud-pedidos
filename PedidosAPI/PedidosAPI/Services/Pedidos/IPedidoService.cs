using PedidosAPI.Entidades;

namespace PedidosAPI.Services
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllPedidos();
        Task<Pedido> GetPedidoById(int id);
        Task<Pedido> AddPedido(Pedido pedidoDto);
        Task<Pedido> UpdatePedido(Pedido pedidoDto);
        Task<bool> DeletePedido(int id);
    }
}
