using ApplicationCore.Common;
using PawsDay.Models.SitterCenter;
using PawsDay.ViewModels.MemberCenter;

namespace PawsDay.Services.SitterCenter
{
    public static class SitterCenterResponseHelper
    {
        //共用Response
        public static TransResultDto<T> ReadResponse<T>(T resource)
        {
            var response = new TransResultDto<T>();
            if (resource == null)
            {
                response.Message = "Not Found";
                return response;
            }

            response.IsSuccess = true;
            response.Data = resource;
            return response;
        }



        //sidebar註冊方式
        public static RegisterTypeDTO GetRegisterType(int type)
        {
            switch (type)
            {
                case (int)RegisterTypes.Facebook:
                    return new RegisterTypeDTO { Name = "FaceBook", Image = "~/images/FaceBook.jpg" };
                case (int)RegisterTypes.Line:
                    return new RegisterTypeDTO { Name = "Line", Image = "~/images/line.jpg" };
                case (int)RegisterTypes.Google:
                    return new RegisterTypeDTO { Name = "Google", Image = "~/images/google.png" };
                default:
                    return null;
            }
        }

        public static string GetServiceType(int type)
        {
            switch (type)
            {
                case 1:
                    return "＄200元起／30分鐘\r\n\r\n<br>👉 專業實名認證寵物保姆到府照顧寵物\r\n\r\n<br>👉 寵物保姆、餵食、環境清潔、陪伴玩耍、回報健康狀況、餵藥等客製服務\r\n\r\n<br>👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n\r\n<br>👉 全程與保姆維持連線，回報寵物狀況\r\n\r\n<br>👉 平台預約全程含青杉保險與品質保障\r\n\r\n<br>👉 鑰匙可以溝通警衛代收、信箱傳遞等方式";
                case 2:
                    return "＄300元起／1小時起／1隻毛孩\r\n    \r\n    <br>👉 寵物免出門! 寵物美容師攜帶工具到府幫寵物做小美容\r\n    \r\n    <br>👉 小美容包含洗澡、按摩、剪指甲、清耳朵、擠肛門腺、修腳底毛、修屁股毛、含環境清理\r\n    \r\n    <br>👉 服務前美容師會先跟毛孩培養感情、餵零食\r\n    \r\n    <br>👉 若有特殊需求或是疾病毛孩，請先與美容師溝通\r\n    \r\n    <br>👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n    \r\n    <br>👉 全程與保姆維持連線，回報寵物狀況\r\n    \r\n    <br>👉 平台預約全程含青杉保險與品質保障";
                default:
                    return "＄100元起／30分鐘起\r\n\r\n<br>👉 無法掌控回家時間? 保姆可到府帶狗狗出門散步\r\n\r\n<br>👉 出門不能帶狗狗進餐廳? 保姆可約地點接狗狗散步\r\n\r\n<br>👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n\r\n<br>👉 全程與保姆維持連線，回報寵物狀況\r\n\r\n<br>👉 平台預約全程含青杉保險與品質保障\r\n\r\n<br>👉 鑰匙可以溝通警衛代收、信箱傳遞等方式";
            }
        }


    }
}
