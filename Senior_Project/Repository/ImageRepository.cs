using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Doctor_Appointment.Infrastucture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Repository
{
    public class ImageRepository
    {
        public ApplicationDbContext db;

        public ImageRepository()
        {
            this.db = new ApplicationDbContext();
        }
        public string UploadAndGetImage(HttpPostedFile file)
        {
            BinaryReader br = new BinaryReader(file.InputStream);
            byte[] ImageBytes = br.ReadBytes((Int32)file.InputStream.Length);

            Account acc = new Account(
                "deh0sqxwl",
                "212524559265538",
                "1p5EO6Mj_IBdALes5ke3wUMMw6w");
            var _cloudinary = new Cloudinary(acc);

            var uploadResult = new ImageUploadResult();


            if (file.ContentLength > 0)
            {
                MemoryStream stream = new MemoryStream(ImageBytes);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                };

                uploadResult = _cloudinary.Upload(uploadParams);
            }

            return uploadResult.Url.ToString();
        }
    }
}