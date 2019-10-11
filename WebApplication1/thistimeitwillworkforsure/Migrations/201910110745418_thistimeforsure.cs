namespace thistimeitwillworkforsure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thistimeforsure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Humen", "PIN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Humen", "PIN");
        }
    }
}
