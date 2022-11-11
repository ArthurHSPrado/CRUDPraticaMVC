using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class GenericResponse<T> : BaseResponse 
    {
        public T Object { get; set; }

    }
}
