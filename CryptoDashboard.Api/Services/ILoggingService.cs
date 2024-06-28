using System;
using System.Threading.Tasks;

namespace CryptoDashboard.Api.Services
{
    public interface ILoggingService
    {
        Task LogDateRangeSelection(DateTime startDate, DateTime endDate);
        Task LogError(string action, Exception ex);
    }
}
