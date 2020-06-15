using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAT_Download_Service.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatRatesData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDownloadDate = table.Column<DateTime>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    TruckType = table.Column<string>(nullable: true),
                    DestinationPostalCode = table.Column<string>(nullable: true),
                    DestinationState = table.Column<string>(nullable: true),
                    DestinationCity = table.Column<string>(nullable: true),
                    DestinationGeoLabelShort = table.Column<string>(nullable: true),
                    DestinationGeoLabelLong = table.Column<string>(nullable: true),
                    OriginPostalCode = table.Column<string>(nullable: true),
                    OriginState = table.Column<string>(nullable: true),
                    OriginCity = table.Column<string>(nullable: true),
                    OriginGeoType = table.Column<string>(nullable: true),
                    OriginGeoLabelShort = table.Column<string>(nullable: true),
                    OriginGeoLabelLong = table.Column<string>(nullable: true),
                    PCMilerPracticalMileage = table.Column<int>(nullable: false),
                    Error = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatRatesData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractRateDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginGeoExpansion = table.Column<string>(nullable: true),
                    DestinationGeoExpansion = table.Column<string>(nullable: true),
                    AvgLinehaulRate = table.Column<double>(nullable: false),
                    LowLinehaulRate = table.Column<double>(nullable: false),
                    HighLinehaulRate = table.Column<double>(nullable: false),
                    FuelSurcharge = table.Column<double>(nullable: false),
                    Companies = table.Column<int>(nullable: false),
                    Reports = table.Column<int>(nullable: false),
                    LinehaulRateStdDev = table.Column<double>(nullable: false),
                    DATRateDataId = table.Column<int>(nullable: false),
                    AccessorialsExcludingFuel = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractRateDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractRateDataModel_DatRatesData_DATRateDataId",
                        column: x => x.DATRateDataId,
                        principalTable: "DatRatesData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpotRateDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginGeoExpansion = table.Column<string>(nullable: true),
                    DestinationGeoExpansion = table.Column<string>(nullable: true),
                    AvgLinehaulRate = table.Column<double>(nullable: false),
                    LowLinehaulRate = table.Column<double>(nullable: false),
                    HighLinehaulRate = table.Column<double>(nullable: false),
                    FuelSurcharge = table.Column<double>(nullable: false),
                    Companies = table.Column<int>(nullable: false),
                    Reports = table.Column<int>(nullable: false),
                    LinehaulRateStdDev = table.Column<double>(nullable: false),
                    DATRateDataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpotRateDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpotRateDataModel_DatRatesData_DATRateDataId",
                        column: x => x.DATRateDataId,
                        principalTable: "DatRatesData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractRateDataModel_DATRateDataId",
                table: "ContractRateDataModel",
                column: "DATRateDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpotRateDataModel_DATRateDataId",
                table: "SpotRateDataModel",
                column: "DATRateDataId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractRateDataModel");

            migrationBuilder.DropTable(
                name: "SpotRateDataModel");

            migrationBuilder.DropTable(
                name: "DatRatesData");
        }
    }
}
