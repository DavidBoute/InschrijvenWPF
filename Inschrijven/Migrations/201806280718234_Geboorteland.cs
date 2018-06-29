namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Geboorteland : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leerlings", "Geboorteland", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leerlings", "Geboorteland");
        }
    }
}
