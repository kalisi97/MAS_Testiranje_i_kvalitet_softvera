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
    public class UITestsMesto : IDisposable
    {
        private readonly IWebDriver _driver;
        private IWebElement NazivElement => _driver.FindElement(By.Id("naziv"));
        private IWebElement PttElement => _driver.FindElement(By.Id("pttBroj"));
        private IWebElement BrojStanovnikaElement => _driver.FindElement(By.Id("brojStanovnika"));
        private IWebElement ButtonCreate => _driver.FindElement(By.Id("btnCreate"));
        public void PopuniNaziv(string naziv) => NazivElement.SendKeys(naziv);
        public void PopuniPtt(int? pttBroj) => PttElement.SendKeys(Convert.ToString(pttBroj));
        public void PopuniBrojStanovnika(int? brojStanovnika) => BrojStanovnikaElement.SendKeys(Convert.ToString(brojStanovnika));
        public void ClickCreate() => ButtonCreate.Click();
        public string NazivElementErrorMessage => _driver.FindElement(By.Id("spanNaziv")).Text;
        public string PttElementErrorMessage => _driver.FindElement(By.Id("spanPttBroj")).Text;
        public string BrojStanovnikaElementErrorMessage => _driver.FindElement(By.Id("spanBrojStanovnika")).Text;

        public UITestsMesto()
        {
            _driver = new ChromeDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void CreateMestoEmptyField()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44341/Mesto/Create");

            PopuniNaziv("");
            PopuniPtt(null);
            PopuniBrojStanovnika(null); //ovo je u redu, moze biti null
            ClickCreate();

            Assert.AreEqual("Create - MAS_TestiranjeSoftvera_Projekat", _driver.Title);
            Assert.AreEqual(NazivElementErrorMessage, "Unesite naziv!");
            Assert.AreEqual(PttElementErrorMessage, "Nevalidan PTT broj!");
            Assert.AreEqual(BrojStanovnikaElementErrorMessage, "");

        }
        [Test]
        public void CreateMestoIncorrectField()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44341/Mesto/Create");

            PopuniNaziv("kragujevac");
            PopuniPtt(10);
            PopuniBrojStanovnika(-1);
            ClickCreate();

            Assert.AreEqual("Create - MAS_TestiranjeSoftvera_Projekat", _driver.Title);
            Assert.AreEqual(NazivElementErrorMessage, "Naziv mora poceti velikim slovom!");
            Assert.AreEqual(PttElementErrorMessage, "Nevalidan PTT broj!");
            Assert.AreEqual(BrojStanovnikaElementErrorMessage, "Broj stanovnika ne moze biti 0 ili manji od 0!");

        }
    }
}
