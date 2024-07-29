using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace ProductManagement.Application.DTOs.Response
{
    public class BaseResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Errors { get; set; }
    }

    public class BaseResponseDto<TData> : BaseResponseDto
    {
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData Data { get; set;}       

    }
}
