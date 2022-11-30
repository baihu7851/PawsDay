using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApplicationCore.Entities;
using System.Security.Cryptography.X509Certificates;

#nullable disable

namespace Infrastructure.Data
{
    public partial class PawsDayContext : DbContext
    {
        public PawsDayContext()
        {
        }

        public PawsDayContext(DbContextOptions<PawsDayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountInfo> AccountInfos { get; set; }
        public virtual DbSet<AdProject> AdvertiseLists { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Aptitude> Aptitudes { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<CartSchedule> CartSchedules { get; set; }
        public virtual DbSet<Collect> Collects { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<Disposition> Dispositions { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<FacebookRegister> FacebookRegisters { get; set; }
        public virtual DbSet<GoogleRegister> GoogleRegisters { get; set; }
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }
        public virtual DbSet<LineRegister> LineRegisters { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<OfficialContact> OfficialContacts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderCancel> OrderCancels { get; set; }
        public virtual DbSet<OrderPetDetail> OrderPetDetails { get; set; }

        public virtual DbSet<OrderSchedule> OrderSchedules { get; set; }
        public virtual DbSet<PetDisposition> PetDispositions { get; set; }
        public virtual DbSet<PetInfomation> PetInfomations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductServiceArea> ProductServiceAreas { get; set; }
        public virtual DbSet<ProductServicePetType> ProductServicePetTypes { get; set; }
        public virtual DbSet<ProductServiceTime> ProductServiceTimes { get; set; }
        public virtual DbSet<RegisterSitter> RegisterSitters { get; set; }
        public virtual DbSet<RegisterType> RegisterTypes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<UserContact> UserContacts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<BlockToken> BlockToken { get; set; }
        public virtual DbSet<LineBotKeyWord> LineBotKeyWord { get; set; }
        public virtual DbSet<LineBotTemplate> LineBotTemplate { get; set; }
        public virtual DbSet<LineBotTemplateDetail> LineBotTemplateDetail { get; set; }
        public virtual DbSet<LineBotHistory> LineBotHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountInfo>(entity =>
            {
                entity.ToTable("AccountInfo");

                entity.Property(e => e.AccountInfoId)
                    .HasColumnName("AccountInfoID")
                    .HasComment("帳號編號");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasComment("帳號");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.ExpirationTime)
                    .HasColumnType("datetime")
                    .HasComment("有效時間");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("密碼");

                entity.Property(e => e.Verify).HasComment("驗證是否通過");
            });

            modelBuilder.Entity<AdProject>(entity =>
            {
                entity.ToTable("AdProject");
                entity.HasKey("AdProjectId");

                entity.Property(e => e.AdProjectId)
                    .HasColumnName("AdProjectId")
                    .HasComment("廣告編號");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("datetime")
                    .HasComment("開始時間");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasComment("結束時間");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("價格");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AdProject)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Advertise_Product");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.AnswerId)
                    .HasColumnName("AnswerID")
                    .HasComment("答案紀錄編號");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.Property(e => e.AnswerInput).HasComment("答案");
                entity.Property(e => e.QuestionId).HasComment("題目編號");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_MemberId");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.ToTable("License");

                entity.Property(e => e.LicenseId)
                    .HasColumnName("LicenseID")
                    .HasComment("證照編號");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.Property(e => e.LicenseUrl)
                    .IsRequired()
                    .HasComment("證照連結");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_License_MemberId");

            });

            modelBuilder.Entity<Aptitude>(entity =>
            {
                entity.ToTable("Aptitude");

                entity.Property(e => e.AptitudeId)
                    .HasColumnName("AptitudeID")
                    .HasComment("測驗編號");

                entity.Property(e => e.AptitudeExtrovert).HasComment("保姆人格測驗項度E");

                entity.Property(e => e.AptitudeIntrovert).HasComment("保姆人格測驗項度I");

                entity.Property(e => e.AptitudeThinking).HasComment("保姆人格測驗項度T");

                entity.Property(e => e.AptitudeFeeling).HasComment("保姆人格測驗項度F");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aptitude_Member");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.CartId)
                    .HasColumnName("CartID")
                    .HasComment("購物車編號");

                entity.Property(e => e.County).HasComment("服務縣市");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("加入時間");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasComment("顧客編號");

                entity.Property(e => e.District).HasComment("服務地區");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號");


                entity.HasOne(d => d.CountyNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.County)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_County");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Member");

                entity.HasOne(d => d.DistrictNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.District)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_District");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Product");
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.ToTable("CartDetail");

                entity.Property(e => e.CartDetailId)
                    .HasColumnName("CartDetailID")
                    .HasComment("編號");

                entity.Property(e => e.CartId)
                    .HasColumnName("CartID")
                    .HasComment("購物車編號");

                entity.Property(e => e.PetType).HasComment("寵物類型");


                entity.Property(e => e.ShapeType).HasComment("體型");


                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartDetail_Cart");
            });

            modelBuilder.Entity<CartSchedule>(entity =>
            {
                entity.ToTable("CartSchedule");

                entity.Property(e => e.CartScheduleId)
                    .HasColumnName("CartScheduleID")
                    .HasComment("編號");

                entity.Property(e => e.CartId)
                    .HasColumnName("CartID")
                    .HasComment("購物車編號");

                entity.Property(e => e.Schedule).HasComment("服務時程編號");

                entity.Property(e => e.ServiceDate)
                    .HasColumnType("datetime")
                    .HasComment("服務日期");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartSchedules)
                    .HasForeignKey(d => d.CartId)   
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartSchedule_Cart");
            });

            modelBuilder.Entity<Collect>(entity =>
            {
                entity.ToTable("Collect");

                entity.Property(e => e.CollectId)
                    .HasColumnName("CollectID")
                    .HasComment("編號");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("收藏商品編號");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Collects)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collect_Member");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Collects)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collect_Product");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.ContactId)
                    .HasColumnName("ContactID");

                entity.Property(e => e.ContactContent)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ReplyContent).HasMaxLength(500);

                entity.Property(e => e.ReplyTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.ToTable("County");

                entity.Property(e => e.CountyId)
                    .HasColumnName("CountyID")
                    .HasComment("縣市編號");

                entity.Property(e => e.CountyName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment("縣市名");
            });

            modelBuilder.Entity<Disposition>(entity =>
            {
                entity.ToTable("Disposition");

                entity.Property(e => e.DispositionId)
                    .HasColumnName("DispositionID")
                    .HasComment("性格編號");

                entity.Property(e => e.DispositionType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Disposition")
                    .HasComment("性格描述");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("DistrictID")
                    .HasComment("地區編號");

                entity.Property(e => e.CountyId)
                    .HasColumnName("CountyID")
                    .HasComment("縣市編號");

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment("地區名");
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.ToTable("Evaluation");

                entity.Property(e => e.EvaluationId)
                    .HasColumnName("EvaluationID")
                    .HasComment("評論編號");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("留言時間");

                entity.Property(e => e.EvaluationScore)
                    .HasColumnName("Evaluation")
                    .HasComment("評價");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("評論");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("對應訂單");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("留言者");

                entity.Property(e => e.UserType).HasComment("留言身分");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evaluation_Order");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evaluation_Member");
            });

            modelBuilder.Entity<FacebookRegister>(entity =>
            {
                entity.HasKey("FbregisterId");
                entity.ToTable("FacebookRegister");

                entity.Property(e => e.FbregisterId)
                    .HasColumnName("FBRegisterID")
                    .HasComment("編號");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasComment("FB登入序號");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.FacebookRegisters)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacebookRegister_Member");
            });

            modelBuilder.Entity<GoogleRegister>(entity =>
            {
                entity.ToTable("GoogleRegister");

                entity.Property(e => e.GoogleRegisterId)
                    .HasColumnName("GoogleRegisterID")
                    .HasComment("編號");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasComment("Google登入序號");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.GoogleRegisters)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoogleRegister_Member");
            });

            modelBuilder.Entity<InvoiceType>(entity =>
            {
                entity.ToTable("InvoiceType");
                entity.HasKey("InvoiceTypeId"); 

                entity.Property(e => e.InvoiceTypeId)
                    .HasColumnName("InvoiceTypeID")
                    .HasComment("發票類別編號");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("InvoiceType")
                    .HasComment("發票類別");
                
            });

            modelBuilder.Entity<LineRegister>(entity =>
            {
                entity.ToTable("LineRegister");

                entity.Property(e => e.LineRegisterId)
                    .HasColumnName("LineRegisterID")
                    .HasComment("編號");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasComment("Line登入序號");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.LineRegisters)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineRegister_Member");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號(平台唯一會員編號)");

                entity.Property(e => e.AccountInfoId).HasComment("帳號密碼資訊");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasComment("詳細地址");

                entity.Property(e => e.Birth)
                    .HasColumnType("date")
                    .HasComment("出生日期");

                entity.Property(e => e.County).HasComment("縣市");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("資料成立時間");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasComment("資料編輯時間");

                entity.Property(e => e.District).HasComment("區域");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("信箱");

                entity.Property(e => e.IsDelete).HasComment("是否刪除");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("姓名");

                entity.Property(e => e.NickName)
                    .HasMaxLength(20)
                    .HasComment("暱稱");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("電話");

                entity.Property(e => e.ProfileImage).HasComment("照片");

                entity.Property(e => e.RegisterType).HasComment("註冊方式");

                entity.Property(e => e.Sex).HasComment("性別");

                entity.Property(e => e.Status).HasComment("會員狀態");

                entity.HasOne(d => d.AccountInfoNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.AccountInfoId)
                    .HasConstraintName("FK_Member_AccountInfo");

                entity.HasOne(d => d.CountyNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.County)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_County");

                entity.HasOne(d => d.DistrictNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.District)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_District");

                entity.HasOne(d => d.RegisterTypeNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RegisterType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_RegisterType");


            });

            modelBuilder.Entity<OfficialContact>(entity =>
            {
                entity.ToTable("OfficialContact");

                entity.Property(e => e.OfficialContactId)
                    .HasColumnName("OfficialContactID")
                    .HasComment("對話編號");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("發話時間");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("對話內容");


                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("使用者ID");

                entity.Property(e => e.IsUserSpeak)
                    .IsRequired()
                    .HasComment("使用者發話");

                entity.Property(e => e.UserType).HasComment("使用者種類");

                entity.Property(e=>e.OrderId).HasComment("訂單編號");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OfficialContacts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OfficialContact_Member");

            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("訂單編號");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("服務詳細地址");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("訂單總額");

                entity.Property(e => e.BookingEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("訂購人信箱");

                entity.Property(e => e.BookingName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("訂購人姓名");

                entity.Property(e => e.BookingPhone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("訂購人電話");


                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("訂單建立時間");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasComment("顧客會員編號");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("折扣優惠");



                entity.Property(e => e.InvoiceId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("InvoiceID")
                    .IsFixedLength(true)
                    .HasComment("發票號碼");


                entity.Property(e => e.InvoiceType).HasComment("發票開立方式");

                entity.Property(e => e.UnoformNumber)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasColumnName("UnoformNumber")
                    .HasComment("統一編號");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("CompanyName")
                    .HasComment("公司抬頭");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("托育聯絡人姓名");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(false)
                    .HasComment("訂單號碼(顯示用)");

                entity.Property(e => e.OrderStatus).HasComment("訂單狀態");

                entity.Property(e => e.BeginTime).IsRequired().HasComment("服務起始時間");
                entity.Property(e => e.EndTime).IsRequired().HasComment("服務結束時間");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("托育聯絡人電話");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號(已鎖死不會被改變)");

                entity.Property(e => e.ProductIntro)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("商品說明(已鎖死不會被改變)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("服務類型(已鎖死不會被改變)");

                entity.Property(e => e.ProductImageUrl)
                    .IsRequired()
                    .HasComment("商品照片(已鎖死不會被改變)");

                entity.Property(e => e.SitterId)
                    .HasColumnName("SitterID")
                    .HasComment("保姆會員編號");

                entity.Property(e => e.SitterName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("保姆名字(已鎖死不會被改變)");



                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Member1");


                entity.HasOne(d => d.InvoiceTypeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.InvoiceType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_InvoiceType");


                entity.HasOne(d => d.Sitter)
                    .WithMany(p => p.OrderSitters)
                    .HasForeignKey(d => d.SitterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Member");



            });

            modelBuilder.Entity<OrderCancel>(entity =>
            {
                entity.ToTable("OrderCancel");

                entity.Property(e => e.OrderCancelId)
                    .HasColumnName("OrderCancelID")
                    .HasComment("編號");

                entity.Property(e => e.CancelDate)
                    .HasColumnType("datetime")
                    .HasComment("取消日期時間");

                entity.Property(e => e.CancelReason)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("取消原因");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("訂單編號");

                entity.Property(e => e.RefundPersent)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("退還總價百分比");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderCancels)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderCancel_Order");
            });

            modelBuilder.Entity<OrderPetDetail>(entity =>
            {
                entity.HasKey(e => e.OrderPetId)
                    .HasName("PK_OrderPetDetails");

                entity.ToTable("OrderPetDetail");

                entity.Property(e => e.OrderPetId)
                    .HasColumnName("OrderPetID")
                    .HasComment("訂單寵物編號");

                entity.Property(e => e.BirthMonth).HasComment("出生月");

                entity.Property(e => e.BirthYear).HasComment("出生年");

                entity.Property(e => e.Ligation).HasComment("是否結紮");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("訂單編號");

                entity.Property(e => e.PetDiscription)
                    .HasComment("個性敘述");

                entity.Property(e => e.PetIntro)
                    .HasComment("補充敘述");

                entity.Property(e => e.PetName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("寵物名字");

                entity.Property(e => e.PetSex).HasComment("寵物性別");

                entity.Property(e => e.PetType).HasComment("寵物類型");

                entity.Property(e => e.ServiceCount).HasComment("服務計次");

                entity.Property(e => e.ShapeType).HasComment("體型");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("單價");

                entity.Property(e => e.Vaccine).HasComment("是否定期打預防針");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPetDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPetDetail_Order");
            });

            

            modelBuilder.Entity<OrderSchedule>(entity =>
            {
                entity.ToTable("OrderSchedule");

                entity.Property(e => e.OrderScheduleId)
                    .HasColumnName("OrderScheduleID")
                    .HasComment("編號");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("訂單編號");

                entity.Property(e => e.Schedule).HasComment("服務詳細時程");

                entity.Property(e => e.ServiceDate)
                    .HasColumnType("datetime")
                    .HasComment("服務日期");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderSchedules)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSchedule_Order");

                entity.HasOne(d => d.ScheduleNavigation)
                    .WithMany(p => p.OrderSchedules)
                    .HasForeignKey(d => d.Schedule)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSchedule_Schedule");
            });

            

            modelBuilder.Entity<PetDisposition>(entity =>
            {
                entity.ToTable("PetDisposition");

                entity.Property(e => e.PetDispositionId)
                    .HasColumnName("PetDispositionID")
                    .HasComment("寵物性格編號");

                entity.Property(e => e.DispositionType).HasComment("性格描述");

                entity.Property(e => e.PetId)
                    .HasColumnName("PetID")
                    .HasComment("寵物編號");

                entity.HasOne(d => d.DispositionTypeNavigation)
                    .WithMany(p => p.PetDispositions)
                    .HasForeignKey(d => d.DispositionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PetDisposition_Disposition");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.PetDispositions)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PetDisposition_PetInfomation");
            });

            modelBuilder.Entity<PetInfomation>(entity =>
            {
                entity.HasKey(e => e.PetId)
                    .HasName("PK_PetInfomations");

                entity.ToTable("PetInfomation");

                entity.Property(e => e.PetId)
                    .HasColumnName("PetID")
                    .HasComment("寵物編號");

                entity.Property(e => e.BirthMonth).HasComment("出生月");

                entity.Property(e => e.BirthYear).HasComment("出生年");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("資料建立時間");


                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasComment("資料編輯時間");

                entity.Property(e => e.Intro)
                    .HasMaxLength(300)
                    .HasComment("補充介紹");

                entity.Property(e => e.Ligation).HasComment("是否結紮");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.Property(e => e.PetName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("寵物名字");

                entity.Property(e => e.PetSex).HasComment("寵物性別");

                entity.Property(e => e.PetType).HasComment("寵物類型");

                entity.Property(e => e.ShapeType).HasComment("體型");

                entity.Property(e => e.Vaccine).HasComment("是否定期打預防針");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PetInfomations)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PetInfomation_Member");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasComment("編輯時間");

                entity.Property(e => e.Introduce)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("商品描述");

                entity.Property(e => e.IsDelete).HasComment("是否刪除");

                entity.Property(e => e.ProductStatus).HasComment("商品上架狀態");

                entity.Property(e => e.ServiceType).HasComment("服務類型");

                entity.Property(e => e.SitterId)
                    .HasColumnName("SitterID")
                    .HasComment("保姆編號");


                entity.HasOne(d => d.ServiceTypeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ServiceType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ServiceType");

                entity.HasOne(d => d.Sitter)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SitterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Member");
            });

            modelBuilder.Entity<ProductDiscount>(entity =>
            {
                entity.ToTable("ProductDiscount");

                entity.Property(e => e.ProductDiscountId)
                    .HasColumnName("ProductDiscountID")
                    .HasComment("商品折扣編號");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("設定折扣");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號");

                entity.Property(e => e.Quantity).HasComment("數量");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDiscounts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDiscount_Product");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.ImagesId)
                    .HasName("PK_ProductImages");

                entity.ToTable("ProductImage");

                entity.Property(e => e.ImagesId)
                    .HasColumnName("ImagesID")
                    .HasComment("圖片編號");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasComment("圖片連結");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號");

                entity.Property(e => e.Sort).HasComment("圖片排序");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImage_Product");
            });

            modelBuilder.Entity<ProductServiceArea>(entity =>
            {
                entity.ToTable("ProductServiceArea");

                entity.Property(e => e.ProductServiceAreaId)
                    .HasColumnName("ProductServiceAreaID")
                    .HasComment("編號");

                entity.Property(e => e.County).HasComment("可服務縣市");

                entity.Property(e => e.District).HasComment("可服務地區");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號");

                entity.HasOne(d => d.CountyNavigation)
                    .WithMany(p => p.ProductServiceAreas)
                    .HasForeignKey(d => d.County)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductServiceArea_County");

                entity.HasOne(d => d.DistrictNavigation)
                    .WithMany(p => p.ProductServiceAreas)
                    .HasForeignKey(d => d.District)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductServiceArea_District");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductServiceAreas)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductServiceArea_Product");
            });

            modelBuilder.Entity<ProductServicePetType>(entity =>
            {
                entity.ToTable("ProductServicePetType");

                entity.Property(e => e.ProductServicePetTypeId)
                    .HasColumnName("ProductServicePetTypeID")
                    .HasComment("編號");

                entity.Property(e => e.OvernightPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("夜間單價");

                entity.Property(e => e.PetType).HasComment("可服務寵物類型");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("一般單價");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號");

                entity.Property(e => e.ShapeType).HasComment("可服務寵物體型");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductServicePetTypes)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductServicePetType_Product");
            });

            modelBuilder.Entity<ProductServiceTime>(entity =>
            {
                entity.ToTable("ProductServiceTime");

                entity.Property(e => e.ProductServiceTimeId)
                    .HasColumnName("ProductServiceTimeID")
                    .HasComment("編號");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品編號");

                entity.Property(e => e.ServiceDay).HasComment("可服務星期別");

                entity.Property(e => e.ServicePartTime).HasComment("可服務時段");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductServiceTimes)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductServiceTime_Product");

            });

            modelBuilder.Entity<RegisterSitter>(entity =>
            {
                entity.HasKey(e => e.SitterId)
                    .HasName("PK_RegisterSitters");

                entity.ToTable("RegisterSitter");

                entity.Property(e => e.SitterId)
                    .HasColumnName("SitterID")
                    .HasComment("保姆編號(不對外公開)");

                entity.Property(e => e.Account)
                    .HasMaxLength(20)
                    .IsFixedLength(true)
                    .HasComment("收款帳號");

                entity.Property(e => e.Bank)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("收款銀行");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("送審時間");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasComment("編輯時間");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength(true)
                    .HasComment("身分證編號");

                entity.Property(e => e.Idimageback)
                    .IsRequired()
                    .HasColumnName("IDImageback")
                    .HasComment("身分證反面");

                entity.Property(e => e.Idimagefont)
                    .IsRequired()
                    .HasColumnName("IDImagefont")
                    .HasComment("身分證正面");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("會員編號");

                entity.Property(e => e.PetExperience)
                    .IsRequired()
                    .HasColumnName("PetExperience")
                    .HasComment("寵物經驗");

                entity.Property(e => e.RegisterStatus).HasComment("保姆審核狀態");

                entity.Property(e => e.Score).HasComment("測驗總分");

                entity.Property(e => e.SitterInfo)
                    .HasMaxLength(500)
                    .HasComment("保姆介紹");

                entity.Property(e => e.SitterName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("保姆名字(預設會員資料)");

                entity.Property(e => e.SitterPicture)
                    .HasComment("保姆照片(預設會員資料)");

                entity.Property(e => e.VerifyTime)
                    .HasColumnType("datetime")
                    .HasComment("審核時間");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.RegisterSitters)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisterSitter_Member");

            });

            modelBuilder.Entity<RegisterType>(entity =>
            {
                entity.HasKey(e => e.RegisterId)
                    .HasName("PK_RegisterTypes");

                entity.ToTable("RegisterType");

                entity.Property(e => e.RegisterId)
                    .HasColumnName("RegisterID")
                    .HasComment("註冊方式編號");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("RegisterType")
                    .HasComment("註冊方式");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.ScheduleId)
                    .HasColumnName("ScheduleID")
                    .HasComment("時程別編號");


                entity.Property(e => e.TimeDesrcipt)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("時程細節說明");

            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.ToTable("ServiceType");

                entity.Property(e => e.ServiceTypeId)
                    .HasColumnName("ServiceTypeID")
                    .HasComment("服務類型編號");

                entity.Property(e => e.ServiceIntro)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("服務說明");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasColumnName("ServiceType")
                    .HasComment("服務類型");
            });


            modelBuilder.Entity<UserContact>(entity =>
            {
                entity.ToTable("UserContact");

                entity.Property(e => e.UserContactId)
                    .HasColumnName("UserContactID")
                    .HasComment("對話編號");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("發話時間");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasComment("顧客ID");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasComment("對話內容");

                entity.Property(e => e.SitterId)
                    .HasColumnName("SitterID")
                    .HasComment("保姆ID");

                entity.Property(e => e.IsMemberSpeak)
                    .IsRequired()
                    .HasComment("顧客發話");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.UserContactMembers)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserContact_Member");

                entity.HasOne(d => d.Sitter)
                    .WithMany(p => p.UserContactSitters)
                    .HasForeignKey(d => d.SitterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserContact_Member1");

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey("RoleId");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleId")
                    .HasComment("角色編號");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                   .HasColumnName("RoleName")
                   .HasComment("角色名稱");


            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
                entity.HasKey("UserRoleId");

                entity.Property(e => e.UserRoleId)
                    .HasColumnName("UserRoleId")
                    .HasComment("使用者角色編號");

                entity.Property(e => e.UserId)
                    .IsRequired()
                   .HasColumnName("UserId")
                   .HasComment("使用者Id");

                entity.Property(e => e.RoleType)
                   .IsRequired()
                  .HasColumnName("RoleType")
                  .HasComment("角色Id");


                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Role");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Member");

            });

            modelBuilder.Entity<BlockToken>(entity =>
            {
                entity.ToTable("BlockToken");
                entity.HasKey("BlockTokenId");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("Token");

                entity.Property(e=>e.ExpireTime)
                    .IsRequired()
                    .HasColumnName("ExpireTime")
                    .HasComment("有效時間");
            });

            modelBuilder.Entity<LineBotKeyWord>(entity =>
            {
                entity.ToTable("LineBotKeyWord");
                entity.HasKey("KeyWordId");

                entity.Property(e => e.KeyWord)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("KeyWord");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Action");

                entity.Property(e => e.CanBeEdit)
                    .IsRequired()
                    .HasColumnName("CanBeEdit");
            });

            modelBuilder.Entity<LineBotTemplate>(entity =>
            {
                entity.ToTable("LineBotTemplate");
                entity.HasKey("TemplateId");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Title");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Text");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("ImageUrl");

                entity.Property(e => e.Time)
                    .HasColumnName("Time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<LineBotTemplateDetail>(entity =>
            {
                entity.ToTable("LineBotTemplateDetail");
                entity.HasKey("TemplateDetailId");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Text");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("Type");

                entity.Property(e => e.Url)
                    .HasColumnName("Url");

                entity.Property(e => e.Template)
                    .HasColumnName("TemplateId")
                    .HasComment("模板編號");

                entity.HasOne(d => d.LineBotTemplate)
                    .WithMany(p=> p.LineBotTemplateDetails)
                    .HasForeignKey(d => d.Template)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineBotTemplateDetail_LineBotTemplate");
            });

            modelBuilder.Entity<LineBotHistory>(entity =>
            {
                entity.ToTable("LineBotHistory");
                entity.HasKey("HistoryId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserId");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Text");

                entity.Property(e => e.CreateTime)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasColumnName("CreateTime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
