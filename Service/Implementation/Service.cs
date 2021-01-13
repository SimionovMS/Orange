using System.Collections.Generic;
using Service.Interface;
using RestSharp;
using Mv.Integrations;

namespace Service.Implementation
{
    public class Service : IService
    {

        private RestRequest _restRequest;
        private readonly RestClient _restClient;
        private readonly string _apiKey;

        public Service(string apiKey, string apiUrl)
        {
            _apiKey = apiKey;
            _restClient = new RestClient(apiUrl);
        }

        public IEnumerable<object> GetAllMovies()
        {
            var list = new List<object>();
            var obj = new
            {
                Id = 1,
                Rating = 1,
            };

            list.Add(obj);

            var apiKey = Configuration.GetConnectionString("DefaultConnection")
            
            var integration = new ApiClient("","");

            return list;
        }

      
    }
}