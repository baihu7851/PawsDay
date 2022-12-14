//// Generated by Selenium IDE
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Remote;
//using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium.Interactions;
//using NUnit.Framework;
//using System.Xml.Linq;

//namespace PawsDaySeleniumTest
//{
//    [TestFixture]
//    public class SeleniumTest
//    {
//        private IWebDriver driver;
//        public IDictionary<string, object> vars { get; private set; }
//        private IJavaScriptExecutor js;
//        [SetUp]
//        public void SetUp()
//        {
//            driver = new ChromeDriver();
//            js = (IJavaScriptExecutor)driver;
//            vars = new Dictionary<string, object>();
//        }
//        [TearDown]
//        protected void TearDown()
//        {
//            driver.Quit();
//        }
//        [Test]
//        public void project()
//        {
//            driver.Navigate().GoToUrl("https://pawsday-frontend.azurewebsites.net/");
//            driver.Manage().Window.Size = new System.Drawing.Size(1280, 680);
//            Actions builder = new Actions(driver);

//            driver.FindElement(By.CssSelector(".menu-button-login > img")).Click();
//            {
//                var element = driver.FindElement(By.CssSelector(".menu-button-login > img"));

//                builder.MoveToElement(element).Perform();
//            }
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
//            driver.FindElement(By.CssSelector(".menu:nth-child(2) > .member-body p")).Click();
//            {
//                var element = driver.FindElement(By.TagName("body"));
//                builder.MoveToElement(element, 0, 0).Perform();
//            }
//            driver.FindElement(By.LinkText("使用 電子郵件 繼續")).Click();
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

//            driver.FindElement(By.Id("login-email-input")).Click();
//            {
//                var element = driver.FindElement(By.Id("login-email-input"));
//                builder.MoveToElement(element, 0, 0).Perform();
//            }
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
//            driver.FindElement(By.Id("login-email-input")).SendKeys("kitsurua@hotmail.com");
//            driver.FindElement(By.Id("login-password-input")).Click();
//            driver.FindElement(By.Id("login-password-input")).SendKeys("test1234");
//            driver.FindElement(By.CssSelector(".list-group > .login-btn")).Click();
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

//            driver.FindElement(By.Id("search-input")).Click();
//            driver.FindElement(By.Id("search-input")).SendKeys("到府洗澡");
//            driver.FindElement(By.Id("search-button")).Click();
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

//            driver.FindElement(By.Id("county")).Click();
//            {
//                var dropdown = driver.FindElement(By.Id("county"));
//                dropdown.FindElement(By.XPath("//option[. = '台北市']")).Click();
//            }
//            driver.FindElement(By.Id("district")).Click();
//            {
//                var dropdown = driver.FindElement(By.Id("district"));
//                dropdown.FindElement(By.XPath("//option[. = '萬華區']")).Click();
//            }

//            driver.FindElement(By.CssSelector(".filter-submit")).Click();
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
//            driver.FindElement(By.CssSelector(".commodity-card:nth-child(2) img")).Click();
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

//            var right_button = driver.FindElement(By.CssSelector(".right-btn"));
//            js.ExecuteScript("arguments[0].click();", right_button);

//            var continue_button = driver.FindElement(By.XPath("//div[@id='commodity']/div[2]/div/div[3]/div/div/div[2]/div[2]/div/button[6]/h3"));
//            js.ExecuteScript("arguments[0].click();", continue_button);

//            //var continue_button = driver.FindElement(By.XPath("//div[@id='commodity']/div[2]/div/div[3]/div/div/div[2]/div[2]/div[5]/button[6]/h3"));
//            //js.ExecuteScript("arguments[0].click();", continue_button);

//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
//            var time_button = driver.FindElement(By.CssSelector("#heading2 > .accordion-button"));
//            js.ExecuteScript("arguments[0].click();", time_button);
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

//            var parttime_1 = driver.FindElement(By.CssSelector("#collapse2 .time-item:nth-child(1)"));
//            js.ExecuteScript("arguments[0].click();", parttime_1);

//            var partime_2 = driver.FindElement(By.CssSelector("#collapse2 .time-item:nth-child(2)"));
//            js.ExecuteScript("arguments[0].click();", partime_2);

//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

//            driver.FindElement(By.XPath("//select[@id='city']"));
//            {
//                var dropdown = driver.FindElement(By.Id("city"));
//                js.ExecuteScript("arguments[0].click();", dropdown);
//                dropdown.FindElement(By.XPath("//option[. = '台北市']")).Click();
//            }
//            driver.FindElement(By.Id("district"));
//            {
//                var dropdown = driver.FindElement(By.Id("district"));
//                js.ExecuteScript("arguments[0].click();", dropdown);
//                dropdown.FindElement(By.XPath("//option[. = '信義區']")).Click();
//            }
//            driver.FindElement(By.Id("pettype"));
//            {
//                var dropdown = driver.FindElement(By.Id("pettype"));
//                js.ExecuteScript("arguments[0].click();", dropdown);
//                dropdown.FindElement(By.XPath("//option[. = '狗狗']")).Click();
//            }
//            driver.FindElement(By.Id("shapetype"));
//            {
//                var dropdown = driver.FindElement(By.Id("shapetype"));
//                js.ExecuteScript("arguments[0].click();", dropdown);
//                dropdown.FindElement(By.XPath("//option[. = '迷你型(5kg以下)']")).Click();
//            }

//            driver.FindElement(By.Id("create-cart"));
//            {
//                var tobook = driver.FindElement(By.Id("create-cart"));
//                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
//                js.ExecuteScript("arguments[0].click();", tobook);
//            }
//            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
//            var cart = driver.FindElement(By.CssSelector(".bi-cart2"));
//            {
//                var element = driver.FindElement(By.CssSelector(".bi-cart2"));
//                builder.MoveToElement(element).Click().Perform();
//            }

//            var cartitem = driver.FindElement(By.CssSelector(".cart-item:nth-child(1) > .checkbox"));
//            js.ExecuteScript("arguments[0].click();", cartitem);

//            var checkout = driver.FindElement(By.Id("checkout-btn"));
//            js.ExecuteScript("arguments[0].click();", checkout);


//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

//            driver.FindElement(By.CssSelector("#sameuser-booker > .mx-2")).Click();
//            var tostep2 = driver.FindElement(By.CssSelector(".continue-btn:nth-child(2)"));
//            js.ExecuteScript("arguments[0].click();", tostep2);

//            driver.FindElement(By.CssSelector(".item-details:nth-child(3)"));
//            {
//                var element = driver.FindElement(By.CssSelector(".item-details:nth-child(3)"));
//                js.ExecuteScript("arguments[0].click();", element);
//            }
//            var input = driver.FindElement(By.Id("bookingAddressInput"));
//            js.ExecuteScript("arguments[0].click();", input);

//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

//            driver.FindElement(By.Id("bookingAddressInput")).SendKeys("忠孝東路三段8號10樓");

//            driver.FindElement(By.CssSelector(".dropdown-toggle"));
//            {
//                var dropdown = driver.FindElement(By.CssSelector(".dropdown-toggle"));
//                js.ExecuteScript("arguments[0].click();", dropdown);
//                dropdown.FindElement(By.XPath("//label[@value='0']"));
//                {
//                    var item = dropdown.FindElement(By.XPath("//label[@value='0']"));
//                    js.ExecuteScript("arguments[0].click();", item);
//                }
//            }
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
//            driver.FindElement(By.Name("trait"));
//            {
//                var trait = driver.FindElement(By.Name("trait"));
//                js.ExecuteScript("arguments[0].click();", trait);
//            }

//            driver.FindElement(By.CssSelector("#contactUser > .mx-2"));
//            {
//                var target = driver.FindElement(By.CssSelector("#contactUser > .mx-2"));
//                js.ExecuteScript("arguments[0].click();", target);
//            }
//            driver.FindElement(By.CssSelector(".pawsday-btn:nth-child(5)"));
//            {
//                var target = driver.FindElement(By.CssSelector(".pawsday-btn:nth-child(5)"));
//                js.ExecuteScript("arguments[0].click();", target);
//            }
//            driver.FindElement(By.Id("payBtn"));
//            {
//                var target = driver.FindElement(By.Id("payBtn"));
//                js.ExecuteScript("arguments[0].click();", target);
//            }
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

//            driver.FindElement(By.Id("CCpart1")).Click();
//            driver.FindElement(By.Id("CCpart1")).SendKeys("4311");
//            driver.FindElement(By.Id("CCpart2")).SendKeys("2222");
//            driver.FindElement(By.Id("CCpart3")).SendKeys("2222");
//            driver.FindElement(By.Id("CCpart4")).SendKeys("2222");
//            driver.FindElement(By.Id("creditMM")).Click();
//            driver.FindElement(By.Id("creditMM")).SendKeys("11");
//            driver.FindElement(By.Id("creditYY")).SendKeys("22");
//            driver.FindElement(By.Id("CreditBackThree")).Click();
//            driver.FindElement(By.Id("CreditBackThree")).SendKeys("111");
//            driver.FindElement(By.Id("CellPhoneCheck")).Click();
//            driver.FindElement(By.Id("CellPhoneCheck")).SendKeys("0910630809");
//            driver.FindElement(By.Id("CreditPaySubmit")).Click();
//            {
//                var element = driver.FindElement(By.Id("CreditPaySubmit"));
//                builder.MoveToElement(element).Perform();
//            }
//            {
//                var element = driver.FindElement(By.TagName("body"));
//                builder.MoveToElement(element, 0, 0).Perform();
//            }
//            driver.FindElement(By.XPath("//button[contains(.,\'關閉 Turn off\')]")).Click();
//            driver.FindElement(By.Id("CreditPaySubmit")).Click();
//            driver.FindElement(By.Id("btnConfirm")).Click();
//            driver.FindElement(By.Id("GetOTPPwd")).Click();
//            driver.FindElement(By.Id("OTP")).Click();
//            driver.FindElement(By.Id("OTP")).SendKeys("1234");
//            driver.FindElement(By.Id("OTPSend")).Click();
//            driver.FindElement(By.LinkText("返回訂單列表")).Click();
//            driver.FindElement(By.CssSelector(".order:nth-child(1) .order-h3")).Click();
//            driver.FindElement(By.Id("cancel-model")).Click();
//            driver.FindElement(By.Id("address-city")).Click();
//            {
//                var dropdown = driver.FindElement(By.Id("address-city"));
//                dropdown.FindElement(By.XPath("//option[. = '聯絡不上保姆']")).Click();
//            }
//            driver.FindElement(By.Id("cancel-btn")).Click();
//        }
//    }
//}

