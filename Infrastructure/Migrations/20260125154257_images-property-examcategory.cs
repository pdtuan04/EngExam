using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class imagespropertyexamcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ExamCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ImageUrl",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ExamCategories");
        }
    }
}
