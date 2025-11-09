using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Core.CustomEntities
{
    public class ResponseData<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public Paged? Meta { get; set; }

        public ResponseData(T data, string message = "OK")
        {
            Data = data;
            Message = message;
        }

    }
}
