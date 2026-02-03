using OpenQA.Selenium;
using ATSser.Framework.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ATSser.Framework.Pages;

public sealed class LoginPage//Defines Login page object - Represents login functionality
{//sealed Prevents inheritance - Keeps page behavior controlled
    private readonly IWebDriver _driver;//Holds WebDriver reference - Enables browser interaction
    private readonly TimeSpan _timeout;//Wait duration for elements - Avoids hard-coded waits

    public LoginPage(IWebDriver driver, int timeoutSeconds = 15)//Constructor injection - Decouples page from tests
    {//timeoutSeconds = 15 Default wait value - Sensible UI wait standard
        _driver = driver;
        _timeout = TimeSpan.FromSeconds(timeoutSeconds);
    }

    private By LoginButtonHome => By.XPath("//a[span[normalize-space()='Log In']]");//Locator for landing page login Handles pre-login screen
    //XPath with text normalization - Robust against UI spacing changes
    private By EmailInput => By.Name("email");//Locator for email field - User input handling
    //Name-based locator - Fast and stable
    private By PasswordInput => By.Name("password");//Locator for password field - Secure input
    //Name-based locator - Simple and reliable
    private By SubmitButton => By.XPath("//div[text()='Login']");//Locator for login submit - Triggers authentication
    //Text-based XPath - Matches visible user action
    public void OpenLoginForm()//Opens login modal/page - Supports multi-step login flows
    {
        // Cogmento shows a landing page with a Login button
        var login = WaitHelper.WaitForClickable(_driver, LoginButtonHome, _timeout);//Waits until login button is clickable - Prevents click interception
        login.Click();
    }

    public void Login(string username, string password)//Performs login action - Encapsulates business flow
    {
        var email = WaitHelper.WaitForVisible(_driver, EmailInput, _timeout);//Waits for field visibility - Reliable synchronization
        email.Clear();//Clears old text - Avoids residual data
        email.SendKeys(username); //Inputs credentials - User simulation

        var pass = WaitHelper.WaitForVisible(_driver, PasswordInput, _timeout);
        pass.Clear();
        pass.SendKeys(password);

        var submit = WaitHelper.WaitForClickable(_driver, SubmitButton, _timeout);
        submit.Click();//Submits login form - Completes authentication
    }
}
//Design Principles Followed
//Page Object Model	- Page logic isolated
//Single Responsibility	- Login only
//Explicit waits only	- No flaky sleeps
//Readable locators	- Maintenance friendly
//Business-level methods	- Login() not EnterEmail()
