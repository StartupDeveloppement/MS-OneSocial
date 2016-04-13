namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAbout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "Image", c => c.String());
            AddColumn("dbo.UserProfile", "Description", c => c.String());
            AddColumn("dbo.UserProfile", "Cv", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "Cv");
            DropColumn("dbo.UserProfile", "Description");
            DropColumn("dbo.UserProfile", "Image");
        }
    }
}
