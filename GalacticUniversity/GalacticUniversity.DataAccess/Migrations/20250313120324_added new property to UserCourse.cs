using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticUniversity.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addednewpropertytoUserCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectureID",
                table: "UserCourses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_LectureID",
                table: "UserCourses",
                column: "LectureID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_lectures_LectureID",
                table: "UserCourses",
                column: "LectureID",
                principalTable: "lectures",
                principalColumn: "LectureID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_lectures_LectureID",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_UserCourses_LectureID",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "LectureID",
                table: "UserCourses");
        }
    }
}
