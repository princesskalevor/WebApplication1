# BloodConnect - Blood Donation Management System

A comprehensive blood donation management system built for KNUST students using ASP.NET Core MVC.

## 🚀 Features

### Core Functionality
- **Donor Management** - Complete CRUD operations for donor registration and management
- **Blood Request System** - Request blood donations with full tracking
- **Course Management** - Manage academic courses for donor scoring
- **Score Tracking** - Track donor academic performance
- **Dashboard** - Real-time statistics and urgent request display
- **Search & Filter** - Find donors by blood type and hall

### Technical Features
- **Entity Framework Core** - Database ORM with SQL Server
- **Responsive Design** - Tailwind CSS for modern UI
- **CRUD Operations** - Full Create, Read, Update, Delete functionality
- **Data Validation** - Client and server-side validation
- **Modern UI** - Lucide icons and smooth transitions

## 📋 Prerequisites

- .NET 8.0 SDK
- SQL Server Express (or higher)
- Visual Studio 2022 (or VS Code)

## 🛠️ Setup Instructions

### 1. Database Setup

1. **Create the Database**
   ```sql
   -- Run the database creation script provided
   CREATE DATABASE [BLOODCONNECT]
   ```

2. **Run the Table Creation Script**
   ```bash
   # Option 1: Run the batch file
   setup-database.bat
   
   # Option 2: Run SQL script manually
   sqlcmd -S "DESKTOP-IF49OJQ\SQLEXPRESS" -d "BLOODCONNECT" -i "Database\CreateTables.sql"
   ```

### 2. Application Setup

1. **Clone/Download the project**
2. **Update Connection String** (if needed)
   ```json
   // appsettings.json
   "ConnectionStrings": {
     "AppConnection": "Server=DESKTOP-IF49OJQ\\SQLEXPRESS;Database=BloodConnect;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```

3. **Build and Run**
   ```bash
   dotnet build
   dotnet run
   ```

4. **Access the Application**
   - Navigate to `https://localhost:5001` or `http://localhost:5000`

## 📊 Database Schema

### Tables Created
- **DONOR** - Donor information (ID, Name, Blood Type, Hall, Department)
- **BLOODREQUEST** - Blood request tracking (Patient, Blood Type, Status, etc.)
- **COURSE** - Academic courses (ID, Name, Code)
- **SCORES** - Donor academic scores (DonorID, CourseID, Mark, Grade)

### Sample Data
The setup script includes sample data for testing:
- 5 sample donors with different blood types
- 3 sample blood requests
- 5 sample courses
- 10 sample scores

## 🎯 Available Routes

### Donor Management
- `GET /Donor` - List all donors
- `GET /Donor/Details/{id}` - View donor details
- `GET /Donor/Create` - Create new donor
- `POST /Donor/Create` - Submit new donor
- `GET /Donor/Edit/{id}` - Edit donor form
- `POST /Donor/Edit/{id}` - Update donor
- `GET /Donor/Delete/{id}` - Delete confirmation
- `POST /Donor/Delete/{id}` - Confirm deletion

### Blood Request Management
- `GET /BloodRequest` - List all blood requests
- `GET /BloodRequest/Details/{id}` - View request details
- `GET /BloodRequest/Create` - Create new request
- `POST /BloodRequest/Create` - Submit new request
- `GET /BloodRequest/Edit/{id}` - Edit request form
- `POST /BloodRequest/Edit/{id}` - Update request
- `GET /BloodRequest/Delete/{id}` - Delete confirmation
- `POST /BloodRequest/Delete/{id}` - Confirm deletion

### Course Management
- `GET /Course` - List all courses
- `GET /Course/Details/{id}` - View course details
- `GET /Course/Create` - Create new course
- `POST /Course/Create` - Submit new course
- `GET /Course/Edit/{id}` - Edit course form
- `POST /Course/Edit/{id}` - Update course
- `GET /Course/Delete/{id}` - Delete confirmation
- `POST /Course/Delete/{id}` - Confirm deletion

### Scores Management
- `GET /Scores` - List all donor scores

## 🎨 UI Features

### Design System
- **Tailwind CSS** - Utility-first CSS framework
- **Lucide Icons** - Modern icon set
- **Responsive Design** - Mobile-first approach
- **Color Scheme** - Red/Yellow gradient theme

### Components
- **Data Tables** - Sortable, responsive tables
- **Forms** - Validated input forms with error handling
- **Cards** - Information display cards
- **Alerts** - Success/error message alerts
- **Navigation** - Responsive navigation menu

## 🔧 Configuration

### Connection Strings
Update the connection string in `appsettings.json` to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "AppConnection": "Server=YOUR_SERVER\\INSTANCE;Database=BloodConnect;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### Database Service
The `DatabaseService.cs` contains raw SQL queries for advanced operations. Update the connection string there as well:

```csharp
public DatabaseService()
{
    _connStr = "Server=YOUR_SERVER\\INSTANCE;Database=BloodConnect;Trusted_Connection=True;TrustServerCertificate=True";
}
```

## 🚀 Deployment

### Development
```bash
dotnet run --environment Development
```

### Production
```bash
dotnet publish -c Release
dotnet run --environment Production
```

## 📝 Usage Examples

### Adding a New Donor
1. Navigate to `/Donor/Create`
2. Fill in donor information (Name, Blood Type, Hall, Department)
3. Submit the form
4. View the donor in the donor list

### Creating a Blood Request
1. Navigate to `/BloodRequest/Create`
2. Fill in patient information and blood requirements
3. Submit the request
4. Track the request status

### Managing Courses
1. Navigate to `/Course/Create`
2. Add course name and code
3. View courses and associated scores

## 🐛 Troubleshooting

### Common Issues

1. **Database Connection Error**
   - Verify SQL Server is running
   - Check connection string format
   - Ensure database exists

2. **Entity Framework Errors**
   - Run `dotnet ef database update`
   - Check model configurations

3. **Build Errors**
   - Restore packages: `dotnet restore`
   - Clean solution: `dotnet clean`
   - Rebuild: `dotnet build`

## 📞 Support

For issues or questions:
- Check the troubleshooting section
- Review the database setup script
- Verify all prerequisites are installed

## 🎉 Success!

Once setup is complete, you should have:
- ✅ Working BloodConnect application
- ✅ Sample data in all tables
- ✅ Full CRUD functionality for all entities
- ✅ Responsive UI with modern design
- ✅ Database properly configured

Happy coding! 🩸💉
