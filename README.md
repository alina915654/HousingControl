# HousingControl

Система управления жилищным фондом для учета многоквартирных домов, управляющих организаций, проверок и жалоб жителей.

## О проекте

Веб-приложение для автоматизации деятельности управляющих компаний и ТСЖ. Позволяет вести учет домов, проводить проверки, фиксировать нарушения и обрабатывать жалобы жильцов. Реализовано в качестве учебного проекта по производственной практике.

## 🛠 Технологии

*   **Бэкенд:** ASP.NET Core / C#
*   **Фронтенд:** Visual Studio 2022
*   **База данных:** MS SQL Server 2019
*   **ORM:** Entity Framework Core
*   **Инструменты разработки:** Visual Studio 2022

## 📋 Предварительные требования

Перед запуском убедитесь, что на вашем компьютере установлены:

1.  [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2.  [SQL Server 2019 Express](https://www.microsoft.com/ru-ru/sql-server/sql-server-downloads)
3.  [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/ru-ru/sql/ssms/download-sql-server-management-studio-ssms)
4.  [Visual Studio 2022](https://visualstudio.microsoft.com/ru/vs/)

## 🚀 Запуск проекта

### 1. Клонирование репозитория
git clone https://github.com/alina915654/HousingControl.git
cd HousingControl

2. Настройка базы данных

Вариант А: Автоматическое создание (через миграции EF Core)
Убедитесь, что SQL Server запущен

В файле appsettings.json проверьте строку подключения:


"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HousingControlDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

В консоли диспетчера пакетов выполните:

Update-Database
Вариант Б: Ручное создание (из скрипта)
Откройте SSMS и подключитесь к вашему серверу

Создайте новую базу данных HousingControl

Откройте и выполните SQL-скрипт из папки Database/ проекта

3. Запуск приложения

- Через Visual Studio:
Откройте HousingControl.sln

- Восстановите пакеты NuGet:
Нажмите Ctrl + F5

- Через командную строку:
dotnet restore
dotnet run

Приложение будет доступно по адресу https://localhost:7000 или http://localhost:5000.

📊 Структура базы данных
База данных включает следующие таблицы:

ManagementOrganizations - Управляющие организации (УК, ТСЖ, ЖСК)

Buildings - Многоквартирные дома

Inspections - Проверки домов

Violations - Нарушения, выявленные при проверках

Complaints - Жалобы жителей

Users - Пользователи системы (Инспекторы, Администраторы)

👤 Учетные записи по умолчанию
Для входа в систему используйте:

- Инспектор:
Логин: inspector
Пароль: Password123!

- Администратор:
Логин: admin
Пароль: Admin123!

📄 Лицензия
Этот проект создан в учебных целях.

👤 Автор
Алина - alina915654 на GitHub
