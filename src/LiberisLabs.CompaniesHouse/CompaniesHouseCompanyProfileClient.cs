using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LiberisLabs.CompaniesHouse.Response.CompanyProfile;
using LiberisLabs.CompaniesHouse.UriBuilders;

namespace LiberisLabs.CompaniesHouse
{
    public class CompaniesHouseCompanyProfileClient : ICompaniesHouseCompanyProfileClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICompanyProfileUriBuilder _companyProfileUriBuilder;

        public CompaniesHouseCompanyProfileClient(IHttpClientFactory httpClientFactory, ICompanyProfileUriBuilder companyProfileUriBuilder)
        {
            _httpClientFactory = httpClientFactory;
            _companyProfileUriBuilder = companyProfileUriBuilder;
        }

        public async Task<CompaniesHouseClientResponse<CompanyProfile>> GetCompanyProfileAsync(string companyNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var httpClient = _httpClientFactory.CreateHttpClient())
            {
                var requestUri = _companyProfileUriBuilder.Build(companyNumber);

                var response = await httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);

                // Return a null profile on 404s, but raise exception for all other error codes
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                    response.EnsureSuccessStatusCode();

                CompanyProfile result = response.IsSuccessStatusCode
                    ? await response.Content.ReadAsAsync<CompanyProfile>(cancellationToken).ConfigureAwait(false)
                    : null;

                return new CompaniesHouseClientResponse<CompanyProfile>(result);
            }
        }
    }
}