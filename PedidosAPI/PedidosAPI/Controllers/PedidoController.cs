using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosAPI.DataBase;
using PedidosAPI.Entidades;

namespace PedidosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PedidoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> GetAllPedidos()
        {
            var pedidos = await _dataContext.Pedidos.ToListAsync();

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedidoById(int id)
        {
            var pedido = await _dataContext.Pedidos.FindAsync(id);

            if (pedido is null) return NotFound("Pedido não encontrado");

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> AddPedido(Pedido pedido)
        {
            _dataContext.Pedidos.Add(pedido);
            await _dataContext.SaveChangesAsync();

            return Ok(pedido);
        }

        [HttpPut]
        public async Task<ActionResult<Pedido>> UpdatePedido(Pedido pedido)
        {
            var dbPedido = await _dataContext.Pedidos.FindAsync(pedido.Id);

            if (dbPedido is null) return NotFound("Pedido não encontrado");

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

            return Ok(pedido);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _dataContext.Pedidos.FindAsync(id);

            if (pedido is null) return NotFound("Pedido não encontrado");

            foreach (var item in pedido.ItensPedido)
            {
                _dataContext.ItensPedido.Remove(item);
                await _dataContext.SaveChangesAsync();
            }

            _dataContext.Pedidos.Remove(pedido);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}