using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ToDoGrid.Models
{
	public enum Sex
	{
		[Display(Name = "Женский")]
		Female = 1,
		
		[Display(Name = "Мужской")]
		Male = 2,
	}
}