using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShiftsLogger.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftTypes",
                columns: table => new
                {
                    ShiftTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftTypes", x => x.ShiftTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shifts_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shifts_ShiftTypes_ShiftTypeId",
                        column: x => x.ShiftTypeId,
                        principalTable: "ShiftTypes",
                        principalColumn: "ShiftTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shifts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Address", "Name" },
                values: new object[,]
                {
                    { new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), "666 Sleepy Str. SW, Edmonton, AB, Canada", "Home" },
                    { new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), "777 Lucky Str. SW, Edmonton, AB, Canada", "Office" }
                });

            migrationBuilder.InsertData(
                table: "ShiftTypes",
                columns: new[] { "ShiftTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), "Morning Shift" },
                    { new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), "Evening Shift" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Role" },
                values: new object[,]
                {
                    { new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d"), "johndoe@example.com", "John", "Doe", "Designer" },
                    { new Guid("cbe1f697-611c-431d-8503-e62d78615184"), "janesmith@example.com", "Jane", "Smith", "Software Developer" }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "ShiftId", "Description", "EndTime", "LocationId", "ShiftTypeId", "StartTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("2c1733c1-589c-4fd2-978e-46a8695c37ea"), "Worked a shift on 2024-05-22", new DateTime(2024, 5, 22, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("64bc7c73-07df-418c-a92b-60751c1b37c4"), "Worked a shift on 2024-06-02", new DateTime(2024, 6, 2, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("80655015-d164-4133-b8a0-af5e8851f210"), "Worked a shift on 2024-06-11", new DateTime(2024, 6, 11, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("82796461-6ede-4f6d-a988-9a0f7f35ca3d"), "Worked a shift on 2024-07-01", new DateTime(2024, 7, 1, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("8e321961-7dff-4ac6-94a1-da0e82562dca"), "Worked a shift on 2024-07-31", new DateTime(2024, 7, 31, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 7, 31, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("90286d40-395f-43a2-8212-109905a746ac"), "Worked a shift on 2024-07-11", new DateTime(2024, 7, 11, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("973e71ef-e48d-44fc-b990-5ac61ae69cbe"), "Worked a shift on 2024-08-01", new DateTime(2024, 8, 1, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("9bab4782-a30a-4438-8ccb-b4a4760aeefe"), "Worked a shift on 2024-06-22", new DateTime(2024, 6, 22, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("aec53468-e3fb-41ff-a6b8-78d24ecbad84"), "Worked a shift on 2024-07-12", new DateTime(2024, 7, 12, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("b789f457-e47d-482d-b5ea-4a22e0ed9727"), "Worked a shift on 2024-07-02", new DateTime(2024, 7, 2, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("c99791da-0e1a-441f-9f75-1919c1d23cb8"), "Worked a shift on 2024-07-22", new DateTime(2024, 7, 22, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("da160e9a-de5f-4c8d-96d9-b2eee5f32d97"), "Worked a shift on 2024-07-21", new DateTime(2024, 7, 21, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 7, 21, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("e2e73feb-4dbf-4e70-9977-82353927f84e"), "Worked a shift on 2024-06-01", new DateTime(2024, 6, 1, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("e7d814d7-caa4-45db-aeca-4ddd37bbb38b"), "Worked a shift on 2024-05-23", new DateTime(2024, 5, 23, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("f1ef0cdf-4e06-4871-8cd9-8319e44895f4"), "Worked a shift on 2024-06-21", new DateTime(2024, 6, 21, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("f760b149-30b7-4ecc-9320-910cd129286f"), "Worked a shift on 2024-06-12", new DateTime(2024, 6, 12, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_LocationId",
                table: "Shifts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ShiftTypeId",
                table: "Shifts",
                column: "ShiftTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "ShiftTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
