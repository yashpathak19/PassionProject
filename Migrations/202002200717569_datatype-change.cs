namespace PassionProject_PhoneBlog_n01364240.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatypechange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Phones", "PhoneBattery", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Phones", "PhoneBattery", c => c.String());
        }
    }
}
