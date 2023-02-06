using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedPeopleWithRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "people");

            migrationBuilder.CreateTable(
                name: "TB_Cities",
                schema: "people",
                columns: table => new
                {
                    id = table.Column<int>(name: "_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_People",
                schema: "people",
                columns: table => new
                {
                    id = table.Column<int>(name: "_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Cityid = table.Column<int>(name: "City_id", type: "int", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_People", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_People_TB_Cities_City_id",
                        column: x => x.Cityid,
                        principalSchema: "people",
                        principalTable: "TB_Cities",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_PhoneNumbers",
                schema: "people",
                columns: table => new
                {
                    id = table.Column<int>(name: "_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Personid = table.Column<int>(name: "Person_id", type: "int", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PhoneNumbers", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_PhoneNumbers_TB_People_Person_id",
                        column: x => x.Personid,
                        principalSchema: "people",
                        principalTable: "TB_People",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_People_City_id",
                schema: "people",
                table: "TB_People",
                column: "City_id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PhoneNumbers_Person_id",
                schema: "people",
                table: "TB_PhoneNumbers",
                column: "Person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PhoneNumbers",
                schema: "people");

            migrationBuilder.DropTable(
                name: "TB_People",
                schema: "people");

            migrationBuilder.DropTable(
                name: "TB_Cities",
                schema: "people");
        }
    }
}
