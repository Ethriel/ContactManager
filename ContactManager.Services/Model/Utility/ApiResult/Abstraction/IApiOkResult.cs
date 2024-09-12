namespace ContactManager.Services.Model.Utility.ApiResult.Abstraction
{
    public interface IApiOkResult : IApiResult
    {
        object Data { get; set; }
        void SetOkResult(ApiResultStatus apiResultStatus, string message = null, object data = null);
    }
}
