using System;
using System.Net;

namespace MagicVilla_VillaAPI.models
{
	public class APIResponse
	{
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        // for default IsSuccess is true ...
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}

