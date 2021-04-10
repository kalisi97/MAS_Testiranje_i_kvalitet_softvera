using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedUITests
{

        public class UITestsOsoba : IDisposable
        {
            private readonly IWebDriver _driver;

            public UITestsOsoba()
            {
                _driver = new ChromeDriver();
            }

            public void Dispose()
            {
                _driver.Quit();
                _driver.Dispose();
            }

            [Test]
            public void WhenExecuted_ReturnsIndexView()
            {
                _driver.Navigate()
                    .GoToUrl("https://localhost:44341/Osoba");
                Assert.AreEqual("Index - MAS_TestiranjeSoftvera_Projekat", _driver.Title);
            }

            [TestCase("mBroj","spanMBroj")]
            [TestCase("ime", "spanIme")]
            [TestCase("prezime", "spanPrezime")]
            [TestCase("datumRodjenja", "spanDatumRodjenja")]
            [TestCase("email", "spanEmail")]
            [TestCase("mestoId", "spanMesto")]
            public void CreateOsobaEmptyField(string elementId, string spanId)
            {
                _driver.Navigate()
                    .GoToUrl("https://localhost:44341/Osoba/Create");

                _driver.FindElement(By.Id(elementId)).SendKeys("");
                _driver.FindElement(By.Id("btnCreate")).Click();
                var errorMessage = _driver.FindElement(By.Id(spanId)).Text;

                Assert.AreEqual("Create - MAS_TestiranjeSoftvera_Projekat", _driver.Title);
                string expectedMessage = string.Empty;
                switch(elementId)
                {
                 case "mBroj":
                    expectedMessage = "Unesite maticni broj!"; break;
                case "ime":
                    expectedMessage = "Unesite ime!"; break;
                case "prezime":
                    expectedMessage = "Unesite prezime!"; break;
                case "datumRodjenja":
                    expectedMessage = "Unesite datum rodjenja!"; break;
                case "email":
                    expectedMessage = "Unesite email!"; break;
                case "mestoId":
                    expectedMessage = "Odaberite mesto!"; break;
                default: break;
                }
                Assert.AreEqual(expectedMessage, errorMessage);
            }

            [TestCase("mBroj", "spanMBroj","123")]
            [TestCase("ime", "spanIme","katarina")]
            [TestCase("prezime", "spanPrezime","simic")]
            [TestCase("email", "spanEmail","email@sl.com")]
            public void CreateOsobaIncorrectField(string elementId, string spanId, object value)
            {
                _driver.Navigate()
                    .GoToUrl("https://localhost:44341/Osoba/Create");

                _driver.FindElement(By.Id(elementId)).SendKeys(value.ToString());
                _driver.FindElement(By.Id("btnCreate")).Click();
                var errorMessage = _driver.FindElement(By.Id(spanId)).Text;

                Assert.AreEqual("Create - MAS_TestiranjeSoftvera_Projekat", _driver.Title);
                string expectedMessage = string.Empty;
                switch (elementId)
                {
                    case "mBroj":
                        expectedMessage = "Maticni broj, nije u dobrom formatu!"; break;
                    case "ime":
                        expectedMessage = "Ime mora poceti velikim slovom!"; break;
                    case "prezime":
                        expectedMessage = "Prezime mora poceti velikim slovom!"; break;
                    case "email":
                        expectedMessage = "Email se mora završavati domenom: .rs"; break;
                    default: break;
                }
                Assert.AreEqual(expectedMessage, errorMessage);
            }
    }
    }

