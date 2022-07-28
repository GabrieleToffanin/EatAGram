using Eatagram.Framework.Logger.LogSetup;
using Eatagram.SDK.Models;
using Eatagram.SDK.Models.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Services.Common
{
    /// <summary>
    /// Provides default functionalities for calling the
    /// Eatagram services
    /// </summary>
    public abstract class HttpServiceBase
    {
        /// <summary>
        /// Base client for making requests to the Http
        /// </summary>
        private readonly HttpClient _client;

        private readonly AuthenticationToken _authenticationToken;

        /// <summary>
        /// Base url for where the calls are directed
        /// </summary>
        private readonly string _baseUrl;

        protected HttpServiceBase(AuthenticationToken authenticatedUser)
        {
            _client = new HttpClient();
            _baseUrl = "https://localhost:5000/";
            _authenticationToken = authenticatedUser ?? new AuthenticationToken();
        }


        private protected async Task<HttpResponseMessage<TResult?>> InvokeAsync<TRequest, TResult>
            (TRequest request, string specificUri, HttpMethod httpMethod) where TRequest : class
                                                                          where TResult : class
        {
            Uri fullUri = new Uri(new Uri(_baseUrl), specificUri);

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                RequestUri = fullUri,
                Method = httpMethod,
            };

            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _authenticationToken.Token);

            if(request != null)
            {
                var json = JsonConvert.SerializeObject(request);

                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            try
            {
                var response = await _client.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    TResult result = JsonConvert.DeserializeObject<TResult>(jsonResponse);

                    return new HttpResponseMessage<TResult?>(response, result);
                }

                return new HttpResponseMessage<TResult?>(response);
            }
            catch (Exception ex)
            {
                SeriLogger.Error(ex.Message);

                return new HttpResponseMessage<TResult?>(
                    new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable));
            }
        }



    }
}
