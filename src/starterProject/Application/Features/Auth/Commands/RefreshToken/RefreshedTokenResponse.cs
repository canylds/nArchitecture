using Core.Application.Responses;
using Core.Security.JWT;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshedTokenResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public Domain.Entities.RefreshToken RefreshToken { get; set; }

    public RefreshedTokenResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public RefreshedTokenResponse(AccessToken accessToken, Domain.Entities.RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
