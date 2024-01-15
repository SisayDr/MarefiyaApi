using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarefiyaApi.Migrations
{
    public partial class _43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNos",
                schema: "Marefiya",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "RoomNo",
                schema: "Marefiya",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNo",
                schema: "Marefiya",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "RoomNos",
                schema: "Marefiya",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
