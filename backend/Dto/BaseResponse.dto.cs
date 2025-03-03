namespace backend.Dto
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }

        public BaseResponse(string message, int code, T data = default)
        {
            Message = message;
            Code = code;
            Data = data;
        }
    }
}