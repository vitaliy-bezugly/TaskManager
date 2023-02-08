namespace TaskManager.Api.Services.Abstract;

public interface IClaimParser
{
    Guid GetUserId();
    string GetEmail();
    string GetUsername();
}
