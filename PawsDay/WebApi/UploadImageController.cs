using CloudinaryDotNet.Actions;
using Infrastructure.Model;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PawsDay.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        private readonly UploadImageService _uploadImageService;
        public UploadImageController(UploadImageService uploadImageService)
        {
            _uploadImageService = uploadImageService;
        }

        [HttpPost]
        public ActionResult<Infra_ResultDto> UploadImage([FromForm] List<IFormFile> file)
        {
            var response = _uploadImageService.Upload(file);
            return response;
        }
    }
}
