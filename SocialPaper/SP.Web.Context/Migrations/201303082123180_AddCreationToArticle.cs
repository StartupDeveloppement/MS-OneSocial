namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreationToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Creation", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Article", "Publication", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Article", "Publication", c => c.DateTime(nullable: false));
            DropColumn("dbo.Article", "Creation");
        }
    }
}
