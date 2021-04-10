using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MAS_TestiranjeSoftvera_Projekat.Extensions.Enums;

namespace UnitTests.Mocks
{
    //Set-up
    public class MockClass
    {
        public static Mock<IRepositoryMesto> MockRepositoryMesto()
        {

            var mesta = new List<Mesto>()
            {
                new Mesto
                {
                    MestoId = 1,
                    Naziv = "Prokuplje",
                    BrojStanovnika = 2560,
                    PttBroj = 18400
                },
                new Mesto
                {
                    MestoId = 2,
                    Naziv = "Beograd",
                    PttBroj = 11000,
                    BrojStanovnika = 1659440
                }
            };

            var mockRepository = new Mock<IRepositoryMesto>();

            mockRepository.Setup(repo => repo.SelectAll()).Returns(mesta);
            mockRepository.Setup(repo => repo.SelectById(It.IsAny<int>())).Returns((int i) => mesta.SingleOrDefault(x => x.MestoId == i));
            mockRepository.Setup(i => i.Insert(It.IsAny<Mesto>())).Callback((Mesto item) =>
            {
                var id = mesta.Count();
                item.MestoId = id+1;
                mesta.Add(item);
              
            }).Returns("Uspesno!");
            mockRepository.Setup(m => m.Update(It.IsAny<Mesto>())).Callback((Mesto target) =>
            {
                var original = mesta.FirstOrDefault(
                    q => q.MestoId == target.MestoId);

                if (original != null)
                {
                    original.Naziv = target.Naziv;
                    original.BrojStanovnika = target.BrojStanovnika;
                    original.PttBroj = target.PttBroj;
                }

            }).Returns("Uspesno!");
            mockRepository.Setup(m => m.Delete(It.IsAny<Mesto>())).Callback((Mesto i) =>
            {
                var original = mesta.FirstOrDefault(
                    q => q.MestoId == i.MestoId);

                if (original != null)
                {
                    mesta.Remove(original);
                }

            }).Returns("Uspesno!");
            return mockRepository;
        }
    
        public static Mock<IRepositoryOsoba> MockRepositoryOsoba()
        {
            var osobe = new List<Osoba>()
            {
                new Osoba
                {
                    OsobaId = 1,
                    MaticniBroj = "1234567891234",
                    Ime = "Katarina",
                    Prezime = "Simic",
                    Tezina = 47,
                    Visina = 161,
                    Email = "kaca@gmail.rs",
                    Telefon = "+381642119548",
                    BojaOciju = BojaOciju.Crna,
                    Adresa = "XXI srpske divizije 64",
                    DatumRodjenja = new DateTime(1997,04,17),
                    MestoId = 1,
                    Napomena = "Napomena"
                }
            };

            var mockRepository = new Mock<IRepositoryOsoba>();
            mockRepository.Setup(repo => repo.SelectAll()).Returns(osobe);
            mockRepository.Setup(repo => repo.SelectById(It.IsAny<int>())).Returns((int i) => osobe.SingleOrDefault(x => x.MestoId == i));
            mockRepository.Setup(i => i.Insert(It.IsAny<Osoba>())).Callback((Osoba item) =>
            {
                var id = osobe.Count();
                item.MestoId = id + 1;
                osobe.Add(item);

            }).Returns("Uspesno!");
            mockRepository.Setup(m => m.Update(It.IsAny<Osoba>())).Callback((Osoba target) =>
            {
                var original = osobe.FirstOrDefault(
                    q => q.OsobaId == target.OsobaId);

                if (original != null)
                {
                    original.Ime = target.Ime;
                    original.Prezime = target.Prezime;
                    original.MestoId = target.MestoId;
                    original.Telefon = target.Telefon;
                    original.Napomena = target.Napomena;
                    original.Tezina = target.Tezina;
                    original.Visina = target.Visina;
                    original.BojaOciju = target.BojaOciju;
                    original.Adresa = target.Adresa;
                    original.MaticniBroj = target.MaticniBroj;
                    original.DatumRodjenja = target.DatumRodjenja;
                    original.Email = target.Email;
                }

            }).Returns("Uspesno!");
            mockRepository.Setup(m => m.Delete(It.IsAny<Osoba>())).Callback((Osoba i) =>
            {
                var original = osobe.FirstOrDefault(
                    q => q.OsobaId == i.OsobaId);

                if (original != null)
                {
                    osobe.Remove(original);
                }

            }).Returns("Uspesno!");


            return mockRepository;
        }
    }
}
