using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Cryptocop.Software.API.Models;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Models.Exceptions;
using Cryptocop.Software.API.Services.Helpers;
using System.Collections.Generic;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class ExchangeService : IExchangeService
    {
        private HttpClient _httpClient;
        public ExchangeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Envelope<ExchangeDto>> GetExchanges(int pageNumber = 1)
        {
            if (pageNumber == 0) { throw new ResourceNotFoundException(); }
            List<ExchangeDto> exchangeList = new List<ExchangeDto>();
            //IEnumerable<ExchangeDto> exchangeItems = exchangeList;

            for (int i = 1; i < pageNumber + 1; i++)
            {
                //System.Console.WriteLine("Here first\n");
                var pageExchanges = await _httpClient.GetAsync($"markets?page={i}");
                var exchangeDtoList = await HttpResponseMessageExtensions.DeserializeJsonToList<ExchangeDto>(pageExchanges, true);

                exchangeList.AddRange(exchangeDtoList);
                System.Console.WriteLine(exchangeList.Count());
            }

            var envelope = new Envelope<ExchangeDto>(pageNumber, exchangeList);
            return envelope;


        }
    }
}