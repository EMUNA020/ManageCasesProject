using BL.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface INotificationService
    {
        Task SendCaseCreationNotificationAsync(List<string> parties, string caseId);

    }
    public class NotificationService : INotificationService
    {
        public async Task SendCaseCreationNotificationAsync(List<string> parties, string caseId)
        {
            foreach (var party in parties)
            {
                await SendEmailAsync(party, caseId);
            }
        }

        private async Task SendEmailAsync(string email, string caseId)
        {
            // This function would use an email service to send the notification
            Console.WriteLine($"Sending email to {email} regarding Case ID: {caseId}");
            await Task.Delay(500); // Simulate async email sending process
        }
    }
}
