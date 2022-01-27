using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalShelter.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalGenders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalGenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnimalNumber = table.Column<string>(nullable: false),
                    Age = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    InProcessDate = table.Column<DateTime>(nullable: false),
                    LocationLatitude = table.Column<decimal>(nullable: false),
                    LocationLongitude = table.Column<decimal>(nullable: false),
                    AgeInWeeks = table.Column<decimal>(nullable: false),
                    AnimalGenderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_AnimalGenders_AnimalGenderId",
                        column: x => x.AnimalGenderId,
                        principalTable: "AnimalGenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AnimalGenders",
                columns: new[] { "Id", "Gender" },
                values: new object[] { 1, "Male" });

            migrationBuilder.InsertData(
                table: "AnimalGenders",
                columns: new[] { "Id", "Gender" },
                values: new object[] { 2, "Female" });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AnimalGenderId",
                table: "Animals",
                column: "AnimalGenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "AnimalGenders");
        }
    }
}
