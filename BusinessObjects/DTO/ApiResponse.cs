using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ApiResponse<T>
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse()
        {
            Success = false;
            Message = null;
            Data = default;
        }

        public ApiResponse(T data)
        {
            Success = true;
            Message = null;
            Data = data;
        }

        public ApiResponse(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
            Data = default;
        }
        public ApiResponse(bool isSuccess, string errorMessage)
        {
            Success = isSuccess;
            Message = errorMessage;
            Data = default;
        }

        public ApiResponse(string successMessage, T data)
        {
            Success = true;
            Message = successMessage;
            Data = data;
        }

    }
}
