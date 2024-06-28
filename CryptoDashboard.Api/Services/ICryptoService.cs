using CryptoDashboard.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoDashboard.Api.Services
{
    public interface ICryptoService
    {
        Task<PairAverageDifference> HighestAverageDifference(DateTime startDate, DateTime endDate);
        Task<LowAndHigh> GetLowAndHigh(string pairName, DateTime startDate, DateTime endDate);
        Task<List<CryptoPair>> Pairs(DateTime startDate, DateTime endDate);
    }
}
