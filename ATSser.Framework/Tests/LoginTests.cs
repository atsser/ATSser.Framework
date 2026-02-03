using NUnit.Framework;
using ATSser.Framework.Core;
using ATSser.Framework.Pages;
using OpenQA.Selenium;

namespace ATSser.Framework.Tests;//Test layer namespace - Clear separation from pages & core

[TestFixture]//Marks class as NUnit test class - Required for NUnit execution
public sealed class LoginTests : BaseTest//Inherits BaseTest - Gets Driver & Settings automatically
{//sealed Prevents inheritance - Keeps test behavior controlled
    [Test]//Marks method as test case - NUnit execution trigger
    public void Login_Smoke()//Smoke test for login - Validates basic app availability
    {
        var login = new LoginPage(Driver, Settings.DefaultTimeoutSeconds);//Creates Login page object - Injects driver & timeout
        login.OpenLoginForm();//Opens login modal/page - Handles pre-login landing page

        // NOTE: Put valid credentials in appsettings.json (Settings:Username/Password)
        // or pass them as environment variables to avoid committing secrets.
        login.Login(Settings.Username, Settings.Password);//Performs login - Business-level action

        var dashboard = new DashboardPage(Driver, Settings.DefaultTimeoutSeconds);//Creates Dashboard page object - Post-login validation
        Assert.That(dashboard.IsLoaded(), Is.True, "Dashboard was not loaded. Check credentials or app locators.");//Checks dashboard visibility - Confirms successful login
        //Assertion - Determines test pass/fail //Meaningful error output - Faster debugging
    }
}

//      LoginTests
//         ?
//      BaseTest(Driver + Settings)
//         ?
//      LoginPage(Open + Login)
//         ?
//      DashboardPage(IsLoaded)
//         ?
//      Assertion

//Why This Test Is Well Designed
//Inherits BaseTest	- No duplicate setup code
//Uses Page Objects	- Clean, readable tests
//No locators in test	- Easy maintenance
//Explicit assertions	- Clear test intent
//Config-driven credentials	- CI/CD & security ready