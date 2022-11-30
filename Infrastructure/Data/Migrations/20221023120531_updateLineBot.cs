using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class updateLineBot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineBotKeyWord",
                columns: table => new
                {
                    KeyWordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanBeEdit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineBotKeyWord", x => x.KeyWordId);
                });

            migrationBuilder.CreateTable(
                name: "LineBotTemplate",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineBotTemplate", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "LineBotTemplateDetail",
                columns: table => new
                {
                    TemplateDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: false, comment: "模板編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineBotTemplateDetail", x => x.TemplateDetailId);
                    table.ForeignKey(
                        name: "FK_LineBotTemplateDetail_LineBotTemplate",
                        column: x => x.TemplateId,
                        principalTable: "LineBotTemplate",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LineBotTemplateDetail_TemplateId",
                table: "LineBotTemplateDetail",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineBotKeyWord");

            migrationBuilder.DropTable(
                name: "LineBotTemplateDetail");

            migrationBuilder.DropTable(
                name: "LineBotTemplate");

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
    }
}
