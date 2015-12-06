using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace GAPT.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required, StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required, StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [ForeignKey("Type"), Display(Name = "Type")]
        public int? ItemTypeId { get; set; }
        public virtual ItemType Type { get; set; }

        [Required]
        public string ColorHex { get; set; }

        [Url, Display(Name = "Item URL")]
        public string ItemUrl { get; set; }
        [Url, Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}