using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BM.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    SlotId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.SlotId);
                    table.ForeignKey(
                        name: "FK_Slots_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[] { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "Interviewer" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[] { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), "Candidate" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "RoleId" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Mary", new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "RoleId" },
                values: new object[] { new Guid("44fe7cef-31d0-41ea-a113-41d0de131f97"), "John", new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee") });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "SlotId", "DateEnd", "DateStart", "UserId" },
                values: new object[] { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), new DateTime(2021, 7, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "SlotId", "DateEnd", "DateStart", "UserId" },
                values: new object[] { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), new DateTime(2021, 7, 19, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "SlotId", "DateEnd", "DateStart", "UserId" },
                values: new object[] { new Guid("52c5b675-319f-4302-8076-1fd5d8eb9ccc"), new DateTime(2021, 7, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("44fe7cef-31d0-41ea-a113-41d0de131f97") });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "SlotId", "DateEnd", "DateStart", "UserId" },
                values: new object[] { new Guid("ad1afa51-0514-427b-a53a-790b28b0168f"), new DateTime(2021, 7, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("44fe7cef-31d0-41ea-a113-41d0de131f97") });

            migrationBuilder.CreateIndex(
                name: "IX_Slots_UserId",
                table: "Slots",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
