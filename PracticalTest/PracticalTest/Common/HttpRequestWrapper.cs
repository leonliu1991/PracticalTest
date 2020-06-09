using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace PracticalTest.Common
{
    public abstract class HttpRequestWrapper
    {
        private RestRequest restRequest;
        private RestClient restClient;
        private Boolean isLogged = false;

        public HttpRequestWrapper()
        {
            restRequest = new RestRequest();
            isLogged = true;
        }

        public virtual HttpRequestWrapper SetResourse(string resource)
        {
            restRequest.Resource = resource;
            return this;
        }

        public virtual HttpRequestWrapper SetMethod(Method method)
        {
            restRequest.Method = method;
            return this;
        }

        public virtual HttpRequestWrapper AddHeaders(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                restRequest.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            return this;
        }

        public virtual HttpRequestWrapper AddAuthorizationTokenHeader(string token)
        {
            restRequest.AddHeader("Authorization", "bearer " + token);
            return this;
        }

        public virtual HttpRequestWrapper AddJsonContentTypeHeader()
        {
            restRequest.AddHeader("Content-Type", "application/json");
            return this;
        }

        public virtual HttpRequestWrapper AddJsonContent(object data)
        {
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type", "application/json");
            string jsonString = JsonConvert.SerializeObject(data);
            restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return this;
        }

        public virtual HttpRequestWrapper ClearRequest()
        {
            restRequest.Parameters.Clear();
            return this;
        }

        public virtual HttpRequestWrapper AddJsonContent(IDictionary<string, string> body)
        {
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddBody(body);
            return this;
        }

        public virtual HttpRequestWrapper AddNonJsonContent(string body)
        {
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddBody(body);
            return this;
        }

        public virtual HttpRequestWrapper AddParameter(string name, object value)
        {
            restRequest.AddParameter(name, value);
            return this;
        }

        public virtual HttpRequestWrapper AddParameters(IDictionary<string, object> parameters)
        {
            foreach (var item in parameters)
            {
                restRequest.AddParameter(item.Key, item.Value);
            }

            return this;
        }

        public virtual IRestResponse Execute(string url)
        {
            try
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                restClient = new RestClient(url);
                var response = restClient.Execute(restRequest);

                if (isLogged)
                {
                    Console.WriteLine(string.Format("*****Log:The request url: {0}",
                        restClient.BaseUrl + restRequest.Resource));
                    foreach (var paramter in restRequest.Parameters)
                    {
                        Console.WriteLine(string.Format("*****Log:The request paramter: {0}", paramter));
                    }

                    Console.WriteLine(string.Format("*****Log:The response status code: {0}", response.StatusCode));
                    Console.WriteLine(string.Format("*****Log:The response content: {0}", response.Content));
                }

                return response;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public virtual T Execute<T>(string url)
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            restClient = new RestClient(url);

            var response = restClient.Execute(restRequest);
            var data = JsonConvert.DeserializeObject<T>(response.Content);

            if (isLogged)
            {
                foreach (var paramter in restRequest.Parameters)
                {
                    Console.WriteLine(string.Format("*****Log:The request paramter: {0}", paramter));
                }

                Console.WriteLine(string.Format("*****Log:The status code: {0}", response.StatusCode));
                Console.WriteLine(string.Format("*****Log:The content: {0}", response.Content));
            }

            return data;
        }
    }
}