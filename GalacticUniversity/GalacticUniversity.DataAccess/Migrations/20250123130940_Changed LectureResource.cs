using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticUniversity.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLectureResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lectureResources_lectures_LectureID",
                table: "lectureResources");

            migrationBuilder.AlterColumn<int>(
                name: "LectureID",
                table: "lectureResources",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_lectureResources_lectures_LectureID",
                table: "lectureResources",
                column: "LectureID",
                principalTable: "lectures",
                principalColumn: "LectureID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lectureResources_lectures_LectureID",
                table: "lectureResources");

            migrationBuilder.AlterColumn<int>(
                name: "LectureID",
                table: "lectureResources",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lectureResources_lectures_LectureID",
                table: "lectureResources",
                column: "LectureID",
                principalTable: "lectures",
                principalColumn: "LectureID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
