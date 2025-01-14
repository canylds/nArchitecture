using Core.Application.Dtos;

namespace Application.Features.Users.Queries.GetPagedList;

public class GetPagedListUserListItemDto : IDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public GetPagedListUserListItemDto()
    {
        Email = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public GetPagedListUserListItemDto(int id, string email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}