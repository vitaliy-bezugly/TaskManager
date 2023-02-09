namespace TaskManager.Api.Contracts.v1.Responses;

public class ChangePasswordSuccessResponse
{
    public string newPassword { get; set; }
    public string access_token { get; set; }
}
