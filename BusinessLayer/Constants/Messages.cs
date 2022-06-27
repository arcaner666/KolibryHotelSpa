namespace BusinessLayer.Constants;

public static class Messages
{
    #region Authorization
    public const string AuthorizationCanNotGetClaimsPrincipal = "AuthorizationCanNotGetClaimsPrincipal";
    public const string AuthorizationDenied = "AuthorizationDenied";

    #endregion

    #region ContactForm
    public const string ContactFormAdded = "ContactFormAdded";
    public const string ContactFormAlreadyExists = "ContactFormAlreadyExists";
    public const string ContactFormDeleted = "ContactFormDeleted";
    public const string ContactFormListedById = "ContactFormListedById";
    public const string ContactFormNotFound = "ContactFormNotFound";
    public const string ContactFormsListed = "ContactFormsListed";
    public const string ContactFormsNotFound = "ContactFormsNotFound";
    #endregion

    #region Claim
    public const string ClaimListedByTitle = "ClaimListedByTitle";
    public const string ClaimNotFound = "ClaimNotFound";
    public const string ClaimsListed = "ClaimsListed";
    public const string ClaimsNotFound = "ClaimsNotFound";
    #endregion

    #region Currency
    public const string CurrenciesNotFound = "CurrenciesNotFound";
    public const string CurrenciesListed = "CurrenciesListed";
    public const string CurrencyExchangeRatesUpdated = "CurrencyExchangeRatesUpdated";
    public const string CurrencyExchangeRatesCanNotUpdated = "CurrencyExchangeRatesCanNotUpdated";
    public const string CurrencyListedByTitle = "CurrencyListedByTitle";
    public const string CurrencyNotFound = "CurrencyNotFound";
    #endregion

    #region PayTr
    public const string PayTrIframeTokenGenerated = "PayTrIframeTokenGenerated";
    public const string PayTrCanNotGenerateIframeToken = "PayTrCanNotGenerateIframeToken";
    #endregion

    #region Person
    public const string PersonAdded = "PersonAdded";
    public const string PersonAlreadyExists = "PersonAlreadyExists";
    public const string PersonDeleted = "PersonDeleted";
    public const string PersonExtAdded = "PersonExtAdded";
    public const string PersonExtDeleted = "PersonExtDeleted";
    public const string PersonExtsListed = "PersonExtsListed";
    public const string PersonExtsListedById = "PersonExtsListedById";
    public const string PersonExtUpdated = "PersonExtUpdated";
    public const string PersonListedByEmail = "PersonListedByEmail";
    public const string PersonListedById = "PersonListedById";
    public const string PersonListedByPhone = "PersonListedByPhone";
    public const string PersonLoggedIn = "PersonLoggedIn";
    public const string PersonLoggedOut = "PersonLoggedOut";
    public const string PersonNotFound = "PersonNotFound";
    public const string PersonTokenExpired = "PersonTokenExpired";
    public const string PersonTokenInvalid = "PersonTokenInvalid";
    public const string PersonTokensRefreshed = "PersonTokensRefreshed";
    public const string PersonUpdated = "PersonUpdated";
    public const string PersonWrongPassword = "PersonWrongPassword";
    #endregion

    #region PersonClaim
    public const string PersonClaimAdded = "PersonClaimAdded";
    public const string PersonClaimAlreadyExists = "PersonClaimAlreadyExists";
    public const string PersonClaimDeleted = "PersonClaimDeleted";
    public const string PersonClaimExtsListedByPersonId = "PersonClaimExtsListedByPersonId";
    public const string PersonClaimListedById = "PersonClaimListedById";
    public const string PersonClaimNotFound = "PersonClaimNotFound";
    public const string PersonClaimsNotFound = "PersonClaimsNotFound";
    #endregion

    #region Invoice
    public const string InvoiceAdded = "InvoiceAdded";
    public const string InvoiceAlreadyExists = "InvoiceAlreadyExists";
    public const string InvoiceDeleted = "InvoiceDeleted";
    public const string InvoiceExtAdded = "InvoiceExtAdded";
    public const string InvoiceExtDeleted = "InvoiceExtDeleted";
    public const string InvoiceExtsListed = "InvoiceExtsListed";
    public const string InvoiceExtsListedById = "InvoiceExtsListedById";
    public const string InvoiceExtUpdated = "InvoiceExtUpdated";
    public const string InvoiceListedById = "InvoiceListedById";
    public const string InvoiceListedByInvoiceId = "InvoiceListedByInvoiceId";
    public const string InvoiceNotFound = "InvoiceNotFound";
    public const string InvoicesNotFound = "InvoicesNotFound";
    public const string InvoiceUpdated = "InvoiceUpdated";
    #endregion

    #region InvoiceDetail
    public const string InvoiceDetailAdded = "InvoiceDetailAdded";
    public const string InvoiceDetailAlreadyExists = "InvoiceDetailAlreadyExists";
    public const string InvoiceDetailDeleted = "InvoiceDetailDeleted";
    public const string InvoiceDetailListedById = "InvoiceDetailListedById";
    public const string InvoiceDetailNotFound = "InvoiceDetailNotFound";
    public const string InvoiceDetailsListedByInvoiceId = "InvoiceDetailsListedByInvoiceId";
    public const string InvoiceDetailsNotFound = "InvoiceDetailsNotFound";
    public const string InvoiceDetailUpdated = "InvoiceDetailUpdated";
    #endregion

    #region Suite
    public const string SuiteAdded = "SuiteAdded";
    public const string SuiteAlreadyExists = "SuiteAlreadyExists";
    public const string SuiteDeleted = "SuiteDeleted";
    public const string SuiteListedById = "SuiteListedById";
    public const string SuiteNotFound = "SuiteNotFound";
    public const string SuitesListed = "SuitesListed";
    public const string SuitesNotFound = "SuitesNotFound";
    public const string SuiteUpdated = "SuiteUpdated";
    #endregion
}
