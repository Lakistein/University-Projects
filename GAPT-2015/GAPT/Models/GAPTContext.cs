using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GAPT.Models
{
    public class GAPTContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public GAPTContext()
            : base("name=GAPTContext")
        {
        }

        public System.Data.Entity.DbSet<GAPT.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<GAPT.Models.ItemType> ItemTypes { get; set; }

        public System.Data.Entity.DbSet<GAPT.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<GAPT.Models.Outfit> Outfits { get; set; }

        public System.Data.Entity.DbSet<GAPT.Models.OutfitItem> OutfitItem { get; set; }

        public System.Data.Entity.DbSet<GAPT.Models.Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<GAPT.Models.ShoppingCartItem> ShoppingCartItems { get; set; }

        public System.Data.Entity.DbSet<GAPT.Models.ShoppingCartOutfitItems> ShoppingCartOutfitItems { get; set; }
    }
}
