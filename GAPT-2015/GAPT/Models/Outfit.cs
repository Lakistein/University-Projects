using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GAPT.Models
{
    public class Outfit
    {
        public int Id { get; set; }
        [Required, StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public float Discount { get; set; }

        [Display(Name = "Is Female")]
        public bool IsFemale { get; set; }

        [Display(Name = "Is Child")]
        public bool IsChild { get; set; }

        public List<Item> Items { get; set; }


        public Outfit()
        {
            Items = new List<Item>();
        }

        public decimal GetTotalPrice()
        {
            return Items.Sum(item => item.Price);
        }

        public decimal GetDiscountedPrice()
        {
            return (Price * (100 - (decimal)Discount) / 100);
        }
    }
}