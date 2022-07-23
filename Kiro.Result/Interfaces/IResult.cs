using Kiro.Result.Enums;

namespace Kiro.Result.Interfaces
{
    public interface IResult
    {
        ResultStatus Status { get; }

        object? GetValue();
    }
}

