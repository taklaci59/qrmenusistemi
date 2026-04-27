# 📱 QR Menü & Sipariş Sistemi

Restoran ve kafeler için tasarlanmış, güvenli QR kod doğrulama mekanizmasına sahip, modern bir dijital menü ve sipariş yönetim sistemi.

## ✨ Özellikler

- **Güvenli QR Doğrulama:** Her masa için benzersiz ve HMAC-SHA256 ile imzalanmış QR kodları. Token doğrulaması olmadan menüye erişim engellenir.
- **Dijital Menü:** Kategorize edilmiş ürün listesi, ürün detayları ve görseller.
- **Sepet ve Sipariş:** Kullanıcıların masalarından doğrudan sipariş verebilmesini sağlayan sepet sistemi.
- **Ödeme Sistemi (Görsel):** Sipariş onay süreci görsel olarak simüle edilmiştir; gerçek bir ödeme altyapısı (iyzico, Stripe vb.) içermez. Kullanıcıların kendi ödeme yöntemlerini entegre etmesi gerekir.
- **Admin Paneli:** Siparişleri anlık takip etme, durum güncelleme (Hazırlanıyor, Servis Edildi vb.), ürün ve kategori yönetimi.
- **Session Tabanlı Takip:** Doğrulanan masa numarasının session üzerinde güvenli bir şekilde saklanması.

## 🛠 Teknoloji Yığını

- **Backend:** .NET 9 MVC
- **Database:** SQLite & Entity Framework Core
- **Güvenlik:** HMAC-SHA256 URL Signing, Cookie Authentication
- **Frontend:** Bootstrap 5, JavaScript, CSS3

## 🚀 Kurulum ve Çalıştırma (Önemli)

Bu proje QR kod doğrulaması gerektirdiği için doğrudan `/Home/Menu` sayfasına gidilemez.

1. Projeyi çalıştırın:
   ```bash
   dotnet run
   ```
2. **Admin Girişi:** Ürün eklemek ve siparişleri görmek için `/Account/Login` üzerinden giriş yapın.
3. **Menüye Erişim:** Menüyü test etmek için `qr/file.csv` dosyasındaki örnek linkleri kullanın veya geçerli bir token oluşturun.
   - Örnek: `http://localhost:5001/Home/Menu?table=5&token=b6f8489328da`

## 📂 Klasör Yapısı

- `qr/`: Önceden oluşturulmuş QR kod görselleri ve link listesi.
- `Services/UrlSigner.cs`: QR kodlarının güvenliğini sağlayan imzalama algoritması.
- `wwwroot/uploads/`: Ürün görsellerinin saklandığı dizin.

---
## 📄 Lisans & Uyarılar

> [!IMPORTANT]
> **Ödeme Sistemi Hakkında:** Bu projedeki sepet ve ödeme onay süreci tamamen **görsel simülasyondur**. Sistem üzerinde gerçek bir para transferi veya ödeme altyapısı (Sanal POS) bulunmamaktadır. Ticari kullanım için iyzico, Stripe veya benzeri bir ödeme yönteminin geliştirici tarafından manuel olarak entegre edilmesi gerekmektedir.

Bu proje MIT lisansı ile lisanslanmıştır.

---
*Developed with ❤️ by taklaci59*

