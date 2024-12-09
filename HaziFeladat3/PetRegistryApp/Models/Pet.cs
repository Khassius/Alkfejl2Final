using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetRegistryApp.Models
{
	public enum PetType
	{
		Funny,
		Angry,
		Sus,
		Sad
	}

	public class Pet
	{
		[Key]
		public int ID { get; set; }
		[Required(ErrorMessage ="You must provide your pets name!")]
		public string Name { get; set; }

		[Required]
		public PetType PetType { get; set; }

		[Required]
		public bool Retired { get; set; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime ContractExp { get; set; }

		[Required(ErrorMessage ="You must provide your pets gender!")]
		public string Gender { get; set; }
		[Required(ErrorMessage ="You must provide your pets age!")]
		[Range(0, 1000, ErrorMessage ="Your pets age must be between 0-1000!")]
		public int Age { get; set; }
		[Required(ErrorMessage ="You must provide your pets weight!")]
		[Range(0, 1000, ErrorMessage = "Your pets weight must be between 0-10000!")]
		public int Weight { get; set; }
		[Required]
		[Url(ErrorMessage = "Invalid URL.")]
		[RegularExpression(@".*\.(?:png|jpg|jpeg|gif|bmp)(\?.*)?$", ErrorMessage = "Only images are allowed (png, jpg, jpeg, gif, bmp).")]
		[Display(Name = "Photo")]
		public string PhotoURL { get; set; }
		[Required(ErrorMessage ="You must choose a category, if there is none, then create one.")]
		[ForeignKey("ReferencedCategory")]
		[Display(Name = "Category")]
		public int CategoryID { get; set; }
		[Display(Name = "Category")]
		public virtual Category? ReferencedCategory { get; set; }
	}
}
