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
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_db = dbContext;
		}

		public void Update(Category category)
		{
			var objFromDb = _db.Category.FirstOrDefault(u => u.Id == category.Id);
			objFromDb.Name = category.Name;
			objFromDb.DisplayOrder = category.DisplayOrder;
		}
	}
}
