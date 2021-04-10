using MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer.Implementations
{
    public class MestoService : IMestoService
    {
        private readonly IRepositoryMesto mestoRepository;

        public MestoService(IRepositoryMesto mestoRepository)
        {
            this.mestoRepository = mestoRepository;
        }

        public string Delete(Mesto entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            if (entity.MestoId == null)
                throw new Exception("Id mesta ne moze biti null!");
            var mesto = SelectById(entity.MestoId);
            if(mesto == null)
                throw new Exception("Ovo mesto ne postoji!");
            try
            {
               var message = mestoRepository.Delete(mesto);
                return message;
            }
            catch (Exception)
            {
                throw new Exception("Greska prilikom brisanja mesta!");
            }
        }

        public string Insert(Mesto entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            if (!Validate(entity, out string poruka))
                throw new Exception("Nevalidan unos za parametar: " + poruka);
            try
            {
               
                var message = mestoRepository.Insert(entity);
                return message;
            }
            catch (Exception ex)
            {
                throw new Exception("Greska prilikom unosa mesta!");
            }
        }

        public bool Validate(Mesto entity, out string poruka)
        {
            poruka = String.Empty;
            if (string.IsNullOrEmpty(entity.Naziv))
            {
                poruka = "Naziv, unesite naziv!";
                return false;
            }
            else
            {
               if(!char.IsUpper(entity.Naziv.ElementAt(0)))
                {
                    poruka = "Naziv, mora poceti velikim slovom!";
                    return false;
                }
            }
            
            if (entity.PttBroj == null)
            {
                poruka = "PttBroj, unesite ptt broj!";
                return false;
            }
            if(entity.PttBroj < 11000)
            {
                poruka = "PttBroj, proverite broj!";
                return false;
            }
            if(entity.BrojStanovnika != null && entity.BrojStanovnika < 0)
            {
                poruka = "BrojStanovnika, broj stanovnika ne moze biti manji od nule";
                return false;
            }
            return true;
        }

        public IEnumerable<Mesto> SelectAll()
        {
            try
            {
                return mestoRepository.SelectAll();
            }
            catch (Exception)
            {

                throw new Exception("Greska prilikom ucitavanja mesta!");
            }  
        }

        public Mesto SelectById(int? id)
        {
            if (id == null)
                throw new Exception("Id mesta ne moze biti null!");
            var mesto = mestoRepository.SelectById(id);
            if (mesto == null)
                throw new Exception("Trazeno mesto ne postoji!");
            return mesto;
        }

        public string Update(Mesto entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            if (entity.MestoId == null)
                throw new Exception("Id mesta ne moze biti null!");
            if(entity.MestoId < 1)
                throw new Exception("Id mesta ne moze biti manji od 1!");
            if (!Validate(entity, out string poruka))
                throw new Exception("Nevalidan unos za parametar: " + poruka);
           
            try
            {
                var message = mestoRepository.Update(entity);
                return message;
            }
            catch (Exception ex)
            {
                throw new Exception("Greska prilikom unosa mesta!");
            }
        }
    }
}
