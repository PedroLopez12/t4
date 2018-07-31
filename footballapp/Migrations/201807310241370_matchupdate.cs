namespace footballapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matchupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "Tournament_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "Tournament_Id");
        }
    }
}
