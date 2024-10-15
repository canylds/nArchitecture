using Core.Application.Responses;
using Core.Security.JWT;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdatedUserFromAuthResponse : IResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AccessToken AccessToken { get; set; }

    public UpdatedUserFromAuthResponse()
    {
        Email = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        AccessToken = null!;
    }

    public UpdatedUserFromAuthResponse(int id, string email, string firstName, string lastName, AccessToken accessToken)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        AccessToken = accessToken;
    }
}