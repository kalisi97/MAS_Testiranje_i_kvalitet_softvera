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

namespace UnitTests.Tests
{
    [TestFixture]
    public class MestoTests
    {
        private Mock<IRepositoryMesto> mockRepositoryMesto = Mocks.MockClass.MockRepositoryMesto();
        private MestoService service;

        public MestoTests()
        {
            //Act
            service = new MestoService(mockRepositoryMesto.Object);
        }

        [Test]
        public void TestSelectAll()
        {
            //Arange
            var result = service.SelectAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<List<Mesto>>(result);
        }

        [TestCase(null)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(100)]
        public void TestSelectById(int? id)
        {
            
            switch(id)
            {
                case 1:
                    var mesto = service.SelectById(id);
                    Assert.That(mesto.MestoId == 1);
                    Assert.IsNotNull(mesto);
                    break;

                case null:
                    Assert.Throws<Exception>(() =>
                                  service.SelectById(id), "Id mesta ne moze biti null!");
                    break;

                default:
                    Assert.Throws<Exception>(() =>
                                 service.SelectById(id), "Trazeno mesto ne postoji!");
                    break;
              
            }
        }

        [Test]
        public void TestInsertValidEntity()
        {
            //Act

            var newMesto = new Mesto
            {
                Naziv = "Novo mesto",
                PttBroj = 18000,
                BrojStanovnika = 256000
            };

            //Arrange

            var result = service.Insert(newMesto);

            //Assert

            Assert.NotNull(result);
            Assert.That(result.Equals("Uspesno!"));

            var mesto = service.SelectAll().Last();

            Assert.AreEqual(newMesto.Naziv, mesto.Naziv);

        }
        [TestCase("Naziv","krusevac")]
        [TestCase("Ptt",10)]
        [TestCase("BrojStanovnika",-1)]
        public void TestInsertInvalidEntityShouldThrowException(string polje, object vrednost)
        {
            Mesto newMesto = new Mesto
            {
                Naziv = "Naziv",
                PttBroj = 11000,
                BrojStanovnika = 1000000
            };

            switch(polje)
            {
                case "Naziv":
                    newMesto.Naziv = vrednost.ToString();
                    Assert.Throws<Exception>(() => service.Insert(newMesto), "Nevalidan unos za parametar: Naziv, mora poceti velikim slovom!");
                    break;
                case "Ptt":
                    newMesto.PttBroj = Convert.ToInt32(vrednost);
                    Assert.Throws<Exception>(() => service.Insert(newMesto), "Nevalidan unos za parametar: PttBroj, proverite broj!");
                    break;
                case "BrojStanovnika":
                    newMesto.BrojStanovnika = Convert.ToInt32(vrednost);
                    Assert.Throws<Exception>(() => service.Insert(newMesto), "Nevalidan unos za parametar: BrojStanovnika, broj stanovnika ne moze biti manji od nule");
                    break;
                        default: Assert.Throws<Exception>(() => service.Insert(newMesto)); break;
            }
        }
        
        [Test]
        public void TestUpdateValidEntity()
        {
            //Act
            var mesto = service.SelectById(1);
            mesto.Naziv = "Izmenjen naziv mesta";
            //Arange
            var result = service.Update(mesto);
            //Assert
            Assert.That(result.Equals("Uspesno!"));

            var izmenjenoMesto = service.SelectById(1);

            Assert.AreEqual(mesto.Naziv, izmenjenoMesto.Naziv);

        }
    
        [TestCase("MestoId",-1)]
        [TestCase("Ptt",10)]
        [TestCase("Naziv", "izmenjen naziv")]
        [TestCase("Ptt", 10)]
        [TestCase("BrojStanovnika", -1)]
        public void TestUpdateInvalidEntityShouldThrowException(string polje, object vrednost)
        {
            var mesta = service.SelectAll();
            Mesto updateMesto = mesta.First();

            switch (polje)
            {
                case "MestoId":
                    Mesto invalid1 = new Mesto()
                    {
                        MestoId = Convert.ToInt32(vrednost),
                        BrojStanovnika = updateMesto.BrojStanovnika,
                        Naziv = updateMesto.Naziv,
                        PttBroj = updateMesto.PttBroj
                    };
                   
                    Assert.Throws<Exception>(() => service.Update(invalid1), "Id mesta ne moze biti manji od 1!");
                    break;
                case "Naziv":
                    Mesto invalid2 = new Mesto()
                    {
                        MestoId = updateMesto.MestoId,
                        BrojStanovnika = updateMesto.BrojStanovnika,
                        Naziv = vrednost.ToString(),
                        PttBroj = updateMesto.PttBroj
                    };
                    Assert.Throws<Exception>(() => service.Update(invalid2), "Nevalidan unos za parametar: Naziv, mora poceti velikim slovom!");
                    break;
                case "Ptt":
                    Mesto invalid3 = new Mesto()
                    {
                        MestoId = updateMesto.MestoId,
                        BrojStanovnika = updateMesto.BrojStanovnika,
                        Naziv = updateMesto.Naziv,
                        PttBroj = Convert.ToInt32(vrednost)
                    };
                    Assert.Throws<Exception>(() => service.Update(invalid3), "Nevalidan unos za parametar: PttBroj, proverite broj!");
                    break;
                case "BrojStanovnika":
                    Mesto invalid4 = new Mesto()
                    {
                        MestoId = updateMesto.MestoId,
                        BrojStanovnika = Convert.ToInt32(vrednost),
                        Naziv = updateMesto.Naziv,
                        PttBroj = updateMesto.PttBroj
                    };
                    Assert.Throws<Exception>(() => service.Update(invalid4), "Nevalidan unos za parametar: BrojStanovnika, broj stanovnika ne moze biti manji od nule");
                    break;
                default: break;
            }
        }
   
        [TestCase(1)]
        [TestCase(null)]
        public void TestDeleteEntity(int? id)
        {
            switch(id)
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
                                service.SelectById(1), "Id mesta ne moze biti null!");

                    break;
                default: break;
            }
        }
    }
}
