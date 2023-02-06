using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SpecifiedDeleteActionPeopleToCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_People_TB_Cities_City_id",
                schema: "people",
                table: "TB_People");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_People_TB_Cities_City_id",
                schema: "people",
                table: "TB_People",
                column: "City_id",
                principalSchema: "people",
                principalTable: "TB_Cities",
                principalColumn: "_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_People_TB_Cities_City_id",
                schema: "people",
                table: "TB_People");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_People_TB_Cities_City_id",
                schema: "people",
                table: "TB_People",
                column: "City_id",
                principalSchema: "people",
                principalTable: "TB_Cities",
                principalColumn: "_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
