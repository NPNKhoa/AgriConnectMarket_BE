namespace AgriConnectMarket.SharedKernel.Result
{
    public sealed class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        private Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null!);
        public static Result Fail(string error) => new(false, error);
    }

    public sealed class Result<T>
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public T? Value { get; }

        private Result(bool isSuccess, string error, T value)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, null, value);
        public static Result<T> Fail(string error) => new(false, error, default!);
    }
}
