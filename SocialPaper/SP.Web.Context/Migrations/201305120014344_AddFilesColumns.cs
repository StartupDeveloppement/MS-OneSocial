namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilesColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Picture", c => c.String());
            AddColumn("dbo.Article", "File", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "File");
            DropColumn("dbo.Article", "Picture");
        }
    }
}
