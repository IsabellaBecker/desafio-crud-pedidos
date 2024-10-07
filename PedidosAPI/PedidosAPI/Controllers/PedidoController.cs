using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PedidosAPI.Entidades;
using PedidosAPI.Services;

namespace PedidosAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> GetAllPedidos()
        {
            var pedidos =  await _pedidoService.GetAllPedidos();

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedidoById(int id)
        {
            var pedidos = await _pedidoService.GetPedidoById(id);
            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> AddPedido(Pedido pedido)
        {
            var pedidos = await _pedidoService.AddPedido(pedido);
            return Ok(pedidos);
        }

        [HttpPut]
        public async Task<ActionResult<Pedido>> UpdatePedido(Pedido pedido)
        {
            var dbPedido = await _pedidoService.UpdatePedido(pedido);

            if (dbPedido is null) return NotFound("Pedido não encontrado");
      
            return Ok(dbPedido);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var ret = await _pedidoService.DeletePedido(id);

            if (!ret) return NotFound("Pedido não encontrado");

            return Ok();
        }
    }
}