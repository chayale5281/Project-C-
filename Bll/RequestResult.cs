using System;

namespace Bll

{
    [Serializable]
    public class RequestResult
    {
        public Object Data { get; set; }
        public string Message { get; set; }
        public bool status { get; set; }
    }
}
