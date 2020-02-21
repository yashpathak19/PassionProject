namespace PassionProject_PhoneBlog_n01364240.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeaturePhones",
                c => new
                    {
                        Feature_FeatureID = c.Int(nullable: false),
                        Phone_PhoneID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Feature_FeatureID, t.Phone_PhoneID })
                .ForeignKey("dbo.Features", t => t.Feature_FeatureID, cascadeDelete: true)
                .ForeignKey("dbo.Phones", t => t.Phone_PhoneID, cascadeDelete: true)
                .Index(t => t.Feature_FeatureID)
                .Index(t => t.Phone_PhoneID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeaturePhones", "Phone_PhoneID", "dbo.Phones");
            DropForeignKey("dbo.FeaturePhones", "Feature_FeatureID", "dbo.Features");
            DropIndex("dbo.FeaturePhones", new[] { "Phone_PhoneID" });
            DropIndex("dbo.FeaturePhones", new[] { "Feature_FeatureID" });
            DropTable("dbo.FeaturePhones");
        }
    }
}
