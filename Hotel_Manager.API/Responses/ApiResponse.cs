using Hotel_Manager.Core.CustomEntities;

namespace Hotel_Manager.API.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public Metadata? Meta { get; set; }
        public ApiResponse(T data, string message = "OK") 
        {
            Data = data;
            Message = message;  
        }
    }
}
