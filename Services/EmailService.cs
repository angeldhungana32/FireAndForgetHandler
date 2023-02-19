using FireAndForgetHandler.Model.Dto;

namespace FireAndForgetHandler.Services
{
    public class EmailService 
    {
        public EmailService() { }

        public async Task SendEmailAsync(EmailRequest email) { 
        
            Console.WriteLine($"Mocking sending email to {email.EmailAddress}");

            await Task.Delay(5000);
        }
    }
}
