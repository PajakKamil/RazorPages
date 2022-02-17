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
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_db = dbContext;
			Category = new CategoryRepository(_db);
			Food = new FoodRepository(_db);
			MenuItem = new MenuItemRepository(_db);
			ShoppingCart = new ShoppingCartRepository(_db);
			OrderHeader = new OrderHeaderRepository(_db);
			OrderDetails = new OrderDetailsRepository(_db);
			ApplicationUser = new ApplicationUserRepository(_db);
		}

		//Tutaj dodawać obsługę każdego repozytoruim
		public IFoodRepository Food { get; private set; }
		public ICategoryRepository Category { get; private set; }
		public IMenuItemRepository MenuItem { get; private set; }
		public IShoppingCartRepository ShoppingCart { get; private set; }
		public IOrderHeaderRepository OrderHeader { get; private set; }
		public IOrderDetailsRepository OrderDetails { get; private set; }
		public IApplicationUserRepository ApplicationUser { get; private set; }

		public void Save()
		{
			_db.SaveChanges();
		}

		public void Dispose() //Służy do zwalniania zasobów, które nie są dłużej potrzebne
		{
			_db.Dispose();
		}
	}
}
