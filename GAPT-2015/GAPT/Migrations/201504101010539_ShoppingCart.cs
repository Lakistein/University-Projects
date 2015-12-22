namespace GAPT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "OutfitId", "dbo.Outfits");
            DropIndex("dbo.Carts", new[] { "OutfitId" });
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        OutfitId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateIndex("dbo.Carts", "OutfitId");
            AddForeignKey("dbo.Carts", "OutfitId", "dbo.Outfits", "ID", cascadeDelete: true);
        }
    }
}
