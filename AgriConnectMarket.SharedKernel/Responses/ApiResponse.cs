namespace AgriConnectMarket.SharedKernel.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }

        protected ApiResponse(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
            => new(true, message, data);

        public static ApiResponse<T> FailResponse(string message)
            => new(false, message, default);
    }

    public class ApiResponse : ApiResponse<object?>
    {
        public ApiResponse(bool success, string message)
            : base(success, message, null) { }

        public static ApiResponse Success(string message = "Success")
            => new(true, message);

        public static ApiResponse Fail(string message)
            => new(false, message);
    }
}
