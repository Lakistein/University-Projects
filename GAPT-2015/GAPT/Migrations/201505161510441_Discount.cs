namespace GAPT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Discount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Outfits", "Discount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Outfits", "Discount");
        }
    }
}
