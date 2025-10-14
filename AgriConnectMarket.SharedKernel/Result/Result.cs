namespace AgriConnectMarket.SharedKernel.Result
{
    public sealed class Result<T>
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public T? Value { get; }

        protected Result(bool isSuccess, string error, T value)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, null, value);
        public static Result<T> Fail(string error) => new(false, error, default!);
    }
}
