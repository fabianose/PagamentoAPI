using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTestPaymentApi.Context;
using TechTestPaymentApi.Entities;

namespace TechTestPaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly VendasContext _context;

        public PedidosController(VendasContext context)
        {
            _context = context;
        }

        // Registrar a venda
        [HttpPost("Resgistrar venda")]
        public IActionResult Create (Pedido pedido)
        {
            if (pedido.ItemVendido == "")
                return NotFound();

            _context.Add(pedido);
            pedido.StatusDoPedido = "Aguardando pagamento";
            _context.SaveChanges();
            return Ok(pedido);
        }

        // Buscar a venda
        [HttpGet("Buscar venda")]
        public IActionResult ObterPorId(int id)
        {
            var pedido = _context.TabelaPedidos.Find(id);
            if(pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        //  Atualizar a venda
        [HttpPut("Atualizar venda")]
        public IActionResult Atualizar(int id, string status, Pedido pedido)
        {
            var pedidoBanco = _context.TabelaPedidos.Find(id);

            if (pedidoBanco == null)
                return NotFound();

            if (pedidoBanco.StatusDoPedido == "Entregue" || pedidoBanco.StatusDoPedido == "Cancelado")
                return BadRequest(new { Erro = "Não é possivel alterar o status do pedido" });  

            if (pedidoBanco.StatusDoPedido == "Enviado para Transportadora" && (status == "Entregue"))
            {
                pedidoBanco.StatusDoPedido = status;
                _context.TabelaPedidos.Update(pedidoBanco);
                _context.SaveChanges();
                return Ok(status);
            }
                 
            
            if (pedidoBanco.StatusDoPedido == "Pagamento aprovado" && (status == "Enviado para Transportadora" || status == "Cancelado"))
            {
                pedidoBanco.StatusDoPedido = status;
                _context.TabelaPedidos.Update(pedidoBanco);
                _context.SaveChanges();
                return Ok(status);
            }                      
            
            if (pedidoBanco.StatusDoPedido == "Aguardando pagamento" && (status == "Pagamento aprovado" || status == "Cancelado"))
            {
                pedidoBanco.StatusDoPedido = status;
                _context.TabelaPedidos.Update(pedidoBanco);
                _context.SaveChanges();
                return Ok(status);
            }
            return BadRequest(status);
                
        }
    }
}