﻿using System;
using System.Net;

namespace MagicVilla_Web.models
{
	public class APIResponse
	{
        public HttpStatusCode StatusCode { get; set; }
        // for default IsSuccess is true ...
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}

