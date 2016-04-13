namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublicationDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "FbPublication", c => c.DateTime());
            AddColumn("dbo.Article", "TwitterPublication", c => c.DateTime());
            AddColumn("dbo.Article", "GooglePublication", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "GooglePublication");
            DropColumn("dbo.Article", "TwitterPublication");
            DropColumn("dbo.Article", "FbPublication");
        }
    }
}
