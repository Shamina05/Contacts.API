using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    public class BaseResponse<T>
    {
        public ErrorStatus ErrorStatus { get; set; }
        public ErrorInfo ErrorInfo { get; set; }

        public T Result { get; set; }
    }

    public enum ErrorStatus
    {
        //call was successfull
        Success = 0,
        Pending = 1, 

        //call failed 
        Failed = 2, 

        //Error Processing the call 
        Error = 3
    }

    public class ErrorInfo
    {
        public string ErrorMessage { get; set; }
        public string Response { get; set; }
        public Exception Exception { get; set; }
    }
}
