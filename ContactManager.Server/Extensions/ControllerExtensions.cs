using ContactManager.Services.Model.Utility.ApiResult.Abstraction;
using ContactManager.Services.Model.Utility.ApiResult.Implementation;
using Microsoft.AspNetCore.Mvc;

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
                    logger.LogWarning(((ApiErrorResult)apiResult).LoggerMessage);
                    return controller.NotFound(apiResult);
                case ApiResultStatus.BadRequest:
                default:
                    logger.LogError(((ApiErrorResult)apiResult).LoggerMessage);
                    return controller.BadRequest(apiResult);
            }
        }
    }
}
