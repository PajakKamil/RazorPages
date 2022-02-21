using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.Models.ViewModel
{
	public class _OrderDetailVM
	{
		public OrderHeader OrderHeader { get; set; }
		public List<OrderDetails> OrderDetailsList { get; set; }
	}
}
