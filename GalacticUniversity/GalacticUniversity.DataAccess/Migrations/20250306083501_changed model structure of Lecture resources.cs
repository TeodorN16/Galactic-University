using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticUniversity.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changedmodelstructureofLectureresources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourcePath",
                table: "lectureResources");

            migrationBuilder.RenameColumn(
                name: "ResourceType",
                table: "lectureResources",
                newName: "FileUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "lectureResources",
                newName: "ResourceType");

            migrationBuilder.AddColumn<string>(
                name: "ResourcePath",
                table: "lectureResources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
