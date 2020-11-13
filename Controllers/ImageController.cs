﻿//using CloudinaryDotNet;
//using Doctor_Appointment.Models;
//using Doctor_Appointment.Models.DTO.AI;
//using Doctor_Appointment.Repository;
//using Doctor_Appointment.Utils.Constant;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
//using System.Web.UI;

//namespace Doctor_Appointment.Controllers
//{
//    [RoutePrefix("api/Image")]
//    public class ImageController : ApiController
//    {
 
//        public ImageController()
//        {
//        }

//        [HttpPost]
//        [Route("CreateImage")]
//        public async Task<IHttpActionResult> CreateImage()
//        {
//            //new PatientRepository().UpdateUser(HttpContext.Current);
//            //var image = HttpContext.Current.Request.Files[0];

//            //new PatientRepository().UploadAndGetImage(image);


//             return Ok(new Response
//             {
//                 status = 0,
//                 message = ResponseMessages.Success,
//                 data = {}
//             });
//        }

//        [HttpPost]
//        [Route("TestChatbot")]
//        public async Task<IHttpActionResult> TestChatbot(ChatbotDataDTO chatbotData)
//        {
//            var json = JsonConvert.SerializeObject(chatbotData);
//            var data = new StringContent(json, Encoding.UTF8, "application/json");

//            HttpClient client = new HttpClient();
//            HttpResponseMessage response = await client.PostAsync("http://localhost:8000/chatbot", data);
//            response.EnsureSuccessStatusCode();
//            var responseBody = await response.Content.ReadAsStringAsync();

//            var dataReturn = JObject.Parse(responseBody)["response"].ToString();

//            return Ok(new
//            {
//                result = dataReturn
//            });
//        }

//        [HttpPost]
//        [Route("Prediction")]
//        public async Task<IHttpActionResult> Prediction(PredictionDTO predictionDTO)
//        {
//            var json = JsonConvert.SerializeObject(predictionDTO);
//            var data = new StringContent(json, Encoding.UTF8, "application/json");

//            HttpClient client = new HttpClient();
//            HttpResponseMessage response = await client.PostAsync("http://localhost:8000/predict", data);
//            response.EnsureSuccessStatusCode();
//            var responseBody = await response.Content.ReadAsStringAsync();

//            var dataReturn = JObject.Parse(responseBody)["response"].ToString();

//            return Ok(new
//            {
//                result = dataReturn
//            });
//        }
//    }

//    public class FileClass
//    {
//        public string Name { get; set; }
//        public byte[] Content { get; set; }
//    }
//}
