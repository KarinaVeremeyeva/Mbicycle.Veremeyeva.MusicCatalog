using System.Net.Http;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Typed client for the account api
    /// </summary>
    public class AccountApiClient
    {
        /// <summary>
        /// Http client
        /// </summary>
        public HttpClient Client { get; }

        /// <summary>
        /// AccountApiClient constructor
        /// </summary>
        /// <param name="client">Http client</param>
        public AccountApiClient(HttpClient client)
        {
            Client = client;
        }
    }
}
