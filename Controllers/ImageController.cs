using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        public byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }

        [HttpPost]
        [Route("CreateImage")]
        public async Task<IHttpActionResult> GetAllHospitalSpecialities()
        {
            var s1 = HttpContext.Current.Request.Files[0];

           

            


           
        

            Image image1 = Image.FromFile("E:\\#dallas2019\\aaa.PNG");
            byte[] image = imgToByteArray(image1);

            Account acc = new Account(
                "deh0sqxwl",
                "212524559265538",
                "1p5EO6Mj_IBdALes5ke3wUMMw6w");

            var _cloudinary = new Cloudinary(acc);

            //var image = new File("E:\\#dallas2019\\aaa.PNG").OpenRead();

            var uploadResult = new ImageUploadResult();

            var a = s1.InputStream.ReadByte();
            var b = image;

            if (s1.ContentLength > 0)
            {
                //using (var stream = image)
                MemoryStream stream = new MemoryStream(s1.InputStream.ReadByte());
                var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription("asdasd.png", stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                var s = "123";
                //}
            }

                return Ok();
        }
    }

    public class FileClass
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
