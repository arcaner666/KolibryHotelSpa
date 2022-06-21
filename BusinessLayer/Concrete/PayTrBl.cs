using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using Entities.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Concrete;

public class PayTrBl : IPayTrBl
{
    private readonly IMapper _mapper;

    public PayTrBl(
        IMapper mapper
    )
    {
        _mapper = mapper;
    }

    public IDataResult<PayTrIframeDto> GetIframeToken(PayTrIframeDto payTrIframeDto)
    {
        // ####################### DÜZENLEMESİ ZORUNLU ALANLAR #######################
        //
        // API Entegrasyon Bilgileri - Mağaza paneline giriş yaparak BİLGİ sayfasından alabilirsiniz.
        string merchant_id = "288185";
        string merchant_key = "CR9Wfa4XjuDZq3YE";
        string merchant_salt = "b4BFeLnMax469SMo";
        //
        // Müşterinizin sitenizde kayıtlı veya form vasıtasıyla aldığınız eposta adresi
        string emailstr = payTrIframeDto.Email;
        //
        // Tahsil edilecek tutar. 9.99 için 9.99 * 100 = 999 gönderilmelidir.
        int payment_amountstr = payTrIframeDto.PaymentAmount * 100;
        //
        // Sipariş numarası: Her işlemde benzersiz olmalıdır!! Bu bilgi bildirim sayfanıza yapılacak bildirimde geri gönderilir.
        string merchant_oid = payTrIframeDto.MerchantOid;
        //
        // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız ad ve soyad bilgisi
        string user_namestr = payTrIframeDto.NameSurname;
        //
        // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız adres bilgisi
        string user_addressstr = payTrIframeDto.Address;
        //
        // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız telefon bilgisi
        string user_phonestr = payTrIframeDto.Phone;
        //
        // Başarılı ödeme sonrası müşterinizin yönlendirileceği sayfa
        // !!! Bu sayfa siparişi onaylayacağınız sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
        // !!! Siparişi onaylayacağız sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
        string merchant_ok_url = payTrIframeDto.MerchantOkUrl;
        //
        // Ödeme sürecinde beklenmedik bir hata oluşması durumunda müşterinizin yönlendirileceği sayfa
        // !!! Bu sayfa siparişi iptal edeceğiniz sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
        // !!! Siparişi iptal edeceğiniz sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
        string merchant_fail_url = payTrIframeDto.MerchantFailUrl;
        //        
        // !!! Eğer bu örnek kodu sunucuda değil local makinanızda çalıştırıyorsanız
        // buraya dış ip adresinizi (https://www.whatismyip.com/) yazmalısınız. Aksi halde geçersiz paytr_token hatası alırsınız.
        string user_ip = payTrIframeDto.UserIp;
        //
        // ÖRNEK user_basket oluşturma - Ürün adedine göre object'leri çoğaltabilirsiniz
        List<InvoiceDetailDto> user_basket = payTrIframeDto.UserBasket;
        /* ############################################################################################ */


        // İşlem zaman aşımı süresi - dakika cinsinden
        string timeout_limit = "30";
        //
        // Hata mesajlarının ekrana basılması için entegrasyon ve test sürecinde 1 olarak bırakın. Daha sonra 0 yapabilirsiniz.
        string debug_on = "1";
        //
        // Mağaza canlı modda iken test işlem yapmak için 1 olarak gönderilebilir.
        string test_mode = "1";
        //
        // Taksit yapılmasını istemiyorsanız, sadece tek çekim sunacaksanız 1 yapın
        string no_installment = "0";
        //
        // Sayfada görüntülenecek taksit adedini sınırlamak istiyorsanız uygun şekilde değiştirin.
        // Sıfır (0) gönderilmesi durumunda yürürlükteki en fazla izin verilen taksit geçerli olur.
        string max_installment = "0";
        //
        // Para birimi olarak TL, EUR, USD gönderilebilir. USD ve EUR kullanmak için kurumsal@paytr.com 
        // üzerinden bilgi almanız gerekmektedir. Boş gönderilirse TL geçerli olur.
        string currency = payTrIframeDto.Currency;
        //
        // Türkçe için tr veya İngilizce için en gönderilebilir. Boş gönderilirse tr geçerli olur.
        string lang = payTrIframeDto.Language;

        // Gönderilecek veriler oluşturuluyor
        NameValueCollection data = new NameValueCollection();
        data["merchant_id"] = merchant_id;
        data["user_ip"] = user_ip;
        data["merchant_oid"] = merchant_oid;
        data["email"] = emailstr;
        data["payment_amount"] = payment_amountstr.ToString();
        //
        // Sepet içerİği oluşturma fonksiyonu, değiştirilmeden kullanılabilir.
        string user_basketstr = JsonConvert.SerializeObject(user_basket);
        data["user_basket"] = user_basketstr;
        //
        // Token oluşturma fonksiyonu, değiştirilmeden kullanılmalıdır.
        string birlestir = string.Concat(merchant_id, user_ip, merchant_oid, emailstr, payment_amountstr.ToString(), user_basketstr, no_installment, max_installment, currency, test_mode, merchant_salt);
        HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
        byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(birlestir));
        data["paytr_token"] = Convert.ToBase64String(b);
        //
        data["debug_on"] = debug_on;
        data["test_mode"] = test_mode;
        data["no_installment"] = no_installment;
        data["max_installment"] = max_installment;
        data["user_name"] = user_namestr;
        data["user_address"] = user_addressstr;
        data["user_phone"] = user_phonestr;
        data["merchant_ok_url"] = merchant_ok_url;
        data["merchant_fail_url"] = merchant_fail_url;
        data["timeout_limit"] = timeout_limit;
        data["currency"] = currency;
        data["lang"] = lang;

        using WebClient client = new();
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        byte[] result = client.UploadValues("https://www.paytr.com/odeme/api/get-token", "POST", data);
        string ResultAuthTicket = Encoding.UTF8.GetString(result);
        dynamic json = JValue.Parse(ResultAuthTicket);

        if (json.status == "success")
        {
            payTrIframeDto.IframeToken = json.token;
            return new SuccessDataResult<PayTrIframeDto>(payTrIframeDto, Messages.PayTrIframeTokenGenerated);
        }

        return new ErrorDataResult<PayTrIframeDto>("PAYTR IFRAME failed. reason:" + json.reason + ""); ;
    }
}
