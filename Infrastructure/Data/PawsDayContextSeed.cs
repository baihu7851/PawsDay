using ApplicationCore.Common;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PawsDayContextSeed
    {
    }

    public static class SeedData
    {
        public static List<ServiceType> ServiceTypes()
        {
            return new List<ServiceType>
            {
                new ServiceType{ ServiceTypeId=1, TypeName="到府照顧",ServiceIntro="＄200元起／30分鐘\r\n👉 專業實名認證寵物保姆到府照顧寵物\r\n👉 寵物保姆、餵食、環境清潔、陪伴玩耍、回報健康狀況、餵藥等客製服務\r\n👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n👉 全程與保姆維持連線，回報寵物狀況\r\n👉 平台預約全程含青杉保險與品質保障\r\n👉 鑰匙可以溝通警衛代收、信箱傳遞等方式"},
                new ServiceType{ ServiceTypeId=2,TypeName="到府洗澡",ServiceIntro="＄300元起／1小時起／1隻毛孩\r\n👉 寵物免出門! 寵物美容師攜帶工具到府幫寵物做小美容\r\n👉 小美容包含洗澡、按摩、剪指甲、清耳朵、擠肛門腺、修腳底毛、修屁股毛、含環境清理\r\n👉 服務前美容師會先跟毛孩培養感情、餵零食\r\n👉 若有特殊需求或是疾病毛孩，請先與美容師溝通\r\n👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n👉 全程與保姆維持連線，回報寵物狀況\r\n👉 平台預約全程含青杉保險與品質保障"},
                new ServiceType{ ServiceTypeId=3,TypeName="陪伴散步",ServiceIntro="＄100元起／30分鐘起\r\n👉 無法掌控回家時間? 保姆可到府帶狗狗出門散步\r\n👉 出門不能帶狗狗進餐廳? 保姆可約地點接狗狗散步\r\n👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n👉 全程與保姆維持連線，回報寵物狀況\r\n👉 平台預約全程含青杉保險與品質保障\r\n👉 鑰匙可以溝通警衛代收、信箱傳遞等方式"}
            };
        }
        public static List<County> Counties()
        {
            return new List<County>
            {
                new County{CountyId=1, CountyName="台北市" },
                new County{CountyId=2, CountyName="新北市" },
            };
        }
        public static List<District> Districts()
        {
            return new List<District>
            {
                new District{ DistrictId=1, DistrictName="萬華區",CountyId=1},
                new District{ DistrictId=2,DistrictName="大安區",CountyId=1},
                new District{ DistrictId=3,DistrictName="板橋區",CountyId=2},
                new District{ DistrictId=4,DistrictName="中和區",CountyId=2},
            };
        }
        public static List<Schedule> Schedules()
        {
            return new List<Schedule>
            {
                new Schedule{ ScheduleId=1,TimeDesrcipt="00:00~00:29",PartTimeId=0},
                new Schedule{ ScheduleId=2,TimeDesrcipt="00:30~00:59",PartTimeId=0},

                new Schedule{ ScheduleId=3,TimeDesrcipt="01:00~01:29",PartTimeId=0},
                new Schedule{ ScheduleId=4,TimeDesrcipt="01:30~01:59",PartTimeId=0},

                new Schedule{ ScheduleId=5,TimeDesrcipt="02:00~02:29",PartTimeId=0},
                new Schedule{ ScheduleId=6,TimeDesrcipt="02:30~02:59",PartTimeId=0},

                new Schedule{ ScheduleId=7,TimeDesrcipt="03:00~03:29",PartTimeId=0},
                new Schedule{ ScheduleId=8,TimeDesrcipt="03:30~03:59",PartTimeId=0},

                new Schedule{ ScheduleId=9,TimeDesrcipt="04:00~04:29",PartTimeId=0},
                new Schedule{ ScheduleId=10,TimeDesrcipt="04:30~04:59",PartTimeId=0},

                new Schedule{ ScheduleId=11,TimeDesrcipt="05:00~05:29",PartTimeId=0},
                new Schedule{ ScheduleId=12,TimeDesrcipt="05:30~05:59",PartTimeId=0},
                new Schedule{ ScheduleId=13, TimeDesrcipt="06:00~06:29",PartTimeId=1},
                new Schedule{ ScheduleId=14,TimeDesrcipt="06:30~06:59",PartTimeId=1},

                new Schedule{ ScheduleId=15, TimeDesrcipt="07:00~07:29",PartTimeId=1},
                new Schedule{ ScheduleId=16,TimeDesrcipt="07:30~07:59",PartTimeId=1},

                new Schedule{ ScheduleId=17, TimeDesrcipt="08:00~08:29",PartTimeId=1},
                new Schedule{ ScheduleId=18,TimeDesrcipt="08:30~08:59",PartTimeId=1},

                new Schedule{ ScheduleId=19, TimeDesrcipt="09:00~09:29",PartTimeId=1},
                new Schedule{ ScheduleId=20,TimeDesrcipt="09:30~09:59",PartTimeId=1},

                new Schedule{ ScheduleId=21, TimeDesrcipt="10:00~10:29",PartTimeId=1},
                new Schedule{ ScheduleId=22,TimeDesrcipt="10:30~10:59",PartTimeId=1},

                new Schedule{ ScheduleId=23, TimeDesrcipt="11:00~11:29",PartTimeId=1},
                new Schedule{ ScheduleId=24,TimeDesrcipt="11:30~11:59",PartTimeId=1},

                new Schedule{ ScheduleId=25,TimeDesrcipt="12:00~12:29",PartTimeId=2},
                new Schedule{ ScheduleId=26,TimeDesrcipt="12:30~12:59",PartTimeId=2},

                new Schedule{ ScheduleId=27,TimeDesrcipt="13:00~13:29",PartTimeId=2},
                new Schedule{ ScheduleId=28,TimeDesrcipt="13:30~13:59",PartTimeId=2},

                new Schedule{ ScheduleId=29,TimeDesrcipt="14:00~14:29",PartTimeId=2},
                new Schedule{ ScheduleId=30,TimeDesrcipt="14:30~14:59",PartTimeId=2},

                new Schedule{ ScheduleId=31,TimeDesrcipt="15:00~15:29",PartTimeId=2},
                new Schedule{ ScheduleId=32,TimeDesrcipt="15:30~15:59",PartTimeId=2},

                new Schedule{ ScheduleId=33,TimeDesrcipt="16:00~16:29",PartTimeId=2},
                new Schedule{ ScheduleId=34,TimeDesrcipt="16:30~16:59",PartTimeId=2},

                new Schedule{ ScheduleId=35,TimeDesrcipt="17:00~17:29",PartTimeId=2},
                new Schedule{ ScheduleId=36,TimeDesrcipt="17:30~17:59",PartTimeId=2},

                new Schedule{ ScheduleId=37,TimeDesrcipt="18:00~18:29",PartTimeId=3},
                new Schedule{ ScheduleId=38,TimeDesrcipt="18:30~18:59",PartTimeId=3},

                new Schedule{ ScheduleId=39,TimeDesrcipt="19:00~19:29",PartTimeId=3},
                new Schedule{ ScheduleId=40,TimeDesrcipt="19:30~19:59",PartTimeId=3},

                new Schedule{ ScheduleId=41,TimeDesrcipt="20:00~20:29",PartTimeId=3},
                new Schedule{ ScheduleId=42,TimeDesrcipt="20:30~20:59",PartTimeId=3},

                new Schedule{ ScheduleId=43,TimeDesrcipt="21:00~21:29",PartTimeId=3},
                new Schedule{ ScheduleId=44,TimeDesrcipt="21:30~21:59",PartTimeId=3},

                new Schedule{ ScheduleId=45,TimeDesrcipt="22:00~22:29",PartTimeId=3},
                new Schedule{ ScheduleId=46,TimeDesrcipt="22:30~22:59",PartTimeId=3},

                new Schedule{ ScheduleId=47,TimeDesrcipt="23:00~23:29",PartTimeId=3},
                new Schedule{ ScheduleId=48,TimeDesrcipt="23:30~23:59",PartTimeId=3},


            };
        }

        public static List<RegisterType> RegisterTypes()
        {
            return new List<RegisterType>
            {
                new RegisterType{ RegisterId=1,TypeName="信箱"},
                new RegisterType{ RegisterId=2,TypeName="Line"},
                new RegisterType{ RegisterId=3,TypeName="Google"},
                new RegisterType{ RegisterId=4,TypeName="Facebook"},
            };
        }
        public static List<Member> Members()
        {
            return new List<Member>
            {
                new Member{
                    MemberId=1,
                    RegisterType=1,
                    Name="Felix",
                    Sex=true,
                    Birth= new  DateTime(1996,09,15),
                    County=1,
                    District=1,
                    Address="中華路二段",
                    Phone="0910666777",
                    Email="felix@gmail.com",
                    Status=1,
                    IsDelete=false,
                    CreateTime=DateTime.Now,
                    ProfileImage="https://raw.githubusercontent.com/Ning0809/FileStorage/main/%E4%B8%8D%E4%BA%8C%E9%A6%AC%E9%BC%A0.jpg"
                },
                new Member{
                    MemberId=2,
                    RegisterType=1,
                    Name="Anna",
                    Sex=false,
                    Birth= new  DateTime(1998,02,20),
                    County=1,
                    District=2,
                    Address="忠孝東路五段",
                    Phone="0955666321",
                    Email="anna@gmail.com",
                    Status=1,
                    IsDelete=false,
                    CreateTime=DateTime.Now,
                    ProfileImage="https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg"

                },
                new Member{
                    MemberId=3,
                    RegisterType=1,
                    Name="Jake",
                    Sex=true,
                    Birth= new  DateTime(1992,03,31),
                    County=2,
                    District=1,
                    Address="民族路一段",
                    Phone="0910555444",
                    Email="jake@gmail.com",
                    Status=1,
                    IsDelete=false,
                    CreateTime=DateTime.Now,
                    ProfileImage="https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg"

                },
                new Member{
                    MemberId=4,
                    RegisterType=1,
                    Name="Wendy",
                    Sex=false,
                    Birth= new  DateTime(2000,08,10),
                    County=2,
                    District=2,
                    Address="德光路一段",
                    Phone="0910888999",
                    Email="wendy@gmail.com",
                    Status=1,
                    IsDelete=false,
                    CreateTime=DateTime.Now,
                    ProfileImage="https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg"

                },
            };
        }
        public static List<PetInfomation> PetInfomations()
        {
            return new List<PetInfomation>
            {
                new PetInfomation
                {
                    PetId=1,
                    MemberId=1,
                    PetName="糖糖",
                    PetType=0,
                    ShapeType=2,
                    PetSex=false,
                    BirthYear=2022,
                    Ligation=false,
                    Vaccine=true,
                    CreateTime=DateTime.Now
                },
                new PetInfomation
                {
                    PetId=2,
                    MemberId=2,
                    PetName="豆皮",
                    PetType=1,
                    ShapeType=1,
                    PetSex=false,
                    BirthYear=2017,
                    Ligation=true,
                    Vaccine=true,
                    CreateTime=DateTime.Now
                },
                new PetInfomation
                {
                    PetId=3,
                    MemberId=3,
                    PetName="斑比",
                    PetType=0,
                    ShapeType=2,
                    PetSex=false,
                    BirthYear=2019,
                    Ligation=true,
                    Vaccine=true,
                    CreateTime=DateTime.Now
                },
                new PetInfomation
                {
                    PetId=4,
                    MemberId=4,
                    PetName="熊熊",
                    PetType=0,
                    ShapeType=0,
                    PetSex=false,
                    BirthYear=2018,
                    Ligation=true,
                    Vaccine=true,
                    CreateTime=DateTime.Now
                },
                new PetInfomation
                {
                    PetId=5,
                    MemberId=4,
                    PetName="Hippo",
                    PetType=1,
                    ShapeType=0,
                    PetSex=false,
                    BirthYear=2019,
                    Ligation=true,
                    Vaccine=true,
                    CreateTime=DateTime.Now
                }
            };
        }
        public static List<Disposition> Dispositions()
        {
            return new List<Disposition>
            {
                new Disposition{DispositionId=1, DispositionType="活潑" },
                new Disposition{DispositionId=2,DispositionType="友善" },
                new Disposition{DispositionId=3,DispositionType="內向" },
                new Disposition{DispositionId=4,DispositionType="黏人" },
                new Disposition{DispositionId=5,DispositionType="敏感" },
                new Disposition{DispositionId=6,DispositionType="固執" },
                new Disposition{DispositionId=7,DispositionType="頑皮" },
                new Disposition{DispositionId=8,DispositionType="膽小" },
                new Disposition{DispositionId=9,DispositionType="暴力" },
                new Disposition{DispositionId=10,DispositionType="好奇" },
            };
        }
        public static List<PetDisposition> PetDispositions()
        {
            return new List<PetDisposition>
            {
                new PetDisposition{ PetDispositionId=1,DispositionType=1,PetId=1},
                new PetDisposition{ PetDispositionId=2,DispositionType=1,PetId=2},
                new PetDisposition{ PetDispositionId=3,DispositionType=2,PetId=2},
                new PetDisposition{ PetDispositionId=4,DispositionType=1,PetId=3},
                new PetDisposition{ PetDispositionId=5,DispositionType=3,PetId=4},
                new PetDisposition{ PetDispositionId=6,DispositionType=1,PetId=5},
                new PetDisposition{ PetDispositionId=7,DispositionType=2,PetId=5},
            };
        }

        public static List<RegisterSitter> RegisterSitters()
        {
            return new List<RegisterSitter>
            {
                new RegisterSitter
                {
                    SitterId=1,
                    MemberId=3,
                    Id="F120120120",
                    Idimagefont="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp",
                    Idimageback="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp",
                    Score=90,
                    CreateTime=new DateTime(2018,09,09),
                    VerifyTime=DateTime.Now,
                    SitterName="傑克是你嗎",
                    SitterPicture="https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg",
                    SitterInfo="I'm Jake",
                    Bank="822",
                    Account="1111111",
                    RegisterStatus=1,
                    PetExperience="我養過狗"
                },
                new RegisterSitter
                {
                    SitterId=2,
                    MemberId=4,
                    Id="F220120120",
                    Idimagefont="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp",
                    Idimageback="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp",
                    Score=80,
                    CreateTime=new DateTime(2015,03,03),
                    VerifyTime=DateTime.Now,
                    SitterName="溫蒂你好嗎",
                    SitterPicture="https://raw.githubusercontent.com/Ning0809/FileStorage/801f179b51b3724ec0150c65ccff42b34de88980/pug.jpg",
                    SitterInfo="I'm Wendy",
                    Bank="808",
                    Account="222222222",
                    RegisterStatus=1,
                    PetExperience="我養過貓"
                }
            };
        }
        public static List<Aptitude> Aptitudes()
        {
            return new List<Aptitude>
            {
                new Aptitude{AptitudeId=1, MemberId=3,AptitudeExtrovert=60,AptitudeIntrovert=70,AptitudeThinking=50,AptitudeFeeling=20 },
                new Aptitude{AptitudeId=2,MemberId=4,AptitudeExtrovert=80,AptitudeIntrovert=40,AptitudeThinking=60,AptitudeFeeling=90 },
            };
        }

        public static List<Product> Products()
        {
            return new List<Product>
            {
                new Product
                {
                    ProductId=1,
                    ServiceType=1,
                    Introduce="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",
                    SitterId=3,
                    ProductStatus=0,
                    CreateTime=DateTime.Now,
                    EditTime=DateTime.Now,
                    IsDelete=false
                },
                new Product
                {
                    ProductId=2,
                    ServiceType=2,
                    Introduce="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",
                    SitterId=3,
                    ProductStatus=0,
                    CreateTime=DateTime.Now,
                    EditTime=DateTime.Now,
                    IsDelete=false
                },

                new Product
                {
                    ProductId=3,
                    ServiceType=3,
                    Introduce="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",
                    SitterId=3,
                    ProductStatus=1,
                    CreateTime=DateTime.Now,
                    EditTime=DateTime.Now,
                    IsDelete=false
                },
                new Product
                {
                    ProductId=4,
                    ServiceType=1,
                    Introduce="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",
                    SitterId=4,
                    ProductStatus=0,
                    CreateTime=DateTime.Now,
                    EditTime=DateTime.Now,
                    IsDelete=false
                },
                new Product
                {
                    ProductId=5,
                    ServiceType=2,
                    Introduce="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",
                    SitterId=4,
                    ProductStatus=0,
                    CreateTime=DateTime.Now,
                    EditTime=DateTime.Now,
                    IsDelete=false
                },


            };
        }
        public static List<ProductImage> ProductImages()
        {
            return new List<ProductImage>
            {
                new ProductImage
                {
                    ImagesId=1,
                    ProductId=1,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    Sort=1
                },
                new ProductImage
                {
                    ImagesId=2,
                    ProductId=1,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp",
                    Sort=2
                },
                new ProductImage
                {
                    ImagesId=3,
                    ProductId=1,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp",
                    Sort=3
                },
                new ProductImage
                {
                    ImagesId=4,
                    ProductId=2,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    Sort=2
                },
                new ProductImage
                {
                    ImagesId=5,
                    ProductId=2,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp",
                    Sort=1
                },
                new ProductImage
                {
                    ImagesId=6,
                    ProductId=2,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp",
                    Sort=3
                },
                new ProductImage
                {
                    ImagesId=7,
                    ProductId=3,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    Sort=2
                },
                new ProductImage
                {
                    ImagesId=8,
                    ProductId=3,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp",
                    Sort=3
                },
                new ProductImage
                {
                    ImagesId=9,
                    ProductId=3,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp",
                    Sort=1
                },
                new ProductImage
                {
                    ImagesId=10,
                    ProductId=4,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    Sort=1
                },
                new ProductImage
                {
                    ImagesId=11,
                    ProductId=4,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp",
                    Sort=3
                },
                new ProductImage
                {
                    ImagesId=12,
                    ProductId=4,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp",
                    Sort=2
                },
                new ProductImage
                {
                    ImagesId=13,
                    ProductId=5,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    Sort=2
                },
                new ProductImage
                {
                    ImagesId=14,
                    ProductId=5,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample3.webp",
                    Sort=3
                },
                new ProductImage
                {
                    ImagesId=15,
                    ProductId=5,
                    Image="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample5.webp",
                    Sort=1
                },


            };
        }
        public static List<ProductServiceArea> ProductServiceAreass()
        {
            return new List<ProductServiceArea>
            {
                new ProductServiceArea{ ProductServiceAreaId=1,ProductId=1,County=1,District=1},
                new ProductServiceArea{ ProductServiceAreaId=2,ProductId=1,County=1,District=2},
                new ProductServiceArea{ ProductServiceAreaId=3,ProductId=2,County=1,District=1},
                new ProductServiceArea{ ProductServiceAreaId=4,ProductId=2,County=1,District=2},
                new ProductServiceArea{ ProductServiceAreaId=5,ProductId=2,County=2,District=3},
                new ProductServiceArea{ ProductServiceAreaId=6,ProductId=3,County=2,District=4},
                new ProductServiceArea{ ProductServiceAreaId=7,ProductId=3,County=2,District=3},
                new ProductServiceArea{ ProductServiceAreaId=8,ProductId=4,County=2,District=4},
                new ProductServiceArea{ ProductServiceAreaId=9,ProductId=4,County=2,District=3},
                new ProductServiceArea{ ProductServiceAreaId=10,ProductId=5,County=2,District=4},
                new ProductServiceArea{ ProductServiceAreaId=11,ProductId=5,County=2,District=3},

            };
        }
        public static List<ProductServicePetType> ProductServicePetTypes()
        {
            return new List<ProductServicePetType>
            {
                new ProductServicePetType
                {
                    ProductServicePetTypeId=1,
                    ProductId=1,
                    PetType=0,
                    ShapeType=0,
                    Price=300m,
                    OvernightPrice=500m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=2,
                    ProductId=1,
                    PetType=0,
                    ShapeType=1,
                    Price=300m,
                    OvernightPrice=500m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=3,
                    ProductId=1,
                    PetType=0,
                    ShapeType=2,
                    Price=300m,
                    OvernightPrice=500m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=4,
                    ProductId=1,
                    PetType=0,
                    ShapeType=3,
                    Price=500m,
                    OvernightPrice=800m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=5,
                    ProductId=1,
                    PetType=1,
                    ShapeType=0,
                    Price=500m,
                    OvernightPrice=800m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=6,
                    ProductId=1,
                    PetType=1,
                    ShapeType=1,
                    Price=500m,
                    OvernightPrice=800m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=7,
                    ProductId=1,
                    PetType=1,
                    ShapeType=2,
                    Price=500m,
                    OvernightPrice=800m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=8,
                    ProductId=2,
                    PetType=0,
                    ShapeType=0,
                    Price=450m,
                    OvernightPrice=800m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=9,
                    ProductId=2,
                    PetType=1,
                    ShapeType=0,
                    Price=450m,
                    OvernightPrice=800m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=10,
                    ProductId=3,
                    PetType=0,
                    ShapeType=3,
                    Price=500m,
                    OvernightPrice=800m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=11,
                    ProductId=3,
                    PetType=0,
                    ShapeType=4,
                    Price=500m,
                    OvernightPrice=800m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=12,
                    ProductId=4,
                    PetType=0,
                    ShapeType=0,
                    Price=600m,
                    OvernightPrice=1200m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=13,
                    ProductId=4,
                    PetType=0,
                    ShapeType=1,
                    Price=600m,
                    OvernightPrice=1200m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=14,
                    ProductId=4,
                    PetType=0,
                    ShapeType=2,
                    Price=600m,
                    OvernightPrice=1200m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=15,
                    ProductId=5,
                    PetType=0,
                    ShapeType=2,
                    Price=400m,
                    OvernightPrice=1200m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=16,
                    ProductId=5,
                    PetType=0,
                    ShapeType=3,
                    Price=400m,
                    OvernightPrice=1200m
                },
                new ProductServicePetType
                {
                    ProductServicePetTypeId=17,
                    ProductId=5,
                    PetType=0,
                    ShapeType=4,
                    Price=400m,
                    OvernightPrice=1200m
                }
            };
        }
        public static List<ProductServiceTime> ProductServiceTimes()
        {
            return new List<ProductServiceTime>
            {
                new ProductServiceTime{ ProductServiceTimeId=1,ProductId=1,ServiceDay=1,ServicePartTime=1},
                new ProductServiceTime{ ProductServiceTimeId=2,ProductId=1,ServiceDay=1,ServicePartTime=2},
                new ProductServiceTime{ ProductServiceTimeId=3,ProductId=1,ServiceDay=2,ServicePartTime=1},
                new ProductServiceTime{ ProductServiceTimeId=4,ProductId=1,ServiceDay=2,ServicePartTime=2},
                new ProductServiceTime{ ProductServiceTimeId=5,ProductId=1,ServiceDay=3,ServicePartTime=1},
                new ProductServiceTime{ ProductServiceTimeId=6,ProductId=1,ServiceDay=3,ServicePartTime=2},
                new ProductServiceTime{ ProductServiceTimeId=7,ProductId=2,ServiceDay=4,ServicePartTime=2},
                new ProductServiceTime{ ProductServiceTimeId=8,ProductId=2,ServiceDay=4,ServicePartTime=3},
                new ProductServiceTime{ ProductServiceTimeId=9,ProductId=2,ServiceDay=5,ServicePartTime=2},
                new ProductServiceTime{ ProductServiceTimeId=10,ProductId=2,ServiceDay=5,ServicePartTime=3},
                new ProductServiceTime{ ProductServiceTimeId=11,ProductId=3,ServiceDay=6,ServicePartTime=1},
                new ProductServiceTime{ ProductServiceTimeId=12,ProductId=3,ServiceDay=6,ServicePartTime=1},
                new ProductServiceTime{ ProductServiceTimeId=13,ProductId=4,ServiceDay=1,ServicePartTime=3},
                new ProductServiceTime{ ProductServiceTimeId=14,ProductId=4,ServiceDay=2,ServicePartTime=3},
                new ProductServiceTime{ ProductServiceTimeId=15,ProductId=4,ServiceDay=3,ServicePartTime=3},
                new ProductServiceTime{ ProductServiceTimeId=16,ProductId=5,ServiceDay=6,ServicePartTime=1},
                new ProductServiceTime{ ProductServiceTimeId=17,ProductId=5,ServiceDay=6,ServicePartTime=2},
                new ProductServiceTime{ ProductServiceTimeId=18,ProductId=5,ServiceDay=0,ServicePartTime=1},
                new ProductServiceTime{ ProductServiceTimeId=19,ProductId=5,ServiceDay=0,ServicePartTime=2},
            };
        }
        public static List<ProductDiscount> ProductDiscounts()
        {
            return new List<ProductDiscount>
            {
                new ProductDiscount{ ProductDiscountId=1, ProductId=1,Quantity=3,Discount=0.8m},
                new ProductDiscount{ ProductDiscountId=2,ProductId=2,Quantity=2,Discount=0.9m},
                new ProductDiscount{ ProductDiscountId=3,ProductId=4,Quantity=4,Discount=0.6m},

            };

        }

        public static List<AdProject> AdProject()
        {
            return new List<AdProject>
            {
                new AdProject
                {
                    AdProjectId=1,
                    ProductId=1,
                    BeginDate=new DateTime(2022,09,01),
                    EndDate=new DateTime(2022,09,30),
                    Price=300m
                },
                new AdProject
                {
                    AdProjectId=2,
                    ProductId=4,
                    BeginDate=new DateTime(2022,09,01),
                    EndDate=new DateTime(2022,09,30),
                    Price=300m
                },

            };

        }

        public static List<Cart> Carts()
        {
            return new List<Cart>
            {
                new Cart{ CartId = 1, ProductId=1,CustomerId=1,CreateTime=DateTime.Now,County=1,District=1},
                new Cart{ CartId = 2, ProductId=3,CustomerId=2,CreateTime=DateTime.Now,County=2,District=1},
            };
        }

        public static List<CartDetail> CartDetails()
        {
            return new List<CartDetail>
            {
                new CartDetail
                {
                    CartDetailId = 1,
                    CartId=1,
                    PetType=0,
                    ShapeType=0,
                },
                new CartDetail
                {
                    CartDetailId = 2,
                    CartId=1,
                    PetType=1,
                    ShapeType=0,
                },
                new CartDetail
                {
                    CartDetailId = 3,
                    CartId=2,
                    PetType=0,
                    ShapeType=2,
                }
            };
        }

        public static List<CartSchedule> CartSchedules()
        {
            return new List<CartSchedule>
            {
                new CartSchedule{ CartScheduleId=1,CartId=1,ServiceDate=new DateTime(2022,09,20),Schedule=1},
                new CartSchedule{ CartScheduleId=2,CartId=1,ServiceDate=new DateTime(2022,09,20),Schedule=2},
                new CartSchedule{ CartScheduleId=3,CartId=2,ServiceDate=new DateTime(2022,09,27),Schedule=3},
                new CartSchedule{ CartScheduleId=4,CartId=2,ServiceDate=new DateTime(2022,09,27),Schedule=4},
            };
        }

        public static List<InvoiceType> InvoiceTypes()
        {
            return new List<InvoiceType>
            {
                new InvoiceType{ InvoiceTypeId=1,TypeName="電子發票"},
                new InvoiceType{ InvoiceTypeId=2,TypeName="公司發票"}
            };
        }
        public static List<Order> Orders()
        {
            return new List<Order>
            {
                new Order
                {
                    OrderId=1,
                    OrderNumber="KS0001",
                    InvoiceType=1,
                    InvoiceId="AB11111111",
                    ProductId=1,
                    SitterId=3,
                    CustomerId=1,
                    CreateTime=new DateTime(2022,08,15),
                    BeginTime=new DateTime(2022,09,20,06,00,00),
                    EndTime=new DateTime(2022,09,20,06,59,00),
                    OrderStatus=0,
                    Amount=600m,
                    Discount=0m,
                    BookingName="Felix",
                    BookingPhone="0910555666",
                    BookingEmail="felix@gmail.com",
                    Address="中華路二段",
                    Name="Felix",
                    Phone="0910555666",
                    SitterName="Jake",
                    ProductName="到府照顧",
                    ProductImageUrl="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    ProductIntro="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",

                },
                new Order
                {
                    OrderId=2,
                    OrderNumber="KS0002",
                    InvoiceType=2,
                    InvoiceId="AB11111111",
                    UnoformNumber="12345678",
                    CompanyName="日日毛掌公司",
                    ProductId=5,
                    SitterId=4,
                    CustomerId=2,
                    CreateTime=new DateTime(2022,09,02),
                    BeginTime=new DateTime(2022,09,27,12,00,00),
                    EndTime=new DateTime(2022,09,27,12,59,00),
                    OrderStatus=0,
                    Amount=800m,
                    Discount=0.8m,
                    BookingName="Anna",
                    BookingPhone="0910888999",
                    BookingEmail="anna@gmail.com",
                    Address="德光路一段",
                    Name="Anna",
                    Phone="0910888999",
                    SitterName="Wendy",
                    ProductName="到府洗澡",
                    ProductImageUrl="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    ProductIntro="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",
                 },
                new Order
                {
                    OrderId=3,
                    OrderNumber="KS0003",
                    InvoiceType=1,
                    InvoiceId="AB11111112",
                    ProductId=5,
                    SitterId=4,
                    CustomerId=2,
                    CreateTime=new DateTime(2022,09,02),
                    BeginTime=new DateTime(2022,09,26,12,00,00),
                    EndTime=new DateTime(2022,09,26,12,59,00),
                    OrderStatus=1,
                    Amount=800m,
                    Discount=0.8m,
                    BookingName="Anna",
                    BookingPhone="0910888999",
                    BookingEmail="anna@gmail.com",
                    Address="德光路一段",
                    Name="Anna",
                    Phone="0910888999",
                    SitterName="Wendy",
                    ProductName="到府洗澡",
                    ProductImageUrl="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    ProductIntro="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",
                    },
                new Order
                {
                    OrderId=4,
                    OrderNumber="KS0004",
                    InvoiceType=1,
                    InvoiceId="AB11111115",
                    ProductId=1,
                    SitterId=3,
                    CustomerId=1,
                    CreateTime=new DateTime(2022,09,24),
                    BeginTime=new DateTime(2022,10,14,06,00,00),
                    EndTime=new DateTime(2022,10,14,06,59,00),
                    OrderStatus=0,
                    Amount=600m,
                    Discount=0m,
                    BookingName="Felix",
                    BookingPhone="0910555666",
                    BookingEmail="felix@gmail.com",
                    Address="中華路二段",
                    Name="Felix",
                    Phone="0910555666",
                    SitterName="Jake",
                    ProductName="到府照顧",
                    ProductImageUrl="https://raw.githubusercontent.com/Ning0809/FileStorage/main/dogsample2.webp",
                    ProductIntro="毛小愛Fluv是一個為現代毛爸媽打造的寵物保姆平台APP，使用美國寵物保姆訓練課程和AI數據媒合寵物保姆，所有保姆、美容師、遛狗師等皆有通過「真心愛動物審核」，確保毛小孩會像家人般被對待—一對一、不關籠、全程拍照。為了貫徹我們「愛」的理念，毛小愛同時協助訓練再度中年婦女成為寵物保姆找到再次工作機會和與動物救援機構合作，使用部分營收中途浪浪和協助送養！",
                }
            };
        }

        public static List<OrderPetDetail> OrderPetDetails()
        {
            return new List<OrderPetDetail>
            {
                new OrderPetDetail
                {
                    OrderPetId=1,
                    OrderId=1,
                    PetName="糖糖",
                    PetType=0,
                    ShapeType=2,
                    PetSex=false,
                    BirthYear=2022,
                    Ligation=false,
                    Vaccine=true,
                    UnitPrice=300m,
                    ServiceCount=2
                },
                new OrderPetDetail
                {
                    OrderPetId=2,
                    OrderId=2,
                    PetName="豆皮",
                    PetType=1,
                    ShapeType=1,
                    PetSex=false,
                    BirthYear=2017,
                    Ligation=true,
                    Vaccine=true,
                    UnitPrice=400m,
                    ServiceCount=2
                },
                new OrderPetDetail
                {
                    OrderPetId=3,
                    OrderId=3,
                    PetName="豆皮",
                    PetType=1,
                    ShapeType=1,
                    PetSex=false,
                    BirthYear=2017,
                    Ligation=true,
                    Vaccine=true,
                    UnitPrice=400m,
                    ServiceCount=2
                },
                new OrderPetDetail
                {
                    OrderPetId=4,
                    OrderId=4,
                    PetName="Hippo",
                    PetType=0,
                    ShapeType=2,
                    PetSex=false,
                    BirthYear=2022,
                    Ligation=false,
                    Vaccine=true,
                    UnitPrice=300m,
                    ServiceCount=2
                }

            };
        }

        public static List<OrderSchedule> OrderSchedules()
        {
            return new List<OrderSchedule>
            {
                new OrderSchedule{ OrderScheduleId=1,OrderId=1,ServiceDate=new DateTime(2022,09,20),Schedule=1},
                new OrderSchedule{ OrderScheduleId=2,OrderId=1,ServiceDate=new DateTime(2022,09,20),Schedule=2},
                new OrderSchedule{ OrderScheduleId=3,OrderId=2,ServiceDate=new DateTime(2022,09,27),Schedule=3},
                new OrderSchedule{ OrderScheduleId=4,OrderId=2,ServiceDate=new DateTime(2022,09,27),Schedule=4},
                new OrderSchedule{ OrderScheduleId=5,OrderId=3,ServiceDate=new DateTime(2022,09,26),Schedule=4},
                new OrderSchedule{ OrderScheduleId=6,OrderId=3,ServiceDate=new DateTime(2022,09,26),Schedule=4},
                new OrderSchedule{ OrderScheduleId=7,OrderId=4,ServiceDate=new DateTime(2022,10,14),Schedule=1},
                new OrderSchedule{ OrderScheduleId=8,OrderId=4,ServiceDate=new DateTime(2022,10,14),Schedule=2},
            };
        }


        public static List<Evaluation> Evaluations()
        {
            return new List<Evaluation>
            {
                new Evaluation{EvaluationId=1, OrderId=2,UserId=2,UserType=1,EvaluationScore=4,Message="讚",CreateTime=new DateTime(2022,9,11)},
                new Evaluation{EvaluationId=2, OrderId=2,UserId=4,UserType=2,EvaluationScore=5,Message="狗很乖",CreateTime=new DateTime(2022,9,12)},
            };
        }


        public static List<Role> Roles()
        {
            return new List<Role>
            {
                new Role{ RoleId=1,RoleName="NormalUser"},
                new Role{ RoleId=2,RoleName="SitterUser"},
                new Role{ RoleId=3,RoleName= "Administrator" },
            };
        }

        public static List<UserRole> UserRoles()
        {
            return new List<UserRole>
            {
                new UserRole{ UserRoleId=1,UserId=1,RoleType=1},
                new UserRole{ UserRoleId=2,UserId=2,RoleType=1},
                new UserRole{ UserRoleId=3,UserId=3,RoleType=2},
                new UserRole{ UserRoleId=4,UserId=4,RoleType=2},
            };
        }

        public static List<OrderCancel> OrderCancels()
        {
            return new List<OrderCancel>
            {
                new OrderCancel{ OrderCancelId=1,OrderId=3,CancelDate=new DateTime(2022,09,03),CancelReason="訂錯日期",RefundPersent=1m}
            };
        }

    }
}
