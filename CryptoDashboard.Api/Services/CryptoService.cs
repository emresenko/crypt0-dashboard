using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoDashboard.Api.Data;
using CryptoDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoDashboard.Api.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly CryptoDbContext _context;

        public CryptoService(CryptoDbContext context)
        {
            _context = context;
        }

        public async Task<PairAverageDifference> HighestAverageDifference(DateTime startDate, DateTime endDate)
        {
            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            var result = await _context.CryptoPairs
                .Where(p => p.Date >= startDate && p.Date <= endDate)
                .GroupBy(p => p.PairName)
                .Select(g => new PairAverageDifference
                {
                    PairName = g.Key,
                    AvgDiff = (double)g.Average(p => Math.Abs(p.Open - p.Price)),
                    PercentageDiff = (double)g.Average(p => Math.Abs(p.Open - p.Price) / p.Open * 100)
                })
                .OrderByDescending(g => g.AvgDiff)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<LowAndHigh> GetLowAndHigh(string pairName, DateTime startDate, DateTime endDate)
        {

            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            var prices = await _context.CryptoPairs
                .Where(p => p.PairName == pairName && p.Date >= startDate && p.Date <= endDate)
                .ToListAsync();

            return new LowAndHigh
            {
                LowestPrice = prices.Min(p => p.Low),
                HighestPrice = prices.Max(p => p.High)
            };
        }

        public async Task<List<CryptoPair>> Pairs(DateTime startDate, DateTime endDate)
        {
            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            return await _context.CryptoPairs
                .Where(p => p.Date >= startDate && p.Date <= endDate)
                .ToListAsync();
        }
    }
}
