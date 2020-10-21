using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.User;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
    [RoutePrefix("api/Image")]
    public class ImageController : ApiController
    {
        private readonly Cloudinary _cloudinary; 

        public ImageController()
        {
        }

        [HttpPost]
        [Route("CreateImage")]
        public async Task<IHttpActionResult> CreateImage()
        {
            new PatientRepository().UpdateUser(HttpContext.Current);
            //var image = HttpContext.Current.Request.Files[0];

            //new PatientRepository().UploadAndGetImage(image);


             return Ok(new Response
             {
                 status = 0,
                 message = ResponseMessages.Success,
                 data = {}
             });
        }
    }

    public class FileClass
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
