namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BijkomendeInfo_VerhoogdeZorgVraag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BijkomendeInfoes", "LeerProblemen", c => c.String());
            AddColumn("dbo.BijkomendeInfoes", "VerhoogdeZorgVraag", c => c.Boolean());
            AddColumn("dbo.BijkomendeInfoes", "VerslagBuitengewoonOnderwijs", c => c.Boolean());
            AddColumn("dbo.BijkomendeInfoes", "GemotiveerdVerslag", c => c.Boolean());
            AddColumn("dbo.BijkomendeInfoes", "OndersteuningsUur", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BijkomendeInfoes", "OndersteuningsUur");
            DropColumn("dbo.BijkomendeInfoes", "GemotiveerdVerslag");
            DropColumn("dbo.BijkomendeInfoes", "VerslagBuitengewoonOnderwijs");
            DropColumn("dbo.BijkomendeInfoes", "VerhoogdeZorgVraag");
            DropColumn("dbo.BijkomendeInfoes", "LeerProblemen");
        }
    }
}
