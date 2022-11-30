using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class updatetemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 332, DateTimeKind.Local).AddTicks(4530));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 332, DateTimeKind.Local).AddTicks(6193));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 317, DateTimeKind.Local).AddTicks(3936));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 319, DateTimeKind.Local).AddTicks(4055));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 319, DateTimeKind.Local).AddTicks(4172));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 319, DateTimeKind.Local).AddTicks(4182));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 320, DateTimeKind.Local).AddTicks(6762));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 320, DateTimeKind.Local).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 320, DateTimeKind.Local).AddTicks(7253));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 320, DateTimeKind.Local).AddTicks(7257));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 320, DateTimeKind.Local).AddTicks(7261));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(6755), new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(7337) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(8069), new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(8075) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(8082), new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(8085) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(8092), new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(8094) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(8101), new DateTime(2022, 10, 23, 20, 10, 34, 325, DateTimeKind.Local).AddTicks(8103) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 323, DateTimeKind.Local).AddTicks(2537));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 23, 20, 10, 34, 323, DateTimeKind.Local).AddTicks(5405));
        }
    }
}
