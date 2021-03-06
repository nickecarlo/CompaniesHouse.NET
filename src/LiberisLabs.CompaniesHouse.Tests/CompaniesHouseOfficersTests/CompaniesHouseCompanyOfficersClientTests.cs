﻿using System;
using System.Net.Http;
using FluentAssertions;
using LiberisLabs.CompaniesHouse.Tests.ResourceBuilders;
using LiberisLabs.CompaniesHouse.UriBuilders;
using Moq;
using NUnit.Framework;
using Officers = LiberisLabs.CompaniesHouse.Response.Officers.Officers;

namespace LiberisLabs.CompaniesHouse.Tests.CompaniesHouseOfficersTests
{
    [TestFixture]
    public class CompaniesHouseCompanyOfficersClientTests
    {
        private CompaniesHouseOfficersClient _client;

        private CompaniesHouseClientResponse<Officers> _result;
        private ResourceBuilders.Officers _officers;

        [Test]
        public void GivenACompaniesHouseCompanyProfileClient_WhenGettingACompanyProfile()
        {
            _officers = new OfficersBuilder().Build();
            var resource = new OfficersResourceBuilder(_officers).Create();

            var uri = new Uri("https://wibble.com/search/companies");

            HttpMessageHandler handler = new StubHttpMessageHandler(uri, resource);
            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(x => x.CreateHttpClient())
                .Returns(new HttpClient(handler));

            var uriBuilder = new Mock<IOfficersUriBuilder>();
            uriBuilder.Setup(x => x.Build(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(uri);

            _client = new CompaniesHouseOfficersClient(httpClientFactory.Object, uriBuilder.Object);

            _result = _client.GetOfficersAsync("abc", 0, 25).Result;

            _result.Data.ShouldBeEquivalentTo(_officers);
        }
    }
}
