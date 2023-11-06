using Constack.Widemapp.Common.Exceptions;
using Constack.Widemapp.Common.Extensions;
using Microsoft.AspNetCore.Http;

namespace Constack.Widemapp.Common.Helpers
{
    public static class RecaptchaHelper
    {
        private static IHttpContextAccessor _httpContext;

        public static void Configure(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        public static async Task ValidateRecaptcha()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var content = new List<KeyValuePair<string, string>>();
                    content.Add(new KeyValuePair<string, string>("secret", ""));
                    content.Add(new KeyValuePair<string, string>("response", _httpContext.HttpContext.Request.Headers["Recaptcha-Token"].ToString()));
                    HttpResponseMessage response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(content));
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                    var objectResponse = JsonExtension.Deserialize<GResponseModel>(responseBody);
                    if (!objectResponse.Success)
                    {
                        throw new BadRequestException("ReCaptcha has expired or missing");
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new BadRequestException(e.Message);
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
        }
    }

    public class GResponseModel
    {
        public bool Success { get; set; }
    }
}
