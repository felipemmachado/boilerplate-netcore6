using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task ForgotPassword(string email, string router)
        {
            throw new System.NotImplementedException();
        }
    }
}
