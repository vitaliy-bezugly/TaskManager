using TaskManager.Api.Contracts.v1.Responses.Abstract;

namespace TaskManager.Api.Contracts.v1.Responses;

public class GetTaskResponse : BaseResponse
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsImportant { get; set; }
    public bool IsComplited { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ExpirationTime { get; set; }
}