using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.ApplicationDbRead
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ImageUrl",
                value: "images/category_img.jpg");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("2af67565-75f7-4511-9b67-3762e917c173"),
                column: "ImageUrl",
                value: "images/category_img.jpg");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("48b31fd9-e2a2-4b6a-9884-e2b6c664715b"),
                column: "ImageUrl",
                value: "images/category_img.jpg");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("c5f9dd20-276f-4a4a-bbb1-26b795a8514c"),
                column: "ImageUrl",
                value: "images/category_img.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ImageUrl",
                value: "/uploads/images/category_img.jpg");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("2af67565-75f7-4511-9b67-3762e917c173"),
                column: "ImageUrl",
                value: "/uploads/images/category_img.jpg");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("48b31fd9-e2a2-4b6a-9884-e2b6c664715b"),
                column: "ImageUrl",
                value: "/uploads/images/category_img.jpg");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("c5f9dd20-276f-4a4a-bbb1-26b795a8514c"),
                column: "ImageUrl",
                value: "/uploads/images/category_img.jpg");
        }
    }
}
