namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVisitsColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Visits", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "Visits");
        }
    }
}
