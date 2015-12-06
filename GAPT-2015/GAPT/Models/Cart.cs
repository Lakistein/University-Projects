using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GAPT.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }

        public int? ItemId { get; set; }
        public int? ShoppingCartItemId { get; set; }
        public int Count { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public virtual ShoppingCartItem ShoppingCartItem { get; set; }

        public List<ShoppingCartItem> Products;

        public Cart()
        {
            Products = new List<ShoppingCartItem>();
        }
    }

    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsOutfit { get; set; }
        public int RecordId { get; set; }
        public decimal Discount { get; set; }
        public bool ApplyDiscount { get; set; }

        [NotMapped]
        public List<Item> Items { get; set; } // Empty if not outfit

        public decimal TotalPrice
        {
            get
            {
                if(ApplyDiscount) return (Price * (100 - Discount) / 100);
                else return Items.Sum(x => x.Price);
            }
        }

        public ShoppingCartItem()
        {
            Items = new List<Item>();
        }
    }

    public class ShoppingCartOutfitItems
    {
        public int Id { get; set; }
        public int CartRecordId { get; set; }
        public int ShoppingCartItemId { get; set; }
        public int ItemId { get; set; }

        public virtual ShoppingCartItem ShoppingCartItem { get; set; }
        public virtual Item Item { get; set; }
    }
}