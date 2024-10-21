namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string SectionName = "Auth";

    public const string EmailAuthenticatorDontExists = "E-posta doğrulayıcı bulunmuyor.";
    public const string OtpAuthenticatorDontExists = "OTP doğrulayıcı bulunmuyor.";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "Zaten doğrulanmış OTP doğrulayıcı bulunuyor.";
    public const string EmailActivationKeyDontExists = "E-posta Aktivasyon Anahtarı bulunmuyor.";
    public const string UserDontExists = "Kullanıcı bulunmuyor.";
    public const string UserHaveAlreadyAAuthenticator = "Kullanıcının zaten bir doğrulayıcısı var.";
    public const string RefreshDontExists = "Yenileme bulunmuyor.";
    public const string InvalidRefreshToken = "Geçersiz yenileme token'ı.";
    public const string UserMailAlreadyExists = "Kullanıcı e-postası zaten mevcut.";
    public const string PasswordDontMatch = "Parola eşleşmiyor.";
}
