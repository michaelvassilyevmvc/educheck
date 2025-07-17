﻿using System.Net;

namespace vassilyev.EduCheckV2App.WebAPI.Helpers;

public class APIResponse
{
    public bool IsSuccess { get; set; }
    public Object Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> ErrorMessages { get; set; } = new List<string>();
}