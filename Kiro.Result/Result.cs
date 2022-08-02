using Kiro.Result.Enums;
using Kiro.Result.Interfaces;

namespace Kiro.Result
{
    public class Result<TFail, TSuccess> : IResult
    {
        /// <summary>
        /// Represents a successful operation and accepts a value as the result of the operation
        /// </summary>
        /// <param name="value">Sets the SuccessValue property</param>
        public Result(TSuccess value)
        {
            SuccessValue = value;
        }

        protected Result(ResultStatus status, TSuccess value) : this(status)
        {
            SuccessValue = value;
        }

        protected Result(ResultStatus status, TFail value) : this(status)
        {
            FailValue = value;
        }

        protected Result(ResultStatus status)
        {
            Status = status;
        }

        public TFail FailValue { get; }
        public TSuccess SuccessValue { get; }
        public ResultStatus Status { get; protected set; } = ResultStatus.Ok;

        public bool IsSuccessful => Status == ResultStatus.Ok;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        /// <returns>Fail or success value or null if value is not set</returns>
        public object? GetValue()
        {
            return IsSuccessful ? (object?)SuccessValue : FailValue;
        }

        /// <summary>
        /// Represents a successful operation and accepts a value as the result of the operation
        /// </summary>
        /// <param name="value">Sets the SuccessValue property</param>
        /// <returns>A successful result instance with a success value</returns>
        public static Result<TFail, TSuccess> Success(TSuccess value) =>
            new Result<TFail, TSuccess>(ResultStatus.Ok, value);

        /// <summary>
        /// Represents a successful operation with no value
        /// </summary>
        /// <returns>A successful result instance without a success value</returns>
        public static Result<TFail, TSuccess> Success() => new Result<TFail, TSuccess>(ResultStatus.Ok);

        /// <summary>
        /// Represents a bad request operation and accepts a value as the result of the operation
        /// </summary>
        /// <param name="value">Sets the FailValue property</param>
        /// <returns>A failed result instance with a fail value</returns>
        public static Result<TFail, TSuccess> BadRequest(TFail value) =>
            new Result<TFail, TSuccess>(ResultStatus.BadRequest, value);

        /// <summary>
        /// Represents a bad request operation without value
        /// </summary>
        /// <returns>A failed result instance without a fail value</returns>
        public static Result<TFail, TSuccess> BadRequest() => new Result<TFail, TSuccess>(ResultStatus.BadRequest);

        /// <summary>
        /// Represents a not found operation and accepts a value as the result of the operation
        /// </summary>
        /// <param name="value">Sets the FailValue property</param>
        /// <returns>A failed result instance with a fail value</returns>
        public static Result<TFail, TSuccess> NotFound(TFail value) =>
            new Result<TFail, TSuccess>(ResultStatus.NotFound, value);

        /// <summary>
        /// Represents a not found operation without value
        /// </summary>
        /// <returns>A failed result instance without a fail value</returns>
        public static Result<TFail, TSuccess> NotFound() => new Result<TFail, TSuccess>(ResultStatus.NotFound);

        /// <summary>
        /// Represents an unauthorized operation and accepts a value as the result of the operation
        /// </summary>
        /// <param name="value">Sets the FailValue property</param>
        /// <returns>A failed result instance with a fail value</returns>
        public static Result<TFail, TSuccess> Unauthorized(TFail value) =>
            new Result<TFail, TSuccess>(ResultStatus.Unauthorized, value);

        /// <summary>
        /// Represents an unauthorized operation without value
        /// </summary>
        /// <returns>A failed result instance without a fail value</returns>
        public static Result<TFail, TSuccess> Unauthorized() => new Result<TFail, TSuccess>(ResultStatus.NotFound);

        public static implicit operator TSuccess(Result<TFail, TSuccess> result) => result.SuccessValue;

        public static implicit operator TFail(Result<TFail, TSuccess> result) => result.FailValue;
    }
}