using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;


namespace Doctor_Appointment.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();

            myMessage.AddTo(message.Destination);
            myMessage.From = new EmailAddress("dangphucthinha9@gmail.com");
            myMessage.Subject = message.Subject;
            myMessage.HtmlContent = message.Body;

            // Create a Web transport for sending email.
            var client = new SendGridClient("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");


            await client.SendEmailAsync(myMessage);
        }
    }
}