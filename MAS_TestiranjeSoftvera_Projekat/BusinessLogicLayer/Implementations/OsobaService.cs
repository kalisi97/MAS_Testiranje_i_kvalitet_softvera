using MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer.Implementations
{
    public class OsobaService : IOsobaService
    {
        private readonly IRepositoryOsoba repository;

        public OsobaService(IRepositoryOsoba repository)
        {
            this.repository = repository;
        }

        public string Delete(Osoba entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            if (entity.OsobaId == null)
                throw new Exception("Id osobe ne moze biti null!");
            var osoba = SelectById(entity.OsobaId);
            if (osoba == null)
                throw new Exception("Ovo osoba ne postoji!");
            try
            {
                var message = repository.Delete(osoba);
                return message;
            }
            catch (Exception)
            {
                throw new Exception("Greska prilikom brisanja osobe!");
            }
        }

        public string Insert(Osoba entity)
        {
          
                if (entity == null)
                    throw new ArgumentNullException();
                if (!Validate(entity, out string poruka))
                    throw new Exception("Nevalidan unos za parametar: " + poruka);
           
              
                try
                {
                    var message = repository.Insert(entity);
                    return message;
                }
                catch (Exception ex)
                {
                    throw new Exception("Greska prilikom unosa osobe!");
                }
            
        }

        public IEnumerable<Osoba> SelectAll()
        {
            try
            {
                return repository.SelectAll();
            }
            catch (Exception)
            {

                throw new Exception("Greska prilikom ucitavanja osoba!");
            }
        }

        public Osoba SelectById(int? id)
        {

            if (id == null)
                throw new Exception("Id osobe ne moze biti null!");
            var osoba = repository.SelectById(id);
            if (osoba == null)
                throw new Exception("Trazena osoba ne postoji!");
            return osoba;
        }

        public string Update(Osoba entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            if (entity.OsobaId == null)
                throw new Exception("Id osobe ne moze biti null!");
            if (!Validate(entity, out string poruka))
                throw new Exception("Nevalidan unos za parametar: " + poruka);

            try
            {
                var o = SelectById(entity.OsobaId);
            }
            catch (Exception)
            {
                throw new Exception("Trazena osoba ne postoji!");
            }
         
          
            try
            {
                var message = repository.Update(entity);
                return message;
            }
            catch (Exception ex)
            {
                throw new Exception("Greska prilikom unosa osobe!");
            }
        }

        public bool Validate(Osoba entity, out string poruka)
        {
            poruka = string.Empty;
            if (string.IsNullOrEmpty(entity.Ime))
            {
                poruka = "Ime, unesite ime!";
                return false;
            }
            else
            {
                if (!char.IsUpper(entity.Ime.ElementAt(0)))
                {
                    poruka = "Ime, mora poceti velikim slovom!";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(entity.Prezime))
            {
                poruka = "Prezime, unesite prezime!";
                return false;
            }
            else
            {
                if (!char.IsUpper(entity.Prezime.ElementAt(0)))
                {
                    poruka = "Prezime, mora poceti velikim slovom!";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(entity.MaticniBroj))
            {
                poruka = "Maticni broj, unesite maticni broj";
                return false;
            }
            else
            {
                if(entity.MaticniBroj.Length != 13)
                {
                    poruka = "Maticni broj, nije u dobrom formatu!";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(entity.Email))
            {
                poruka = "Email, unesite email!";
                return false;
            }
            else
            {
                if(!entity.Email.EndsWith(".rs") || !entity.Email.Contains("@"))
                {
                    poruka = "Email, mora sadrzati @ sa dozvoljenim .rs domenom!";
                    return false;
                }
            }
            if(entity.Visina != null && entity.Visina < 35)
            {
                poruka = "Visina, minimalna dozvoljena vrednost je 35";
                return false;
            }
            if(entity.Tezina != null && (entity.Tezina > 250 || entity.Tezina < 10))
            {
                poruka = "Tezina, dozvoljene su vrednosti [10,250]";
                return false;
            }
            if(string.IsNullOrEmpty(entity.DatumRodjenja.ToString()))
            {
                poruka = "Datum rodjenja, unesite datum rodjenja!";
                return false;
            }
            if(entity.MestoId == null)
            {
                poruka = "Mesto, odaberite mesto!";
                return false;
            }
            if(!string.IsNullOrEmpty(entity.Telefon) && !entity.Telefon.StartsWith("+381") && (entity.Telefon.Length>13 || entity.Telefon.Length <12 ))
            {
                poruka = "Telefon, unesite telefon u formatu +381 6x .. !";
                return false;
            }
         return true;
        }
    }
}
