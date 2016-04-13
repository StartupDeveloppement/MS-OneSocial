namespace SP.Web.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiteAndNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserProfile", "UserName", c => c.String());
            AddColumn("UserProfile", "SiteName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("UserProfile", "UserName");
            DropColumn("UserProfile", "SiteName");
        }
    }
}
