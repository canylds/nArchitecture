using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exception.Types;
using Core.Security.Enums;
using Core.Security.Hashing;
using Domain.Entities;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    {
        if (emailAuthenticator is null)
            throw new BusinessException(AuthMessages.EmailAuthenticatorDontExists);

        return Task.CompletedTask;
    }

    public Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is null)
            throw new BusinessException(AuthMessages.OtpAuthenticatorDontExists);

        return Task.CompletedTask;
    }

    public Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
            throw new BusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);

        return Task.CompletedTask;
    }

    public Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.ActivationKey is null)
            throw new BusinessException(AuthMessages.EmailActivationKeyDontExists);

        return Task.CompletedTask;
    }

    public Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            throw new BusinessException(AuthMessages.UserDontExists);

        return Task.CompletedTask;
    }

    public Task UserShouldNotBeHaveAuthenticator(User user)
    {
        if (user.AuthenticatorType != AuthenticatorType.None)
            throw new BusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);

        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            throw new BusinessException(AuthMessages.RefreshDontExists);

        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.RevokedDate != null && DateTime.UtcNow >= refreshToken.ExpirationDate)
            throw new BusinessException(AuthMessages.InvalidRefreshToken);

        return Task.CompletedTask;
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email);

        if (doesExists)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);

        return Task.CompletedTask;
    }
}
