using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Mv.Integrations
{
    public class ApiClient
    {
        private RestRequest _restRequest;
        private readonly RestClient _restClient;
        private readonly string _apiKey;

        public ApiClient()
        {
            _apiKey = apiKey;
            _restClient = new RestClient(apiUrl);
        }

        public IList<object> GetReturns(int pageNumber, string state)
        {
            var newOrders = new List<object>();
            var resource = $"returns?state={state}&page={pageNumber}";

            _restRequest = SetBaseRequest();
            _restRequest.Resource = FormatResource(resource);

            var response = _restClient.Execute<List<object>>(_restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data.Count <= 0) return newOrders;

                newOrders.AddRange(response.Data);
                newOrders.AddRange(GetReturns(pageNumber + 1, state));
            }
            else
            {
                //_importReport.AddNotification("Failed to get returns: API response = " + response.Content, NotificationType.Error);
            }

            return newOrders;
        }

        private RestRequest SetBaseRequest()
        {
            var request = new RestRequest();

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-key", _apiKey);

            return request;
        }
        private string FormatResource(string resource)
        {
            return string.Format("/{0}/{1}", "", resource);
        }
    }
}
