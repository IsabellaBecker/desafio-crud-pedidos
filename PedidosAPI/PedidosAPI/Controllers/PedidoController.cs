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
    }
}
