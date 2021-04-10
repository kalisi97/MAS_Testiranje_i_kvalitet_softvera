using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static MAS_TestiranjeSoftvera_Projekat.Extensions.Enums;

namespace MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Implementations
{
    public class RepositoryMesto : IRepositoryMesto
    {
        DatabaseConnection broker;
        SqlTransaction transakcija;
        public RepositoryMesto()
        {
            broker = DatabaseConnection.Sesion();
        }
        public string Delete(Mesto entity)
        {
            transakcija = null;
            string poruka = string.Empty;

            try
            {

                DatabaseConnection.broker.konekcija.Open();
                transakcija = broker.konekcija.BeginTransaction();
                broker.komanda =
                    new SqlCommand("", broker.konekcija, transakcija);
                broker.komanda.CommandText =
                    $"delete from Mesto where MestoId = {entity.MestoId}";
               
                broker.komanda.ExecuteNonQuery();

                Save();

                poruka = "Uspesno!";
                return poruka;
            }
            catch (Exception ex)
            {
                transakcija.Rollback();
                poruka = ex.Message.ToString();
                return poruka;
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }

        public string Insert(Mesto entity)
        {
            transakcija = null;
            string poruka = string.Empty;

            try
            {
                entity.MestoId = broker.VratiSifru("MestoId", "Mesto");
                DatabaseConnection.broker.konekcija.Open();

                broker.komanda =
                    new SqlCommand("PROC_INSERT_MESTO", broker.konekcija);
                broker.komanda.CommandType = CommandType.StoredProcedure;

                broker.komanda.Parameters.AddWithValue("MestoId", Convert.ToInt32(entity.MestoId));
                broker.komanda.Parameters.AddWithValue("Naziv", SqlDbType.NVarChar).Value = entity.Naziv;
                broker.komanda.Parameters.AddWithValue("Ptt", SqlDbType.Int).Value = entity.PttBroj;
                if (entity.BrojStanovnika != null)
                    broker.komanda.Parameters.AddWithValue("BrojStanovnika", SqlDbType.Int).Value = entity.BrojStanovnika;
                else
                    broker.komanda.Parameters.AddWithValue("BrojStanovnika", DBNull.Value);

                int result = broker.komanda.ExecuteNonQuery();

                if (result != -1)
                {
                    poruka = "Uspesno!";
                }
                else
                {
                    throw new Exception("Doslo je do greske!");
                }
                return poruka;
            }
            catch (Exception ex)
            {

               throw new Exception(ex.Message);
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }

        public void Save()
        {
            transakcija.Commit();
        }

        public IEnumerable<Mesto> SelectAll()
        {
            List<Mesto> mesta = new List<Mesto>();

            try
            {
                broker.konekcija.Open();
                broker.komanda = new SqlCommand("", broker.konekcija, transakcija)
                {
                    CommandText = $"Select * from Mesto"
                };

                SqlDataReader citac = broker.komanda.ExecuteReader();

                while (citac.Read())
                {
                    Mesto m = new Mesto();
                    m.MestoId = citac.GetInt32(0);
                    m.Naziv = citac.GetString(1);
                    m.PttBroj = citac.GetInt32(2);
                    m.BrojStanovnika = GetIntValueOrDefault(citac, "BrojStanovnika");
                    mesta.Add(m);
                }
                return mesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }

        }

        public Mesto SelectById(int? id)
        {
            Mesto m = new Mesto();
            try
            {
                broker.konekcija.Open();
                broker.komanda = new SqlCommand("", broker.konekcija, transakcija)
                {
                    CommandText = $"Select * from Mesto where MestoId={id}"
                };
                SqlDataReader citac = broker.komanda.ExecuteReader();
                List<Osoba> osobe = new List<Osoba>();

                while (citac.Read())
                {
                    m.MestoId = citac.GetInt32(0);
                    m.Naziv = citac.GetString(1);
                    m.PttBroj = citac.GetInt32(2);
                    m.BrojStanovnika = GetIntValueOrDefault(citac, "BrojStanovnika");
                  
                }
                citac.Close();

                broker.komanda = new SqlCommand("", broker.konekcija, transakcija);
                broker.komanda.CommandText = $"Select * from Osoba where MestoId={id}";
              
                citac = broker.komanda.ExecuteReader();
                
                while (citac.Read())
                {
                    Osoba osoba = new Osoba
                    {
                        OsobaId = citac.GetInt32(0),
                        MaticniBroj = citac.GetString(1),
                        Ime = citac.GetString(2),
                        Prezime = citac.GetString(3),
                        Visina = GetIntValueOrDefault(citac,"Visina"),
                        Tezina = GetIntValueOrDefault(citac, "Tezina"),
                        BojaOciju = (BojaOciju)GetIntValueOrDefault(citac,"BojaOciju"),
                        Telefon = GetStringValueOrDefault(citac, "Telefon"),
                        Email = citac.GetString(8),
                        DatumRodjenja = citac.GetDateTime(9),
                        Adresa = GetStringValueOrDefault(citac, "Adresa"),
                        Napomena = GetStringValueOrDefault(citac, "Napomena"),
                        MestoId = citac.GetInt32(12),
                        Mesto = m
                    };
                    osobe.Add(osoba);
                }
                citac.Close();
                
                m.Osobe = osobe;
                return m;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }

        private string GetStringValueOrDefault(SqlDataReader citac, string nazivKolone)
        {
            string data = (citac.IsDBNull(citac.GetOrdinal(nazivKolone)))
                      ? "" : citac[nazivKolone].ToString();
            return data;
        }

        public int GetIntValueOrDefault(SqlDataReader citac, string nazivKolone)
        {
            int data = (citac.IsDBNull(citac.GetOrdinal(nazivKolone)))
                        ? 0 : int.Parse(citac[nazivKolone].ToString());
            return data;
        }

        public string Update(Mesto entity)
        {
            transakcija = null;
            string poruka = string.Empty;

            try
            {
                
                DatabaseConnection.broker.konekcija.Open();
                transakcija = broker.konekcija.BeginTransaction();
                broker.komanda =
                    new SqlCommand("", broker.konekcija, transakcija);
                broker.komanda.CommandText =
                    $"update Mesto set Naziv ='{entity.Naziv}', Ptt = {entity.PttBroj}, BrojStanovnika =  @BrojStanovnika where MestoId = {entity.MestoId}";
                if (entity.BrojStanovnika != null)
                {
                    broker.komanda.Parameters.AddWithValue("BrojStanovnika", entity.BrojStanovnika) ;
                }
                else
                {
                    broker.komanda.Parameters.AddWithValue("BrojStanovnika", DBNull.Value);
                }
                broker.komanda.ExecuteNonQuery();

                Save();

                poruka = "Uspesno!";
                return poruka;
            }
            catch (Exception ex)
            {
                transakcija.Rollback();
                poruka = ex.Message.ToString();
                return poruka;
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }

        }

    }
}
