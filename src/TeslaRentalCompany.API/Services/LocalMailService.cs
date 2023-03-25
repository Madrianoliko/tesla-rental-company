namespace TeslaRentalCompany.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string mailTo = String.Empty;
        private readonly string mailFrom = String.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            mailTo = configuration["mailSettings:mailToAddress"];
            mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            // send mail -- output to console window
            Console.WriteLine($"Mail from {mailFrom} to {mailTo}, " +
                $"with {nameof(IMailService)}.");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Message : {message}");
        }
    }
}
