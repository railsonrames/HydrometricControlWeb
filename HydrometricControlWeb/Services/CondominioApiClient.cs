using HydrometricControlWeb.Services.Interfaces;
using HydrometricControlWeb.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services
{
    public class CondominioApiClient : ApiClient
    {
        private readonly ICondominioApiClient _apiClient;

        public CondominioApiClient(ICondominioApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResult<IEnumerable<CondominioDTO>>> Listar()
        {
            var response = await _apiClient.Listar();
            return await GetResult<IEnumerable<CondominioDTO>>(response);
        }
    }
}
