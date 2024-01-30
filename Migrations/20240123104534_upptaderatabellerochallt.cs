using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class upptaderatabellerochallt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpportunityName",
                table: "WorkTaskOpportunities",
                newName: "Company");

            migrationBuilder.CreateTable(
                name: "UserWorkTaskOpportunities",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkTaskOpportunityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkTaskOpportunities", x => new { x.UserId, x.WorkTaskOpportunityId });
                    table.ForeignKey(
                        name: "FK_UserWorkTaskOpportunities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkTaskOpportunities_WorkTaskOpportunities_WorkTaskOpportunityId",
                        column: x => x.WorkTaskOpportunityId,
                        principalTable: "WorkTaskOpportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkTaskOpportunities_WorkTaskOpportunityId",
                table: "UserWorkTaskOpportunities",
                column: "WorkTaskOpportunityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWorkTaskOpportunities");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "WorkTaskOpportunities",
                newName: "OpportunityName");
        }
    }
}
