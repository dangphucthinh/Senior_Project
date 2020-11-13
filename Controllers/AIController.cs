using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.AI;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
    [RoutePrefix("api/AI")]
    public class AIController : ApiController
    {
        [HttpPost]
        [Route("Chatbot")]
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
                status = 0,
                message = ResponseMessages.Success,
                data = dataReturn
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

            var dataReturn2 = "Pediatrics";

            var doctors = await new DoctorRepository().GetDoctorInfoBySpecialtyName(dataReturn2);

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = new
                {
                    disease = dataReturn,
                    spec = dataReturn2,
                    doctors = doctors
                }
               
            });
        }
    }
}
