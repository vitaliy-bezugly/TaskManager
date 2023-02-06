namespace TaskManager.Refactored.Services.Abstract;

public interface IClaimParser
{
    Guid GetUserId();
    string GetEmail();
    string GetUsername();
}
