using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Models;

namespace NezerdoFarmApp.Mappings;

public class AuthMappings
{
    public static UserDto? MapToUserDto(User user)
    {
        if (user.Email != null)
            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
            };
        return null;
    }
}