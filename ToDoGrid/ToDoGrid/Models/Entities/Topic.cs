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
    public class Topic
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Название темы
        /// </summary>    
        [Required]
        [Display(Name = "Название темы", Order = 5)]
        public string Name { get; set; }

        
        /// <summary>
        /// Дела
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<Task> Tasks { get; set; }

    }
}