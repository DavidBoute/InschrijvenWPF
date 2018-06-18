namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scholen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schools", "SchoolId", "dbo.OnderwijsSoorts");
            DropIndex("dbo.Schools", new[] { "SchoolId" });
            AddColumn("dbo.Schools", "OnderwijsSoort_OnderwijsSoortId", c => c.Int(nullable: false));
            CreateIndex("dbo.Schools", "OnderwijsSoort_OnderwijsSoortId");
            AddForeignKey("dbo.Schools", "OnderwijsSoort_OnderwijsSoortId", "dbo.OnderwijsSoorts", "OnderwijsSoortId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schools", "OnderwijsSoort_OnderwijsSoortId", "dbo.OnderwijsSoorts");
            DropIndex("dbo.Schools", new[] { "OnderwijsSoort_OnderwijsSoortId" });
            DropColumn("dbo.Schools", "OnderwijsSoort_OnderwijsSoortId");
            CreateIndex("dbo.Schools", "SchoolId");
            AddForeignKey("dbo.Schools", "SchoolId", "dbo.OnderwijsSoorts", "OnderwijsSoortId");
        }
    }
}
