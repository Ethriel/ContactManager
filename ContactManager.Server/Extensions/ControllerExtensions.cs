using ContactManager.Services.Model.Utility.ApiResult.Abstraction;
using ContactManager.Services.Model.Utility.ApiResult.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactManager.Server.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ActionResultByApiResult(this Controller controller, IApiResult apiResult, ILogger logger)
        {
            switch (apiResult.ApiResultStatus)
            {
                case ApiResultStatus.Ok:
                    return controller.Ok(apiResult);
                case ApiResultStatus.NotFound:
                    logger.LogWarning(message: ((IApiErrorResult)apiResult).LoggerMessage);
                    return controller.NotFound(apiResult);
                case ApiResultStatus.Conflict:
                    logger.LogWarning(message: ((IApiErrorResult)apiResult).LoggerMessage);
                    return controller.Conflict(apiResult);
                case ApiResultStatus.Empty:
                    logger.LogInformation(message: apiResult.Message);
                    return controller.NoContent();
                case ApiResultStatus.BadRequest:
                default:
                    logger.LogError(message: ((IApiErrorResult)apiResult).LoggerMessage);
                    return controller.BadRequest(apiResult);
            }
        }
    }
}
