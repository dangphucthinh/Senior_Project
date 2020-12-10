using CloudinaryDotNet;
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.AI;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using SendGrid;
using SendGrid.Helpers.Mail;
using Doctor_Appointment.Models.DTO.Email;
using System;
using System.IO;
using System.Collections.Generic;
using CloudinaryDotNet.Actions;
using System.Linq;

namespace Doctor_Appointment.Controllers
{
    [RoutePrefix("api/Image")]
    public class ImageController : ApiController
    {

        public ImageController()
        {
        }

        [HttpPost]
        [Route("CreateImage")]
        public async Task<IHttpActionResult> CreateImage()
        {

            string content = "Xin Chào ";
            var message = new Message(new string[] { "dangphucthinh.lfc@gmail.com" }, "StoreManagenment", content);
            await new EmailRepository().SendEmailAsync(message);


            //new PatientRepository().UpdateUser(HttpContext.Current);
            //var image = HttpContext.Current.Request.Files[0];

            //new PatientRepository().UploadAndGetImage(image);
            //await new EmailService().SendAsync(new IdentityMessage {
            //    Destination="dangphucthinh.lfc@gmail.com", 
            //    Body ="dsfsdf",
            //    Subject ="asfdsfsdf"});

            return Ok();
        }

        //private class FileInfo
        [HttpGet]
        [Route("UploadImages")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> UploadImages()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var outPutDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(outPutDirectory, "Images\\");
            List<FileInfo> listFile = new List<FileInfo>();
            DirectoryInfo d = new DirectoryInfo(configPath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
            //string str = "";
            foreach (FileInfo file in Files)
            {
                listFile.Add(file);
            }
            
            foreach(var f in listFile)
            {
                byte[] buff = null;
                FileStream fs = new FileStream(f.FullName,
                                               FileMode.Open,
                                               FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = f.Length;
                buff = br.ReadBytes((int)numBytes);
                Account acc = new Account(
                "deh0sqxwl",
                "212524559265538",
                "1p5EO6Mj_IBdALes5ke3wUMMw6w");
                var _cloudinary = new Cloudinary(acc);

                var uploadResult = new ImageUploadResult();


                if (numBytes > 0)
                {
                    MemoryStream stream = new MemoryStream(buff);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(f.Name, stream),
                        Transformation = new Transformation()
                                .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);

                    string ext = f.Extension;
                    string name_no_ext = f.Name.Replace(ext, "");
                    var user = db.Users.FirstOrDefault(u => u.FirstName + " " + u.LastName == name_no_ext);
                    if (user != null)
                        user.Avatar = uploadResult.Url.ToString();
                    //var user  = new ApplicationUser() { }
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return Ok();
            }
            //StreamReader r = new StreamReader(configPath);
            return Ok();
        }

        [HttpPost]
        [Route("TestChatbot")]
        public async Task<IHttpActionResult> TestChatbot(ChatbotDataDTO chatbotData)
        {
            var json = JsonConvert.SerializeObject(chatbotData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("http://localhost:8000/chatbot", data);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var dataReturn = JObject.Parse(responseBody)["response"].ToString();

            return Ok(new
            {
                result = dataReturn
            });
        }

        [HttpPost]
        [Route("Prediction")]
        public async Task<IHttpActionResult> Prediction(PredictionDTO predictionDTO)
        {
            var json = JsonConvert.SerializeObject(predictionDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("http://localhost:8000/predict", data);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var dataReturn = JObject.Parse(responseBody)["response"].ToString();

            return Ok(new
            {
                result = dataReturn
            });
        }
    }

    public class FileClass
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
