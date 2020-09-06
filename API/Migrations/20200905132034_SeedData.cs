using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LikeButton.API.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "Id", "Abstract", "DatePublished", "Title" },
                values: new object[] { 1, "EarningType", new DateTime(2020, 9, 5, 18, 50, 33, 904, DateTimeKind.Local).AddTicks(9250), "EarningType" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "Id", "Abstract", "DatePublished", "Title" },
                values: new object[] { 2, "AddressType", new DateTime(2020, 9, 5, 18, 50, 33, 906, DateTimeKind.Local).AddTicks(291), "AddressType" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "Id", "Abstract", "DatePublished", "Title" },
                values: new object[] { 3, "ContactType", new DateTime(2020, 9, 5, 18, 50, 33, 906, DateTimeKind.Local).AddTicks(332), "ContactType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
