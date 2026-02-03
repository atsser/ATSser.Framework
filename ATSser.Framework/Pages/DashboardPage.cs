using OpenQA.Selenium;
using ATSser.Framework.Utilities;
using Newtonsoft.Json;
using OpenQA.Selenium.Support.UI;
using System.Reflection.Metadata;

namespace ATSser.Framework.Pages;

public sealed class DashboardPage //Defines Dashboard page object - Represents one application page
{//sealed Prevents inheritance - Enforces controlled page behavior
    private readonly IWebDriver _driver;//Stores WebDriver reference - Enables browser interaction
    private readonly TimeSpan _timeout;//Stores wait duration - Makes waits configurable

    public DashboardPage(IWebDriver driver, int timeoutSeconds = 15)//Constructor injection - Decouples test & page logic
    {//timeoutSeconds=15 Default timeout value - Avoids magic numbers
        _driver = driver;
        _timeout = TimeSpan.FromSeconds(timeoutSeconds);//Converts seconds to TimeSpan - Required for wait utilities
    }

    // After login, Cogmento app typically has a left menu and a home icon. This is a lightweight check.
    private By AnyLeftMenuItem => By.CssSelector("div.ui.left.fixed.vertical.menu");//Locator for dashboard element - Identifies successful login
                                  //CssSelector CSS selector locator - Fast and readable
    public bool IsLoaded()//Verifies page load status - Used for assertions
    {
        try//Attempts wait - Handles dynamic UI
        {
            WaitHelper.WaitForVisible(_driver, AnyLeftMenuItem, _timeout);//Explicit wait for element - Ensures synchronization
            return true;//Page loaded successfully - Test flow continues
        }
        catch//Handles timeout/failure - Avoids test crash
        {
            return false;
        }
    }
}
//Why This Page Object Is Well Designed
//Constructor injection	- Easy to mock & reuse
//Explicit waits only - Stable, predictable tests
//Boolean page check  - Clean assertions
//No test logic inside - Pure page responsibility
//Lightweight locator - Faster validation