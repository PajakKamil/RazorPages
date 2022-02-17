using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository
{
	public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
	{
		private readonly ApplicationDbContext _db;
		public OrderDetailsRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_db = dbContext;
		}
		public void Update(OrderDetails obj)
		{
			_db.OrderDetails.Update(obj); //Update wszystkiego!
		}
	}
}
