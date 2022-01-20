using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoGrid.Models
{
    public class PerformerImage
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }
        public DateTime? DateChanged { get; set; }
        public string FileName { get; set; }

    }
}