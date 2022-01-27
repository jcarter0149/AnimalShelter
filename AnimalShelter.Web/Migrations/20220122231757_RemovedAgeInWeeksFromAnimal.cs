using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalShelter.Web.Migrations
{
    public partial class RemovedAgeInWeeksFromAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeInWeeks",
                table: "Animals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AgeInWeeks",
                table: "Animals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
