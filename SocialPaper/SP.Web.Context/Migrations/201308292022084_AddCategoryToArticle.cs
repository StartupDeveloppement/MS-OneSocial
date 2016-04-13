namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "Category");
        }
    }
}
