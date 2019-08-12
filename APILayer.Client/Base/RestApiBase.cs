using APILayer.Entities;
using APILayer.Entities.Commom;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APILayer.Client
{
    public class RestApiBase
    {
        protected readonly string JsonMediaType = "application/json";
        protected readonly IConfigurationRoot ConfigurationRoot;

        public RestApiBase(IConfigurationRoot configurationRoot)
        {
            this.ConfigurationRoot = configurationRoot;
        }

        /// <returns>The <see cref="Task{TResult}"/></returns>
        protected async Task<SwaggerResponse<T>> CreateGenericSwaggerResponse<T>(HttpResponseMessage response) where T : class
        {
            var headers = this.CreateHeadersDictionary(response);
            var status = ((int)response.StatusCode).ToString();
            var responseData = await this.CreateResponseData(response, status, headers);

            try
            {
                var result = default(T);
                result = JsonConvert.DeserializeObject<T>(responseData);
                return new SwaggerResponse<T>(status, headers, result);
            }
            catch(Exception ex)
            {
                throw new SwaggerException(responseData, ex);
            }

        }

        protected async Task<SwaggerResponse> CreateSwaggerResponse(HttpResponseMessage response, bool isArrayByteData = false)
        {
            var headers = this.CreateHeadersDictionary(response);
            var status = ((int)response.StatusCode).ToString();

            if (isArrayByteData)
            {
                var responseData = await this.CreateByteArrayResponseData(response, status, headers);

                return new SwaggerResponse(status, headers, responseData);
            }
            else
            {
                var responseData = await this.CreateResponseData(response, status, headers);

                return new SwaggerResponse(status, headers, responseData);
            }
        }

        private async Task<string> CreateResponseData(HttpResponseMessage response, string status, IDictionary<string, IEnumerable<string>> headers)
        {
            var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            switch (status)
            {
                case "200":
                case "201":
                case "204":
                    return responseData;

                default:
                    this.ThrowSwaggerException(status, responseData, headers);
                    return null;
            }
        }

        private async Task<byte[]> CreateByteArrayResponseData(HttpResponseMessage response, string status, IDictionary<string, IEnumerable<string>> headers)
        {
            byte[] responseData;
            responseData = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            switch (status)
            {
                case "200":
                case "201":
                case "204":
                    return responseData;

                default:
                    this.ThrowSwaggerException(status, Encoding.UTF8.GetString(responseData, 0, responseData.Length), headers);
                    return null;
            }
        }

        private IDictionary<string, IEnumerable<string>> CreateHeadersDictionary(HttpResponseMessage response)
        {
            var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);

            foreach (var item in response.Content.Headers)
            {
                headers[item.Key] = item.Value;
            }

            return headers;
        }

        private void ThrowSwaggerException(string status, string responseData, IDictionary<string, IEnumerable<string>> headers)
        {
            switch (status)
            {
                case "400":
                    throw new SwaggerException("Bad Request", status, responseData, headers, null);

                case "404":
                    throw new SwaggerException("Not Found", status, responseData, headers, null);

                case "409":
                    throw new SwaggerException("Conflict", status, responseData, headers, null);

                case "413":
                case "422":
                    throw new SwaggerException("Client Error", status, responseData, headers, null);

                case "500":
                    throw new SwaggerException("Server Error", status, responseData, headers, null);

                case "502":
                    throw new SwaggerException("Issue description", status, responseData, headers, null);

                default:
                    throw new SwaggerException($"The HTTP status code of the response was not expected ({status})", status, responseData, headers, null);
            }
        }
    }
}
