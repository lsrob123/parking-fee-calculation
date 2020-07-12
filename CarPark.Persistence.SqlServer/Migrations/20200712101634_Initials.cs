using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPark.Persistence.SqlServer.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultFlatRates",
                columns: table => new
                {
                    Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Value = table.Column<decimal>(type: "decimal(11,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultFlatRates", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "FlatRates",
                columns: table => new
                {
                    Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CanExtendToFollowingDay = table.Column<bool>(nullable: false),
                    DayOffsetToMonday = table.Column<int>(nullable: false),
                    EntryHourOffsetFrom = table.Column<decimal>(type: "decimal(11,4)", nullable: false),
                    EntryHourOffsetTo = table.Column<decimal>(type: "decimal(11,4)", nullable: false),
                    ExitHourOffsetFrom = table.Column<decimal>(type: "decimal(11,4)", nullable: false),
                    ExitHourOffsetTo = table.Column<decimal>(type: "decimal(11,4)", nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(11,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatRates", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "HourlyRates",
                columns: table => new
                {
                    Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    FromHour = table.Column<decimal>(type: "decimal(11,4)", nullable: false),
                    ToHour = table.Column<decimal>(type: "decimal(11,4)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(11,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourlyRates", x => x.Key);
                });

            migrationBuilder.InsertData(
                table: "DefaultFlatRates",
                columns: new[] { "Key", "Value" },
                values: new object[] { new Guid("53e52a31-300a-43e3-81c0-d07e04912289"), 20m });

            migrationBuilder.InsertData(
                table: "FlatRates",
                columns: new[] { "Key", "CanExtendToFollowingDay", "DayOffsetToMonday", "EntryHourOffsetFrom", "EntryHourOffsetTo", "ExitHourOffsetFrom", "ExitHourOffsetTo", "Priority", "TotalPrice" },
                values: new object[,]
                {
                    { new Guid("c2e00b43-d42a-4225-8a84-e73b48ef5db3"), false, 4, 102m, 105m, 111.5m, 119.5m, 0, 13m },
                    { new Guid("c96ec84e-d26e-4e95-b5ff-d5695755c6a4"), false, 3, 78m, 81m, 87.5m, 95.5m, 0, 13m },
                    { new Guid("58899bec-15c7-46de-8cda-c9dedce08f07"), false, 2, 54m, 57m, 63.5m, 71.5m, 0, 13m },
                    { new Guid("fc535ed7-553f-4b20-ad30-c5db0e38228e"), false, 1, 30m, 33m, 39.5m, 47.5m, 0, 13m },
                    { new Guid("7ba6627f-37ac-4b7e-8ab2-bb834474471c"), false, 0, 6m, 9m, 15.5m, 23.5m, 0, 13m },
                    { new Guid("28c5f0d3-284b-458e-9f5a-a152889403eb"), true, 4, 114m, 120m, 114m, 126m, 0, 6.5m },
                    { new Guid("714d8101-ea70-44d1-baf7-9b81934c347f"), false, 4, 96m, 102m, 96m, 102m, 0, 6.5m },
                    { new Guid("b69f8bcd-15cc-4317-8102-84b8b313cbe2"), false, 3, 72m, 78m, 72m, 78m, 0, 6.5m },
                    { new Guid("d3c39239-db5a-4dee-8ce9-8e07066b2994"), false, 3, 90m, 96m, 90m, 102m, 0, 6.5m },
                    { new Guid("b9f841b7-966b-4d65-a582-4c2097039af7"), false, 2, 48m, 54m, 48m, 54m, 0, 6.5m },
                    { new Guid("bedd0515-f4ad-43a4-b053-4a47f2809b08"), false, 1, 42m, 48m, 42m, 54m, 0, 6.5m },
                    { new Guid("75bb7456-bd7f-4562-bd0f-366bb06d788f"), false, 1, 24m, 30m, 24m, 30m, 0, 6.5m },
                    { new Guid("90e1933e-9ccd-4ceb-a21b-3016376eece0"), false, 0, 18m, 24m, 18m, 30m, 0, 6.5m },
                    { new Guid("709bb016-f728-4e77-a034-2a57aba74276"), false, 0, 0m, 6m, 0m, 6m, 0, 6.5m },
                    { new Guid("c4081969-5101-4d66-863e-26d3b8334e7c"), false, 6, 144m, 168m, 144m, 168m, 0, 10m },
                    { new Guid("729e1681-08ed-4051-badb-043b32ee64ed"), true, 5, 120m, 144m, 120m, 144m, 0, 10m },
                    { new Guid("fd1c8e1d-64ac-4711-9166-787004e298cb"), false, 2, 66m, 72m, 66m, 78m, 0, 6.5m }
                });

            migrationBuilder.InsertData(
                table: "HourlyRates",
                columns: new[] { "Key", "FromHour", "ToHour", "Value" },
                values: new object[,]
                {
                    { new Guid("4c656f4d-319d-4d93-b75b-7d2d6e135673"), 1m, 2m, 10m },
                    { new Guid("c5303ce4-0d4a-402d-9ab1-365d4f48237b"), 0m, 1m, 5m },
                    { new Guid("5a9d5a44-2bbd-4184-bda4-9922da4c1dde"), 2m, 3m, 15m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlatRates_EntryHourOffsetTo",
                table: "FlatRates",
                column: "EntryHourOffsetTo");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyRates_FromHour_ToHour",
                table: "HourlyRates",
                columns: new[] { "FromHour", "ToHour" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultFlatRates");

            migrationBuilder.DropTable(
                name: "FlatRates");

            migrationBuilder.DropTable(
                name: "HourlyRates");
        }
    }
}
