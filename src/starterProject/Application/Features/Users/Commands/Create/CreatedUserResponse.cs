using Core.Application.Responses;

namespace Application.Features.Users.Commands.Create;

public class CreatedUserResponse : IResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public CreatedUserResponse()
    {
        Email = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public CreatedUserResponse(int id, string email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;

    }
}