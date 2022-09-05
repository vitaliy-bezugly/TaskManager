using Entities;

namespace Mappers;

public static class RoleMapper
{
    public static RoleEntity ToEntity(this string role)
    {
        return new RoleEntity { Role = role };
    }
}
