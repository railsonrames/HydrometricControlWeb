using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services.Models
{
    public abstract class ApiClient
    {
        protected async Task<ApiResponse> Get(HttpResponseMessage httpResponse)
        {
            var result = new ApiResponse { HttpStatusCode = httpResponse.StatusCode };

            if (!httpResponse.IsSuccessStatusCode)
            {
                var jsonErrors = await httpResponse.Content.ReadAsStringAsync();
                await GetErrorsAsync(httpResponse, result);
            }
            return result;
        }

        protected async Task<ApiResult<TResult>> GetResult<TResult>(HttpResponseMessage httpResponse) where TResult : class
        {
            var result = new ApiResult<TResult> { HttpStatusCode = httpResponse.StatusCode };

            if (!httpResponse.IsSuccessStatusCode)
            {
                var jsonErrors = await httpResponse.Content.ReadAsStringAsync();
                await GetErrorsAsync(httpResponse, result);

                return result;
            }

            var json = await httpResponse.Content.ReadAsStringAsync();
            result.Result = JsonConvert.DeserializeObject<TResult>(json);
            return result;
        }

        protected async Task<ApiResult<string>> GetStringResult(HttpResponseMessage httpResponse)
        {
            var result = new ApiResult<string> { HttpStatusCode = httpResponse.StatusCode };

            if (!httpResponse.IsSuccessStatusCode)
            {
                var jsonErrors = await httpResponse.Content.ReadAsStringAsync();
                await GetErrorsAsync(httpResponse, result);

                return result;
            }

            var json = await httpResponse.Content.ReadAsStringAsync();
            result.Result = json;
            return result;
        }

        private async Task GetErrorsAsync(HttpResponseMessage httpResponse, ApiResponse response)
        {
            var jsonErrors = await httpResponse.Content.ReadAsStringAsync();
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    response.Errors = NotifyBadRequest(jsonErrors);
                    break;
                case HttpStatusCode.InternalServerError:
                    response.Errors = NotifyInternalServerError(jsonErrors);
                    break;
                case HttpStatusCode.NotFound:
                    response.Errors = GenericNotify("Erro", new string[] { "O objeto pesquisado não existe." });
                    break;
                default:
                    response.Errors = GenericNotify("Erro", new string[] { $"Ocorreu um erro na requisição referente a: {httpResponse.StatusCode}" });
                    break;
            }
        }

        private IDictionary<string, IEnumerable<string>> GenericNotify(string key, string[] value)
            => new Dictionary<string, IEnumerable<string>> { { key, value } };

        private IDictionary<string, IEnumerable<string>> NotifyInternalServerError(string jsonErrors)
            => JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<string>>>(jsonErrors);

        private IDictionary<string, IEnumerable<string>> NotifyBadRequest(string jsonErrors)
            => JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<string>>>(jsonErrors);
    }
}
