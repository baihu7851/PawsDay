using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class updatelinehistorytype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LineBotHistory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 493, DateTimeKind.Local).AddTicks(6324));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 493, DateTimeKind.Local).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 484, DateTimeKind.Local).AddTicks(2714));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 485, DateTimeKind.Local).AddTicks(3957));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 485, DateTimeKind.Local).AddTicks(3989));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 485, DateTimeKind.Local).AddTicks(3997));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 486, DateTimeKind.Local).AddTicks(1612));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 486, DateTimeKind.Local).AddTicks(1905));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 486, DateTimeKind.Local).AddTicks(1910));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 486, DateTimeKind.Local).AddTicks(1913));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 486, DateTimeKind.Local).AddTicks(1915));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1188), new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1467) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1957), new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1961) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1966), new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1967) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1971), new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1972) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1976), new DateTime(2022, 10, 24, 16, 37, 53, 489, DateTimeKind.Local).AddTicks(1977) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 487, DateTimeKind.Local).AddTicks(5987));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 24, 16, 37, 53, 487, DateTimeKind.Local).AddTicks(8114));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LineBotHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
