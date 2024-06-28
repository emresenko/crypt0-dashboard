using MongoDB.Driver;
using CryptoDashboard.Api.Models;
using System.Threading.Tasks;

namespace CryptoDashboard.Api.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly IMongoCollection<LogEntry> _logCollection;
        private readonly IMongoCollection<ErrorLogEntry> _errorLogCollection;


        public LoggingService(IMongoDatabase mongoDatabase)
        {
            _logCollection = mongoDatabase.GetCollection<LogEntry>("CryptoLogs");
            _errorLogCollection = mongoDatabase.GetCollection<ErrorLogEntry>("ErrorLogs");

        }

        public async Task LogDateRangeSelection(DateTime startDate, DateTime endDate)
        {
            var logEntry = new LogEntry
            {
                BeginDate = startDate,
                EndDate = endDate,
                RequestDate = DateTime.UtcNow
            };
            await _logCollection.InsertOneAsync(logEntry);
        }

        public async Task LogError(string action, Exception ex)
        {
            {
            var errorLog = new ErrorLogEntry
            {
                Action = action,
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                Date = DateTime.UtcNow
            };
            await _errorLogCollection.InsertOneAsync(errorLog);
        }
        }
    }


}
