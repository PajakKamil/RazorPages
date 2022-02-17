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
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDbContext _db;
		public OrderHeaderRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_db = dbContext;
		}
		public void Update(OrderHeader obj)
		{
			_db.OrderHeader.Update(obj); //Update wszystkiego!
		}
	}
}
