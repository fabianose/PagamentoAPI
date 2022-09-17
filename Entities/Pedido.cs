using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTestPaymentApi.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDoPedido { get; set; }
        public string ItemVendido { get; set; }
        public string StatusDoPedido { get; set; }
        public string IdDoVendedor { get; set; }        
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
