using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumHum.Migrations
{
    public partial class addfoodimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "FoodItem",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "FoodItem");
        }
    }
}
