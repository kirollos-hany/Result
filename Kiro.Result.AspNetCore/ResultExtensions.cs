using System;
using System.Collections.Generic;
using Kiro.Result.Enums;
using Kiro.Result.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kiro.Result.AspNetCore
{
    public static class ResultExtensions
    {
        public static ActionResult ToActionResult<TFail, TSuccess>(this Result<TFail, TSuccess> result,
            ControllerBase controller)
        {
            return controller.ToActionResult((IResult)result);
        }
    
        public static ActionResult ToActionResult<TFail, TSuccess>(this ControllerBase controller,
            Result<TFail, TSuccess> result)
        {
            return controller.ToActionResult((IResult)result);
        }
    
        private static ActionResult ToActionResult(this ControllerBase controller, IResult result)
        {
            return _dictionary[result.Status](result, controller);
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

