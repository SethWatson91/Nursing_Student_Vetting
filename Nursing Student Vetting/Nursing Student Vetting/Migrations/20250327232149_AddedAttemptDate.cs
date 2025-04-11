using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nursing_Student_Vetting.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttemptDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AttemptDate",
                table: "StudentTests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "StudentTests",
                keyColumns: new[] { "AttemptNumber", "StudentID", "TestID" },
                keyValues: new object[] { 1, "W00001001", 1 },
                column: "AttemptDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "StudentTests",
                keyColumns: new[] { "AttemptNumber", "StudentID", "TestID" },
                keyValues: new object[] { 1, "W00001001", 2 },
                column: "AttemptDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "StudentTests",
                keyColumns: new[] { "AttemptNumber", "StudentID", "TestID" },
                keyValues: new object[] { 1, "W00001002", 2 },
                column: "AttemptDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "StudentTests",
                keyColumns: new[] { "AttemptNumber", "StudentID", "TestID" },
                keyValues: new object[] { 2, "W00001002", 2 },
                column: "AttemptDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttemptDate",
                table: "StudentTests");
        }
    }
}
