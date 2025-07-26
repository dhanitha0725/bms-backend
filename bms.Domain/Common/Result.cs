namespace bms.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get;}
        public Error? Error { get; }

        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, Error? error = null)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true);
        }

        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(T? value, bool isSuccess, Error? error = null) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value, true);
        }

        public new static Result<T> Failure(Error error)
        {
            return new Result<T>(default, false, error);
        }
    }
}
