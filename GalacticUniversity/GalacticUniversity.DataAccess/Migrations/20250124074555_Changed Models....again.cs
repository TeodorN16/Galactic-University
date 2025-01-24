using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticUniversity.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedModelsagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lectures_courses_CourseID",
                table: "lectures");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "lectures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_lectures_courses_CourseID",
                table: "lectures",
                column: "CourseID",
                principalTable: "courses",
                principalColumn: "CourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lectures_courses_CourseID",
                table: "lectures");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "lectures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lectures_courses_CourseID",
                table: "lectures",
                column: "CourseID",
                principalTable: "courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
