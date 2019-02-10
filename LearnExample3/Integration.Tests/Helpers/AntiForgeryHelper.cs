using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Integration.Tests.Helpers
{
    public static class AntiForgeryHelper
    {
        public static readonly string AntiForgeryFieldName = "__RequestVerificationToken";
        public static readonly string AntiForgeryCookieName = ".AspNetCore.Antiforgery";

        public static async Task<(string fieldValue, string cookieValue)> ExtractAntiForgeryValues(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return (ExtractAntiForgeryToken(content),
                    ExtractAntiForgeryCookieValueFrom(response));
        }

        private static string ExtractAntiForgeryCookieValueFrom(HttpResponseMessage response)
        {
            string antiForgeryCookie =
                        response.Headers
                                .GetValues("Set-Cookie")
                                .FirstOrDefault(x => x.Contains(AntiForgeryCookieName));

            if (antiForgeryCookie is null)
            {
                throw new ArgumentException(
                    $"Cookie '{AntiForgeryCookieName}' not found in HTTP response",
                    nameof(response));
            }
            
            string antiForgeryCookieValue =
                SetCookieHeaderValue.Parse(antiForgeryCookie).Value.ToString();

            return antiForgeryCookieValue;
        }

        private static string ExtractAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch =
                Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' not found in HTML", nameof(htmlBody));
        }
    }
}
