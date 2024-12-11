using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShiftsLogger.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedShiftModelToIncludeHoursWorkedColumnInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("2c1733c1-589c-4fd2-978e-46a8695c37ea"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("64bc7c73-07df-418c-a92b-60751c1b37c4"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("80655015-d164-4133-b8a0-af5e8851f210"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("82796461-6ede-4f6d-a988-9a0f7f35ca3d"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("8e321961-7dff-4ac6-94a1-da0e82562dca"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("90286d40-395f-43a2-8212-109905a746ac"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("973e71ef-e48d-44fc-b990-5ac61ae69cbe"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("9bab4782-a30a-4438-8ccb-b4a4760aeefe"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("aec53468-e3fb-41ff-a6b8-78d24ecbad84"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("b789f457-e47d-482d-b5ea-4a22e0ed9727"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("c99791da-0e1a-441f-9f75-1919c1d23cb8"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("da160e9a-de5f-4c8d-96d9-b2eee5f32d97"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("e2e73feb-4dbf-4e70-9977-82353927f84e"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("e7d814d7-caa4-45db-aeca-4ddd37bbb38b"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("f1ef0cdf-4e06-4871-8cd9-8319e44895f4"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("f760b149-30b7-4ecc-9320-910cd129286f"));

            migrationBuilder.AddColumn<decimal>(
                name: "HoursWorked",
                table: "Shifts",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "DATEDIFF(MINUTE, StartTime, EndTime) / 60.00",
                stored: true);

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "ShiftId", "Description", "EndTime", "LocationId", "ShiftTypeId", "StartTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("0201e763-504b-4762-95a7-2336b1e12070"), "Worked a shift on 2024-06-03", new DateTime(2024, 6, 3, 14, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("0a205db4-da35-40d2-9056-901af675d8b5"), "Worked a shift on 2024-08-03", new DateTime(2024, 8, 3, 15, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("128ce800-27d9-43ca-a88d-2b2eab8dfa0a"), "Worked a shift on 2024-07-24", new DateTime(2024, 7, 24, 6, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("193f8e8c-7a10-418e-85e5-90655409f6ac"), "Worked a shift on 2024-07-03", new DateTime(2024, 7, 3, 14, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("3c2256b5-ef40-4794-a9a2-9787fd1328c3"), "Worked a shift on 2024-06-13", new DateTime(2024, 6, 13, 6, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("5c124381-8d88-440d-b632-11fb473858a5"), "Worked a shift on 2024-08-02", new DateTime(2024, 8, 2, 12, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("6e45dacf-bbc8-4194-8de1-c36f058c35e4"), "Worked a shift on 2024-06-24", new DateTime(2024, 6, 24, 9, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 6, 24, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("83f55b2f-11d2-438c-a520-e0759ad633c6"), "Worked a shift on 2024-07-04", new DateTime(2024, 7, 4, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("907769ad-4421-446a-a0cc-3c47a935ee44"), "Worked a shift on 2024-07-14", new DateTime(2024, 7, 14, 8, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("974244f9-657c-4ed4-b9b9-acf4141365c9"), "Worked a shift on 2024-05-24", new DateTime(2024, 5, 24, 7, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("9d1636e6-fae1-4415-a1e4-3ee0ca0ce1a3"), "Worked a shift on 2024-05-25", new DateTime(2024, 5, 25, 1, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("a8639cf9-b64c-4f52-a223-89830a6453ef"), "Worked a shift on 2024-06-23", new DateTime(2024, 6, 23, 2, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("bd54b1d1-f3bc-4169-a47f-4ddb0846db74"), "Worked a shift on 2024-06-04", new DateTime(2024, 6, 4, 14, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") },
                    { new Guid("ed23c7ce-cda6-4539-ba0b-05e283fcca8a"), "Worked a shift on 2024-07-23", new DateTime(2024, 7, 23, 10, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 7, 23, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("f84f3dcf-b652-44e9-adc4-7b494de151ae"), "Worked a shift on 2024-07-13", new DateTime(2024, 7, 13, 13, 0, 0, 0, DateTimeKind.Local), new Guid("d7367ce4-91aa-483c-ba09-89ca48d97259"), new Guid("9d670142-f75f-40db-9f96-2e7913b8ed05"), new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Local), new Guid("cbe1f697-611c-431d-8503-e62d78615184") },
                    { new Guid("fda66f00-45ad-4e24-b8b6-75b19f26413c"), "Worked a shift on 2024-06-14", new DateTime(2024, 6, 14, 8, 0, 0, 0, DateTimeKind.Local), new Guid("4524ce96-d845-4f38-8699-4d5472285bc2"), new Guid("4b4f9afe-b143-48be-8c1f-b72b2fddbdab"), new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Local), new Guid("487081eb-ce7a-45a9-8112-b0a563310e9d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("0201e763-504b-4762-95a7-2336b1e12070"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("0a205db4-da35-40d2-9056-901af675d8b5"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("128ce800-27d9-43ca-a88d-2b2eab8dfa0a"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("193f8e8c-7a10-418e-85e5-90655409f6ac"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("3c2256b5-ef40-4794-a9a2-9787fd1328c3"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("5c124381-8d88-440d-b632-11fb473858a5"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("6e45dacf-bbc8-4194-8de1-c36f058c35e4"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("83f55b2f-11d2-438c-a520-e0759ad633c6"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("907769ad-4421-446a-a0cc-3c47a935ee44"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("974244f9-657c-4ed4-b9b9-acf4141365c9"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("9d1636e6-fae1-4415-a1e4-3ee0ca0ce1a3"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("a8639cf9-b64c-4f52-a223-89830a6453ef"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("bd54b1d1-f3bc-4169-a47f-4ddb0846db74"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("ed23c7ce-cda6-4539-ba0b-05e283fcca8a"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("f84f3dcf-b652-44e9-adc4-7b494de151ae"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("fda66f00-45ad-4e24-b8b6-75b19f26413c"));

            migrationBuilder.DropColumn(
                name: "HoursWorked",
                table: "Shifts");

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
        }
    }
}
