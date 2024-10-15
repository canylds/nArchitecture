using Core.Application.Responses;

namespace Application.Features.Users.Commands.Update;

public class UpdatedUserResponse : IResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UpdatedUserResponse()
    {
        Email = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public UpdatedUserResponse(int id, string firstName, string lastName, string email)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}