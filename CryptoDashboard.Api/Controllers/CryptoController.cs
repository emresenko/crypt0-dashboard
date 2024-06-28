using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Newtonsoft.Json;
using MongoDB.Driver;
using CryptoDashboard.Api.Services;
using CryptoDashboard.Api.Models;

namespace CryptoDashboard.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoService _cryptoService;
        private readonly IDatabase _cache;
        private readonly ILoggingService _loggingService;

        public CryptoController(ICryptoService cryptoService, IConnectionMultiplexer redis, ILoggingService loggingService)
        {
            _cryptoService = cryptoService;
            _cache = redis.GetDatabase();
            _loggingService = loggingService;
        }

        [HttpGet("highest-avg-diff")]
        public async Task<IActionResult> HighestAverageDifference(DateTime startDate, DateTime endDate)
        {
            string cacheKey = $"CryptoPairs:HighestAvgDiff:{startDate:yyyyMMdd}:{endDate:yyyyMMdd}";
            var cachedData = await _cache.StringGetAsync(cacheKey);

            if (cachedData.HasValue)
            {

                return Ok(JsonConvert.DeserializeObject<PairAverageDifference>(cachedData));
            }

            try
            {
                var result = await _cryptoService.HighestAverageDifference(startDate, endDate);

                var lowHighResult = await _cryptoService.GetLowAndHigh(result.PairName, startDate, endDate);
                var combinedResult = new
                {
                    PairAverageDifference = result,
                    PriceRange = lowHighResult
                };

                await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromHours(1));

                //await _loggingService.LogDateRangeSelection(startDate, endDate);

                return Ok(combinedResult);
            }
            catch (Exception ex)
            {
               // await _loggingService.LogError("HighestAverageDifference", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("pairs")]
        public async Task<IActionResult> Pairs(DateTime startDate, DateTime endDate)
        {
          string cacheKey = $"CryptoPairs:{startDate:yyyyMMdd}:{endDate:yyyyMMdd}";
           var cachedData = await _cache.StringGetAsync(cacheKey);
           
            if (cachedData.HasValue)
            {
                return Ok(JsonConvert.DeserializeObject<List<CryptoPair>>(cachedData));
            }
           
            try
            {
                var result = await _cryptoService.Pairs(startDate, endDate);
               await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromHours(1));
              await _loggingService.LogDateRangeSelection(startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _loggingService.LogError("Pairs", ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
