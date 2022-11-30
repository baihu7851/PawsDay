using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Sex",
                table: "Member",
                type: "bit",
                nullable: true,
                comment: "性別",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "性別");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Member",
                type: "char(10)",
                unicode: false,
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                comment: "電話",
                oldClrType: typeof(string),
                oldType: "char(10)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 10,
                oldComment: "電話");

            migrationBuilder.AlterColumn<int>(
                name: "District",
                table: "Member",
                type: "int",
                nullable: true,
                comment: "區域",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "區域");

            migrationBuilder.AlterColumn<int>(
                name: "County",
                table: "Member",
                type: "int",
                nullable: true,
                comment: "縣市",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "縣市");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birth",
                table: "Member",
                type: "date",
                nullable: true,
                comment: "出生日期",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldComment: "出生日期");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Member",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "詳細地址",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "詳細地址");

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 510, DateTimeKind.Local).AddTicks(6586));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 510, DateTimeKind.Local).AddTicks(7158));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 502, DateTimeKind.Local).AddTicks(7479));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 503, DateTimeKind.Local).AddTicks(5265));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 503, DateTimeKind.Local).AddTicks(5286));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 503, DateTimeKind.Local).AddTicks(5291));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 504, DateTimeKind.Local).AddTicks(129));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 504, DateTimeKind.Local).AddTicks(319));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 504, DateTimeKind.Local).AddTicks(323));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 504, DateTimeKind.Local).AddTicks(353));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 504, DateTimeKind.Local).AddTicks(355));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 12, 14, 9, 506, DateTimeKind.Local).AddTicks(9506), new DateTime(2022, 10, 9, 12, 14, 9, 506, DateTimeKind.Local).AddTicks(9862) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 12, 14, 9, 507, DateTimeKind.Local).AddTicks(511), new DateTime(2022, 10, 9, 12, 14, 9, 507, DateTimeKind.Local).AddTicks(515) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 12, 14, 9, 507, DateTimeKind.Local).AddTicks(519), new DateTime(2022, 10, 9, 12, 14, 9, 507, DateTimeKind.Local).AddTicks(520) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 12, 14, 9, 507, DateTimeKind.Local).AddTicks(523), new DateTime(2022, 10, 9, 12, 14, 9, 507, DateTimeKind.Local).AddTicks(524) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 12, 14, 9, 507, DateTimeKind.Local).AddTicks(527), new DateTime(2022, 10, 9, 12, 14, 9, 507, DateTimeKind.Local).AddTicks(528) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 505, DateTimeKind.Local).AddTicks(5858));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 9, 12, 14, 9, 505, DateTimeKind.Local).AddTicks(7559));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Sex",
                table: "Member",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "性別",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "性別");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Member",
                type: "char(10)",
                unicode: false,
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                comment: "電話",
                oldClrType: typeof(string),
                oldType: "char(10)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "電話");

            migrationBuilder.AlterColumn<int>(
                name: "District",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "區域",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "區域");

            migrationBuilder.AlterColumn<int>(
                name: "County",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "縣市",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "縣市");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birth",
                table: "Member",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "出生日期",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true,
                oldComment: "出生日期");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Member",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "詳細地址",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "詳細地址");

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 317, DateTimeKind.Local).AddTicks(5395));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 317, DateTimeKind.Local).AddTicks(5803));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 311, DateTimeKind.Local).AddTicks(8251));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 312, DateTimeKind.Local).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 312, DateTimeKind.Local).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 312, DateTimeKind.Local).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(92));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(277));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(280));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(282));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(283));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9031), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9196) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9478), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9483), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9483) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9485), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9486) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9488), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9489) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(8477));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(9807));
        }
    }
}
