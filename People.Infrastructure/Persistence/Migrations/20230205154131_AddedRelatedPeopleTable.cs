using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelatedPeopleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_RelatedPeople",
                schema: "people",
                columns: table => new
                {
                    id = table.Column<int>(name: "_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatedPersonEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelationType = table.Column<int>(type: "int", nullable: false),
                    Personid = table.Column<int>(name: "Person_id", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_RelatedPeople", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_RelatedPeople_TB_People_Person_id",
                        column: x => x.Personid,
                        principalSchema: "people",
                        principalTable: "TB_People",
                        principalColumn: "_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_RelatedPeople_Person_id",
                schema: "people",
                table: "TB_RelatedPeople",
                column: "Person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_RelatedPeople",
                schema: "people");
        }
    }
}
