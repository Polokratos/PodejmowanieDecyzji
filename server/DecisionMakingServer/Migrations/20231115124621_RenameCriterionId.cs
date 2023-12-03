using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionMakingServer.Migrations
{
    /// <inheritdoc />
    public partial class RenameCriterionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CriterionID",
                table: "Criteria",
                newName: "CriterionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CriterionId",
                table: "Criteria",
                newName: "CriterionID");
        }
    }
}
