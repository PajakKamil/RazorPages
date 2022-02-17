using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abby.Models;

namespace Abby.DataAccess.Repository.IRepository
{
	public interface IShoppingCartRepository : IRepository<ShoppingCart>
	{
		int IncrementCount(ShoppingCart cart, int count);
		int DecrementCound(ShoppingCart cart, int count);
	}
}
