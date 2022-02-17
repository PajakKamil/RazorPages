using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;

namespace Abby.DataAccess.Repository
{
	public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
	{
		private readonly ApplicationDbContext _db;
		public ShoppingCartRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_db = dbContext;
		}

		public int DecrementCound(ShoppingCart cart, int count)
		{
			cart.Count -= count;
			_db.SaveChanges();
			return cart.Count;
		}

		public int IncrementCount(ShoppingCart cart, int count)
		{
			cart.Count += count;
			_db.SaveChanges();
			return cart.Count;
		}
	}
}
