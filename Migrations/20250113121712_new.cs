using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marie.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Days = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysCount = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hourse = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Educating = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherID);
                });

            migrationBuilder.CreateTable(
                name: "DaySchedules",
                columns: table => new
                {
                    DayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySchedules", x => x.DayID);
                    table.ForeignKey(
                        name: "FK_DaySchedules_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                    table.ForeignKey(
                        name: "FK_DaySchedules_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "TeacherID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedules_SubjectID",
                table: "DaySchedules",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedules_TeacherID",
                table: "DaySchedules",
                column: "TeacherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaySchedules");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
