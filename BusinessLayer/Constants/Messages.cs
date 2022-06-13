namespace BusinessLayer.Constants;

public static class Messages
{
    #region Authorization
    public const string AuthorizationCanNotGetClaimsPrincipal = "AuthorizationCanNotGetClaimsPrincipal";
    public const string AuthorizationDenied = "AuthorizationDenied";
    public const string AuthorizationLoggedIn = "AuthorizationLoggedIn";
    public const string AuthorizationLoggedOut = "AuthorizationLoggedOut";
    public const string AuthorizationRegistered = "AuthorizationRegistered";
    public const string AuthorizationTokenExpired = "AuthorizationTokenExpired";
    public const string AuthorizationTokenInvalid = "AuthorizationTokenInvalid";
    public const string AuthorizationTokensRefreshed = "AuthorizationTokensRefreshed";
    public const string AuthorizationWrongPassword = "AuthorizationWrongPassword";
    #endregion

    #region Currency
    public const string CurrenciesNotFound = "CurrenciesNotFound";
    public const string CurrenciesListed = "CurrenciesListed";
    public const string CurrencyListedByTitle = "CurrencyListedByTitle";
    public const string CurrencyNotFound = "CurrencyNotFound";
    #endregion

    #region Claim
    public const string ClaimListedByTitle = "ClaimListedByTitle";
    public const string ClaimNotFound = "ClaimNotFound";
    public const string ClaimsListed = "ClaimsListed";
    public const string ClaimsNotFound = "ClaimsNotFound";
    #endregion

    #region Person
    public const string PersonAdded = "PersonAdded";
    public const string PersonAlreadyExists = "PersonAlreadyExists";
    public const string PersonExtsListedById = "PersonExtsListedById";
    public const string PersonListedByEmail = "PersonListedByEmail";
    public const string PersonListedById = "PersonListedById";
    public const string PersonListedByPhone = "PersonListedByPhone";
    public const string PersonNotFound = "PersonNotFound";
    public const string PersonUpdated = "PersonUpdated";
    #endregion

    #region PersonClaim
    public const string PersonClaimAdded = "PersonClaimAdded";
    public const string PersonClaimAlreadyExists = "PersonClaimAlreadyExists";
    public const string PersonClaimExtsListedByPersonId = "PersonClaimExtsListedByPersonId";
    public const string PersonClaimsNotFound = "PersonClaimsNotFound";
    #endregion

}
