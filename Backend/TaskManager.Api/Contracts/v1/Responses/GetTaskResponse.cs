using TaskManager.Api.Contracts.v1.Responses.Abstract;

namespace TaskManager.Api.Contracts.v1.Responses;

public class GetTaskResponse : BaseResponse
{
    public string title { get; set; }
    public string? description { get; set; }
    public bool isImportant { get; set; }
    public bool isComplited { get; set; }
    public DateTime creationTime { get; set; }
    public DateTime expirationTime { get; set; }
}