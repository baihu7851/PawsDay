using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class updateblocktoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockToken",
                columns: table => new
                {
                    BlockTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, comment: "有效時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockToken", x => x.BlockTokenId);
                });

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 616, DateTimeKind.Local).AddTicks(6273));

            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "CartID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 616, DateTimeKind.Local).AddTicks(6718));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 611, DateTimeKind.Local).AddTicks(1186));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 611, DateTimeKind.Local).AddTicks(8604));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 611, DateTimeKind.Local).AddTicks(8626));

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 611, DateTimeKind.Local).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 612, DateTimeKind.Local).AddTicks(3157));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 2,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 612, DateTimeKind.Local).AddTicks(3331));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 3,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 612, DateTimeKind.Local).AddTicks(3334));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 4,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 612, DateTimeKind.Local).AddTicks(3336));

            migrationBuilder.UpdateData(
                table: "PetInfomation",
                keyColumn: "PetID",
                keyValue: 5,
                column: "CreateTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 612, DateTimeKind.Local).AddTicks(3337));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(263), new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(424) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(700), new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(702) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(705), new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(706) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(708), new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(709) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(711), new DateTime(2022, 10, 17, 21, 51, 35, 614, DateTimeKind.Local).AddTicks(712) });

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 1,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 613, DateTimeKind.Local).AddTicks(1682));

            migrationBuilder.UpdateData(
                table: "RegisterSitter",
                keyColumn: "SitterID",
                keyValue: 2,
                column: "VerifyTime",
                value: new DateTime(2022, 10, 17, 21, 51, 35, 613, DateTimeKind.Local).AddTicks(2755));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockToken");

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
    }
}
