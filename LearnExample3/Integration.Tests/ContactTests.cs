using Integration.Tests.Factories;
using Integration.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Tests
{
    public class ContactTests : IClassFixture<CustomWebApplicationFactory<LearnExample3.Startup>>
    {
        private readonly CustomWebApplicationFactory<LearnExample3.Startup> _factory;
        public readonly HttpClient _client;

        public ContactTests(CustomWebApplicationFactory<LearnExample3.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task MakePostCallAsync()
        {
            //var client = _factory.CreateClient();
            var initialResponse = await _client.GetAsync("/contact");                       
            var antiForgeryValues = await AntiForgeryHelper.ExtractAntiForgeryValues(initialResponse);

            // Create POST request, adding anti forgery cookie and form field
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/newStudent");

            postRequest.Headers.Add("Cookie",
                new CookieHeaderValue(AntiForgeryHelper.AntiForgeryCookieName,
                                      antiForgeryValues.cookieValue).ToString());

            var formData = new Dictionary<string, string>
            {
                {AntiForgeryHelper.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                {"FirstName", "Sarah"},
                {"LastName", "Smith"},
                {"Age", "18"},
                {"SchoolName", "GreenSChool"}
                // Frequent flyer number omitted
            };

            postRequest.Content = new FormUrlEncodedContent(formData);

            var postResponse = await _client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();

            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains("NewStudent", responseString);
        }
    }
}
