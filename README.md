# Employee Management System (Master-Detail SPA)
**Developer:** Sumaiya Akter

A full-featured **Master-Detail CRUD** application built with **ASP.NET MVC 5**. This project follows the **Code First Approach** and integrates advanced features ranging from Custom Authentication to a dynamic SPA-like user interface.

## 📁 Project Architecture
The project is organized into a clean, layered architecture for scalability:

* **Models:** Core entities including `Employee.cs`, `AcademicDetail.cs` (Detail table), and `Department.cs`.
* **DAL (Data Access Layer):** Managed via `AppDbContext.cs` for seamless SQL Server communication.
* **ViewModels:** Utilizes `EmployeeViewModel.cs` and `UserRoleViewModel.cs` to decouple data entities from the UI.
* **Security:** Implements `SecurityContext.cs` with table mappings for `tblUser` and `tblRole`.

## 🛠️ Tech Stack & Features
* **Framework:** ASP.NET MVC 5
* **Database:** SQL Server (Entity Framework Code First)
* **Authentication:** Custom Role-based authorization using `MyRoleProvider.cs`.
* **SPA Experience:** **jQuery AJAX** for dynamic content loading without page refreshes.
* **Validation:** Robust **Data Annotations** including a custom `PastDateAttribute.cs`.
* **UI/UX:** * Profile Image management with preview.
    * Server-side **Sorting, Searching, and Filtering**.
    * Interactive Dashboard with real-time statistics.

## 🚀 Getting Started
1.  **Clone the Repo:** `git clone https://github.com/yourusername/project-name.git`
2.  **Configuration:** Update the Connection String in `Web.config`.
3.  **Database:** Run `Update-Database` in the Package Manager Console.
4.  **Roles:** Register a user to start managing roles through the Administrator panel.

## 💡 Key Highlights
* **Custom Validation:** `PastDateAttribute` ensures logical data entry for date fields.
* **Granular Security:** Role-based access ensures only admins can reach sensitive management modules.
