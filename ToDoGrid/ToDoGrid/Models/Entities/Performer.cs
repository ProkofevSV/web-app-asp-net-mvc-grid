using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoGrid.Extensions;
using ToDoGrid.Models.Attributes;

namespace ToDoGrid.Models
{
    public class Performer
    {
        ///<summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        ///<summary>
        /// Имя Исполнителя
        /// </summary> 
        [Required]
        [Display(Name = "Исполнитель", Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Фото Исполнителя
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual PerformerImage PerformerImage { get; set; }

        [Display(Name = "Фото исполнителя", Order = 6)]
        [NotMapped]
        public HttpPostedFileBase PerformerImageFile { get; set; }




        /// <summary>
        /// Пол
        /// </summary> 
        [ScaffoldColumn(false)]
        public Sex Sex { get; set; }

        [Display(Name = "Пол", Order = 2)]
        [UIHint("DropDownList")]
        [TargetProperty("Sex")]
        [NotMapped]
        public IEnumerable<SelectListItem> SexDictionary
        {
            get
            {
                var dictionary = new List<SelectListItem>();

                foreach (Sex type in Enum.GetValues(typeof(Sex)))
                {
                    dictionary.Add(new SelectListItem
                    {
                        Value = ((int)type).ToString(),
                        Text = type.GetDisplayValue(),
                        Selected = type == Sex
                    });
                }

                return dictionary;
            }
        }


        ///<summary>
        /// Список предметов, которые ведет преподаватель
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}