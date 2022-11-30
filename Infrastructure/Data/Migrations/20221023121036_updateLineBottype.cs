using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class updateLineBottype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "LineBotTemplateDetail",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "LineBotTemplateDetail",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LineBotTemplate",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "LineBotTemplate",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "LineBotTemplate",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "KeyWord",
                table: "LineBotKeyWord",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "LineBotKeyWord",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "LineBotTemplateDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "LineBotTemplateDetail",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LineBotTemplate",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "LineBotTemplate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "LineBotTemplate",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "KeyWord",
                table: "LineBotKeyWord",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "LineBotKeyWord",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 115, DateTimeKind.Local).AddTicks(7423));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 115, DateTimeKind.Local).AddTicks(8632));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 99, DateTimeKind.Local).AddTicks(4110));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 101, DateTimeKind.Local).AddTicks(3138));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 101, DateTimeKind.Local).AddTicks(3187));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 101, DateTimeKind.Local).AddTicks(3310));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 102, DateTimeKind.Local).AddTicks(6954));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 102, DateTimeKind.Local).AddTicks(7539));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 102, DateTimeKind.Local).AddTicks(7549));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 102, DateTimeKind.Local).AddTicks(7554));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 102, DateTimeKind.Local).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(625), new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1132) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1945), new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1951) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1958), new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1961) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1967), new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1969) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1975), new DateTime(2022, 10, 23, 20, 5, 29, 108, DateTimeKind.Local).AddTicks(1978) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 105, DateTimeKind.Local).AddTicks(3658));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 23, 20, 5, 29, 105, DateTimeKind.Local).AddTicks(6655));
        }
    }
}
