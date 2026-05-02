using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class modified_flashcard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_FlashCards_FlashCardId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_FlashCardId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "FlashCardId",
                table: "Words");

            migrationBuilder.CreateTable(
                name: "FlashCardWords",
                columns: table => new
                {
                    FlashCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMemorized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashCardWords", x => new { x.FlashCardId, x.WordId });
                    table.ForeignKey(
                        name: "FK_FlashCardWords_FlashCards_FlashCardId",
                        column: x => x.FlashCardId,
                        principalTable: "FlashCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlashCardWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashCardWords_WordId",
                table: "FlashCardWords",
                column: "WordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashCardWords");

            migrationBuilder.AddColumn<Guid>(
                name: "FlashCardId",
                table: "Words",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_FlashCardId",
                table: "Words",
                column: "FlashCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_FlashCards_FlashCardId",
                table: "Words",
                column: "FlashCardId",
                principalTable: "FlashCards",
                principalColumn: "Id");
        }
    }
}
