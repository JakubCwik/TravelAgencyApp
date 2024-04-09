using Microsoft.EntityFrameworkCore.Migrations;

namespace BiuroPodrozyAPI.Migrations
{
    public partial class TravelAgencyIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "TravelAgencies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelAgencies_CreatedById",
                table: "TravelAgencies",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelAgencies_Users_CreatedById",
                table: "TravelAgencies",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelAgencies_Users_CreatedById",
                table: "TravelAgencies");

            migrationBuilder.DropIndex(
                name: "IX_TravelAgencies_CreatedById",
                table: "TravelAgencies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TravelAgencies");
        }
    }
}
