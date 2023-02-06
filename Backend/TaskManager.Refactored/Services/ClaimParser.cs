using System.Security.Claims;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Services;

public class ClaimParser : IClaimParser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ClaimParser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        string? userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            throw new InvalidOperationException("Http context accessor does not contain user id");

        return Guid.Parse(userId);
    }
    public string GetEmail()
    {
        string? email = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
            throw new InvalidOperationException("Http context accessor does not contain email");

        return email;
    }
    public string GetUsername()
    {
        string? email = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.GivenName)?.Value;

        if (email == null)
            throw new InvalidOperationException("Http context accessor does not contain username");

        return email;
    }
}