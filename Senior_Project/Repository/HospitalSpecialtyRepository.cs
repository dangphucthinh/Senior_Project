using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Doctor_Appointment.Repository
{
    public class HospitalSpecialtyRepository
    {
        public ApplicationDbContext db;

        public HospitalSpecialtyRepository()
        {
            this.db = new ApplicationDbContext();
        }

        public async Task<IEnumerable<HospitalSpecialty>> GetAllHospitalSpecialty()
        {
            return await this.db.hospitalSpecialties.ToListAsync();
        }

        public async Task<IEnumerable<HospitalSpecialty>> GetHospitalSpecialtyBySearch(string searchPhrase)
        {
            return await this.db.hospitalSpecialties.Where(h => h.Name.Contains(searchPhrase)).ToListAsync();
        }

        //public async Task<IEnumerable<Specialty>> GetAllSpecialties(int HsId)
        //{
        //    return await this.db.specialties.Where(s => s.HsId == HsId).ToListAsync();
        //}

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

        public string UpdateImage(HttpContext context)
        {

            var hosSpecId = context.Request.Form["Id"];

            var user = db.hospitalSpecialties.FirstOrDefault(x => x.Id.ToString() == hosSpecId);
            user.Name = context.Request.Form["Name"];

            var imgHosImg = UploadAndGetImage(context.Request.Files[0]);
            user.HosSpecImg = imgHosImg;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }

            return "asdads";


        }
    }
}