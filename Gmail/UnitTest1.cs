using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;


namespace Gmail
{
    public class Tests
    {
        String username = "bba18148@uom.edu.gr";
        String password = "Kwnstantina9!";
        IWebDriver test;

        [SetUp]
        public void Setup()
        {
            //Step 1 & 2

            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            test = new FirefoxDriver();
            test.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //adding time delay gloabaly when an elements if being searched but not found
            //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //test = new ChromeDriver();
            test.Url= "https://accounts.google.com/signin/v2/identifier?passive=1209600&continue=https%3A%2F%2Faccounts.google.com%2Fb%2F1%2FAddMailService&followup=https%3A%2F%2Faccounts.google.com%2Fb%2F1%2FAddMailService&flowName=GlifWebSignIn&flowEntry=ServiceLogin";
            test.Manage().Window.Maximize();
            }

        [Test]
        public void Test1()
        {
            //Step 3
            test.FindElement(By.XPath("//div[@class='Xb9hP']/input")).SendKeys(username);
            test.FindElement(By.XPath("//div[@id='identifierNext']/div/button")).Click();

            Thread.Sleep(2000); // Test was immediatly typing the password on the 'input' field of the username. I add it a delay until the next page fully loads 

            test.FindElement(By.XPath("//div[@class='aXBtI Wic03c']/div/input")).SendKeys(password);
            test.FindElement(By.XPath("//div[@id='passwordNext']/div/button")).Click();

            //Thread.Sleep(2000);
            
            //Step 4
            String primarySection = test.FindElement(By.XPath("//div[@aria-label='Κύρια']")).GetAttribute("aria-selected");
            //TestContext.Progress.WriteLine("Primary section:" + primarySection);
            //Assert.That(primarySection, Is.EqualTo("true")); //An assertion that by default after the log in, Primary section is selected

            //Step 5
            if (primarySection != "true")
            {
                test.FindElement(By.XPath("//div[@aria-label='Κοινωνικά']")).Click(); //If primary section is not selected after log in, select it
            }

            //Step 6
            int numberOfEmails = test.FindElements(By.XPath("//tr[@role='row']")).Count; // Total number of emails on primary section
            TestContext.Progress.WriteLine("Total number of emails is: "  +numberOfEmails);

            
                
            //Step 7
            String sender = test.FindElement(By.XPath("//tbody/tr[11]/td[5]/div[2]/span/span")).Text;
            TestContext.Progress.WriteLine("Sender is :" + sender);

            String subject = test.FindElement(By.XPath("//tbody/tr[11]/td[6]/div/div/div/span/span")).Text;
            TestContext.Progress.WriteLine("Subject is :" + subject);

            FindNthElement(11);
            FindNthElement(12);
            FindNthElement(14);
            FindNthElement(1);
            FindNthElement(5);



        }

        public void FindNthElement(int x)
        {
            var sender = test.FindElement(By.XPath($"//tbody/tr[{x}]/td[5]/div[2]/span/span")).Text;
            TestContext.Progress.WriteLine($"Sender for {x} is :" + sender);
            
            var subject = test.FindElement(By.XPath($"//tbody/tr[{x}]/td[6]/div/div/div/span/span")).Text;
            TestContext.Progress.WriteLine($"Subject for {x} is :" + subject);
        }
    }

    
}