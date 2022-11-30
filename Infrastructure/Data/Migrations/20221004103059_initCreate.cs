using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class initCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountInfo",
                columns: table => new
                {
                    AccountInfoID = table.Column<int>(type: "int", nullable: false, comment: "帳號編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "帳號"),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "密碼"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "建立時間"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "有效時間"),
                    Verify = table.Column<bool>(type: "bit", nullable: false, comment: "驗證是否通過")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.AccountInfoID);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Phone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ContactContent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ReplyContent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReplyTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                });

            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    CountyID = table.Column<int>(type: "int", nullable: false, comment: "縣市編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "縣市名")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.CountyID);
                });

            migrationBuilder.CreateTable(
                name: "Disposition",
                columns: table => new
                {
                    DispositionID = table.Column<int>(type: "int", nullable: false, comment: "性格編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disposition = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "性格描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disposition", x => x.DispositionID);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictID = table.Column<int>(type: "int", nullable: false, comment: "地區編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "地區名"),
                    CountyID = table.Column<int>(type: "int", nullable: false, comment: "縣市編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceType",
                columns: table => new
                {
                    InvoiceTypeID = table.Column<int>(type: "int", nullable: false, comment: "發票類別編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "發票類別")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceType", x => x.InvoiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "RegisterType",
                columns: table => new
                {
                    RegisterID = table.Column<int>(type: "int", nullable: false, comment: "註冊方式編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "註冊方式")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterTypes", x => x.RegisterID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false, comment: "角色編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "角色名稱")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false, comment: "時程別編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeDesrcipt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "時程細節說明"),
                    PartTimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    ServiceTypeID = table.Column<int>(type: "int", nullable: false, comment: "服務類型編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "服務類型"),
                    ServiceIntro = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "服務說明")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.ServiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號(平台唯一會員編號)")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterType = table.Column<int>(type: "int", nullable: false, comment: "註冊方式"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "姓名"),
                    NickName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "暱稱"),
                    Sex = table.Column<bool>(type: "bit", nullable: false, comment: "性別"),
                    Birth = table.Column<DateTime>(type: "date", nullable: false, comment: "出生日期"),
                    County = table.Column<int>(type: "int", nullable: false, comment: "縣市"),
                    District = table.Column<int>(type: "int", nullable: false, comment: "區域"),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "詳細地址"),
                    Phone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false, comment: "電話"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "信箱"),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "照片"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "會員狀態"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "是否刪除"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "資料成立時間"),
                    EditTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "資料編輯時間"),
                    AccountInfoId = table.Column<int>(type: "int", nullable: true, comment: "帳號密碼資訊")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_Member_AccountInfo",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfo",
                        principalColumn: "AccountInfoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Member_County",
                        column: x => x.County,
                        principalTable: "County",
                        principalColumn: "CountyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Member_District",
                        column: x => x.District,
                        principalTable: "District",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Member_RegisterType",
                        column: x => x.RegisterType,
                        principalTable: "RegisterType",
                        principalColumn: "RegisterID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    AnswerID = table.Column<int>(type: "int", nullable: false, comment: "答案紀錄編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號"),
                    QuestionId = table.Column<int>(type: "int", nullable: false, comment: "題目編號"),
                    AnswerInput = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "答案")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK_Answer_MemberId",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aptitude",
                columns: table => new
                {
                    AptitudeID = table.Column<int>(type: "int", nullable: false, comment: "測驗編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號"),
                    AptitudeExtrovert = table.Column<int>(type: "int", nullable: false, comment: "保姆人格測驗項度E"),
                    AptitudeIntrovert = table.Column<int>(type: "int", nullable: false, comment: "保姆人格測驗項度I"),
                    AptitudeThinking = table.Column<int>(type: "int", nullable: false, comment: "保姆人格測驗項度T"),
                    AptitudeFeeling = table.Column<int>(type: "int", nullable: false, comment: "保姆人格測驗項度F")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aptitude", x => x.AptitudeID);
                    table.ForeignKey(
                        name: "FK_Aptitude_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacebookRegister",
                columns: table => new
                {
                    FBRegisterID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "FB登入序號"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookRegister", x => x.FBRegisterID);
                    table.ForeignKey(
                        name: "FK_FacebookRegister_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoogleRegister",
                columns: table => new
                {
                    GoogleRegisterID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Google登入序號"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleRegister", x => x.GoogleRegisterID);
                    table.ForeignKey(
                        name: "FK_GoogleRegister_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    LicenseID = table.Column<int>(type: "int", nullable: false, comment: "證照編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "證照連結"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.LicenseID);
                    table.ForeignKey(
                        name: "FK_License_MemberId",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineRegister",
                columns: table => new
                {
                    LineRegisterID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Line登入序號"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineRegister", x => x.LineRegisterID);
                    table.ForeignKey(
                        name: "FK_LineRegister_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false, comment: "訂單編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false, comment: "訂單號碼(顯示用)"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號(已鎖死不會被改變)"),
                    SitterID = table.Column<int>(type: "int", nullable: false, comment: "保姆會員編號"),
                    CustomerID = table.Column<int>(type: "int", nullable: false, comment: "顧客會員編號"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "訂單建立時間"),
                    OrderStatus = table.Column<int>(type: "int", nullable: false, comment: "訂單狀態"),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "訂單總額"),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "折扣優惠"),
                    InvoiceType = table.Column<int>(type: "int", nullable: false, comment: "發票開立方式"),
                    InvoiceID = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true, comment: "發票號碼"),
                    UnoformNumber = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true, comment: "統一編號"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "公司抬頭"),
                    BookingName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "訂購人姓名"),
                    BookingPhone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false, comment: "訂購人電話"),
                    BookingEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "訂購人信箱"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "服務詳細地址"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "托育聯絡人姓名"),
                    Phone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false, comment: "托育聯絡人電話"),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "服務起始時間"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "服務結束時間"),
                    SitterName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "保姆名字(已鎖死不會被改變)"),
                    ProductName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "服務類型(已鎖死不會被改變)"),
                    ProductIntro = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "商品說明(已鎖死不會被改變)"),
                    ProductImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "商品照片(已鎖死不會被改變)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_InvoiceType",
                        column: x => x.InvoiceType,
                        principalTable: "InvoiceType",
                        principalColumn: "InvoiceTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Member",
                        column: x => x.SitterID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Member1",
                        column: x => x.CustomerID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PetInfomation",
                columns: table => new
                {
                    PetID = table.Column<int>(type: "int", nullable: false, comment: "寵物編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號"),
                    PetName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "寵物名字"),
                    PetType = table.Column<int>(type: "int", nullable: false, comment: "寵物類型"),
                    ShapeType = table.Column<int>(type: "int", nullable: false, comment: "體型"),
                    PetSex = table.Column<bool>(type: "bit", nullable: false, comment: "寵物性別"),
                    BirthYear = table.Column<int>(type: "int", nullable: false, comment: "出生年"),
                    BirthMonth = table.Column<int>(type: "int", nullable: true, comment: "出生月"),
                    Ligation = table.Column<bool>(type: "bit", nullable: false, comment: "是否結紮"),
                    Vaccine = table.Column<bool>(type: "bit", nullable: false, comment: "是否定期打預防針"),
                    Intro = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "補充介紹"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "資料建立時間"),
                    EditTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "資料編輯時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetInfomations", x => x.PetID);
                    table.ForeignKey(
                        name: "FK_PetInfomation_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceType = table.Column<int>(type: "int", nullable: false, comment: "服務類型"),
                    Introduce = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "商品描述"),
                    SitterID = table.Column<int>(type: "int", nullable: false, comment: "保姆編號"),
                    ProductStatus = table.Column<int>(type: "int", nullable: false, comment: "商品上架狀態"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "建立時間"),
                    EditTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "編輯時間"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "是否刪除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_Member",
                        column: x => x.SitterID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_ServiceType",
                        column: x => x.ServiceType,
                        principalTable: "ServiceType",
                        principalColumn: "ServiceTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisterSitter",
                columns: table => new
                {
                    SitterID = table.Column<int>(type: "int", nullable: false, comment: "保姆編號(不對外公開)")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號"),
                    ID = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false, comment: "身分證編號"),
                    IDImagefont = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "身分證正面"),
                    IDImageback = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "身分證反面"),
                    PetExperience = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "寵物經驗"),
                    Score = table.Column<int>(type: "int", nullable: false, comment: "測驗總分"),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false, comment: "保姆審核狀態"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "送審時間"),
                    VerifyTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "審核時間"),
                    SitterName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "保姆名字(預設會員資料)"),
                    SitterPicture = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "保姆照片(預設會員資料)"),
                    SitterInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "保姆介紹"),
                    Bank = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true, comment: "收款銀行"),
                    Account = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true, comment: "收款帳號"),
                    EditTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "編輯時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterSitters", x => x.SitterID);
                    table.ForeignKey(
                        name: "FK_RegisterSitter_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserContact",
                columns: table => new
                {
                    UserContactID = table.Column<int>(type: "int", nullable: false, comment: "對話編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "顧客ID"),
                    SitterID = table.Column<int>(type: "int", nullable: false, comment: "保姆ID"),
                    Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "對話內容"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "發話時間"),
                    IsMemberSpeak = table.Column<bool>(type: "bit", nullable: false, comment: "顧客發話")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContact", x => x.UserContactID);
                    table.ForeignKey(
                        name: "FK_UserContact_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserContact_Member1",
                        column: x => x.SitterID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false, comment: "使用者角色編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "使用者Id"),
                    RoleType = table.Column<int>(type: "int", nullable: false, comment: "角色Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Member",
                        column: x => x.UserId,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Role",
                        column: x => x.RoleType,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evaluation",
                columns: table => new
                {
                    EvaluationID = table.Column<int>(type: "int", nullable: false, comment: "評論編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false, comment: "對應訂單"),
                    UserID = table.Column<int>(type: "int", nullable: false, comment: "留言者"),
                    UserType = table.Column<int>(type: "int", nullable: false, comment: "留言身分"),
                    Evaluation = table.Column<int>(type: "int", nullable: false, comment: "評價"),
                    Message = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "評論"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "留言時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluation", x => x.EvaluationID);
                    table.ForeignKey(
                        name: "FK_Evaluation_Member",
                        column: x => x.UserID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluation_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficialContact",
                columns: table => new
                {
                    OfficialContactID = table.Column<int>(type: "int", nullable: false, comment: "對話編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true, comment: "訂單編號"),
                    UserType = table.Column<int>(type: "int", nullable: false, comment: "使用者種類"),
                    UserID = table.Column<int>(type: "int", nullable: false, comment: "使用者ID"),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "對話內容"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "發話時間"),
                    IsUserSpeak = table.Column<bool>(type: "bit", nullable: false, comment: "使用者發話")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialContact", x => x.OfficialContactID);
                    table.ForeignKey(
                        name: "FK_OfficialContact_Member",
                        column: x => x.UserID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfficialContact_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderCancel",
                columns: table => new
                {
                    OrderCancelID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false, comment: "訂單編號"),
                    CancelDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "取消日期時間"),
                    CancelReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "取消原因"),
                    RefundPersent = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "退還總價百分比")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCancel", x => x.OrderCancelID);
                    table.ForeignKey(
                        name: "FK_OrderCancel_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderPetDetail",
                columns: table => new
                {
                    OrderPetID = table.Column<int>(type: "int", nullable: false, comment: "訂單寵物編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "寵物名字"),
                    PetType = table.Column<int>(type: "int", nullable: false, comment: "寵物類型"),
                    ShapeType = table.Column<int>(type: "int", nullable: false, comment: "體型"),
                    PetSex = table.Column<bool>(type: "bit", nullable: false, comment: "寵物性別"),
                    BirthYear = table.Column<int>(type: "int", nullable: false, comment: "出生年"),
                    BirthMonth = table.Column<int>(type: "int", nullable: true, comment: "出生月"),
                    Ligation = table.Column<bool>(type: "bit", nullable: false, comment: "是否結紮"),
                    Vaccine = table.Column<bool>(type: "bit", nullable: false, comment: "是否定期打預防針"),
                    OrderID = table.Column<int>(type: "int", nullable: false, comment: "訂單編號"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "單價"),
                    ServiceCount = table.Column<int>(type: "int", nullable: false, comment: "服務計次"),
                    PetDiscription = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "個性敘述"),
                    PetIntro = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "補充敘述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPetDetails", x => x.OrderPetID);
                    table.ForeignKey(
                        name: "FK_OrderPetDetail_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderSchedule",
                columns: table => new
                {
                    OrderScheduleID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false, comment: "訂單編號"),
                    ServiceDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "服務日期"),
                    Schedule = table.Column<int>(type: "int", nullable: false, comment: "服務詳細時程")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSchedule", x => x.OrderScheduleID);
                    table.ForeignKey(
                        name: "FK_OrderSchedule_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSchedule_Schedule",
                        column: x => x.Schedule,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PetDisposition",
                columns: table => new
                {
                    PetDispositionID = table.Column<int>(type: "int", nullable: false, comment: "寵物性格編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DispositionType = table.Column<int>(type: "int", nullable: false, comment: "性格描述"),
                    PetID = table.Column<int>(type: "int", nullable: false, comment: "寵物編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetDisposition", x => x.PetDispositionID);
                    table.ForeignKey(
                        name: "FK_PetDisposition_Disposition",
                        column: x => x.DispositionType,
                        principalTable: "Disposition",
                        principalColumn: "DispositionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PetDisposition_PetInfomation",
                        column: x => x.PetID,
                        principalTable: "PetInfomation",
                        principalColumn: "PetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdProject",
                columns: table => new
                {
                    AdProjectId = table.Column<int>(type: "int", nullable: false, comment: "廣告編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號"),
                    BeginDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "開始時間"),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "結束時間"),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "價格")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdProject", x => x.AdProjectId);
                    table.ForeignKey(
                        name: "FK_Advertise_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false, comment: "購物車編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號"),
                    CustomerID = table.Column<int>(type: "int", nullable: false, comment: "顧客編號"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "加入時間"),
                    County = table.Column<int>(type: "int", nullable: false, comment: "服務縣市"),
                    District = table.Column<int>(type: "int", nullable: false, comment: "服務地區")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_Cart_County",
                        column: x => x.County,
                        principalTable: "County",
                        principalColumn: "CountyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_District",
                        column: x => x.District,
                        principalTable: "District",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Member",
                        column: x => x.CustomerID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Collect",
                columns: table => new
                {
                    CollectID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false, comment: "會員編號"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "收藏商品編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collect", x => x.CollectID);
                    table.ForeignKey(
                        name: "FK_Collect_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collect_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscount",
                columns: table => new
                {
                    ProductDiscountID = table.Column<int>(type: "int", nullable: false, comment: "商品折扣編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "數量"),
                    Discount = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "設定折扣")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscount", x => x.ProductDiscountID);
                    table.ForeignKey(
                        name: "FK_ProductDiscount_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    ImagesID = table.Column<int>(type: "int", nullable: false, comment: "圖片編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "圖片連結"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "圖片排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ImagesID);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductServiceArea",
                columns: table => new
                {
                    ProductServiceAreaID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號"),
                    County = table.Column<int>(type: "int", nullable: false, comment: "可服務縣市"),
                    District = table.Column<int>(type: "int", nullable: false, comment: "可服務地區")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServiceArea", x => x.ProductServiceAreaID);
                    table.ForeignKey(
                        name: "FK_ProductServiceArea_County",
                        column: x => x.County,
                        principalTable: "County",
                        principalColumn: "CountyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductServiceArea_District",
                        column: x => x.District,
                        principalTable: "District",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductServiceArea_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductServicePetType",
                columns: table => new
                {
                    ProductServicePetTypeID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號"),
                    PetType = table.Column<int>(type: "int", nullable: false, comment: "可服務寵物類型"),
                    ShapeType = table.Column<int>(type: "int", nullable: false, comment: "可服務寵物體型"),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "一般單價"),
                    OvernightPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "夜間單價")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServicePetType", x => x.ProductServicePetTypeID);
                    table.ForeignKey(
                        name: "FK_ProductServicePetType_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductServiceTime",
                columns: table => new
                {
                    ProductServiceTimeID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "商品編號"),
                    ServiceDay = table.Column<int>(type: "int", nullable: false, comment: "可服務星期別"),
                    ServicePartTime = table.Column<int>(type: "int", nullable: false, comment: "可服務時段")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServiceTime", x => x.ProductServiceTimeID);
                    table.ForeignKey(
                        name: "FK_ProductServiceTime_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartDetail",
                columns: table => new
                {
                    CartDetailID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: false, comment: "購物車編號"),
                    PetType = table.Column<int>(type: "int", nullable: false, comment: "寵物類型"),
                    ShapeType = table.Column<int>(type: "int", nullable: false, comment: "體型")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetail", x => x.CartDetailID);
                    table.ForeignKey(
                        name: "FK_CartDetail_Cart",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartSchedule",
                columns: table => new
                {
                    CartScheduleID = table.Column<int>(type: "int", nullable: false, comment: "編號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: false, comment: "購物車編號"),
                    ServiceDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "服務日期"),
                    Schedule = table.Column<int>(type: "int", nullable: false, comment: "服務時程編號")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSchedule", x => x.CartScheduleID);
                    table.ForeignKey(
                        name: "FK_CartSchedule_Cart",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "County",
                columns: new[] { "CountyID", "CountyName" },
                values: new object[,]
                {
                    { 1, "台北市" },
                    { 2, "新北市" }
                });

            migrationBuilder.InsertData(
                table: "Disposition",
                columns: new[] { "DispositionID", "Disposition" },
                values: new object[,]
                {
                    { 9, "暴力" },
                    { 8, "膽小" },
                    { 7, "頑皮" },
                    { 6, "固執" },
                    { 10, "好奇" },
                    { 4, "黏人" },
                    { 3, "內向" },
                    { 2, "友善" },
                    { 1, "活潑" },
                    { 5, "敏感" }
                });

            migrationBuilder.InsertData(
                table: "District",
                columns: new[] { "DistrictID", "CountyID", "DistrictName" },
                values: new object[,]
                {
                    { 1, 1, "萬華區" },
                    { 2, 1, "大安區" },
                    { 3, 2, "板橋區" },
                    { 4, 2, "中和區" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceType",
                columns: new[] { "InvoiceTypeID", "InvoiceType" },
                values: new object[,]
                {
                    { 1, "電子發票" },
                    { 2, "公司發票" }
                });

            migrationBuilder.InsertData(
                table: "RegisterType",
                columns: new[] { "RegisterID", "RegisterType" },
                values: new object[,]
                {
                    { 4, "Facebook" },
                    { 1, "信箱" },
                    { 2, "Line" },
                    { 3, "Google" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 2, "SitterUser" },
                    { 3, "Administrator" },
                    { 1, "NormalUser" }
                });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "ScheduleID", "PartTimeId", "TimeDesrcipt" },
                values: new object[,]
                {
                    { 26, 2, "12:30~12:59" },
                    { 27, 2, "13:00~13:29" },
                    { 28, 2, "13:30~13:59" },
                    { 29, 2, "14:00~14:29" },
                    { 30, 2, "14:30~14:59" },
                    { 31, 2, "15:00~15:29" },
                    { 32, 2, "15:30~15:59" },
                    { 33, 2, "16:00~16:29" },
                    { 34, 2, "16:30~16:59" },
                    { 35, 2, "17:00~17:29" },
                    { 36, 2, "17:30~17:59" },
                    { 37, 3, "18:00~18:29" },
                    { 39, 3, "19:00~19:29" },
                    { 40, 3, "19:30~19:59" },
                    { 41, 3, "20:00~20:29" },
                    { 42, 3, "20:30~20:59" },
                    { 43, 3, "21:00~21:29" }
                });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "ScheduleID", "PartTimeId", "TimeDesrcipt" },
                values: new object[,]
                {
                    { 44, 3, "21:30~21:59" },
                    { 45, 3, "22:00~22:29" },
                    { 46, 3, "22:30~22:59" },
                    { 47, 3, "23:00~23:29" },
                    { 48, 3, "23:30~23:59" },
                    { 38, 3, "18:30~18:59" },
                    { 25, 2, "12:00~12:29" },
                    { 13, 1, "06:00~06:29" },
                    { 23, 1, "11:00~11:29" },
                    { 1, 0, "00:00~00:29" },
                    { 2, 0, "00:30~00:59" },
                    { 3, 0, "01:00~01:29" },
                    { 4, 0, "01:30~01:59" },
                    { 5, 0, "02:00~02:29" },
                    { 6, 0, "02:30~02:59" },
                    { 7, 0, "03:00~03:29" },
                    { 8, 0, "03:30~03:59" },
                    { 9, 0, "04:00~04:29" },
                    { 10, 0, "04:30~04:59" },
                    { 24, 1, "11:30~11:59" },
                    { 11, 0, "05:00~05:29" },
                    { 14, 1, "06:30~06:59" },
                    { 15, 1, "07:00~07:29" },
                    { 16, 1, "07:30~07:59" },
                    { 17, 1, "08:00~08:29" },
                    { 18, 1, "08:30~08:59" },
                    { 19, 1, "09:00~09:29" },
                    { 20, 1, "09:30~09:59" },
                    { 21, 1, "10:00~10:29" },
                    { 22, 1, "10:30~10:59" },
                    { 12, 0, "05:30~05:59" }
                });

            migrationBuilder.InsertData(
                table: "ServiceType",
                columns: new[] { "ServiceTypeID", "ServiceIntro", "ServiceType" },
                values: new object[,]
                {
                    { 2, "＄300元起／1小時起／1隻毛孩\r\n👉 寵物免出門! 寵物美容師攜帶工具到府幫寵物做小美容\r\n👉 小美容包含洗澡、按摩、剪指甲、清耳朵、擠肛門腺、修腳底毛、修屁股毛、含環境清理\r\n👉 服務前美容師會先跟毛孩培養感情、餵零食\r\n👉 若有特殊需求或是疾病毛孩，請先與美容師溝通\r\n👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n👉 全程與保姆維持連線，回報寵物狀況\r\n👉 平台預約全程含青杉保險與品質保障", "到府洗澡" },
                    { 1, "＄200元起／30分鐘\r\n👉 專業實名認證寵物保姆到府照顧寵物\r\n👉 寵物保姆、餵食、環境清潔、陪伴玩耍、回報健康狀況、餵藥等客製服務\r\n👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n👉 全程與保姆維持連線，回報寵物狀況\r\n👉 平台預約全程含青杉保險與品質保障\r\n👉 鑰匙可以溝通警衛代收、信箱傳遞等方式", "到府照顧" },
                    { 3, "＄100元起／30分鐘起\r\n👉 無法掌控回家時間? 保姆可到府帶狗狗出門散步\r\n👉 出門不能帶狗狗進餐廳? 保姆可約地點接狗狗散步\r\n👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n👉 全程與保姆維持連線，回報寵物狀況\r\n👉 平台預約全程含青杉保險與品質保障\r\n👉 鑰匙可以溝通警衛代收、信箱傳遞等方式", "陪伴散步" }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "MemberID", "AccountInfoId", "Address", "Birth", "County", "CreateTime", "District", "EditTime", "Email", "IsDelete", "Name", "NickName", "Phone", "ProfileImage", "RegisterType", "Sex", "Status" },
                values: new object[,]
                {
                    { 1, null, "中華路二段", new DateTime(1996, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2022, 10, 4, 18, 30, 59, 311, DateTimeKind.Local).AddTicks(8251), 1, null, "felix@gmail.com", false, "Felix", null, "0910666777", "https://raw.githubusercontent.com/Ning0809/FileStorage/main/%E4%B8%8D%E4%BA%8C%E9%A6%AC%E9%BC%A0.jpg", 1, true, 1 },
                    { 2, null, "忠孝東路五段", new DateTime(1998, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2022, 10, 4, 18, 30, 59, 312, DateTimeKind.Local).AddTicks(5418), 2, null, "anna@gmail.com", false, "Anna", null, "0955666321", "https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg", 1, false, 1 },
                    { 3, null, "民族路一段", new DateTime(1992, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2022, 10, 4, 18, 30, 59, 312, DateTimeKind.Local).AddTicks(5435), 1, null, "jake@gmail.com", false, "Jake", null, "0910555444", "https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg", 1, true, 1 },
                    { 4, null, "德光路一段", new DateTime(2000, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2022, 10, 4, 18, 30, 59, 312, DateTimeKind.Local).AddTicks(5439), 2, null, "wendy@gmail.com", false, "Wendy", null, "0910888999", "https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg", 1, false, 1 }
                });

            migrationBuilder.InsertData(
                table: "Aptitude",
                columns: new[] { "AptitudeID", "AptitudeExtrovert", "AptitudeFeeling", "AptitudeIntrovert", "AptitudeThinking", "MemberID" },
                values: new object[,]
                {
                    { 1, 60, 20, 70, 50, 3 },
                    { 2, 80, 90, 40, 60, 4 }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderID", "Address", "Amount", "BeginTime", "BookingEmail", "BookingName", "BookingPhone", "CompanyName", "CreateTime", "CustomerID", "Discount", "EndTime", "InvoiceID", "InvoiceType", "Name", "OrderNumber", "OrderStatus", "Phone", "ProductID", "ProductImageUrl", "ProductIntro", "ProductName", "SitterID", "SitterName", "UnoformNumber" },
                values: new object[,]
                {
                    { 1, "中華路二段", 600m, new DateTime(2022, 9, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), "felix@gmail.com", "Felix", "0910555666", null, new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0m, new DateTime(2022, 9, 20, 6, 59, 0, 0, DateTimeKind.Unspecified), "AB11111111", 1, "Felix", "KS0001", 0, "0910555666", 1, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", "到府照顧", 3, "Jake", null },
                    { 4, "中華路二段", 600m, new DateTime(2022, 10, 14, 6, 0, 0, 0, DateTimeKind.Unspecified), "felix@gmail.com", "Felix", "0910555666", null, new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0m, new DateTime(2022, 10, 14, 6, 59, 0, 0, DateTimeKind.Unspecified), "AB11111115", 1, "Felix", "KS0004", 0, "0910555666", 1, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", "到府照顧", 3, "Jake", null },
                    { 3, "德光路一段", 800m, new DateTime(2022, 9, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), "anna@gmail.com", "Anna", "0910888999", null, new DateTime(2022, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0.8m, new DateTime(2022, 9, 26, 12, 59, 0, 0, DateTimeKind.Unspecified), "AB11111112", 1, "Anna", "KS0003", 1, "0910888999", 5, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", "到府洗澡", 4, "Wendy", null },
                    { 2, "德光路一段", 800m, new DateTime(2022, 9, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), "anna@gmail.com", "Anna", "0910888999", "日日毛掌公司", new DateTime(2022, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0.8m, new DateTime(2022, 9, 27, 12, 59, 0, 0, DateTimeKind.Unspecified), "AB11111111", 2, "Anna", "KS0002", 0, "0910888999", 5, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", "到府洗澡", 4, "Wendy", "12345678" }
                });

            migrationBuilder.InsertData(
                table: "PetInfomation",
                columns: new[] { "PetID", "BirthMonth", "BirthYear", "CreateTime", "EditTime", "Intro", "Ligation", "MemberID", "PetName", "PetSex", "PetType", "ShapeType", "Vaccine" },
                values: new object[,]
                {
                    { 1, null, 2022, new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(92), null, null, false, 1, "糖糖", false, 0, 2, true },
                    { 2, null, 2017, new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(277), null, null, true, 2, "豆皮", false, 1, 1, true },
                    { 3, null, 2019, new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(280), null, null, true, 3, "斑比", false, 0, 2, true },
                    { 5, null, 2019, new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(283), null, null, true, 4, "Hippo", false, 1, 0, true },
                    { 4, null, 2018, new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(282), null, null, true, 4, "熊熊", false, 0, 0, true }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductID", "CreateTime", "EditTime", "Introduce", "IsDelete", "ProductStatus", "ServiceType", "SitterID" },
                values: new object[,]
                {
                    { 5, new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9488), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9489), "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", false, 0, 2, 4 },
                    { 4, new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9485), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9486), "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", false, 0, 1, 4 },
                    { 3, new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9483), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9483), "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", false, 1, 3, 3 },
                    { 2, new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9478), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9480), "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", false, 0, 2, 3 },
                    { 1, new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9031), new DateTime(2022, 10, 4, 18, 30, 59, 314, DateTimeKind.Local).AddTicks(9196), "毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！", false, 0, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "RegisterSitter",
                columns: new[] { "SitterID", "Account", "Bank", "CreateTime", "EditTime", "ID", "IDImageback", "IDImagefont", "MemberID", "PetExperience", "RegisterStatus", "Score", "SitterInfo", "SitterName", "SitterPicture", "VerifyTime" },
                values: new object[,]
                {
                    { 1, "1111111", "822", new DateTime(2018, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "F120120120", "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp", "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp", 3, "我養過狗", 1, 90, "I'm Jake", "傑克是你嗎", "https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg", new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(8477) },
                    { 2, "222222222", "808", new DateTime(2015, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "F220120120", "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp", "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp", 4, "我養過貓", 1, 80, "I'm Wendy", "溫蒂你好嗎", "https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg", new DateTime(2022, 10, 4, 18, 30, 59, 313, DateTimeKind.Local).AddTicks(9807) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleType", "UserId" },
                values: new object[,]
                {
                    { 2, 1, 2 },
                    { 1, 1, 1 },
                    { 3, 2, 3 },
                    { 4, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "AdProject",
                columns: new[] { "AdProjectId", "BeginDate", "EndDate", "Price", "ProductID" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 300m, 1 },
                    { 2, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 300m, 4 }
                });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "CartID", "County", "CreateTime", "CustomerID", "District", "ProductID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 10, 4, 18, 30, 59, 317, DateTimeKind.Local).AddTicks(5395), 1, 1, 1 },
                    { 2, 2, new DateTime(2022, 10, 4, 18, 30, 59, 317, DateTimeKind.Local).AddTicks(5803), 2, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Evaluation",
                columns: new[] { "EvaluationID", "CreateTime", "Evaluation", "Message", "OrderID", "UserID", "UserType" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "讚", 2, 2, 1 },
                    { 2, new DateTime(2022, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "狗很乖", 2, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderPetDetail",
                columns: new[] { "OrderPetID", "BirthMonth", "BirthYear", "Ligation", "OrderID", "PetDiscription", "PetIntro", "PetName", "PetSex", "PetType", "ServiceCount", "ShapeType", "UnitPrice", "Vaccine" },
                values: new object[,]
                {
                    { 2, null, 2017, true, 2, null, null, "豆皮", false, 1, 2, 1, 400m, true },
                    { 4, null, 2022, false, 4, null, null, "Hippo", false, 0, 2, 2, 300m, true },
                    { 3, null, 2017, true, 3, null, null, "豆皮", false, 1, 2, 1, 400m, true },
                    { 1, null, 2022, false, 1, null, null, "糖糖", false, 0, 2, 2, 300m, true }
                });

            migrationBuilder.InsertData(
                table: "OrderSchedule",
                columns: new[] { "OrderScheduleID", "OrderID", "Schedule", "ServiceDate" },
                values: new object[,]
                {
                    { 3, 2, 3, new DateTime(2022, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, 4, new DateTime(2022, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, 4, new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 1, 1, new DateTime(2022, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 4, 2, new DateTime(2022, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 4, 1, new DateTime(2022, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 2, new DateTime(2022, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 3, 4, new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PetDisposition",
                columns: new[] { "PetDispositionID", "DispositionType", "PetID" },
                values: new object[,]
                {
                    { 7, 2, 5 },
                    { 6, 1, 5 },
                    { 5, 3, 4 },
                    { 1, 1, 1 },
                    { 4, 1, 3 },
                    { 2, 1, 2 },
                    { 3, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductDiscount",
                columns: new[] { "ProductDiscountID", "Discount", "ProductID", "Quantity" },
                values: new object[,]
                {
                    { 1, 0.8m, 1, 3 },
                    { 3, 0.6m, 4, 4 },
                    { 2, 0.9m, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "ImagesID", "Image", "ProductID", "Sort" },
                values: new object[,]
                {
                    { 2, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp", 1, 2 },
                    { 11, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp", 4, 3 },
                    { 12, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp", 4, 2 },
                    { 9, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp", 3, 1 },
                    { 3, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp", 1, 3 },
                    { 10, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", 4, 1 },
                    { 6, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp", 2, 3 },
                    { 5, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp", 2, 1 },
                    { 4, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", 2, 2 },
                    { 13, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", 5, 2 },
                    { 14, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp", 5, 3 },
                    { 15, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp", 5, 1 },
                    { 1, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", 1, 1 },
                    { 7, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp", 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "ImagesID", "Image", "ProductID", "Sort" },
                values: new object[] { 8, "https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp", 3, 3 });

            migrationBuilder.InsertData(
                table: "ProductServiceArea",
                columns: new[] { "ProductServiceAreaID", "County", "District", "ProductID" },
                values: new object[,]
                {
                    { 7, 2, 3, 3 },
                    { 6, 2, 4, 3 },
                    { 2, 1, 2, 1 },
                    { 1, 1, 1, 1 },
                    { 9, 2, 3, 4 },
                    { 5, 2, 3, 2 },
                    { 4, 1, 2, 2 },
                    { 3, 1, 1, 2 },
                    { 10, 2, 4, 5 },
                    { 11, 2, 3, 5 },
                    { 8, 2, 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "ProductServicePetType",
                columns: new[] { "ProductServicePetTypeID", "OvernightPrice", "PetType", "Price", "ProductID", "ShapeType" },
                values: new object[,]
                {
                    { 12, 1200m, 0, 600m, 4, 0 },
                    { 13, 1200m, 0, 600m, 4, 1 },
                    { 14, 1200m, 0, 600m, 4, 2 },
                    { 15, 1200m, 0, 400m, 5, 2 },
                    { 16, 1200m, 0, 400m, 5, 3 },
                    { 17, 1200m, 0, 400m, 5, 4 },
                    { 5, 800m, 1, 500m, 1, 0 },
                    { 2, 500m, 0, 300m, 1, 1 },
                    { 4, 800m, 0, 500m, 1, 3 },
                    { 3, 500m, 0, 300m, 1, 2 },
                    { 11, 800m, 0, 500m, 3, 4 },
                    { 10, 800m, 0, 500m, 3, 3 },
                    { 6, 800m, 1, 500m, 1, 1 },
                    { 7, 800m, 1, 500m, 1, 2 },
                    { 9, 800m, 1, 450m, 2, 0 },
                    { 8, 800m, 0, 450m, 2, 0 },
                    { 1, 500m, 0, 300m, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductServiceTime",
                columns: new[] { "ProductServiceTimeID", "ProductID", "ServiceDay", "ServicePartTime" },
                values: new object[,]
                {
                    { 17, 5, 6, 2 },
                    { 16, 5, 6, 1 },
                    { 1, 1, 1, 1 },
                    { 2, 1, 1, 2 },
                    { 3, 1, 2, 1 },
                    { 4, 1, 2, 2 },
                    { 5, 1, 3, 1 },
                    { 13, 4, 1, 3 },
                    { 15, 4, 3, 3 },
                    { 14, 4, 2, 3 },
                    { 7, 2, 4, 2 },
                    { 8, 2, 4, 3 },
                    { 9, 2, 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductServiceTime",
                columns: new[] { "ProductServiceTimeID", "ProductID", "ServiceDay", "ServicePartTime" },
                values: new object[,]
                {
                    { 10, 2, 5, 3 },
                    { 18, 5, 0, 1 },
                    { 11, 3, 6, 1 },
                    { 12, 3, 6, 1 },
                    { 6, 1, 3, 2 },
                    { 19, 5, 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "CartDetail",
                columns: new[] { "CartDetailID", "CartID", "PetType", "ShapeType" },
                values: new object[,]
                {
                    { 1, 1, 0, 0 },
                    { 2, 1, 1, 0 },
                    { 3, 2, 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "CartSchedule",
                columns: new[] { "CartScheduleID", "CartID", "Schedule", "ServiceDate" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2022, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 2, new DateTime(2022, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, 3, new DateTime(2022, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, 4, new DateTime(2022, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdProject_ProductID",
                table: "AdProject",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_MemberID",
                table: "Answer",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Aptitude_MemberID",
                table: "Aptitude",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_County",
                table: "Cart",
                column: "County");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerID",
                table: "Cart",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_District",
                table: "Cart",
                column: "District");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductID",
                table: "Cart",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_CartID",
                table: "CartDetail",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_CartSchedule_CartID",
                table: "CartSchedule",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_MemberID",
                table: "Collect",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Collect_ProductID",
                table: "Collect",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_OrderID",
                table: "Evaluation",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_UserID",
                table: "Evaluation",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FacebookRegister_MemberID",
                table: "FacebookRegister",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleRegister_MemberID",
                table: "GoogleRegister",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_License_MemberID",
                table: "License",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_LineRegister_MemberID",
                table: "LineRegister",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_AccountInfoId",
                table: "Member",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_County",
                table: "Member",
                column: "County");

            migrationBuilder.CreateIndex(
                name: "IX_Member_District",
                table: "Member",
                column: "District");

            migrationBuilder.CreateIndex(
                name: "IX_Member_RegisterType",
                table: "Member",
                column: "RegisterType");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialContact_OrderId",
                table: "OfficialContact",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialContact_UserID",
                table: "OfficialContact",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_InvoiceType",
                table: "Order",
                column: "InvoiceType");

            migrationBuilder.CreateIndex(
                name: "IX_Order_SitterID",
                table: "Order",
                column: "SitterID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCancel_OrderID",
                table: "OrderCancel",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPetDetail_OrderID",
                table: "OrderPetDetail",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedule_OrderID",
                table: "OrderSchedule",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedule_Schedule",
                table: "OrderSchedule",
                column: "Schedule");

            migrationBuilder.CreateIndex(
                name: "IX_PetDisposition_DispositionType",
                table: "PetDisposition",
                column: "DispositionType");

            migrationBuilder.CreateIndex(
                name: "IX_PetDisposition_PetID",
                table: "PetDisposition",
                column: "PetID");

            migrationBuilder.CreateIndex(
                name: "IX_PetInfomation_MemberID",
                table: "PetInfomation",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ServiceType",
                table: "Product",
                column: "ServiceType");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SitterID",
                table: "Product",
                column: "SitterID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscount_ProductID",
                table: "ProductDiscount",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductID",
                table: "ProductImage",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServiceArea_County",
                table: "ProductServiceArea",
                column: "County");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServiceArea_District",
                table: "ProductServiceArea",
                column: "District");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServiceArea_ProductID",
                table: "ProductServiceArea",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServicePetType_ProductID",
                table: "ProductServicePetType",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServiceTime_ProductID",
                table: "ProductServiceTime",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterSitter_MemberID",
                table: "RegisterSitter",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_MemberID",
                table: "UserContact",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_SitterID",
                table: "UserContact",
                column: "SitterID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleType",
                table: "UserRole",
                column: "RoleType");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdProject");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Aptitude");

            migrationBuilder.DropTable(
                name: "CartDetail");

            migrationBuilder.DropTable(
                name: "CartSchedule");

            migrationBuilder.DropTable(
                name: "Collect");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Evaluation");

            migrationBuilder.DropTable(
                name: "FacebookRegister");

            migrationBuilder.DropTable(
                name: "GoogleRegister");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "LineRegister");

            migrationBuilder.DropTable(
                name: "OfficialContact");

            migrationBuilder.DropTable(
                name: "OrderCancel");

            migrationBuilder.DropTable(
                name: "OrderPetDetail");

            migrationBuilder.DropTable(
                name: "OrderSchedule");

            migrationBuilder.DropTable(
                name: "PetDisposition");

            migrationBuilder.DropTable(
                name: "ProductDiscount");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductServiceArea");

            migrationBuilder.DropTable(
                name: "ProductServicePetType");

            migrationBuilder.DropTable(
                name: "ProductServiceTime");

            migrationBuilder.DropTable(
                name: "RegisterSitter");

            migrationBuilder.DropTable(
                name: "UserContact");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Disposition");

            migrationBuilder.DropTable(
                name: "PetInfomation");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "InvoiceType");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.DropTable(
                name: "AccountInfo");

            migrationBuilder.DropTable(
                name: "County");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "RegisterType");
        }
    }
}
