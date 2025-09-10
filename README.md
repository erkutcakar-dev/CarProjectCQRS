# 🚗 Car Project CQRS - Akıllı Araç Kiralama Yönetim Sistemi

## 📋 Proje Genel Bakış

Car Project CQRS, ASP.NET Core 9.0 ile geliştirilmiş, CQRS (Command Query Responsibility Segregation) desenini uygulayan kapsamlı bir araç kiralama yönetim sistemidir. Proje, akıllı AI destekli araç öneri sistemi, gerçek zamanlı veri entegrasyonu ve hem müşteriler hem de yöneticiler için modern kullanıcı arayüzü özelliklerine sahiptir.

## ✨ Temel Özellikler

### 🎯 Kullanıcı Deneyimi Özellikleri
- **Akıllı Araç Arama**: Marka, yakıt türü, şanzıman, koltuk sayısı, fiyat aralığı ve model yılı filtreleri ile gelişmiş filtreleme sistemi
- **AI Destekli Öneriler**: Groq AI API kullanarak kullanıcı gereksinimlerine göre akıllı araç önerileri
- **Otomatik Mesafe Hesaplama**: Özel veri seti kullanarak şehirler arası gerçek zamanlı kilometre hesaplamaları
- **Yakıt Fiyatı Entegrasyonu**: EIA API'den canlı yakıt fiyatı verileri ve otomatik TRY dönüşümü
- **Hava Durumu Bilgisi**: Daha iyi seyahat planlaması için güncel hava durumu bilgileri
- **Modern UI/UX**: Cental teması ve Sneat admin şablonu ile güzel, responsive arayüz

### 🔧 Admin Panel Özellikleri
- **Kapsamlı Dashboard**: Gerçek zamanlı istatistikleri gösteren 15+ dinamik widget
- **AI Araç Arama Asistanı**: Akıllı araç önerileri için entegre yapay zeka
- **Gelişmiş Filtreleme Sistemi**: Arama, sıralama ve sayfalama ile çoklu kriter filtreleme
- **Gerçek Zamanlı Veri Entegrasyonu**: Canlı yakıt fiyatları, hava durumu ve döviz kurları
- **Yönetim Araçları**: Araçlar, rezervasyonlar, çalışanlar ve hizmetler için tam CRUD işlemleri
- **E-posta Entegrasyonu**: MailKit kullanarak otomatik e-posta bildirimleri

## 🏗️ Mimari ve Teknoloji Yığını

### Backend Teknolojileri
- **Framework**: ASP.NET Core 9.0
- **Desen**: CQRS (Command Query Responsibility Segregation)
- **ORM**: Entity Framework Core 9.0.8
- **Veritabanı**: SQL Server-
- **E-posta Servisi**: MailKit 4.13.0

### Frontend Teknolojileri
- **UI Framework**: Bootstrap 5.3.3
- **Tema**: Cental Araç Kiralama Şablonu + Admin Şablonu
- **İkonlar**: Font Awesome 5.15.4, Bootstrap Icons
- **Fontlar**: Google Fonts (Lato, Montserrat)
- **JavaScript**: jQuery, Owl Carousel, Animate.css

### Veritabanı Yaklaşımı
- **Hibrit Yaklaşım**: Code First ve Database First metodolojisinin karışımı
- **Özel Veri Seti**: Türk şehirleri arası 25,000+ gerçek mesafe kaydı
- **Havaalanı Verisi**: Kapsamlı Türk havaalanı bilgileri
- **Entity Konfigürasyonu**: Optimal performans için özel model konfigürasyonları

## 🌐 Harici API Entegrasyonları

### 1. **Groq AI API** (Araç Önerileri)
- **Endpoint**: `https://api.groq.com/openai/v1/chat/completions`
- **Model**: Llama-3.1-8b-instant
- **Amaç**: Kullanıcı gereksinimlerine göre akıllı araç önerisi
- **Özellikler**: Araç önerileri için doğal dil işleme

### 2. **EIA (Enerji Bilgi İdaresi) API** (Yakıt Fiyatları)
- **Endpoint**: `https://api.eia.gov/v2/petroleum/pri/gnd/data/`
- **Amaç**: Gerçek zamanlı yakıt fiyatı verileri
- **Özellikler**: Otomatik TRY dönüşümü ile haftalık yakıt fiyatı güncellemeleri

### 3. **WeatherAPI** (Hava Durumu Bilgisi)
- **Endpoint**: `http://api.weatherapi.com/v1/current.json`
- **Amaç**: Güncel hava durumu koşulları
- **Özellikler**: Sıcaklık, nem, rüzgar hızı ve hava durumu koşulları

### 4. **ExchangeRate-API** (Döviz Dönüşümü)
- **Endpoint**: `https://v6.exchangerate-api.com/v6/{api_key}/latest/USD`
- **Amaç**: Gerçek zamanlı USD'den TRY'ye dönüşüm
- **Özellikler**: Yakıt fiyatı hesaplamaları için canlı döviz kuru güncellemeleri

## 📊 Veritabanı Şeması


## 📁 Proje Klasör Yapısı

```
CarProjectCQRS/
├── 📁 Controllers/                    # MVC Controller'ları
│   ├── AboutController.cs
│   ├── AdminController.cs
│   ├── AdminMessageController.cs
│   ├── AIController.cs
│   ├── BookingController.cs
│   ├── CarController.cs
│   ├── DistanceController.cs
│   ├── EmployeeController.cs
│   ├── FeatureController.cs
│   ├── MainUiController.cs
│   ├── MessageController.cs
│   ├── ReservationController.cs
│   ├── ServiceController.cs
│   ├── SliderController.cs
│   ├── TestimonialController.cs
│   └── TurkeyAirportController.cs
│
├── 📁 Context/                        # Entity Framework DbContext
│   └── CarProjectDbContext.cs
│
├── 📁 CQRSPattern/                    # CQRS Pattern Implementasyonu
│   ├── 📁 Commands/                   # Command Sınıfları
│   │   ├── 📁 AboutCommands/
│   │   ├── 📁 CarCommands/
│   │   ├── 📁 DistanceCommands/
│   │   ├── 📁 EmployeeCommands/
│   │   ├── 📁 FeatureCommands/
│   │   ├── 📁 MessageCommands/
│   │   ├── 📁 ReservationCommands/
│   │   ├── 📁 ServiceCommands/
│   │   ├── 📁 SliderCommands/
│   │   ├── 📁 TestimonialCommands/
│   │   └── 📁 TurkeyAirportCommands/
│   │
│   ├── 📁 Handlers/                   # Handler Sınıfları
│   │   ├── 📁 AboutHandlers/
│   │   ├── 📁 CarHandlers/
│   │   ├── 📁 DistanceHandlers/
│   │   ├── 📁 EmployeeHandlers/
│   │   ├── 📁 FeatureHandlers/
│   │   ├── 📁 MessageHandlers/
│   │   ├── 📁 ReservationHandlers/
│   │   ├── 📁 ServiceHandlers/
│   │   ├── 📁 SliderHandlers/
│   │   ├── 📁 TestimonialHandlers/
│   │   └── 📁 TurkeyAirportHandlers/
│   │
│   ├── 📁 Queries/                    # Query Sınıfları
│   │   ├── 📁 AboutQueries/
│   │   ├── 📁 CarQueries/
│   │   ├── 📁 DistanceQueries/
│   │   ├── 📁 EmployeeQueries/
│   │   ├── 📁 FeatureQueries/
│   │   ├── 📁 MessageQueries/
│   │   ├── 📁 ReservationQueries/
│   │   ├── 📁 ServiceQueries/
│   │   ├── 📁 SliderQueries/
│   │   ├── 📁 TestimonialQueries/
│   │   └── 📁 TurkeyAirportQueries/
│   │
│   └── 📁 Results/                    # Result DTO'ları
│       ├── 📁 About/
│       ├── 📁 Car/
│       ├── 📁 Distance/
│       ├── 📁 Employee/
│       ├── 📁 Feature/
│       ├── 📁 Message/
│       ├── 📁 Reservation/
│       ├── 📁 Service/
│       ├── 📁 Slider/
│       ├── 📁 Testimonial/
│       └── 📁 TurkeyAirport/
│
├── 📁 Entities/                       # Domain Modelleri
│   ├── About.cs
│   ├── Car.cs
│   ├── Distance.cs
│   ├── Employee.cs
│   ├── Feature.cs
│   ├── Message.cs
│   ├── Reservation.cs
│   ├── Service.cs
│   ├── Slider.cs
│   ├── Testimonial.cs
│   └── TurkeyAirport.cs
│
├── 📁 Migrations/                      # Entity Framework Migrations
│   ├── 20250830111322_mig_firstmig.cs
│   ├── 20250830111322_mig_firstmig.Designer.cs
│   ├── 20250907181410_AddMessageTable.cs
│   ├── 20250907181410_AddMessageTable.Designer.cs
│   └── CarProjectDbContextModelSnapshot.cs
│
├── 📁 Models/                         # View Modelleri ve DTO'lar
│   ├── CarBookingModels.cs
│   ├── CarouselViewModel.cs
│   ├── DashboardViewModel.cs
│   ├── EiaGasPriceResponse.cs
│   └── ErrorViewModel.cs
│
├── 📁 Services/                       # Business Logic Servisleri
│   ├── AIService.cs                   # Groq AI API Entegrasyonu
│   └── MailService.cs                 # E-posta Gönderim Servisi
│
├── 📁 ViewComponents/                 # Yeniden Kullanılabilir UI Bileşenleri
│   ├── 📁 AdminViewComponents/
│   │   ├── AdminFooterComponentPartial.cs
│   │   ├── AdminHeadComponentPartial.cs
│   │   ├── AdminNavbarComponentPartial.cs
│   │   └── AdminSidebarComponentPartial.cs
│   └── 📁 MainUiViewComponents/
│       └── [17 adet ViewComponent]
│
├── 📁 Views/                          # Razor View Dosyaları
│   ├── 📁 About/
│   ├── 📁 Admin/
│   ├── 📁 AdminMessage/
│   ├── 📁 AI/
│   ├── 📁 Booking/
│   ├── 📁 Car/
│   ├── 📁 Distance/
│   ├── 📁 Employee/
│   ├── 📁 Feature/
│   ├── 📁 MainUi/
│   ├── 📁 Reservation/
│   ├── 📁 Service/
│   ├── 📁 Shared/
│   ├── 📁 Slider/
│   ├── 📁 Testimonial/
│   ├── 📁 TurkeyAirport/
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
│
├── 📁 wwwroot/                        # Statik Dosyalar
│   ├── 📁 Cental-1.0.0/              # Ana Website Teması
│   ├── 📁 css/
│   ├── 📁 js/
│   ├── 📁 lib/                        # JavaScript Kütüphaneleri
│   ├── 📁 sneat-1.0.0/               # Admin Panel Teması
│   └── favicon.ico
│
├── 📁 Properties/
│   └── launchSettings.json
│
├── appsettings.json                   # Uygulama Konfigürasyonu
├── appsettings.Development.json
├── CarProjectCQRS.csproj             # Proje Dosyası
├── Program.cs                        # Uygulama Giriş Noktası
├── README.md                         # Proje Dokümantasyonu
└── READMEeng.md                         # Proje Dokümantasyonu

```

**Ana Özellikler:**
- **CQRS Pattern**: Commands, Queries, Handlers ve Results klasörleri
- **Entity Framework**: Migrations ve DbContext
- **Dual Theme**: Cental (Ana Site) + Sneat (Admin Panel)
- **Service Layer**: AI ve Mail servisleri
- **ViewComponents**: Modüler UI bileşenleri
- **External APIs**: Groq AI, EIA, WeatherAPI, ExchangeRate-API entegrasyonları


### Temel Varlıklar
- **Cars**: Fiyatlandırma, özellikler ve kullanılabilirlik ile araç bilgileri
- **Reservations**: Teslim alma/bırakma konumları ile rezervasyon yönetimi
- **Employees**: Personel yönetim sistemi
- **Messages**: Müşteri iletişim takibi
- **Distances**: 25,000+ şehirler arası mesafe kaydı ile özel veri seti
- **TurkeyAirports**: Kapsamlı Türk havaalanı veritabanı
- **Features/Services**: Şirket hizmet teklifleri
- **Testimonials**: Müşteri geri bildirim sistemi
- **Sliders**: Ana sayfa carousel yönetimi
- **About**: Şirket bilgileri ve misyon

### Veri Seti Bilgileri
- **Mesafe Veri Seti**: Türk şehirleri arası 25,000+ gerçek mesafe kaydı
- **Havaalanı Veri Seti**: İllerle birlikte tam Türk havaalanı bilgileri
- **Veri Kaynağı**: Doğru mesafe hesaplamaları için özel derlenmiş veri seti
- **Güncelleme Sıklığı**: Periyodik güncellemelerle statik veri seti

## 🎨 Kullanıcı Arayüzü Özellikleri

### Ana Website (Cental Teması)
- **Responsive Tasarım**: Bootstrap 5 ile mobil öncelikli yaklaşım
- **Modern Carousel**: Öne çıkan içerik için dinamik slider sistemi
- **Etkileşimli Bileşenler**: 
  - Araç kategori filtreleri
  - Tarih seçicileri ile rezervasyon formu
  - AJAX gönderimi ile iletişim formu
  - Testimonial carousel
  - Takım üyesi vitrin
- **Renk Paleti**: Özel birincil (#EA001E) ve ikincil (#1F2E4E) renkler
- **Tipografi**: Lato ve Montserrat font aileleri

### Admin Panel (Sneat Teması)
- **Dashboard Widget'ları**: Gösteren 15+ dinamik kart:
  - Toplam araç sayısı
  - Aktif rezervasyonlar
  - Çalışan istatistikleri
  - Mesafe veritabanı boyutu
  - Gerçek zamanlı yakıt fiyatları
  - Güncel hava durumu koşulları
- **AI Entegrasyonu**: Akıllı araç öneri arayüzü
- **Gelişmiş Filtreleme**: Çoklu kriter arama ve sıralama işlevselliği
- **Veri Görselleştirme**: İş içgörüleri için grafikler ve istatistikler


## 🔧 Konfigürasyon

### API Anahtarları Kurulumu
1. **Groq AI API**: [Groq Console](https://console.groq.com/)'dan API anahtarı alın
2. **EIA API**: [EIA API](https://www.eia.gov/opendata/)'da kayıt olun
3. **WeatherAPI**: [WeatherAPI](https://www.weatherapi.com/)'den anahtar alın
4. **ExchangeRate-API**: [ExchangeRate-API](https://www.exchangerate-api.com/)'da kayıt olun

### E-posta Konfigürasyonu
- `appsettings.json`'da SMTP ayarlarını yapılandırın
- Kimlik doğrulama için Gmail Uygulama Şifresi kullanın
- Gmail hesabında 2FA'yı etkinleştirin

## 📱 Detaylı Özellikler

### Araç Arama ve Rezervasyon
- **Gelişmiş Filtreler**: Marka, model, yakıt türü, şanzıman, koltuk sayısı, fiyat aralığı, model yılı
- **Sıralama Seçenekleri**: Fiyat (artan/azalan), yıl, değerlendirme, marka
- **AI Önerileri**: Doğal dil tabanlı araç önerileri
- **Gerçek Zamanlı Kullanılabilirlik**: Canlı araç kullanılabilirlik durumu
- **Mesafe Hesaplama**: Şehirler arası otomatik kilometre hesaplama

### Admin Yönetimi
- **Dashboard Analitiği**: Gerçek zamanlı iş metrikleri
- **Araç Yönetimi**: Detaylı özelliklerle araç ekleme, düzenleme, silme
- **Rezervasyon Yönetimi**: Tam rezervasyon yaşam döngüsü yönetimi
- **Çalışan Yönetimi**: Personel bilgileri ve rol yönetimi
- **Mesaj Yönetimi**: Müşteri iletişim takibi
- **Hizmet Yönetimi**: Şirket hizmet teklifleri

### Veri Entegrasyonu
- **Canlı Yakıt Fiyatları**: Gerçek zamanlı yakıt maliyeti güncellemeleri
- **Hava Durumu Verisi**: Seyahat planlaması için güncel hava durumu koşulları
- **Döviz Kurları**: Fiyatlandırma için canlı döviz dönüşümü
- **Mesafe Veritabanı**: Kapsamlı şehirler arası mesafe bilgileri

## 🛠️ Geliştirme Özellikleri

### CQRS Deseni Uygulaması
- **Komutlar**: Oluşturma, Güncelleme, Silme işlemleri
- **Sorgular**: Optimize edilmiş veri alımı ile okuma işlemleri
- **İşleyiciler**: Ayrı komut ve sorgu işleyicileri


### Kod Organizasyonu
- **Varlıklar**: Domain modelleri
- **Bağlam**: Entity Framework DbContext
- **Kontrolörler**: Web arayüzü için MVC kontrolörleri
- **Servisler**: İş mantığı ve harici API entegrasyonu
- **ViewComponent'ler**: Yeniden kullanılabilir UI bileşenleri
- **Modeller**: Görünüm modelleri ve DTO'lar

## 📈 Performans Özellikleri

- **Async/Await**: Boyunca asenkron programlama
- **Entity Framework Optimizasyonu**: Optimize edilmiş sorgular ve konfigürasyonlar
- **Önbellekleme**: Sık erişilen veriler için stratejik önbellekleme
- **Responsive Tasarım**: Mobil optimize edilmiş kullanıcı arayüzü
- **API Hız Sınırlama**: Uygun API kullanım yönetimi

## 🔒 Güvenlik Özellikleri

- **Girdi Doğrulama**: Kapsamlı veri doğrulama
- **SQL Injection Önleme**: Parametreli sorgular
- **XSS Koruması**: Çıktı kodlama
- **HTTPS**: Güvenli iletişim
- **E-posta Güvenliği**: SSL üzerinden SMTP

## 📊 İş Zekası

### Dashboard Metrikleri
- Toplam araç sayısı
- Aktif rezervasyonlar
- Çalışan istatistikleri
- Mesafe veritabanı kullanımı
- Gerçek zamanlı yakıt fiyatı trendleri
- Hava durumu etki analizi

### Raporlama Özellikleri
- Rezervasyon analitiği
- Araç kullanım raporları
- Müşteri geri bildirim analizi
- Hizmet performans metrikleri

## 🌟 Benzersiz Noktalar

1. **AI Destekli Öneriler**: Araç kiralama alanında ilk türünden AI entegrasyonu
2. **Gerçek Zamanlı Veri Entegrasyonu**: Canlı yakıt fiyatları, hava durumu ve döviz kurları
3. **Kapsamlı Mesafe Veritabanı**: 25,000+ gerçek mesafe kaydı
4. **Hibrit Veritabanı Yaklaşımı**: Code First + Database First ile optimal performans
5. **Modern UI/UX**: Mükemmel kullanıcı deneyimi ile profesyonel tasarım
6. **CQRS Mimarisi**: Ölçeklenebilir ve sürdürülebilir kod tabanı
7. **Çoklu API Entegrasyonu**: Sorunsuz harici servis entegrasyonu

## 🤝 Katkıda Bulunma

1. Depoyu fork edin
2. Özellik dalı oluşturun (`git checkout -b feature/HarikaOzellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'HarikaOzellik ekle'`)
4. Dalı push edin (`git push origin feature/HarikaOzellik`)
5. Pull Request açın

## 👨‍💻 Yazar

**Erkut Çakar**
- E-posta: erkutcakar.dev@gmail.com
- GitHub: [@erkutcakar](https://github.com/erkutcakar)

## 🙏 Teşekkürler

- **Cental Teması**: Araç kiralama website şablonu
- **Sneat Teması**: Admin dashboard şablonu
- **Groq AI**: AI öneri servisi
- **EIA**: Yakıt fiyatı veri sağlayıcısı
- **WeatherAPI**: Hava durumu bilgi servisi
- **ExchangeRate-API**: Döviz dönüşüm servisi

## 📞 Destek

Destek için erkutcakar.dev@gmail.com adresine e-posta gönderin.

---

**ASP.NET Core 9.0 ve modern web teknolojileri ile ❤️ ile geliştirilmiştir**
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013622" src="https://github.com/user-attachments/assets/713ac950-e9ae-4684-91a9-46842f2af696" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013630" src="https://github.com/user-attachments/assets/41c119ab-8546-4991-b47d-ae6adcb80475" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013636" src="https://github.com/user-attachments/assets/72746d2a-65ba-4c91-aa25-e67101b0d584" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013640" src="https://github.com/user-attachments/assets/c14e7cf0-5b7a-472a-aef3-e57b48303c4c" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013654" src="https://github.com/user-attachments/assets/d63f1b43-8a90-45ec-8076-958eef7e63f6" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013700" src="https://github.com/user-attachments/assets/dfd70a3c-043e-480c-8310-ecc2048663e1" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013707" src="https://github.com/user-attachments/assets/e9563617-217e-4177-8afe-f4bea4634753" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013710" src="https://github.com/user-attachments/assets/c5e42882-34d7-4335-975a-72f2d463b19e" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013713" src="https://github.com/user-attachments/assets/3ee94dc3-130b-4e13-b5eb-e3a82d65cd62" 
  ❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013723" src="https://github.com/user-attachments/assets/8a3b16ba-bb51-4cb9-a3e7-874d7c074b40" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 013728" src="https://github.com/user-attachments/assets/f0afde6a-9426-4e01-ba2c-6663e5a8453c" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 014024" src="https://github.com/user-attachments/assets/b96e7b66-66ab-4b58-8530-38dde08eae62" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 014043" src="https://github.com/user-attachments/assets/26e7121d-31d4-42b2-a341-cad143348a51" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 014150" src="https://github.com/user-attachments/assets/032a9ac8-e87f-4f59-90bd-2213bd58bcf4" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 014255" src="https://github.com/user-attachments/assets/6f563d8b-227a-4a1c-8dc5-0b5782baefb3" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 014336" src="https://github.com/user-attachments/assets/ff9387a7-d58b-4678-a719-9d86576458ff" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 021834" src="https://github.com/user-attachments/assets/d2003e9f-efbb-4415-987f-37a87432e1d3" />
❤️
<img width="1920" height="1080" alt="Screenshot 2025-09-10 021909" src="https://github.com/user-attachments/assets/ccbf925c-5a00-4b5c-a250-a71065c6573a" />
❤️
<img width="1155" height="1080" alt="Screenshot 2025-09-10 021920" src="https://github.com/user-attachments/assets/3ddac8d8-b9d4-4ff4-a6c6-51db6ecd05a0" />
❤️
<img width="1168" height="1080" alt="Screenshot 2025-09-10 021933" src="https://github.com/user-attachments/assets/0a08dc6e-82d0-4276-9566-3c401bc781c1" />
❤️
<img width="452" height="554" alt="Screenshot 2025-09-10 022057" src="https://github.com/user-attachments/assets/ef0d791c-8967-4da4-8fc9-ade44fd75291" />
❤️
<img width="1644" height="869" alt="Screenshot 2025-09-10 022301" src="https://github.com/user-attachments/assets/b0206fe6-c3f0-45fc-8af4-10c80c7c0fe7" />
❤️

<img width="1644" height="869" alt="Screenshot 2025-09-10 022301" src="https://github.com/user-attachments/assets/5c888b0a-3e4c-468d-ad1b-ce328f4f1a43" />
