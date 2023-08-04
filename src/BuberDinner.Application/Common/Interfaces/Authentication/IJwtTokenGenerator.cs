using BuberDinner.Domain.Entities;

namespace BuberDinner.Application;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
