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
	public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
	{
		private readonly ApplicationDbContext _db;
		public MenuItemRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_db = dbContext;
		}

		public void Update(MenuItem obj)
		{
			var objFromDb = _db.MenuItem.FirstOrDefault(u => u.Id == obj.Id);
			objFromDb.Name = obj.Name;
			objFromDb.Description = obj.Description;
			objFromDb.Category = obj.Category;
			objFromDb.Price = obj.Price;
			objFromDb.FoodId = obj.FoodId;
			objFromDb.CategoryId = obj.CategoryId;
			if (objFromDb.Image != null)
				objFromDb.Image = obj.Image;
		}
	}
}
