using System.Net.Sockets;

namespace ATSser.Framework;

public sealed class FrameworkSettings
{
    public string BaseUrl { get; set; } = "https://ui.cogmento.com/";//Application URL under test	- Avoids hard-coded URLs
    //Default value Cogmento app URL	Easy environment switch
    public string Browser { get; set; } = "chrome"; //Browser selection	- Enables cross-browser testing
    //Default value	"chrome"	- Chrome as default browser Most stable & popular // chrome | edge | firefox (chrome implemented)
    public bool Headless { get; set; } = false;//Runs browser without UI	CI/CD execution
    //Default value	false	- Visible browser locally	Debug-friendly
    // Provide your own credentials if you want to run the login flow end-to-end
    public string Username { get; set; } = "your_email";//Login username/email	End-to-end auth testing
    //Default value	"your_email"	- Placeholder Prevents secret leaks
    public string Password { get; set; } = "your_password";//Login password	Authentication
    //Default value	"your_password"	- Placeholder	Secure by design

    public int DefaultTimeoutSeconds { get; set; } = 15;//Explicit wait timeout	Synchronization control
    //Default value	15	Standard UI wait	Prevents flaky tests
}

// Why This Class Is Critical
// Central config	- One place to change behavior
// Strong typing	- Compile-time safety
// CI friendly	- Supports env var overrides
// Clean defaults	- Runs out-of-the-box
// No secrets in code	- Security best practice

//       appsettings.json / Env Vars
//               ?
//       FrameworkSettings
//               ?
//       BaseTest
//               ?
//       DriverFactory / Pages / Tests