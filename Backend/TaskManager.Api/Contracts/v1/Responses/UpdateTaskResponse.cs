using TaskManager.Api.Contracts.v1.Responses.Abstract;

namespace TaskManager.Api.Contracts.v1.Responses;

public class UpdateTaskResponse : BaseResponse
{
    public string Title { get; set; }
}
