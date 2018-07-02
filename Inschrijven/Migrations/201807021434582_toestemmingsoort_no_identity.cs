namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toestemmingsoort_no_identity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Toestemmings", "ToestemmingSoort_ToestemmingSoortId", "dbo.ToestemmingSoorts");
            DropPrimaryKey("dbo.ToestemmingSoorts");
            AlterColumn("dbo.ToestemmingSoorts", "ToestemmingSoortId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ToestemmingSoorts", "ToestemmingSoortId");
            AddForeignKey("dbo.Toestemmings", "ToestemmingSoort_ToestemmingSoortId", "dbo.ToestemmingSoorts", "ToestemmingSoortId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toestemmings", "ToestemmingSoort_ToestemmingSoortId", "dbo.ToestemmingSoorts");
            DropPrimaryKey("dbo.ToestemmingSoorts");
            AlterColumn("dbo.ToestemmingSoorts", "ToestemmingSoortId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ToestemmingSoorts", "ToestemmingSoortId");
            AddForeignKey("dbo.Toestemmings", "ToestemmingSoort_ToestemmingSoortId", "dbo.ToestemmingSoorts", "ToestemmingSoortId", cascadeDelete: true);
        }
    }
}
