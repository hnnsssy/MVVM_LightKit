using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.Model
{
    public static class Repositories
    {
        public static GenericUnitOfWork Work { get; set; }
        public static IGenericRepository<Client> RClients;
        public static IGenericRepository<Excursion> RExcursions { get; set; }
        public static IGenericRepository<Guest> RGuests { get; set; }
        public static IGenericRepository<TicketSale> RTicketSales { get; set; }
        public static IGenericRepository<TicketSaleClient> RTicketSaleClients{ get; set; }
        public static IGenericRepository<TicketSaleGuest> RTicketSaleGuests { get; set; }

        public static void InitializeGenericRepositories()
        {
            Work = new GenericUnitOfWork(new RestDBContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
            RClients = Work.Repository<Client>();
            RExcursions = Work.Repository<Excursion>();
            RGuests = Work.Repository<Guest>();
            RTicketSales = Work.Repository<TicketSale>();
            RTicketSaleClients = Work.Repository<TicketSaleClient>();
            RTicketSaleGuests = Work.Repository<TicketSaleGuest>();
        }
    }
}
