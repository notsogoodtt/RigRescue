using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RigRescue.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentIdPath",
                table: "TechnicianProfiles",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentIdPath",
                table: "TechnicianProfiles");
        }
    }
}
