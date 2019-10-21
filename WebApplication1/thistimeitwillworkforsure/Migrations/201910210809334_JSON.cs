namespace thistimeitwillworkforsure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JSON : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deployments", "JSON", c => c.String());
            AddColumn("dbo.FireFighters", "JSON", c => c.String());
            AddColumn("dbo.Resources", "JSON", c => c.String());
            AddColumn("dbo.Users", "JSON", c => c.String());
            AddColumn("dbo.Vehicles", "JSON", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "JSON");
            DropColumn("dbo.Users", "JSON");
            DropColumn("dbo.Resources", "JSON");
            DropColumn("dbo.FireFighters", "JSON");
            DropColumn("dbo.Deployments", "JSON");
        }
    }
}
