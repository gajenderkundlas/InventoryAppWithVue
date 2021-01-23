using System;
using System.Collections.Generic;
using System.Text;

namespace InvBusinessLayer.Model
{
   public class ResponseViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorDetails { get; set; }
        public int ErrorCode { get; set; }
        public  List<T> Data { get; set; }
    }
}
