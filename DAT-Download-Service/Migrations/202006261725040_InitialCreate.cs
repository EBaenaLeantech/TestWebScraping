namespace DAT_Download_Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DATRateDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataDownloadDate = c.DateTime(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        TruckType = c.String(),
                        DestinationPostalCode = c.String(),
                        DestinationState = c.String(),
                        DestinationCity = c.String(),
                        DestinationGeoLabelShort = c.String(),
                        DestinationGeoLabelLong = c.String(),
                        OriginPostalCode = c.String(),
                        OriginState = c.String(),
                        OriginCity = c.String(),
                        OriginGeoType = c.String(),
                        OriginGeoLabelShort = c.String(),
                        OriginGeoLabelLong = c.String(),
                        PCMilerPracticalMileage = c.Int(nullable: false),
                        Error = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractRateDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AccessorialsExcludingFuel = c.Double(nullable: false),
                        OriginGeoExpansion = c.String(),
                        DestinationGeoExpansion = c.String(),
                        AvgLinehaulRate = c.Double(nullable: false),
                        LowLinehaulRate = c.Double(nullable: false),
                        HighLinehaulRate = c.Double(nullable: false),
                        FuelSurcharge = c.Double(nullable: false),
                        Companies = c.Int(nullable: false),
                        Reports = c.Int(nullable: false),
                        LinehaulRateStdDev = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DATRateDatas", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SpotRateDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        OriginGeoExpansion = c.String(),
                        DestinationGeoExpansion = c.String(),
                        AvgLinehaulRate = c.Double(nullable: false),
                        LowLinehaulRate = c.Double(nullable: false),
                        HighLinehaulRate = c.Double(nullable: false),
                        FuelSurcharge = c.Double(nullable: false),
                        Companies = c.Int(nullable: false),
                        Reports = c.Int(nullable: false),
                        LinehaulRateStdDev = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DATRateDatas", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpotRateDataModels", "Id", "dbo.DATRateDatas");
            DropForeignKey("dbo.ContractRateDataModels", "Id", "dbo.DATRateDatas");
            DropIndex("dbo.SpotRateDataModels", new[] { "Id" });
            DropIndex("dbo.ContractRateDataModels", new[] { "Id" });
            DropTable("dbo.SpotRateDataModels");
            DropTable("dbo.ContractRateDataModels");
            DropTable("dbo.DATRateDatas");
        }
    }
}
