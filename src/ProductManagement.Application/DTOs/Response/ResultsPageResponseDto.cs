using System.Collections.Generic;
using System;

namespace ProductManagement.Application.DTOs.Response
{
    public class ResultsPageResponseDto<TData>
    {
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public List<TData> Items { get; set; } = new List<TData>();
    }
}
