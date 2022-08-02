using System;
using System.Collections.Generic;
using Kiro.Result.Enums;
using Kiro.Result.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kiro.Result.AspNetCore
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Convert a <see cref="Result{TFail, TSuccess}"/> to a <see cref="ActionResult"/>
        /// </summary>
        /// <param name="controller">The controller this is called from</param>
        /// <param name="result">The Result to convert to an ActionResult</param>
        /// <returns><see cref="ActionResult"/> mapped from the result object</returns>
        public static ActionResult ToActionResult<TFail, TSuccess>(this Result<TFail, TSuccess> result,
            ControllerBase controller)
        {
            return controller.ToActionResult((IResult)result);
        }

        /// <summary>
        /// Convert a <see cref="Result{TFail, TSuccess}"/> to a <see cref="ActionResult"/>
        /// </summary>
        /// <param name="controller">The controller this is called from</param>
        /// <param name="result">The Result to convert to an ActionResult</param>
        /// <returns><see cref="ActionResult"/> mapped from the result object</returns>
        public static ActionResult ToActionResult<TFail, TSuccess>(this ControllerBase controller,
            Result<TFail, TSuccess> result)
        {
            return controller.ToActionResult((IResult)result);
        }

        private static ActionResult ToActionResult(this ControllerBase controller, IResult result)
        {
            if (_dictionary.TryGetValue(result.Status, out var mapFunction))
            {
                return mapFunction(result, controller);
            }

            throw new NotSupportedException($"Result status {result.Status} not supported.");
        }

        private static readonly IDictionary<ResultStatus, Func<IResult, ControllerBase, ActionResult>> _dictionary =
            new Dictionary<ResultStatus, Func<IResult, ControllerBase, ActionResult>>
            {
                {
                    ResultStatus.Ok, (result, controller) =>
                    {
                        var value = result.GetValue();
                        return value is null ? (ActionResult)controller.Ok() : controller.Ok(value);
                    }
                },
                {
                    ResultStatus.BadRequest, (result, controller) =>
                    {
                        var value = result.GetValue();
                        return value is null ? (ActionResult)controller.BadRequest() : controller.BadRequest(value);
                    }
                },
                {
                    ResultStatus.NotFound, (result, controller) =>
                    {
                        var value = result.GetValue();
                        return value is null ? (ActionResult)controller.NotFound() : controller.NotFound(value);
                    }
                },
                {
                    ResultStatus.Unauthorized, (result, controller) =>
                    {
                        var value = result.GetValue();
                        return value is null ? (ActionResult)controller.Unauthorized() : controller.Unauthorized(value);
                    }
                }
            };
    }
}