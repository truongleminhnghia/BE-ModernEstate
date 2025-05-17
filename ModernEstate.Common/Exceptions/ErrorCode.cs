namespace ModernEstate.Common.Exceptions
{
    public enum ErrorCode
    {
        UNAUTHORIZED,
        FORBIDDEN,
        TOKEN_EXPIRED,
        INVALID_TOKEN,
        INVALID_CREDENTIALS,
        USER_NOT_FOUND,
        INTERNAL_SERVER_ERROR,
        ACCOUNT_EXISTS,
        EMAIL_ALREADY_EXISTS,
        EMAIL_DOT_NOT_EXISTS,
        LIST_EMPTY,
        INVALID_PASSWORD,
        INVALID_EMAIL,
        TOKEN_NOT_NULL,
        ACCOUNT_STATUS_NOT_NULL,
        ROLE_NOT_NULL,
        ROLE_HAS_EXISTED,
        NOT_FOUND,
        INVALID_ACCOUNT_ROLE
    }
}