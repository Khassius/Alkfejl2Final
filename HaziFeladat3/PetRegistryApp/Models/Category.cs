using System.ComponentModel.DataAnnotations;

namespace PetRegistryApp.Models
{
	public class Category
	{
		[Key]
		public int ID { get; set; }

		[Display(Name = "Category")]
		[Required(ErrorMessage ="You must provide the name of the category!")]
		public string CategoryName { get; set; }

		[Display(Name = "Description")]
		[Required(ErrorMessage = "You must describe the category!")]
		public string CategoryDescription { get; set; }
		
	}
}
