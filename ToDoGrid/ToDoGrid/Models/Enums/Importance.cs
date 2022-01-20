using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ToDoGrid.Models
{
	public enum Importance
	{
		[Display(Name = "Не важно")]
		DoNotMatter = 1,

		[Display(Name = "Средняя важность")]
		MediumImportance = 2,

		[Display(Name = "Важно")]
		Important = 3,

		[Display(Name = "Очень важно")]
		VeryImportant = 4,
	}
}