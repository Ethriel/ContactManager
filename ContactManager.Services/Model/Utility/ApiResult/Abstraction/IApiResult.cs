using Newtonsoft.Json;

namespace ContactManager.Services.Model.Utility.ApiResult.Abstraction
{
    public enum ApiResultStatus
    {
        Ok,
        NotFound,
        BadRequest
    }

    public interface IApiResult
    {
        [JsonIgnore]
        ApiResultStatus ApiResultStatus { get; set; }
        string Message { get; set; }
        void SetResult(ApiResultStatus apiResultStatus, string message = null);
    }
}
