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
		}

		//Tutaj dodawać obsługę każdego repozytoruim
		public IFoodRepository Food { get; private set; }
		public ICategoryRepository Category { get; private set; }
		public IMenuItemRepository MenuItem { get; private set; }

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
