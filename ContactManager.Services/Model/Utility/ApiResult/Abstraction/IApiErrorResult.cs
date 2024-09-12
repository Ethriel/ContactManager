using Newtonsoft.Json;

namespace ContactManager.Services.Model.Utility.ApiResult.Abstraction
{
    public interface IApiErrorResult : IApiResult
    {
        IEnumerable<string> Errors { get; set; }
        [JsonIgnore]
        string LoggerMessage { get; set; }

        void SetErrorResult(ApiResultStatus apiResultStatus, string loggerMessage, string message = null, IEnumerable<string> errors = null);
    }
}
