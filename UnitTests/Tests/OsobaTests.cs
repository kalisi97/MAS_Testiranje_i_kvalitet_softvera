using MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer.Implementations;
using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MAS_TestiranjeSoftvera_Projekat.Extensions.Enums;

namespace UnitTests.Tests
{
    [TestFixture]
    public class OsobaTests
    {
        private Mock<IRepositoryOsoba> mockOsobaRepository = Mocks.MockClass.MockRepositoryOsoba();
        private OsobaService service;

        public OsobaTests()
        {
            //Act
            service = new OsobaService(mockOsobaRepository.Object);
        }

        [Test]
        public void TestSelectAll()
        {
            //Arange
            var result = service.SelectAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<List<Osoba>>(result);
        }

        [TestCase(null)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(100)]
        public void TestSelectById(int? id)
        {

            switch (id)
            {
                case 1:
                    var osoba = service.SelectById(id);
                    Assert.That(osoba.OsobaId == 1);
                    Assert.IsNotNull(osoba);
                    break;

                case null:
                    Assert.Throws<Exception>(() =>
                                  service.SelectById(id), "Id osobe ne moze biti null!");
                    break;

                default:
                    Assert.Throws<Exception>(() =>
                                 service.SelectById(id), "Trazena osoba ne postoji!");
                    break;

            }
        }

        [TestCase(1)]
        [TestCase(null)]
        public void TestDeleteEntity(int? id)
        {
            switch (id)
            {
                case null:
                    Assert.Throws<ArgumentNullException>(() =>
                               service.Delete(null));
                    break;
                case 1:
                    var entityToDelete = service.SelectById(1);
                    var result = service.Delete(entityToDelete);
                    Assert.That(result.Equals("Uspesno!"));
                    Assert.Throws<Exception>(() =>
                                service.SelectById(1), "Id osobe ne moze biti null!");

                    break;
                default: break;
            }
        }

        [Test]
        public void TestInsertValidEntity()
        {
            //Act

            var newOsoba = new Osoba
            {

                MaticniBroj = "1234567891233",
                Ime = "Nikola",
                Prezime = "Simic",
                Tezina = 77,
                Visina = 171,
                Email = "nikola@gmail.rs",
                Telefon = "+381642219548",
                BojaOciju = BojaOciju.Crna,
                Adresa = "XXI srpske divizije 64",
                DatumRodjenja = new DateTime(1992, 10, 02),
                MestoId = 1,
                Napomena = "Napomena"
            };

            //Arrange

            var result = service.Insert(newOsoba);

            //Assert

            Assert.NotNull(result);
            Assert.That(result.Equals("Uspesno!"));

            var osoba = service.SelectAll().Last();

            Assert.AreEqual(newOsoba.MaticniBroj, osoba.MaticniBroj);

        }

        [TestCase("Ime", "ime")]
        [TestCase("Prezime", "")]
        [TestCase("MaticniBroj", "-1234")]
        [TestCase("Visina", -1)]
        [TestCase("Tezina", 300)]
        [TestCase("Email", "someone_something.com")]
        [TestCase("DatumRodjenja", null)]
        [TestCase("MestoId", null)]
        public void TestInsertInvalidEntityShouldThrowException(string polje, object vrednost)
        {
            Osoba newOsoba = new Osoba
            {
                OsobaId = 1,
                MaticniBroj = "1234567891239",
                Ime = "Test",
                Prezime = "Test",
                Tezina = 55,
                Visina = 180,
                Email = "test@gmail.rs",
                Telefon = "+381642119548",
                BojaOciju = BojaOciju.Crna,
                Adresa = "XXI srpske divizije 64",
                DatumRodjenja = new DateTime(1997, 04, 17),
                MestoId = 1,
                Napomena = "Napomena"
            };

            switch (polje)
            {
                case "Ime":
                    newOsoba.Ime = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Insert(newOsoba), "Nevalidan unos za parametar: Ime, mora poceti velikim slovom!");
                    break;
                case "Prezime":
                    newOsoba.Prezime = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Insert(newOsoba), "Nevalidan unos za parametar: Prezime, unesite prezime!");
                    break;
                case "MaticniBroj":
                    newOsoba.MaticniBroj = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Insert(newOsoba), "Nevalidan unos za parametar: MaticniBroj, nije u dobrom formatu!");
                    break;
                case "Visina":
                    newOsoba.Visina = Convert.ToInt32(vrednost);
                    Assert.Throws<Exception>(() => service.Insert(newOsoba), "Nevalidan unos za parametar: Visina, minimalna dozvoljena vrednost je 35");
                    break;
                case "Tezina":
                    newOsoba.Tezina = Convert.ToInt32(vrednost);
                    Assert.Throws<Exception>(() => service.Insert(newOsoba), "Nevalidan unos za parametar: Tezina, dozvoljene su vrednosti [10,250]");
                    break;
                case "Email":
                    newOsoba.Email = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Insert(newOsoba), "Nevalidan unos za parametar: Email, mora sadrzati @ sa dozvoljenim .rs domenom!");
                    break;
                case "DatumRodjenja":
                    newOsoba.DatumRodjenja = null;
                    Assert.Throws<Exception>(() => service.Insert(newOsoba), "Nevalidan unos za parametar: Datum rodjenja, unesite datum rodjenja!");
                    break;
                case "MestoId":
                    newOsoba.MestoId = null;
                    Assert.Throws<Exception>(() => service.Insert(newOsoba), "Nevalidan unos za parametar: Mesto, odaberite mesto!");
                    break;
                default: Assert.Throws<Exception>(() => service.Insert(newOsoba)); break;
            }
        }
        [Test]
        public void TestUpdateValidEntity()
        {
            //Act
            var osoba = service.SelectById(1);
            osoba.Adresa = "Izmenjen naziv adrese";
            //Arange
            var result = service.Update(osoba);
            //Assert
            Assert.That(result.Equals("Uspesno!"));

            var izmenjenaOsoba = service.SelectById(1);

            Assert.AreEqual(osoba.Adresa, izmenjenaOsoba.Adresa);

        }
       
        [TestCase("Id", 0)]
        [TestCase("Ime", "ime")]
        [TestCase("Prezime", "")]
        [TestCase("MaticniBroj", "-1234")]
        [TestCase("Visina", -1)]
        [TestCase("Tezina", 300)]
        [TestCase("Email", "someone_something.com")]
        [TestCase("DatumRodjenja", null)]
        [TestCase("MestoId", null)]
        public void TestUpdateInvalidEntityShouldThrowException(string polje, object vrednost)
        {
         
            switch (polje)
            {
                case "Id":
                    var o = service.SelectById(1);
                    o.OsobaId = Convert.ToInt32(vrednost);
                    Assert.Throws<Exception>(() => service.Update(o), "Trazena osoba ne postoji!");
                    break;
                case "Ime":
                    var o1 = service.SelectById(1);
                    o1.Ime = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Update(o1), "Nevalidan unos za parametar: Ime, mora poceti velikim slovom!");
                    break;
                case "Prezime":
                    var o2 = service.SelectById(1);
                    o2.Prezime = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Update(o2), "Nevalidan unos za parametar: Prezime, unesite prezime!");
                    break;
                case "MaticniBroj":
                    var o3 = service.SelectById(1);
                    o3.MaticniBroj = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Update(o3), "Nevalidan unos za parametar: MaticniBroj, nije u dobrom formatu!");
                    break;
                case "Visina":
                    var o4 = service.SelectById(1);
                    o4.Visina = Convert.ToInt32(vrednost);
                    Assert.Throws<Exception>(() => service.Update(o4), "Nevalidan unos za parametar: Visina, minimalna dozvoljena vrednost je 35");
                    break;
                case "Tezina":
                    var o5 = service.SelectById(1);
                    o5.Tezina = Convert.ToInt32(vrednost);
                    Assert.Throws<Exception>(() => service.Update(o5), "Nevalidan unos za parametar: Tezina, dozvoljene su vrednosti [10,250]");
                    break;
                case "Email":
                    var o6 = service.SelectById(1);
                    o6.Email = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Update(o6), "Nevalidan unos za parametar: Email, mora sadrzati @ sa dozvoljenim .rs domenom!");
                    break;
                case "DatumRodjenja":
                    var o7 = service.SelectById(1);
                    o7.DatumRodjenja = null;
                    Assert.Throws<Exception>(() => service.Update(o7), "Nevalidan unos za parametar: Datum rodjenja, unesite datum rodjenja!");
                    break;
                case "MestoId":
                    var o8 = service.SelectById(1);
                    o8.MestoId = null;
                    Assert.Throws<Exception>(() => service.Update(o8), "Nevalidan unos za parametar: Mesto, odaberite mesto!");
                    break;

                default: break;
            }
        }

  
    }
}
