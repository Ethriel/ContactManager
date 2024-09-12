using System.Text.Json.Serialization;
using ContactManager.Services.Model.Utility.ApiResult.Abstraction;

namespace ContactManager.Services.Model.Utility.ApiResult.Implementation
{
    public class ApiErrorResult : ApiResult, IApiErrorResult
    {
        public IEnumerable<string> Errors { get; set; }
        [JsonIgnore]
        public string LoggerMessage { get; set; }

        public ApiErrorResult()
        {

        }

        public ApiErrorResult(ApiResultStatus apiResultStatus, string loggerMessage, string message = null, IEnumerable<string> errors = null)
        {
            SetErrorResult(apiResultStatus, loggerMessage, message, errors);
        }

        public void SetErrorResult(ApiResultStatus apiResultStatus, string loggerMessage, string message = null, IEnumerable<string> errors = null)
        {
            ApiResultStatus = apiResultStatus;
            LoggerMessage = loggerMessage;
            Message = message;
            Errors = errors;
        }
    }
}
