# 🎓 EduSymphony - School Management System

![Project Status](https://img.shields.io/badge/status-active-success.svg)
![.NET Version](https://img.shields.io/badge/.NET-8.0-purple.svg)
![License](https://img.shields.io/badge/license-MIT-blue.svg)

> **EduSymphony** is a modern, comprehensive School Management System designed to streamline academic administration. Built with **ASP.NET Core 8 MVC** and **Tailwind CSS**, it features a robust dual-portal architecture for Administrators and Students, automating complex tasks like GPA calculation and transcript generation.

---

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core 8 MVC
* **Language:** C#
* **Database:** SQL Server (Entity Framework Core)
* **Frontend:** HTML5, Tailwind CSS (Dark Mode Theme)
* **Icons:** Feather Icons
* **Tools:** Visual Studio 2022

---

## ✨ Key Features

### 🔐 Admin Portal
* **Secure Authentication:** Role-based login system.
* **Dashboard:** Visual analytics for attendance trends, grade distribution, and total enrollments.
* **Student Management:** Add, edit, and filter students by course.
* **Course & Exam Management:** Create courses and schedule exams linked specifically to those courses.
* **Automated Grading:** Input raw scores (Mid-term, Quiz, Final) and let the system calculate GPA and Percentage automatically.
* **Result Generation:** Generate professional, printable result cards/transcripts.

### 🎓 Student Portal
* **Personalized Dashboard:** View current GPA, attendance percentage, and active assignments at a glance.
* **Smart Exam Schedule:** Only shows exams relevant to the student's enrolled course.
* **Result History:** Access past grades and performance reports.

---

## 🚀 Getting Started

Follow these instructions to set up the project locally.

### Prerequisites
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (Version 17.8 or newer)
* [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* SQL Server (LocalDB or Express)

### Installation

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/YOUR-USERNAME/SchoolManagementSystem.git](https://github.com/YOUR-USERNAME/SchoolManagementSystem.git)
    ```

2.  **Open the Project**
    * Launch **Visual Studio 2022**.
    * Open `SchoolMangamentSystem.sln`.

3.  **Configure Database**
    * Open `appsettings.json`.
    * Ensure the `ConnectionStrings:DefaultConnection` matches your local SQL Server instance.
    * *Example:* `Server=(localdb)\\mssqllocaldb;Database=SchoolDB;Trusted_Connection=True;`

4.  **Run Migrations**
    * Open **Tools** > **NuGet Package Manager** > **Package Manager Console**.
    * Run the command:
        ```powershell
        Update-Database
        ```

5.  **Run the Application**
    * Press `F5` or click the green **Play** button in Visual Studio.

---

## 🔑 Login Credentials (Demo)

After creating the database, you may need to register a user or insert a seed admin user in the `Users` table.

* **Role:** `Admin`
* **Email:** `admin@school.com` (Example)
* **Password:** `admin123` (Example)

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:
1.  Fork the repository.
2.  Create a new branch (`git checkout -b feature/YourFeature`).
3.  Commit your changes (`git commit -m 'Add some feature'`).
4.  Push to the branch (`git push origin feature/YourFeature`).
5.  Open a Pull Request.

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
