using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ATSser.Framework.Utilities;

public static class WaitHelper//Utility class for explicit waits - Centralizes synchronization logic
{//static No object creation required - Easy reuse across framework
    public static IWebElement WaitForVisible(IWebDriver driver, By locator, TimeSpan timeout)//Waits until element is visible - Prevents ElementNotVisibleException
    {
        var wait = new WebDriverWait(driver, timeout);//Creates explicit wait - Precise timing control
        return wait.Until(d =>//Polls condition until success - Reliable synchronization
        {
            var el = d.FindElement(locator);//Finds element dynamically - Handles delayed DOM load
            return el.Displayed ? el : null;//Returns element only if visible - Ensures UI readiness
        })!;//Suppresses null warning - Safe due to Until behavior
    }

    public static IWebElement WaitForClickable(IWebDriver driver, By locator, TimeSpan timeout)//Waits until element is clickable - Prevents click interception
    {
        var wait = new WebDriverWait(driver, timeout);
        return wait.Until(d =>
        {
            var el = d.FindElement(locator);
            return (el.Displayed && el.Enabled) ? el : null;//Click readiness check - Avoids false positives
        })!;
    }

    public static bool WaitForUrlContains(IWebDriver driver, string text, TimeSpan timeout)//Waits for URL change - Useful for SPA navigation
    {
        var wait = new WebDriverWait(driver, timeout);
        return wait.Until(d => (d.Url ?? string.Empty).Contains(text, StringComparison.OrdinalIgnoreCase));//Case-insensitive match - Robust URL checks
        //Indicates success/failure - Clean assertions in tests
    }
}

//Pages stay clean, waits stay centralized

// Why This WaitHelper Is Well Designed
// Explicit waits only	- No flaky implicit waits
// Lambda-based conditions	- Full control over logic
// Centralized utility	- Easy maintenance
// No Thread.Sleep	- Faster & reliable tests
// Reusable methods	- Cleaner page objects

//      Tests
//        ?
//      Pages
//        ?
//      WaitHelper
//        ?
//      WebDriver