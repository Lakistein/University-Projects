using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GAPT.Models
{
    public class ItemType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Category"), Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}