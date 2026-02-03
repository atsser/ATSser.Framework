using Microsoft.Extensions.Configuration; //Enables reading configuration from JSON and environment variables - Centralized configuration is mandatory for enterprise automation
using NUnit.Framework; //Brings NUnit attributes like [SetUp], [TearDown] - Required to control test lifecycle
using NUnit.Framework.Internal;
using OpenQA.Selenium; //Selenium WebDriver interfaces - Needed to control browser
using System.Text;

namespace ATSser.Framework.Core;

public abstract class BaseTest //Base class for all test classes - Enforces common setup & teardown logic
{
    protected IWebDriver Driver = default!;//Holds WebDriver instance - Shared browser instance for all tests
    protected FrameworkSettings Settings = default!; //Strongly typed settings object	Avoids hard-coded values

    [OneTimeSetUp]//Runs once before all tests in a class	Improves performance
    public void OneTimeSetUp()
    {
        // IMPORTANT:
        // These packages fix the exact compile errors you got earlier:
        // - IConfigurationBuilder.AddEnvironmentVariables()  -> Microsoft.Extensions.Configuration.EnvironmentVariables
        // - IConfigurationRoot.Bind / Get<T>()               -> Microsoft.Extensions.Configuration.Binder
        var config = new ConfigurationBuilder() //Builds configuration from multiple sources - Flexible config management
            .SetBasePath(TestContext.CurrentContext.TestDirectory) //Points to test execution folder - Ensures config file is always found
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false) //Loads config from JSON file - Industry standard config approach
            .AddEnvironmentVariables() //override appsettings.json with env vars if needed //Allows overriding values via environment - Required for CI/CD pipelines
            .Build();//Finalizes configuration - Creates IConfigurationRoot

        // Read from "Settings" section
        Settings = config.GetSection("Settings").Get<FrameworkSettings>() ?? new FrameworkSettings();
        //Reads only the Settings section	Clean separation of config
        //Maps JSON to C# object - Type-safe configuration
        Driver = DriverFactory.Create(Settings);//Creates browser using settings - Single responsibility + scalability
    }

    [SetUp]//Runs before each test - Ensures fresh navigation
    public void SetUp()
    {
        Driver.Navigate().GoToUrl(Settings.BaseUrl);//Opens application URL - Avoids duplication in tests
    }

    [TearDown]//Runs after each test - Hook for screenshots / cleanup
    public void TearDown()
    {
        try
        {
            // If test failed, you can add screenshot here for later
            // var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();Capture screenshot on failure - Debugging & reporting
        }
        catch
        {
            // ignore
        }
    }

    [OneTimeTearDown]//Runs once after all tests - Proper resource cleanup
    public void OneTimeTearDown()
    {
        try { Driver?.Quit(); } catch { /* ignore */ }//Closes browser - Prevents zombie processes
        try { Driver?.Dispose(); } catch { /* ignore */ }//Releases memory - Best practice in long test runs

    }
}
//Why This BaseTest Is Enterprise-Grade
//Centralized WebDriver	- No duplicate browser logic
//Strongly typed config	- No magic strings
//Environment override support	- CI/CD ready
//NUnit lifecycle hooks	- Clean test control
//Scalable architecture	- Easy to extend for parallel runs
//Clean teardown	- Stable long executions