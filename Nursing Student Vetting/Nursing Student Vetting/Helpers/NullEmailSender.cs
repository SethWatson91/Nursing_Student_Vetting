using Microsoft.AspNetCore.Identity.UI.Services;


namespace Nursing_Student_Vetting.Helpers
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Do nothing (no-op implementation)
            return Task.CompletedTask;
        }
    }
}
