using DallEApiDotNet.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DallEApiDotNet.Services
{
    public class ImageGenerator : IImageGenerator
    {
        public async Task<Response> GenerateImg(InputData inputData)
        {
            var Key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Api_Key")["Key"];

            Response? result = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Key);

                var message = await client.
                    PostAsync("https://api.openai.com/v1/images/generations",
                    new StringContent(JsonConvert.SerializeObject(inputData),
                    Encoding.UTF8, "application/json"));

                if (message.IsSuccessStatusCode)
                {
                    var content = await message.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Response>(content);
                }
            }

            return result;
        }
    }
}
