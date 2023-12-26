using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionMakingServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCriterionFromResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Criteria_CriterionId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_CriterionId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "CriterionId",
                table: "Results");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CriterionId",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Results_CriterionId",
                table: "Results",
                column: "CriterionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Criteria_CriterionId",
                table: "Results",
                column: "CriterionId",
                principalTable: "Criteria",
                principalColumn: "CriterionId");
        }
    }
}
