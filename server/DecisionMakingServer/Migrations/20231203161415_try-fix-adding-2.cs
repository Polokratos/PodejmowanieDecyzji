using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionMakingServer.Migrations
{
    /// <inheritdoc />
    public partial class tryfixadding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CriterionId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CriterionId",
                table: "Answers",
                column: "CriterionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Criteria_CriterionId",
                table: "Answers",
                column: "CriterionId",
                principalTable: "Criteria",
                principalColumn: "CriterionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Criteria_CriterionId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CriterionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CriterionId",
                table: "Answers");
        }
    }
}
