namespace GAPT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Outfits", "IsFemale", c => c.Boolean(nullable: false));
            AddColumn("dbo.Outfits", "IsChild", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Outfits", "IsChild");
            DropColumn("dbo.Outfits", "IsFemale");
        }
    }
}
