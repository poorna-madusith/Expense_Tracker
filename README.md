# Expense Tracker Application

A comprehensive financial management web application built with ASP.NET Core MVC that helps users track their income and expenses with an intuitive, responsive interface.

![Expense Tracker Dashboard](./Expense%20Tracker/wwwroot/logo.png)

## Features

- **Secure User Authentication**
  - Complete user registration and login system
  - Profile management with password change functionality
  - ASP.NET Core Identity integration

- **Interactive Dashboard**
  - Summary widgets displaying total income, expenses, and current balance
  - Expense breakdown by category (doughnut chart)
  - Income vs Expense comparison (spline chart)
  - Income breakdown by category (doughnut chart)
  - Recent transactions list

- **Transaction Management**
  - Add, edit, and delete financial transactions
  - Categorize transactions as income or expense
  - Add notes to transactions
  - Date selection for transaction entries

- **Category Management**
  - Create custom categories with icons
  - Categorize as income or expense type
  - Edit and delete categories

- **Responsive Design**
  - Modern dark-themed UI
  - Works seamlessly across devices
  - Intuitive navigation with collapsible sidebar

## Technologies Used

- **Backend**
  - ASP.NET Core 8.0 MVC
  - Entity Framework Core 8.0
  - SQL Server
  - ASP.NET Core Identity

- **Frontend**
  - HTML5, CSS3, JavaScript
  - Bootstrap 5
  - Syncfusion EJ2 for .NET Core (data grid, charts, date picker)

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server (Local or Express)
- Visual Studio 2022 or Visual Studio Code

### Installation Steps

1. **Clone the repository**
   ```
   git clone https://github.com/yourusername/expense-tracker.git
   cd expense-tracker
   ```

2. **Update the connection string**
   - Open `appsettings.json`
   - Update the `DevConnection` string to point to your SQL Server instance

3. **Apply database migrations**
   ```
   dotnet ef database update
   ```
   Or using Package Manager Console in Visual Studio:
   ```
   Update-Database
   ```

4. **Run the application**
   ```
   dotnet run
   ```
   Or press F5 in Visual Studio

5. **Access the application**
   - Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## Usage

1. **Register a new account** or use the demo account if available
2. **Add categories** for your income and expenses
3. **Add transactions** to start tracking your finances
4. **View the dashboard** to see your financial overview
5. **Manage your profile** to update personal information

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Syncfusion for their excellent UI components
- ASP.NET Core team for the amazing framework
- Entity Framework Core team for the ORM