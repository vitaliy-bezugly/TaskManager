namespace TaskManager.Refactored.Contracts.v1.Responses;

public class ChangePasswordSuccessResponse
{
    public string NewPassword { get; set; }
    public string access_token { get; set; }
}
