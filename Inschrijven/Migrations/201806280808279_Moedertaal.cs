namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Moedertaal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BijkomendeInfoes", "Moedertaal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BijkomendeInfoes", "Moedertaal");
        }
    }
}
