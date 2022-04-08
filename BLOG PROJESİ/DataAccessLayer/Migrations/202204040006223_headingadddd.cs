namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class headingadddd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Headings", "HeadingName", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Headings", "HeadingName", c => c.String(maxLength: 50));
        }
    }
}
