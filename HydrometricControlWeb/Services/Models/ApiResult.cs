using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services.Models
{
    public class ApiResult<TResult> : ApiResponse
    {
        public TResult Result { get; set; }
    }
}
