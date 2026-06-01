using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.ApplicationDbRead
{
    /// <inheritdoc />
    public partial class modified1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000002"),
                column: "Content",
                value: "They usually ___ (play) basketball on weekends.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000004"),
                column: "Content",
                value: "I ___ (study) for my TOEIC exam right now.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000006"),
                column: "Content",
                value: "We ___ (see) this movie before.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000008"),
                column: "Content",
                value: "It ___ (rain) since morning.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000010"),
                column: "Content",
                value: "They ___ (win) the match yesterday.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000012"),
                column: "Content",
                value: "While we ___ (play), it started to rain.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000014"),
                column: "Content",
                value: "She told me she ___ (finish) the job.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000016"),
                column: "Content",
                value: "I ___ (study) English for a year before I visited London.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000018"),
                column: "Content",
                value: "Don't worry, she ___ (call) you back later.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000020"),
                column: "Content",
                value: "They ___ (have) dinner when we arrive tonight.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000022"),
                column: "Content",
                value: "They ___ (build) the new bridge by July.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000024"),
                column: "Content",
                value: "By the time you wake up, I ___ (drive) for 3 hours.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000002"),
                column: "Content",
                value: "They usually [play] basketball on weekends.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000004"),
                column: "Content",
                value: "I [am studying] for my TOEIC exam right now.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000006"),
                column: "Content",
                value: "We [have seen] this movie before.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000008"),
                column: "Content",
                value: "It [has been raining] since morning.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000010"),
                column: "Content",
                value: "They [won] the match yesterday.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000012"),
                column: "Content",
                value: "While we [were playing], it started to rain.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000014"),
                column: "Content",
                value: "She told me she [had finished] the job.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000016"),
                column: "Content",
                value: "I [had been studying] English for a year before I visited London.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000018"),
                column: "Content",
                value: "Don't worry, she [will call] you back later.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000020"),
                column: "Content",
                value: "They [will be having] dinner when we arrive tonight.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000022"),
                column: "Content",
                value: "They [will have built] the new bridge by July.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000024"),
                column: "Content",
                value: "By the time you wake up, I [will have been driving] for 3 hours.");
        }
    }
}
