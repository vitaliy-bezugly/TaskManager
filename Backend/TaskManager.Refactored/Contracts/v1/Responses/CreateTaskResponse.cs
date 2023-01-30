using TaskManager.Refactored.Contracts.v1.Responses.Abstract;

namespace TaskManager.Refactored.Contracts.v1.Responses;

public class CreateTaskResponse : BaseResponse
{
    public string Title { get; set; }
}
