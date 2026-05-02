using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modified_flashcard1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashCardWords");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddAt",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "FlashCardId",
                table: "Words",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsMemorized",
                table: "Words",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Words_FlashCardId",
                table: "Words",
                column: "FlashCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_FlashCards_FlashCardId",
                table: "Words",
                column: "FlashCardId",
                principalTable: "FlashCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_FlashCards_FlashCardId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_FlashCardId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "AddAt",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "FlashCardId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "IsMemorized",
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
    }
}
