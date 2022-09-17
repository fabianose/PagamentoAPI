using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechTestPaymentApi;
using TechTestPaymentApi.Entities;

namespace TechTestPaymentApi.Context
{
    public class VendasContext : DbContext
    {
        public VendasContext(DbContextOptions<VendasContext> options) : base(options)
        {
            
        }  

        public DbSet<Pedido> TabelaPedidos { get; set; }  
    }
}