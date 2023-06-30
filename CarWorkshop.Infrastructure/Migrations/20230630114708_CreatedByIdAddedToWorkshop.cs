using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByIdAddedToWorkshop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "CarWorkhops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarWorkhops_CreatedById",
                table: "CarWorkhops",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CarWorkhops_AspNetUsers_CreatedById",
                table: "CarWorkhops",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarWorkhops_AspNetUsers_CreatedById",
                table: "CarWorkhops");

            migrationBuilder.DropIndex(
                name: "IX_CarWorkhops_CreatedById",
                table: "CarWorkhops");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CarWorkhops");

        }
    }
}
