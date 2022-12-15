using System.Net.Http.Headers;

namespace BPR_Blazor.Data
{
    /// <summary>
    /// This class is used to create and allow an HTTP client to be called when needed.
    /// </summary>
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        /// <summary>
        /// This method initialises the HTTP client if it has not been initialised before and returns it.
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient = new HttpClient(); 
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// This stores the token so it can be used later!
        /// </summary>
        /// <param name="token">The token returned by the API when the user logs in!</param>
        public static void AddToken(string token)
        {
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// This deletes the previously stored token!
        /// </summary>
        public static void RemoveToken()
        {
            ApiClient.DefaultRequestHeaders.Authorization = null;
        }
    } 
}
