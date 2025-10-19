[TR]
# MvcBootcamp

**Temelden İleri Seviyeye ASP.NET MVC 5 Bootcamp Projesi**

Bu depo, tam teşekküllü bir içerik yönetim sistemi (CMS) uygulayarak **ASP.NET MVC 5** ve çok katmanlı mimari (n-Tier) prensiplerini öğrenmeye odaklanmış bir bootcamp projesidir.

[![.NET Framework](https://img.shields.io/badge/.NET_Framework-MVC_5-purple.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![GitHub repo size](https://img.shields.io/github/repo-size/abdullahhaktan/MvcBootcamp)](https://github.com/abdullahhaktan/MvcBootcamp)
[![Lisans](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

---

## 💻 Proje Hakkında

Bu proje, temel **CRUD** operasyonlarından gelişmiş mimari desenlere kadar **ASP.NET MVC**'nin ve çok katmanlı tasarımın tüm yönlerini pratik bir şekilde kapsar. Uygulama, hem yönetici hem de yazar panelleri içeren, kategorileri, başlıkları ve içerikleri yönetebilen tam özellikli bir içerik yönetim sistemi (CMS) olarak tasarlanmıştır.

### Öne Çıkan Özellikler 🌟

| Kategori | Açıklama | Uygulanan Teknolojiler/Desenler |
| :--- | :--- | :--- |
| **Mimari** | Sürdürülebilir ve test edilebilir kod yapısı. | **Katmanlı Mimari (n-Tier)**, BLL (Business Logic Layer), DAL (Data Access Layer) |
| **Veri Yönetimi** | Veritabanı işlemleri, filtreleme ve sayfalama. | **Entity Framework**, **Code First** Yaklaşımı, **PagedList** |
| **İş Mantığı** | İş kurallarının ayrılması ve merkezi yönetimi. | Repository Deseni, **Business Layer (Manager Sınıfları)** |
| **Güvenlik** | Kullanıcı kimlik doğrulama ve yetkilendirme. | Form Tabanlı **Authentication** ve **Authorization**, `[AllowAnonymous]` |
| **Doğrulama** | Veri girişlerinin istemci ve sunucu tarafı doğrulaması. | **FluentValidation** (Sunucu tarafı doğrulama) |
| **İletişim** | Kullanıcılar arası özel mesajlaşma modülü. | Gelen/Giden Kutusu, Taslaklar, Çöp Kutusu (Draft, Trash) |
| **İstatistik** | Proje verileri hakkında özet bilgiler. | Linq Sorguları ile temel istatistik hesaplama ve gösterimi |
| **UI/UX** | Yeniden kullanılabilir ve modüler görünüm bileşenleri. | **Partial Views**, Alan Yapısı (**Area**) |

---

## 🛠️ Kurulum ve Çalıştırma

Projenin yerel makinenizde çalıştırılması için aşağıdaki adımları izleyin:

### Gereksinimler

* [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) (Projeyi açmak için Visual Studio gereklidir)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Veritabanı işlemleri için)

### Adımlar

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/abdullahhaktan/MvcBootcamp.git](https://github.com/abdullahhaktan/MvcBootcamp.git)
    ```
2.  **Veritabanını Hazırlayın:**
    * Projenin `Web.config` dosyasındaki bağlantı dizesini (`ConnectionString`) kendi SQL Server ayarlarınıza göre güncelleyin.
    * **Entity Framework Code First Migrations** kullanarak veritabanını oluşturun veya güncelleyin.
3.  **Çözümü Açın:**
    * Visual Studio'yu açın ve projeyi (`MvcBootcamp.sln`) yükleyin.
4.  **Uygulamayı Çalıştırın:**
    * Visual Studio'da `F5` tuşuna basın veya `Debug > Start Debugging` seçeneğini kullanın.

---

[EN]
# MvcBootcamp

**ASP.NET MVC 5 Bootcamp Project — From Basics to Advanced**

This repository is a bootcamp project focused on learning **ASP.NET MVC 5** and multi-layered architecture (n-Tier) principles by implementing a fully functional Content Management System (CMS).

[![.NET Framework](https://img.shields.io/badge/.NET_Framework-MVC_5-purple.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![GitHub repo size](https://img.shields.io/github/repo-size/abdullahhaktan/MvcBootcamp)](https://github.com/abdullahhaktan/MvcBootcamp)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

---

## 💻 About the Project

This project covers all aspects of **ASP.NET MVC** and multi-layered design in a hands-on manner, from basic **CRUD** operations to advanced architectural patterns.  
The application is designed as a fully featured Content Management System (CMS) with both admin and author panels to manage categories, titles, and content.

### Key Features 🌟

| Category | Description | Applied Technologies / Patterns |
| :--- | :--- | :--- |
| **Architecture** | Sustainable and testable code structure. | **n-Tier Architecture**, BLL (Business Logic Layer), DAL (Data Access Layer) |
| **Data Management** | Database operations, filtering, and pagination. | **Entity Framework**, **Code First** Approach, **PagedList** |
| **Business Logic** | Separation and centralized management of business rules. | Repository Pattern, **Business Layer (Manager Classes)** |
| **Security** | User authentication and authorization. | Form-based **Authentication** and **Authorization**, `[AllowAnonymous]` |
| **Validation** | Client-side and server-side input validation. | **FluentValidation** (Server-side validation) |
| **Messaging** | Private messaging module between users. | Inbox/Outbox, Drafts, Trash |
| **Statistics** | Summary insights about project data. | Basic statistics calculated and displayed with **LINQ** queries |
| **UI/UX** | Reusable and modular view components. | **Partial Views**, **Area** structure |

---

## 🛠️ Setup and Run

Follow the steps below to run the project locally:

### Requirements

* [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) (Visual Studio is required to open the project)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (for database operations)

### Steps

1. **Clone the Repository:**
    ```bash
    git clone https://github.com/abdullahhaktan/MvcBootcamp.git
    ```
2. **Prepare the Database:**
    * Update the connection string (`ConnectionString`) in the `Web.config` file according to your SQL Server setup.
    * Use **Entity Framework Code First Migrations** to create or update the database.
3. **Open the Solution:**
    * Launch Visual Studio and open the project (`MvcBootcamp.sln`).
4. **Run the Application:**
    * Press `F5` or use `Debug > Start Debugging` in Visual Studio.

---


## 🖼️


<img width="400" height="435" alt="Ekran görüntüsü 2025-10-05 031103" src="https://github.com/user-attachments/assets/76a2a2a7-be8d-47cc-bac1-b9349041a4f7" />

---
<img width="400" height="436" alt="Ekran görüntüsü 2025-10-05 031146" src="https://github.com/user-attachments/assets/f2deae87-cced-4ed5-9a89-e287bbf58975" />

---
<img width="400" height="442" alt="Ekran görüntüsü 2025-10-05 031313" src="https://github.com/user-attachments/assets/5a95fb67-c9eb-476b-97d0-718352dad0e7" />

---
<img width="400" height="438" alt="Ekran görüntüsü 2025-10-05 031511" src="https://github.com/user-attachments/assets/dc10a0f9-b131-4232-b244-c42f9de942e2" />

---
