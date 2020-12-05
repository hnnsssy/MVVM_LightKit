using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.Model
{
    public class RestDBContext : DbContext
    {
        public RestDBContext() { }
        public RestDBContext(string conStr) : base(conStr) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Excursion> Excursions { get; set; }
        public DbSet<TicketSale> TicketSales { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<TicketSaleClient> TicketSaleClients { get; set; }
        public DbSet<TicketSaleGuest> TicketSaleGuests { get; set; }
        
    }
}
