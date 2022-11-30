using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class updateorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "Order",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "訂單號碼(顯示用)",
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20,
                oldComment: "訂單號碼(顯示用)");

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 465, DateTimeKind.Local).AddTicks(3779));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 465, DateTimeKind.Local).AddTicks(4204));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 459, DateTimeKind.Local).AddTicks(7751));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 460, DateTimeKind.Local).AddTicks(5061));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 460, DateTimeKind.Local).AddTicks(5081));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 460, DateTimeKind.Local).AddTicks(5085));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 460, DateTimeKind.Local).AddTicks(9549));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 460, DateTimeKind.Local).AddTicks(9731));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 460, DateTimeKind.Local).AddTicks(9735));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 460, DateTimeKind.Local).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 460, DateTimeKind.Local).AddTicks(9738));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7145), new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7316) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7600), new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7602) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7604), new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7605) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7607), new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7608) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7610), new DateTime(2022, 10, 9, 22, 18, 45, 462, DateTimeKind.Local).AddTicks(7611) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 461, DateTimeKind.Local).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 9, 22, 18, 45, 461, DateTimeKind.Local).AddTicks(9505));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "Order",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: false,
                comment: "訂單號碼(顯示用)",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "訂單號碼(顯示用)");

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
    }
}
