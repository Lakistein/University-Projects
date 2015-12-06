namespace GAPT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ажурирањеli : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartItems", "RecordId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartItems", "RecordId");
        }
    }
}
