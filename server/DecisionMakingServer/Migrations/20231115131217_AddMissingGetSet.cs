using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionMakingServer.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingGetSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scales_Users_UserId",
                table: "Scales");

            migrationBuilder.DropIndex(
                name: "IX_Scales_UserId",
                table: "Scales");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Scales");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Scales",
                newName: "RankingId");

            migrationBuilder.AddColumn<string>(
                name: "AskOrder",
                table: "Rankings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Rankings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Rankings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScaleId",
                table: "Rankings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rankings_ScaleId",
                table: "Rankings",
                column: "ScaleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rankings_Scales_ScaleId",
                table: "Rankings",
                column: "ScaleId",
                principalTable: "Scales",
                principalColumn: "ScaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rankings_Scales_ScaleId",
                table: "Rankings");

            migrationBuilder.DropIndex(
                name: "IX_Rankings_ScaleId",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "AskOrder",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "ScaleId",
                table: "Rankings");

            migrationBuilder.RenameColumn(
                name: "RankingId",
                table: "Scales",
                newName: "UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Scales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Scales_UserId",
                table: "Scales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scales_Users_UserId",
                table: "Scales",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
