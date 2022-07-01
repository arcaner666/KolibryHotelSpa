namespace BusinessLayer.Constants;

public static class Messages
{
    #region Authorization
    public const string AuthorizationCanNotGetClaimsPrincipal = "Token'dan kullanıcı yetkileri alınamıyor.";
    public const string AuthorizationDenied = "Yetkilendirme reddedildi.";
    #endregion

    #region ContactForm
    public const string ContactFormAdded = "İletişim formu gönderildi.";
    public const string ContactFormAlreadyExists = "Bu iletişim bilgileri zaten gönderilmiş.";
    public const string ContactFormDeleted = "İletişim formu silindi.";
    public const string ContactFormListedById = "İletişim formu sıra numarasına göre getirildi.";
    public const string ContactFormNotFound = "İletişim formu bulunamadı.";
    public const string ContactFormsListed = "İletişim formları listelendi.";
    public const string ContactFormsNotFound = "İletişim formları bulunamadı.";
    #endregion

    #region Claim
    public const string ClaimListedByTitle = "Yetki, yetki adına göre getirildi.";
    public const string ClaimNotFound = "Yetki bulunamadı.";
    public const string ClaimsListed = "Yetkiler listelendi.";
    public const string ClaimsNotFound = "Yetkiler bulunamadı.";
    #endregion

    #region Currency
    public const string CurrenciesNotFound = "Dövizler bulunamadı.";
    public const string CurrenciesListed = "Dövizler listelendi.";
    public const string CurrencyExchangeRatesAreUpToDate = "Döviz kurları güncel.";
    public const string CurrencyExchangeRatesCanNotRetrieveFromSource = "Kaynaktan döviz kurları getirilemedi.";
    public const string CurrencyExchangeRatesCanNotUpdated = "Döviz kurları güncellenemedi.";
    public const string CurrencyExchangeRatesUpdated = "Döviz kurları güncellendi.";
    public const string CurrencyListedByTitle = "Dövizler isimlerine göre listelendi.";
    public const string CurrencyNotFound = "Döviz bulunamadı.";
    public const string CurrencyUpdated = "Döviz güncellendi.";
    #endregion

    #region PayTr
    public const string PayTrIframeTokenGenerated = "PayTR iframe token üretildi.";
    #endregion

    #region Person
    public const string PersonAdded = "Kullanıcı eklendi.";
    public const string PersonAlreadyExists = "Kullanıcı zaten sistemde kayıtlı.";
    public const string PersonDeleted = "Kullanıcı silindi.";
    public const string PersonExtAdded = "Kullanıcı eklendi.";
    public const string PersonExtDeleted = "Kullanıcı silindi.";
    public const string PersonExtsListed = "Kullanıcılar listelendi.";
    public const string PersonExtUpdated = "Kullanıcı güncellendi.";
    public const string PersonListedByEmail = "Kullanıcı e-posta adresine göre getirildi.";
    public const string PersonListedById = "Kullanıcı sıra numarasına göre getirildi.";
    public const string PersonListedByPhone = "Kullanıcı telefon numarasına göre getirildi.";
    public const string PersonLoggedIn = "Giriş Başarılı.";
    public const string PersonLoggedOut = "Çıkış başarılı.";
    public const string PersonNotFound = "Kullanıcı bulunamadı.";
    public const string PersonTokenExpired = "Oturum anahtarının süresi doldu.";
    public const string PersonTokenInvalid = "Oturum anahtarı geçersiz.";
    public const string PersonTokensRefreshed = "Oturum anahtarları yenilendi.";
    public const string PersonUpdated = "Kullanıcı güncellendi.";
    public const string PersonWrongPassword = "Kullanıcı adı veya şifre yanlış.";
    #endregion

    #region PersonClaim
    public const string PersonClaimAdded = "Kullanıcıya yetki atandı.";
    public const string PersonClaimAlreadyExists = "Kullanıcı zaten bu yetkiye sahip.";
    public const string PersonClaimDeleted = "Kullanıcının yetkisi silindi.";
    public const string PersonClaimExtsListedByPersonId = "Kullanıcıların yetkileri kişi sıra numarasına göre listelendi.";
    public const string PersonClaimListedById = "Kullanıcının yetkisi sıra numarasına göre getirildi.";
    public const string PersonClaimNotFound = "Kullanıcı yetkisi bulunamadı.";
    public const string PersonClaimsNotFound = "Kullanıcı yetkileri bulunamadı.";
    #endregion

    #region Invoice
    public const string InvoiceAdded = "Rezervasyon tamamlandı.";
    public const string InvoiceAlreadyExists = "Zaten böyle bir rezervasyon yapılmış.";
    public const string InvoiceDeleted = "Rezervasyon silindi.";
    public const string InvoiceExtAdded = "Rezervasyon tamamlandı.";
    public const string InvoiceExtDeleted = "Rezervasyon silindi.";
    public const string InvoiceExtsListed = "Rezervasyonlar listelendi.";
    public const string InvoiceExtUpdated = "Rezervasyon güncellendi.";
    public const string InvoiceListedById = "Rezervasyon getirildi.";
    public const string InvoiceNotFound = "Rezervasyon bulunamadı.";
    public const string InvoicesNotFound = "Rezervasyonlar bulunamadı.";
    public const string InvoiceUpdated = "Rezervasyon güncellendi.";
    #endregion

    #region InvoiceDetail
    public const string InvoiceDetailAdded = "Rezervasyon detay satırı eklendi.";
    public const string InvoiceDetailAlreadyExists = "İlgili rezervasyonun detay satırı zaten mevcut.";
    public const string InvoiceDetailDeleted = "Rezervasyon detay satırı silindi.";
    public const string InvoiceDetailListedById = "Rezervasyon detay satırı sıra numarasına göre getirildi.";
    public const string InvoiceDetailNotFound = "Rezervasyon detay satırı bulunamadı.";
    public const string InvoiceDetailsListedByInvoiceId = "Rezervasyon detay satırları rezervasyon sıra numarasına göre getirildi.";
    public const string InvoiceDetailsNotFound = "Rezervasyon detay satırları bulunamadı.";
    public const string InvoiceDetailUpdated = "Rezervasyon detay satırları güncellendi.";
    #endregion

    #region Suite
    public const string SuiteAdded = "Oda eklendi.";
    public const string SuiteAlreadyExists = "Oda zaten mevcut.";
    public const string SuiteDeleted = "Oda silindi.";
    public const string SuiteListedById = "Oda sıra numarasına göre listelendi.";
    public const string SuiteNotFound = "Oda bulunamadı.";
    public const string SuitesListed = "Odalar listelendi.";
    public const string SuitesNotFound = "Odalar bulunamadı.";
    public const string SuiteUpdated = "Oda güncellendi.";
    #endregion
}
