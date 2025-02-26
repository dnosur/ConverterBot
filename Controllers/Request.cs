using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;

namespace ConverterBot.Controllers
{
    internal static class Request<T>
    {
        public static T Get(string url)
        {
            //5dfMcb78Y45bJ8TCAqIrbv5Q5jhJBOef
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest();

            RestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                throw new Exception("Error while get response!");
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
