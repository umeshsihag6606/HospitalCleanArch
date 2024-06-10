using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peristance.Migrations
{
    /// <inheritdoc />
    public partial class AddressStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CityId",
                table: "Staffs",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Cities_CityId",
                table: "Staffs",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Cities_CityId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_CityId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Staffs");
        }
    }
}
