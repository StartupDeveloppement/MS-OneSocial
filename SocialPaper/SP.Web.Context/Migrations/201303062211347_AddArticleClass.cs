namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArticleClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Abstract = c.String(),
                        Content = c.String(),
                        Publication = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Article", new[] { "UserId" });
            DropForeignKey("dbo.Article", "UserId", "dbo.UserProfile");
            DropTable("dbo.Article");
        }
    }
}
