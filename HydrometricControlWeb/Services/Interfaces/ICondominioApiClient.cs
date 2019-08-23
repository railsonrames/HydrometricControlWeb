using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services.Interfaces
{
    public interface ICondominioApiClient
    {
        [Get("/api/v1/condominio/lista")]
        Task<HttpResponseMessage> Listar();
    }
}
