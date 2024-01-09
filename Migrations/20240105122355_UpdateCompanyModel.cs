using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompanyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_ResponsibleId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ResponsibleId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ResponsibleUserId",
                table: "Companies",
                column: "ResponsibleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_ResponsibleUserId",
                table: "Companies",
                column: "ResponsibleUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_ResponsibleUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ResponsibleUserId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ResponsibleId",
                table: "Companies",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_ResponsibleId",
                table: "Companies",
                column: "ResponsibleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
