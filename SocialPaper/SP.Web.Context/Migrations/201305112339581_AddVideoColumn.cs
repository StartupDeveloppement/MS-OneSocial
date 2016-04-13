namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Video", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "Video");
        }
    }
}
