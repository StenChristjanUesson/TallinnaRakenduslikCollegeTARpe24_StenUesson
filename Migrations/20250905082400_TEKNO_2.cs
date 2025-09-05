using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Migrations
{
    /// <inheritdoc />
    public partial class TEKNO_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "age",
                table: "Student",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "Main_Language",
                table: "Student",
                newName: "MainLanguage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Student",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "MainLanguage",
                table: "Student",
                newName: "Main_Language");
        }
    }
}
