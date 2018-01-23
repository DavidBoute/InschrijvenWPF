namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OnderwijsSoorts", "OnderwijsSoortId", "dbo.Schools");
            DropIndex("dbo.OnderwijsSoorts", new[] { "OnderwijsSoortId" });
            CreateIndex("dbo.Schools", "SchoolId");
            AddForeignKey("dbo.Schools", "SchoolId", "dbo.OnderwijsSoorts", "OnderwijsSoortId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schools", "SchoolId", "dbo.OnderwijsSoorts");
            DropIndex("dbo.Schools", new[] { "SchoolId" });
            CreateIndex("dbo.OnderwijsSoorts", "OnderwijsSoortId");
            AddForeignKey("dbo.OnderwijsSoorts", "OnderwijsSoortId", "dbo.Schools", "SchoolId");
        }
    }
}
