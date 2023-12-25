using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionMakingServer.Migrations
{
    /// <inheritdoc />
    public partial class CriterionAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriterionAnswers",
                columns: table => new
                {
                    CriterionAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RankingId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LeftCriterionId = table.Column<int>(type: "int", nullable: false),
                    RightCriterionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriterionAnswers", x => x.CriterionAnswerId);
                    table.ForeignKey(
                        name: "FK_CriterionAnswers_Criteria_LeftCriterionId",
                        column: x => x.LeftCriterionId,
                        principalTable: "Criteria",
                        principalColumn: "CriterionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CriterionAnswers_Criteria_RightCriterionId",
                        column: x => x.RightCriterionId,
                        principalTable: "Criteria",
                        principalColumn: "CriterionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CriterionAnswers_Rankings_RankingId",
                        column: x => x.RankingId,
                        principalTable: "Rankings",
                        principalColumn: "RankingId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CriterionAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriterionAnswers_LeftCriterionId",
                table: "CriterionAnswers",
                column: "LeftCriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionAnswers_RankingId",
                table: "CriterionAnswers",
                column: "RankingId");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionAnswers_RightCriterionId",
                table: "CriterionAnswers",
                column: "RightCriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionAnswers_UserId",
                table: "CriterionAnswers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriterionAnswers");
        }
    }
}
