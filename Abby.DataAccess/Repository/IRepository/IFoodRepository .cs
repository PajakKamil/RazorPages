using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abby.Models;

namespace Abby.DataAccess.Repository.IRepository
{
	public interface IFoodRepository : IRepository<Food>
	{
		void Update(Food food);
	}
}
