using System.Threading.Tasks;
using Twilio.OwlFinance.Domain.Model;
using Twilio.OwlFinance.Domain.Model.Api;

namespace Twilio.OwlFinance.Domain.Interfaces.Services
{
    public interface IPingService
    {
        Task<ApiResponse<PingModel>> VerifyPing();
    }
}
