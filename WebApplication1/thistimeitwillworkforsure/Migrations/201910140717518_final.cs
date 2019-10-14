namespace thistimeitwillworkforsure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Humen", newName: "FireFighters");
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        PIN = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.FireFighters", "Status");
            DropColumn("dbo.FireFighters", "PIN");
            DropColumn("dbo.FireFighters", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FireFighters", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.FireFighters", "PIN", c => c.String());
            AddColumn("dbo.FireFighters", "Status", c => c.String());
            DropTable("dbo.Users");
            RenameTable(name: "dbo.FireFighters", newName: "Humen");
        }
    }
}
