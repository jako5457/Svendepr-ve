using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDataLayer.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "TrackingCode",
                value: new Guid("334573c5-5764-4934-a792-1d1f8d4cb1a1"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "TrackingCode",
                value: new Guid("a585b5ca-6d8d-4362-99fa-1cd01e6f878c"));
        }
    }
}
