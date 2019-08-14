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
            var status = ((int)response.StatusCode).ToString();
            var responseData = await this.CreateResponseData(response, status);

            var result = default(T);
            result = JsonConvert.DeserializeObject<T>(responseData);
            return new SwaggerResponse<T>(status, result);
        }

        protected async Task<SwaggerResponse> CreateSwaggerResponse(HttpResponseMessage response)
        {
            var status = ((int)response.StatusCode).ToString();

            var responseData = await this.CreateResponseData(response, status);
            return new SwaggerResponse(status, responseData);
        }

        private async Task<string> CreateResponseData(HttpResponseMessage response, string status)
        {
            var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            switch (status)
            {
                case "200":
                case "201":
                case "204":
                    return responseData;

                default:
                    this.ThrowSwaggerException(status, responseData);
                    return null;
            }
        }

        private void ThrowSwaggerException(string status, string responseData)
        {
            switch (status)
            {
                case "400":
                    throw new SwaggerException("Bad Request", status, responseData, null);

                case "404":
                    throw new SwaggerException("Not Found", status, responseData, null);

                case "409":
                    throw new SwaggerException("Conflict", status, responseData, null);

                case "413":
                case "422":
                    throw new SwaggerException("Client Error", status, responseData, null);

                case "500":
                    throw new SwaggerException("Server Error", status, responseData, null);

                case "502":
                    throw new SwaggerException("Issue description", status, responseData, null);

                default:
                    throw new SwaggerException($"The HTTP status code of the response was not expected ({status})", status, responseData, null);
            }
        }
    }
}
