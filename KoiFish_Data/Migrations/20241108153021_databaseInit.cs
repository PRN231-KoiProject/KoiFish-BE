using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiFish_Data.Migrations
{
    /// <inheritdoc />
    public partial class databaseInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FishPonds");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PondFeatures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PondFeatures_UserId",
                table: "PondFeatures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PondFeatures_Users_UserId",
                table: "PondFeatures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondFeatures_Users_UserId",
                table: "PondFeatures");

            migrationBuilder.DropIndex(
                name: "IX_PondFeatures_UserId",
                table: "PondFeatures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PondFeatures");

            migrationBuilder.CreateTable(
                name: "FishPonds",
                columns: table => new
                {
                    FishPondId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KoiFishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PondId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishPonds", x => x.FishPondId);
                    table.ForeignKey(
                        name: "FK_FishPonds_KoiFishes_KoiFishId",
                        column: x => x.KoiFishId,
                        principalTable: "KoiFishes",
                        principalColumn: "KoiFishId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FishPonds_PondFeatures_PondId",
                        column: x => x.PondId,
                        principalTable: "PondFeatures",
                        principalColumn: "PondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FishPonds_KoiFishId",
                table: "FishPonds",
                column: "KoiFishId");

            migrationBuilder.CreateIndex(
                name: "IX_FishPonds_PondId",
                table: "FishPonds",
                column: "PondId");
        }
    }
}
