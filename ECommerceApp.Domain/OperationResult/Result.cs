using ECommerceApp.Domain.Errors;

namespace ECommerceApp.Domain.OperationResult
{
    public class Result<T> : Result
    {
        public T Data { get; init; }
        public new static Result<T> Failure(string message)
        {
            return new Result<T>
            {
                IsSuccessful = false,
                Message = message
            };
        }

        public new static Result<T> Failure(Error error)
        {
            return new Result<T>
            {
                IsSuccessful = false,
                Error = error
            };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                IsSuccessful = true,
                Data = data
            };
        }
    }

    public class Result
    {
        public bool IsSuccessful { get; init; }
        public string Message { get; init; } 
        public Error Error { get; init; }

        public static Result Failure(string message)
        {
            return new Result
            {
                IsSuccessful = false,
                Message = message
            };
        }

        public static Result Failure(Error error)
        {
            return new Result
            {
                IsSuccessful = false,
                Error = error
            };
        }

        public static Result Success()
        {
            return new Result
            {
                IsSuccessful = true
            };
        }
    }
}
