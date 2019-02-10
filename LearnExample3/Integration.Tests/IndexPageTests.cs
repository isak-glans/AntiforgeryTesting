using Integration.Tests.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Tests
{
    public class IndexPageTests : IClassFixture<CustomWebApplicationFactory<LearnExample3.Startup>>
    {
        private readonly CustomWebApplicationFactory<LearnExample3.Startup> _factory;
        private readonly object _client;

        public IndexPageTests(CustomWebApplicationFactory<LearnExample3.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
