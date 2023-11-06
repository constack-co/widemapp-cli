using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Constack.Widemapp.Common.Extensions
{
    public static class WebEncoderExtenison
    {
        public static string DecodeBase64(this string value, Encoding encoding)
        {
            var bytes = WebEncoders.Base64UrlDecode(value);

            return encoding.GetString(bytes);
        }

        public static string EncodeBase64(this string value, Encoding encoding)
        {
            var bytes = encoding.GetBytes(value);

            return WebEncoders.Base64UrlEncode(bytes);
        }
    }
}
