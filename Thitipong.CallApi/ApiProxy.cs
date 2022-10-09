using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zort.Utilities
{
    public class ApiProxy
    {
        public readonly HttpClient client;

        public ApiProxy(string BaseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endPointPath,Object data)
        {
            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

            return await client.PostAsync(endPointPath, dataContent);
    
        }

    }
}