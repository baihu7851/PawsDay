using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class updatelinehistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineBotHistory",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineBotHistory", x => x.HistoryId);
                });

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 306, DateTimeKind.Local).AddTicks(4608));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 306, DateTimeKind.Local).AddTicks(5145));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 299, DateTimeKind.Local).AddTicks(652));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 300, DateTimeKind.Local).AddTicks(6847));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 300, DateTimeKind.Local).AddTicks(6871));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 300, DateTimeKind.Local).AddTicks(6876));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 301, DateTimeKind.Local).AddTicks(2681));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 301, DateTimeKind.Local).AddTicks(2887));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 301, DateTimeKind.Local).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 301, DateTimeKind.Local).AddTicks(2892));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 301, DateTimeKind.Local).AddTicks(2894));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(2491), new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(2673) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(2993), new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(2995) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(3052), new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(3053) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(3055), new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(3056) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(3059), new DateTime(2022, 10, 24, 16, 35, 27, 303, DateTimeKind.Local).AddTicks(3059) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 302, DateTimeKind.Local).AddTicks(2491));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 24, 16, 35, 27, 302, DateTimeKind.Local).AddTicks(3803));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineBotHistory");

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 244, DateTimeKind.Local).AddTicks(2946));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 244, DateTimeKind.Local).AddTicks(3450));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 237, DateTimeKind.Local).AddTicks(2875));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 238, DateTimeKind.Local).AddTicks(1471));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 238, DateTimeKind.Local).AddTicks(1492));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 238, DateTimeKind.Local).AddTicks(1496));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 238, DateTimeKind.Local).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 238, DateTimeKind.Local).AddTicks(6331));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 238, DateTimeKind.Local).AddTicks(6333));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 238, DateTimeKind.Local).AddTicks(6335));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 238, DateTimeKind.Local).AddTicks(6336));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4201), new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4426) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4760), new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4762) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4765), new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4765) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4768), new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4769) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4771), new DateTime(2022, 10, 24, 2, 29, 55, 241, DateTimeKind.Local).AddTicks(4772) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 239, DateTimeKind.Local).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 24, 2, 29, 55, 240, DateTimeKind.Local).AddTicks(864));
        }
    }
}
