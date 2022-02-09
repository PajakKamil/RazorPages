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
	public class FoodRepository : Repository<Food>, IFoodRepository
	{
		private readonly ApplicationDbContext _db;
		public FoodRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_db = dbContext;
		}

		public void Update(Food food)
		{
			var objFromDb = _db.Food.FirstOrDefault(u => u.Id == food.Id);
			objFromDb.Name = food.Name;
		}
	}
}
