using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionMakingServer.Migrations
{
    /// <inheritdoc />
    public partial class score : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Results",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Results");
        }
    }
}
