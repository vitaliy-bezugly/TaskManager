using System.ComponentModel.DataAnnotations;

namespace Authentication.Common;

public class AuthenticationSuccessResponse
{
    public string access_token { get; set; }
}
