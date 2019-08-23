using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services.Models
{
    public class ApiResponse
    {
        public bool IsSuccessStatusCode
        {
            get
            {
                return (int)HttpStatusCode < 400;
            }
        }

        public HttpStatusCode HttpStatusCode { get; set; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
