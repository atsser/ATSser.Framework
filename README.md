# ATSser.Framework (Selenium + C# Demo)

This is a **ready-to-run** Selenium framework demo (single project inside a solution) :

```
ATSser.Framework
 ├─ Core
 │   ├─ DriverFactory.cs
 │   └─ BaseTest.cs
 ├─ Pages
 │   ├─ LoginPage.cs
 │   └─ DashboardPage.cs
 ├─ Utilities
 │   └─ WaitHelper.cs
 └─ Tests
     └─ LoginTests.cs
```

## Prerequisites
- .NET SDK 8
- Google Chrome installed

> Selenium 4 includes **Selenium Manager**, so ChromeDriver will be resolved automatically.

## Run
From the solution folder:

```bash
dotnet test
```

## Configure
Update `ATSser.Framework/appsettings.json`:

- `Settings:Username`
- `Settings:Password`

### Override via environment variables (recommended for secrets)
On Windows PowerShell:

```powershell
$env:Settings__Username="your_email"
$env:Settings__Password="your_password"
dotnet test
```

## Notes
- This template includes the packages that fix:
  - `AddEnvironmentVariables` extension method
  - `Bind / Get<T>()` (binder)

You can extend this to add:
- Screenshots on failure
- Extent/Allure reporting
- CI pipeline
- Parallel runs
