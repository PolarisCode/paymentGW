﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Payments.API.Contracts;
using Payments.API.Exceptions;

namespace Payments.API
{
    public class HttpRequestSender<T>: IRequestSender<T> where T : new()
    {
        //private const string URL = "https://sub.domain.com/objects.json";
        //private string urlParameters = "?api_key=123";

        public async Task<T> SendAsync(string url, string requestUri, string jsonString)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(url) };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, jsonString);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var result = await response.Content.ReadAsStringAsync();

                client.Dispose();

                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new HttpSenderException(response.StatusCode.ToString(), response.ReasonPhrase);
            }
        }

    }
}