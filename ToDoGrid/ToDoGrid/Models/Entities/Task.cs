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
    public class Task
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        

        //<summary>
        //Задача
        //</summary>    
        [Required]
        [Display(Name = "Задача", Order = 2)]
        public String Name { get; set; }

        /// <summary>
        /// Тема
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<Topic> Topics { get; set; }

        [ScaffoldColumn(false)]
        public List<int> TopicIds { get; set; }

        [Display(Name = "Тема(ы)", Order = 1)]
        [UIHint("MultipleDropDownList")]
        [TargetProperty("TopicIds")]
        [NotMapped]
        public IEnumerable<SelectListItem> TopicDictionary
        {
            get
            {
                var db = new ToDoContext();
                var query = db.Topics;

                if (query != null)
                {
                    var Ids = query.Where(s => s.Tasks.Any(ss => ss.Id == Id)).Select(s => s.Id).ToList();
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.ToSelectList(c => c.Id, c => $"{c.Name}", c => Ids.Contains(c.Id)));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }

        /// <summary>
        /// Исполнитель
        /// </summary> 
        [ScaffoldColumn(false)]
        public int PerformerId { get; set; }
        [ScaffoldColumn(false)]
        public virtual Performer Performer { get; set; }
        [Display(Name = "Исполнитель", Order = 3)]
        [UIHint("DropDownList")]
        [TargetProperty("PerformerId")]
        [NotMapped]
        public IEnumerable<SelectListItem> PerformerDictionary
        {
            get
            {
                var db = new ToDoContext();
                var query = db.Performers;

                if (query != null)
                {
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.OrderBy(d => d.Name).ToSelectList(c => c.Id, c => c.Name, c => c.Id == PerformerId));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }

        /// <summary>
        /// Важность
        /// </summary> 
        [ScaffoldColumn(false)]
        public Importance Importance { get; set; }

        [Display(Name = "Важность", Order = 4)]
        [UIHint("RadioList")]
        [TargetProperty("Importance")]
        [NotMapped]
        public IEnumerable<SelectListItem> ImportanceDictionary
        {
            get
            {
                var dictionary = new List<SelectListItem>();

                foreach (Importance type in Enum.GetValues(typeof(Importance)))
                {
                    dictionary.Add(new SelectListItem
                    {
                        Value = ((int)type).ToString(),
                        Text = type.GetDisplayValue(),
                        Selected = type == Importance
                    });
                }

                return dictionary;
            }
        }

        //<summary>
        //Описание
        //</summary>    
        [Required]
        [Display(Name = "Описание", Order = 6)]
        public String Description { get; set; }

        


        //<summary>
        //Примерное время выполнения
        //</summary>    
        [Required]
        [Display(Name = "Примерное время выполнения", Order = 7)]
        public String Time { get; set; }


        /// <summary>
        /// День недели
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<DayOfWeek> DayOfWeeks { get; set; }

        [ScaffoldColumn(false)]
        public List<int> DayOfWeekIds { get; set; }

        [Display(Name = "День недели", Order = 70)]
        [UIHint("MultipleSelect")]
        [TargetProperty("DayOfWeekIds")]
        [NotMapped]
        public IEnumerable<SelectListItem> DayOfWeekDictionary
        {
            get
            {
                var db = new ToDoContext();
                var query = db.DayOfWeeks;

                if (query != null)
                {
                    var Ids = query.Where(s => s.Tasks.Any(ss => ss.Id == Id)).Select(s => s.Id).ToList();
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.ToSelectList(c => c.Id, c => $"{c.DayOfWeekName}", c => Ids.Contains(c.Id)));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }

       
    }
}