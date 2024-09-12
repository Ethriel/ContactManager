using ContactManager.Services.Model.Utility.ApiResult.Abstraction;
using Newtonsoft.Json;

namespace ContactManager.Services.Model.Utility.ApiResult.Implementation
{


    public abstract class ApiResult : IApiResult
    {
        [JsonIgnore]
        public ApiResultStatus ApiResultStatus { get; set; }
        public string Message { get; set; }

        public ApiResult() { }

        public ApiResult(ApiResultStatus apiResultStatus, string message = null)
        {
            SetResult(apiResultStatus, message);
        }

        public void SetResult(ApiResultStatus apiResultStatus, string message = null)
        {
            ApiResultStatus = apiResultStatus;
            Message = message;
        }

        public static ApiOkResult GetOkResult(ApiResultStatus apiResultStatus, string message = null, object data = null)
        {
            return new ApiOkResult(apiResultStatus, message, data);
        }

        public static ApiErrorResult GetErrorResult(ApiResultStatus apiResultStatus, string loggerMessage, string message = null, IEnumerable<string> errors = null)
        {
            return new ApiErrorResult(apiResultStatus, loggerMessage, message, errors);
        }
    }
}
