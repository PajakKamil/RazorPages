using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abby.Models;

namespace Abby.DataAccess.Repository.IRepository
{
	public interface IMenuItemRepository : IRepository<MenuItem>
	{
		void Update(MenuItem menuItem);
	}
}
