using System;

namespace STARC.Api.Models
{
    public class ApiResponse
    {
        public ApiResponseState State { get; }
        public string Message { get; }
        public Object Data { get; }

        public ApiResponse()
        {

        }

        public ApiResponse(ApiResponseState state, string message, Object data)
        {
            State = state;
            Message = message;
            Data = data;
        }

        public ApiResponse(ApiResponseState state, string message)
        {
            State = state;
            Message = message;
        }

        public ApiResponse(ApiResponseState state, Object data)
        {
            State = state;            
            Data = data;
        }
    }
}
