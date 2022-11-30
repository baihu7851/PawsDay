using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace Infrastructure.Services
{
    public class UploadImageService
    {
        //建構式

        private IConfiguration _configuration;
        Cloudinary cloudinary;
        public UploadImageService(IConfiguration configuration)
        {
            _configuration = configuration;
            Account account = new Account(
                _configuration.GetSection("Cloudinary:cloud_name").Value,
                _configuration.GetSection("Cloudinary:api_key").Value,
                _configuration.GetSection("Cloudinary:api_secret").Value
                    ) ;

            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
        }

        public Infra_ResultDto Upload(List<IFormFile> file)
        {
            var response = new Infra_ResultDto();

            List<string> imgs = new List<string>();

            foreach (var item in file)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(item.FileName, item.OpenReadStream())
                };
                 var uploadResult = cloudinary.Upload(uploadParams).SecureUrl.OriginalString.ToString();
                
                imgs.Add(uploadResult);

                var type = item.ContentType;
                if (!type.Contains("image"))
                {
                    response.Message = "檔案格式錯誤，請上傳圖檔";
                    return response;
                }
                else if (imgs.Count == 0)
                {
                    response.Message = "檔案上傳失敗";
                    return response;
                }
            }
            return new Infra_ResultDto(imgs);
        }


    }
}
