namespace BuildingBlocks.Common
{
    public record Result
    {
        public bool Succeeded { get; init; }
        public IEnumerable<string>? Errors { get; init; }
        // HTTP-like status code to convey result intent (e.g., 200, 400, 401, 500)
        public int StatusCode { get; init; }
        // Optional human-readable message
        public string? Message { get; init; }

        public static Result Success(int statusCode = 200, string? message = null) => new Result { Succeeded = true, StatusCode = statusCode, Message = message };
        public static Result Failure(IEnumerable<string> errors, int statusCode = 400, string? message = null) => new Result { Succeeded = false, Errors = errors, StatusCode = statusCode, Message = message };
    }

    public record Result<T> : Result
    {
        public T? Data { get; init; }
        public static Result<T> Success(T data, int statusCode = 200, string? message = null) => new Result<T> { Succeeded = true, Data = data, StatusCode = statusCode, Message = message };
        public static new Result<T> Failure(IEnumerable<string> errors, int statusCode = 400, string? message = null) => new Result<T> { Succeeded = false, Errors = errors, StatusCode = statusCode, Message = message };
    }
}
