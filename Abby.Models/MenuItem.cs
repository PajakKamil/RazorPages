using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.Models
{
	public class MenuItem
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		public string Image { get; set; }

		[Range(1,1000, ErrorMessage = "Price should be between $1 and $1000")]
		public double Price { get; set; }

		[Required]
		[Display(Name = "Food Type")]
		public int FoodId { get; set; }

		[ForeignKey("FoodId")] //Opcjonalne (ponieważ istnieje skojarzenie nazw i jeżeli nazywają się podobnie, samo przypisze)
		public Food Food { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
	}
}
