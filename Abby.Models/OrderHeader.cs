using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.Models
{
	public class OrderHeader
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string UserId { get; set; }

		[ValidateNever]
		[ForeignKey("UserId")]
		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:C2}")]
		[Display(Name = "Order Total")]
		public double OrderTotal { get; set; }

		[Required]
		[Display(Name ="Pickup Time")]
		public DateTime PickUpTime { get; set; }

		[Required]
		[NotMapped]
		[Display(Name = "Pickup Date")]
		public DateTime PickUpDate { get; set; }

		public string Status { get; set; }

		public string? Comments { get; set; }

		public string? SessionId { get; set; }

		public string? PaymentIntentId { get; set; }

		[Display(Name ="Pickup Name")]
		[Required]
		public string PickUpName { get; set; }

		[Display(Name = "Phone Number")]
		[Required]
		public string? PhoneNumber { get; set; }
	}
}
