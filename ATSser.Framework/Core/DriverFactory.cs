using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace ATSser.Framework.Core;

public static class DriverFactory//Factory class for WebDriver creation - Centralizes browser creation logic
{
    public static IWebDriver Create(FrameworkSettings settings)//Entry method to create WebDriver - Single entry point for all tests
    {
        // Selenium Manager (built-in) will auto-resolve a compatible driver for Chrome/Edge/Firefox
        // as long as the browser is installed. No separate driver download required for this demo.

        var browser = (settings.Browser ?? "chrome").Trim().ToLowerInvariant();//Reads browser from config with fallback - Prevents null config crashes
                                                    //Normalizes browser value - Avoids casing issues(Chrome, CHROME)
        return browser switch//Chooses browser implementation - Easily extensible for Edge/Firefox
        {
            "chrome" => CreateChrome(settings),//Calls Chrome-specific setup - Clean separation of concerns
            _ => CreateChrome(settings) //Default fallback browser - Keeps simple and stable
        };
    }

    private static IWebDriver CreateChrome(FrameworkSettings settings)//Chrome-specific driver creation - Browser-specific options isolated
    {
        var options = new ChromeOptions();//Initializes Chrome options - Required to customize browser

        // Recommended stability flags for CI / Docker-like environments
        options.AddArgument("--no-sandbox");//Disables Chrome sandbox - Required for Docker / CI environments
        options.AddArgument("--disable-dev-shm-usage");//Avoids shared memory issues - Prevents crashes in containers
        options.AddArgument("--disable-gpu");//Disables GPU rendering - Improves stability in headless mode

        if (settings.Headless)//Checks headless config - Enables CI execution
        {
            // "new" headless mode for newer Chrome
            options.AddArgument("--headless=new");//Uses modern Chrome headless - Faster & more reliable
            options.AddArgument("--window-size=1920,1080");//Sets screen resolution - Prevents layout issues
        }

        var driver = new ChromeDriver(options);//Creates Chrome WebDriver - Selenium Manager auto - resolves driver
        driver.Manage().Window.Maximize(); //Maximizes browser window - Consistent UI rendering
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0); // use explicit waits //Disables implicit waits - Enforces explicit waits only
                return driver; //Returns initialized WebDriver Ready for test execution
    }
}
//Why Selenium Manager Is Used Here
//Auto driver resolution - No manual ChromeDriver download
//Version compatibility - Browser & driver stay in sync
//CI friendly	Works - out-of-the-box in pipelines
//Less maintenance - No driver path issues

//Design Pattern Used
//Factory Pattern	- Decouples test code from browser creation
//Single Responsibility	- Driver logic isolated from tests
//Open/Closed Principle	- Easy to add Edge / Firefox later