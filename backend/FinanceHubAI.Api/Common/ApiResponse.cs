namespace FinanceHubAI.Api.Common;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public ApiError? Error { get; init; }

    public static ApiResponse<T> Ok(T data)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Error = null
        };
    }

    public static ApiResponse<T> Fail(string code, string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Data = default,
            Error = new ApiError(code, message)
        };
    }
}

public sealed record ApiError(string Code, string Message);