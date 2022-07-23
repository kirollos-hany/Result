using Kiro.Result.Enums;
using Kiro.Result.Interfaces;

namespace Kiro.Result
{
    public class Result<TFail, TSuccess> : IResult
    {
        public Result(TSuccess value)
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

        public object? GetValue()
        {
            return IsSuccessful ? (object?) SuccessValue : FailValue;
        }

        public static Result<TFail, TSuccess> Success(TSuccess value) => new Result<TFail, TSuccess>(value);


        public static Result<TFail, TSuccess> BadRequest(TFail value) =>
            new Result<TFail, TSuccess>(ResultStatus.BadRequest, value);


        public static Result<TFail, TSuccess> BadRequest() => new Result<TFail, TSuccess>(ResultStatus.BadRequest);

        public static Result<TFail, TSuccess> NotFound(TFail value) =>
            new Result<TFail, TSuccess>(ResultStatus.NotFound, value);

        public static Result<TFail, TSuccess> NotFound() => new Result<TFail, TSuccess>(ResultStatus.NotFound);

        public static Result<TFail, TSuccess> Unauthorized(TFail value) =>
            new Result<TFail, TSuccess>(ResultStatus.Unauthorized, value);

        public static Result<TFail, TSuccess> Unauthorized() => new Result<TFail, TSuccess>(ResultStatus.NotFound);
    }
}