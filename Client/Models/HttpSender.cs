using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class HttpSender
    {
        private static readonly Uri url= new Uri("https://localhost:7287/api/Card");

        private HttpClient client;


        public  HttpResponseMessage RequestAsync(HttpMethod method, StringContent content)
        {

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = content,
                Method = method,
                RequestUri = url
            };

            return client.SendAsync(request).Result;
        }

        public HttpResponseMessage PostRequest(StringContent body)
        {
            var responce=client.PostAsync(url,body);
            return responce.Result;
        }

        public  HttpResponseMessage RequestAsync()
        {

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = url
            };

            return client.SendAsync(request).Result;
        }




        public HttpSender()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(15);
        }

    }
}
