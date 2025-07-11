# 🏥 Nursing Student Vetting App

A web application built to streamline the vetting process for nursing program applicants at Walters State Community College. This app was developed as a **Capstone Project** for the AAS in Computer IT – Programming degree, in collaboration with two teammates.

---

## 📌 Overview

The **Nursing Student Vetting App** allows nursing program administrators to efficiently review and score applicants based on predefined criteria. The goal was to replace manual tracking (e.g., spreadsheets and paperwork) with a secure, scalable digital system to improve accuracy, consistency, and ease of use.

---

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core MVC (C#)
- **Database**: SQL Server with Entity Framework Core (Code-First)
- **Frontend**: Razor Views (MVC), HTML5, CSS
- **Version Control**: Git & GitHub
- **Project Management**: Trello (Agile workflow)

---

## ✅ Features

- Admin login and role-based access
- Input form for applicant details and credentials
- Automatic scoring based on qualifications (e.g., GPA, TEAS scores, prerequisites)
- Sort and filter applicants based on key criteria
- Persistent storage using SQL Server and Entity Framework Core
- Activity tracking and basic audit logging
- Responsive UI and clear UX for administrative users

---

## 📸 Screenshots

<!-- Add screenshots here if you have them -->
> _Coming Soon_

---

## 🚀 Getting Started

> ⚠️ This project is not deployed publicly but can be run locally.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or another local SQL setup)
- Visual Studio 2022 or later

### Setup

```bash
git clone https://github.com/SethWatson91/Nursing_Student_Vetting.git
cd Nursing\ Student\ Vetting/
dotnet ef database update
dotnet run
```
- register user john.doe@example.com
- Close and Restart app
- log out and back in
- select "Manage Users" in the top right
- remove the student role from john.doe@example.com by pressing "Remove Student" 
