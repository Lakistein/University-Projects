namespace GAPT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartItems", "ApplyDiscount", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartItems", "ApplyDiscount");
        }
    }
}
