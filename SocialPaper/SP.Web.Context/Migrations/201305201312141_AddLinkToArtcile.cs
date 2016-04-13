namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLinkToArtcile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "LinkUrl", c => c.String());
            AddColumn("dbo.Article", "isLink", c => c.Boolean(nullable: false, defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "isLink");
            DropColumn("dbo.Article", "LinkUrl");
        }
    }
}
